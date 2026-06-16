#!/usr/bin/env bash
set -euo pipefail

# ================================================================
# build-test-deploy.sh
# Script completo para compilar, probar y desplegar POS Ferretería
# ================================================================

PROJECT_DIR="$(cd "$(dirname "$0")/.." && pwd)"
cd "$PROJECT_DIR"

# Colores
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color

log()  { echo -e "${CYAN}[$(date +%H:%M:%S)]${NC} $1"; }
ok()   { echo -e "${GREEN}  ✅ $1${NC}"; }
fail() { echo -e "${RED}  ❌ $1${NC}"; exit 1; }
warn() { echo -e "${YELLOW}  ⚠️  $1${NC}"; }

# ── 1. Compilar Backend (.NET) ──
step_backend() {
    log "📦 Compilando backend .NET..."
    cd "$PROJECT_DIR/backend-net"
    if dotnet restore > /tmp/pos-dotnet-restore.log 2>&1; then
        ok "dotnet restore exitoso"
    else
        fail "dotnet restore falló. Log: /tmp/pos-dotnet-restore.log"
    fi
    if dotnet build --no-restore -c Release > /tmp/pos-dotnet-build.log 2>&1; then
        ok "dotnet build exitoso"
    else
        fail "dotnet build falló. Log: /tmp/pos-dotnet-build.log"
    fi
    cd "$PROJECT_DIR"
}

# ── 2. Compilar Frontend ──
step_frontend() {
    log "📦 Compilando frontend..."
    cd "$PROJECT_DIR/frontend"
    if npm install --silent > /tmp/pos-npm-install.log 2>&1; then
        ok "npm install exitoso"
    else
        fail "npm install falló. Log: /tmp/pos-npm-install.log"
    fi
    if npm run build > /tmp/pos-npm-build.log 2>&1; then
        ok "npm run build exitoso"
    else
        fail "npm run build falló. Log: /tmp/pos-npm-build.log"
    fi
    cd "$PROJECT_DIR"
}

# ── 3. Reconstruir y levantar contenedores Docker ──
step_docker() {
    log "🐳 Reconstruyendo contenedores Docker..."

    # Verificar que el volumen externo existe
    if ! docker volume inspect pos-ferreteria_postgres_ferreteria_data > /dev/null 2>&1; then
        warn "Volumen 'pos-ferreteria_postgres_ferreteria_data' no existe. Creándolo..."
        docker volume create pos-ferreteria_postgres_ferreteria_data
    fi

    # Verificar si hay datos en la BD (si la tabla users tiene registros)
    log "   Verificando datos en la base de datos..."
    if docker exec pos_ferreteria_db psql -U postgres -d pos_ferreteria -c "SELECT COUNT(*) FROM users;" 2>/dev/null | grep -q "0"; then
        warn "La BD no tiene datos de usuario. Se cargará data_live.sql después del inicio."
        NEEDS_SEED=true
    else
        NEEDS_SEED=false
    fi

    # Bajar servicios (excepto BD para preservar datos)
    log "   Deteniendo servicios (api, frontend)..."
    docker compose stop api frontend 2>/dev/null || true

    # Precargar imágenes base para evitar timeouts por IPv6
    log "   Precargando imágenes base (mcr.microsoft.com)..."
    docker pull --platform linux/amd64 mcr.microsoft.com/dotnet/sdk:8.0 2>/dev/null || true
    docker pull --platform linux/amd64 mcr.microsoft.com/dotnet/aspnet:8.0 2>/dev/null || true
    ok "Imágenes base listas"

    # Reconstruir y levantar
    log "   Reconstruyendo imágenes..."
    docker compose build api frontend --no-cache 2>&1 | tail -10

    log "   Levantando servicios..."
    docker compose up -d db api frontend

    # Esperar a que la API esté lista (acepta 200, 401, 404 como señal de vida)
    log "   Esperando a que la API responda..."
    for i in $(seq 1 30); do
        local status
        status=$(curl -so /dev/null -w "%{http_code}" http://localhost:5002/api/auth/login 2>/dev/null || echo "000")
        if [ "$status" != "000" ]; then
            ok "API respondiendo en http://localhost:5002 (HTTP $status)"
            break
        fi
        if [ "$i" -eq 30 ]; then
            fail "La API no respondió después de 30 segundos. Revisa los logs con: docker compose logs api"
        fi
        sleep 2
    done

    # Cargar datos si es necesario
    if [ "$NEEDS_SEED" = true ]; then
        log "   Cargando datos iniciales desde data_live.sql..."
        # Esperar a que PostgreSQL esté listo
        sleep 5
        if [ -f "$PROJECT_DIR/database/data_live.sql" ]; then
            docker exec -i pos_ferreteria_db psql -U postgres -d pos_ferreteria < "$PROJECT_DIR/database/data_live.sql" && \
                ok "Datos iniciales cargados" || \
                warn "No se pudieron cargar datos iniciales (puede que ya existan)"
        else
            warn "Archivo data_live.sql no encontrado. La BD tiene schema pero sin datos."
        fi
    fi

    log "   Verificando contenedores..."
    docker compose ps
}

# ── 4. Ejecutar pruebas de módulos ──
step_tests() {
    log "🧪 Ejecutando suite de pruebas..."
    cd "$PROJECT_DIR"
    python3 test_ferreteria.py 2>&1
    local exit_code=$?
    if [ $exit_code -eq 0 ]; then
        ok "Todas las pruebas pasaron"
    else
        warn "Algunas pruebas fallaron (código: $exit_code). Revisa el output arriba."
    fi
}

# ── 5. Verificar endpoints clave ──
step_verify() {
    log "🔍 Verificando endpoints clave..."

    # Login de prueba
    local TOKEN
    TOKEN=$(curl -sf -X POST http://localhost:5002/api/auth/login \
        -H "Content-Type: application/json" \
        -d '{"username":"admin","password":"admin123"}' 2>/dev/null | python3 -c "import sys,json; print(json.load(sys.stdin).get('token',''))" 2>/dev/null || echo "")

    if [ -n "$TOKEN" ]; then
        ok "Login funciona (token obtenido)"
    else
        fail "Login falló — credenciales incorrectas o error interno. Verifica los usuarios en la BD."
    fi

    # Categorías
    local CATS
    CATS=$(curl -sf http://localhost:5002/api/products/categories \
        -H "Authorization: Bearer $TOKEN" 2>/dev/null || echo "[]")
    local CAT_COUNT
    CAT_COUNT=$(echo "$CATS" | python3 -c "import sys,json; print(len(json.load(sys.stdin)))" 2>/dev/null || echo "0")
    if [ "$CAT_COUNT" -gt 0 ]; then
        ok "Categorías: $CAT_COUNT categorías disponibles"
    else
        warn "No se encontraron categorías. Revisa los datos en la BD."
    fi

    # Productos
    local PRODS
    PRODS=$(curl -sf "http://localhost:5002/api/products?limit=5" \
        -H "Authorization: Bearer $TOKEN" 2>/dev/null || echo "[]")
    local PROD_COUNT
    PROD_COUNT=$(echo "$PRODS" | python3 -c "import sys,json; data=json.load(sys.stdin); items=data if isinstance(data,list) else data.get('products',[]); print(len(items))" 2>/dev/null || echo "0")
    ok "Productos: $PROD_COUNT productos (primeros 5)"

    # Ventas
    local SALES
    SALES=$(curl -sf "http://localhost:5002/api/sales" \
        -H "Authorization: Bearer $TOKEN" 2>/dev/null || echo "[]")
    local SALE_COUNT
    SALE_COUNT=$(echo "$SALES" | python3 -c "import sys,json; data=json.load(sys.stdin); items=data if isinstance(data,list) else []; print(len(items))" 2>/dev/null || echo "0")
    ok "Ventas: $SALE_COUNT ventas registradas"

    # Frontend
    if curl -sf http://localhost:8082 > /dev/null 2>&1; then
        ok "Frontend respondiendo en http://localhost:8082"
    else
        fail "Frontend no responde en http://localhost:8082"
    fi

    ok "Verificación de endpoints completada"
}

# ── 6. Commit y push ──
step_commit() {
    log "📤 Preparando commit..."

    # Solo incluir los archivos relevantes (no los .bak)
    cd "$PROJECT_DIR"

    # Crear .gitignore si no existe (para excluir .bak)
    if [ ! -f .gitignore ]; then
        echo "*.bak" > .gitignore
        echo "*.bak/" >> .gitignore
        echo "node_modules/" >> .gitignore
        echo "bin/" >> .gitignore
        echo "obj/" >> .gitignore
        echo "dist/" >> .gitignore
        echo "frontend/dist/" >> .gitignore
        git add .gitignore
    fi

    # Agregar archivos modificados (excluyendo .bak)
    git add \
        backend-net/POS.API.Services/AuthService.cs \
        backend-net/POS.API.Services/ProductService.cs \
        frontend/src/pages/NewSale.tsx \
        frontend/src/index.css \
        2>/dev/null || true

    # Mostrar qué se va a commitear
    echo ""
    echo "  Archivos a commitear:"
    git diff --cached --name-only | sed 's/^/    • /'

    if git diff --cached --quiet; then
        warn "No hay cambios nuevos para commitear (los nuestros ya fueron incluídos en cambios previos)"
        return
    fi

    # Commit
    git commit -m "fix: agrega menú de categorías en POS y corrige error de login

- Agrega filtro por categoría en la pantalla de Nueva Venta (NewSale.tsx)
- Corrige AuthService.LoginAsync para incluir Action='Login' en LoginLog
- Completa ProductService.GetCategoriesAsync con todos los campos del DTO
- Agrega categoría por defecto en creación/actualización de productos
- Agrega estilos CSS para el menú de categorías"

    # Push
    log "   Subiendo cambios a origin/main..."
    if git push origin main 2>&1; then
        ok "Cambios subidos exitosamente a origin/main"
    else
        warn "Push falló. Puede que necesites: git pull --rebase origin main y luego intentar de nuevo"
    fi
}

# ── Main ──
echo ""
echo "================================================================"
echo "  POS FERRETERÍA — Build, Test & Deploy"
echo "  $(date '+%Y-%m-%d %H:%M:%S')"
echo "================================================================"
echo ""

# Parse argumentos
RUN_ALL=true
RUN_STEPS=""

while [[ $# -gt 0 ]]; do
    case "$1" in
        --backend)    RUN_ALL=false; RUN_STEPS="$RUN_STEPS backend" ;;
        --frontend)   RUN_ALL=false; RUN_STEPS="$RUN_STEPS frontend" ;;
        --docker)     RUN_ALL=false; RUN_STEPS="$RUN_STEPS docker" ;;
        --tests)      RUN_ALL=false; RUN_STEPS="$RUN_STEPS tests" ;;
        --verify)     RUN_ALL=false; RUN_STEPS="$RUN_STEPS verify" ;;
        --commit)     RUN_ALL=false; RUN_STEPS="$RUN_STEPS commit" ;;
        --help|-h)
            echo "Uso: $0 [opciones]"
            echo ""
            echo "Opciones:"
            echo "  --backend    Compilar backend .NET"
            echo "  --frontend   Compilar frontend"
            echo "  --docker     Reconstruir y levantar contenedores"
            echo "  --tests      Ejecutar suite de pruebas"
            echo "  --verify     Verificar endpoints"
            echo "  --commit     Commit y push"
            echo "  --help       Mostrar esta ayuda"
            echo ""
            echo "Sin argumentos ejecuta todos los pasos en secuencia."
            exit 0
            ;;
        *)
            echo "Opción desconocida: $1"
            echo "Usa --help para ver las opciones disponibles."
            exit 1
            ;;
    esac
    shift
done

if [ "$RUN_ALL" = true ]; then
    step_backend
    step_frontend
    step_docker
    step_tests
    step_verify
    step_commit
else
    for step in $RUN_STEPS; do
        case "$step" in
            backend)  step_backend ;;
            frontend) step_frontend ;;
            docker)   step_docker ;;
            tests)    step_tests ;;
            verify)   step_verify ;;
            commit)   step_commit ;;
        esac
    done
fi

echo ""
echo "================================================================"
echo -e "${GREEN}  ✅ Proceso completado${NC}"
echo "================================================================"
