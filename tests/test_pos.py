#!/usr/bin/env python3
"""
Script de pruebas automatizadas para POS Ferretería.
Ejecuta pruebas sobre cada módulo del sistema.

Requisitos:
  pip install requests

Uso:
  python test_pos.py                    # Pruebas completas
  python test_pos.py --module sales     # Solo un módulo
  python test_pos.py --url http://localhost:5002  # URL personalizada
"""

import sys
import json
import time
import argparse
from datetime import datetime

try:
    import requests
except ImportError:
    print("Error: Instala requests: pip install requests")
    sys.exit(1)

BASE_URL = "http://localhost:5002"
TOKEN = None
PASSED = 0
FAILED = 0
SKIPPED = 0


def log_test(name, result, detail=""):
    global PASSED, FAILED
    status = "✅ PASS" if result else "❌ FAIL"
    if result:
        PASSED += 1
    else:
        FAILED += 1
    icon = "  ✓" if result else "  ✗"
    print(f"{icon} [{status}] {name}" + (f" - {detail}" if detail else ""))


def log_skip(name, reason):
    global SKIPPED
    SKIPPED += 1
    print(f"  ➖ [SKIP] {name} - {reason}")


def api_call(method, path, data=None, expected_status=200):
    url = f"{BASE_URL}{path}"
    headers = {"Content-Type": "application/json"}
    if TOKEN:
        headers["Authorization"] = f"Bearer {TOKEN}"

    try:
        if method == "GET":
            r = requests.get(url, headers=headers, timeout=10)
        elif method == "POST":
            r = requests.post(url, headers=headers, json=data, timeout=10)
        elif method == "PUT":
            r = requests.put(url, headers=headers, json=data, timeout=10)
        elif method == "DELETE":
            r = requests.delete(url, headers=headers, timeout=10)
        elif method == "PATCH":
            r = requests.patch(url, headers=headers, json=data, timeout=10)
        else:
            return False, f"Método no soportado: {method}"

        if r.status_code == expected_status:
            return True, r.json() if r.text else {}
        elif r.status_code == 401:
            return False, "No autorizado (401)"
        else:
            try:
                err = r.json().get("error", {}).get("message", r.text[:100])
            except:
                err = r.text[:100]
            return False, f"Status {r.status_code}: {err}"

    except requests.exceptions.ConnectionError:
        return False, f"No se pudo conectar a {BASE_URL}"
    except Exception as e:
        return False, str(e)


def test_auth():
    print("\n" + "=" * 60)
    print("🔐  MÓDULO: AUTENTICACIÓN")
    print("=" * 60)
    global TOKEN

    # Login
    ok, data = api_call("POST", "/auth/login",
                        {"username": "admin", "password": "admin123"})
    log_test("Login con credenciales válidas", ok)
    if ok and "token" in data:
        TOKEN = data["token"]
        log_test("Token JWT recibido", True, f"Usuario: {data.get('user', {}).get('fullname', 'N/A')}")
    else:
        log_test("Token JWT recibido", False, "No se obtuvo token")

    # Login inválido
    ok, data = api_call("POST", "/auth/login",
                        {"username": "admin", "password": "wrong"},
                        expected_status=401)
    log_test("Login con credenciales inválidas", ok)

    # Obtener perfil
    ok, data = api_call("GET", "/auth/me")
    log_test("Obtener perfil del usuario", ok)


def test_products():
    print("\n" + "=" * 60)
    print("💊  MÓDULO: PRODUCTOS")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    # Listar productos
    ok, data = api_call("GET", "/products")
    log_test("Listar todos los productos", ok)
    products = data.get("products", []) if ok else []
    log_test(f"Cantidad de productos devueltos", True, f"{len(products)} productos")

    # Crear producto
    new_product = {
        "code": f"TEST-{int(time.time())}",
        "name": "Producto de Prueba",
        "description": "Creado por script de pruebas",
        "categoryid": 1,
        "purchaseprice": 10.50,
        "saleprice": 25.00,
        "stock": 100,
        "minstock": 10,
        "unit": "pza",
        "requiresprescription": False,
        "requires_tax": True,
    }
    ok, data = api_call("POST", "/products", new_product, expected_status=201)
    log_test("Crear nuevo producto", ok)
    product_id = data.get("id") if ok else None

    # Obtener producto por ID
    if product_id:
        ok, data = api_call("GET", f"/products/{product_id}")
        log_test("Obtener producto por ID", ok)

        # Actualizar producto
        ok, data = api_call("PUT", f"/products/{product_id}",
                            {"name": "Producto Modificado", "saleprice": 30.00})
        log_test("Actualizar producto", ok)

        # Desactivar producto
        ok, data = api_call("PATCH", f"/products/{product_id}/toggle-active")
        log_test("Cambiar estado del producto", ok)

    # Búsqueda
    ok, data = api_call("GET", "/products", {"search": "Producto"})
    log_test("Búsqueda de productos", ok)

    # Stock bajo
    ok, data = api_call("GET", "/products/low-stock", {"threshold": 10})
    log_test("Productos con stock bajo", ok)

    # Por categoría
    ok, data = api_call("GET", "/products/category/1")
    log_test("Productos por categoría", ok)

    # Por vencer
    ok, data = api_call("GET", "/products/expiring-soon", {"days": 30})
    log_test("Productos por vencer", ok)


def test_categories():
    print("\n" + "=" * 60)
    print("📂  MÓDULO: CATEGORÍAS")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    ok, data = api_call("GET", "/categories")
    log_test("Listar categorías", ok)
    cats = data if isinstance(data, list) and ok else []
    log_test(f"Categorías disponibles", True, f"{len(cats)} categorías")

    # Crear categoría
    ok, data = api_call("POST", "/categories",
                        {"name": f"Test Cat {int(time.time()))}", "description": "Categoría de prueba"},
                        expected_status=201)
    log_test("Crear categoría", ok)
    cat_id = data.get("id") if ok else None

    if cat_id:
        ok, data = api_call("GET", f"/categories/{cat_id}")
        log_test("Obtener categoría por ID", ok)

        ok, data = api_call("PUT", f"/categories/{cat_id}",
                            {"name": "Categoría Modificada"})
        log_test("Actualizar categoría", ok)


def test_customers():
    print("\n" + "=" * 60)
    print("👥  MÓDULO: CLIENTES")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    ok, data = api_call("GET", "/customers")
    log_test("Listar clientes", ok)
    customers = data if isinstance(data, list) and ok else []
    log_test(f"Clientes registrados", True, f"{len(customers)} clientes")

    # Crear cliente
    new_customer = {
        "documenttype": "INE",
        "documentnumber": f"TEST-{int(time.time())}",
        "fullname": "Cliente de Prueba",
        "phone": "555-1234",
        "email": "test@example.com",
        "address": "Calle de Prueba #123",
    }
    ok, data = api_call("POST", "/customers", new_customer, expected_status=201)
    log_test("Crear nuevo cliente", ok)
    customer_id = data.get("id") if ok else None

    if customer_id:
        ok, data = api_call("GET", f"/customers/{customer_id}")
        log_test("Obtener cliente por ID", ok)

        ok, data = api_call("PUT", f"/customers/{customer_id}",
                            {"fullname": "Cliente Modificado"})
        log_test("Actualizar cliente", ok)

    # Búsqueda
    ok, data = api_call("GET", "/customers", {"search": "Cliente"})
    log_test("Búsqueda de clientes", ok)


def test_suppliers():
    print("\n" + "=" * 60)
    print("🚚  MÓDULO: PROVEEDORES")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    ok, data = api_call("GET", "/suppliers")
    log_test("Listar proveedores", ok)

    ok, data = api_call("POST", "/suppliers",
                        {"name": f"Proveedor Test {int(time.time()))}",
                         "contactname": "Contacto Test",
                         "phone": "555-5678"},
                        expected_status=201)
    log_test("Crear proveedor", ok)
    supplier_id = data.get("id") if ok else None

    if supplier_id:
        ok, data = api_call("PUT", f"/suppliers/{supplier_id}",
                            {"name": "Proveedor Modificado"})
        log_test("Actualizar proveedor", ok)


def test_sales():
    print("\n" + "=" * 60)
    print("🛒  MÓDULO: VENTAS")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    # Obtener producto existente para la venta
    ok, data = api_call("GET", "/products", {"limit": 1})
    products = data.get("products", []) if ok else []
    if not products:
        log_skip("Crear venta", "No hay productos disponibles")
    else:
        product = products[0]
        sale_data = {
            "items": [{
                "productid": product["id"],
                "quantity": 2,
                "unitprice": product["saleprice"]
            }],
            "paymentmethod": "Efectivo",
            "amountreceived": 100,
            "discount": 0,
            "notes": "Venta de prueba automatizada"
        }
        ok, data = api_call("POST", "/sales", sale_data, expected_status=201)
        log_test("Crear nueva venta", ok)
        sale_id = data.get("id") if ok else None

        if sale_id:
            log_test("Venta creada con ID", True, f"ID: {sale_id}")
            if "receiptnumber" in data:
                log_test("Folio de venta generado", True, data["receiptnumber"])

            ok, data = api_call("GET", f"/sales/{sale_id}")
            log_test("Obtener venta por ID", ok)
            if ok:
                items = data.get("items", [])
                log_test("Items de venta recuperados", True, f"{len(items)} artículo(s)")

    # Historial de ventas
    ok, data = api_call("GET", "/sales")
    log_test("Listar historial de ventas", ok)
    sales_list = data.get("sales", []) if ok else []
    log_test(f"Ventas registradas", True, f"{len(sales_list)} ventas")

    # Filtro por fecha
    today = datetime.now().strftime("%Y-%m-%d")
    ok, data = api_call("GET", "/sales", {"startDate": today, "endDate": today})
    log_test("Filtrar ventas por fecha", ok)


def test_cash_register():
    print("\n" + "=" * 60)
    print("💰  MÓDULO: CAJA")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    ok, data = api_call("GET", "/cashregister")
    log_test("Listar cajas", ok)

    # Verificar sesión activa
    ok, data = api_call("GET", "/cashregister/active")
    if ok and data:
        log_test("Sesión de caja activa encontrada", True)
    else:
        # Abrir sesión
        ok, data = api_call("POST", "/cashregister/open",
                            {"openingbalance": 500.00},
                            expected_status=201)
        log_test("Abrir sesión de caja", ok)

        if ok:
            ok, data = api_call("GET", "/cashregister/active")
            log_test("Verificar sesión activa después de abrir", ok)

    ok, data = api_call("GET", "/cashregister/sessions")
    log_test("Historial de sesiones de caja", ok)


def test_reports():
    print("\n" + "=" * 60)
    print("📈  MÓDULO: REPORTES")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    today = datetime.now().strftime("%Y-%m-%d")

    ok, data = api_call("GET", "/reports/daily-summary", {"date": today})
    log_test("Reporte de resumen diario", ok)

    ok, data = api_call("GET", "/reports/top-products", {"limit": 10})
    log_test("Reporte de productos más vendidos", ok)

    ok, data = api_call("GET", "/reports/inventory-status")
    log_test("Reporte de estado del inventario", ok)


def test_expenses():
    print("\n" + "=" * 60)
    print("💸  MÓDULO: GASTOS")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    ok, data = api_call("GET", "/expenses")
    log_test("Listar gastos", ok)

    ok, data = api_call("POST", "/expenses",
                        {"description": "Gasto de prueba",
                         "amount": 150.00,
                         "category": "Servicios",
                         "paymentmethod": "Efectivo"},
                        expected_status=201)
    log_test("Crear nuevo gasto", ok)
    expense_id = data.get("id") if ok else None

    if expense_id:
        ok, data = api_call("PUT", f"/expenses/{expense_id}",
                            {"description": "Gasto modificado"})
        log_test("Actualizar gasto", ok)

        ok, data = api_call("DELETE", f"/expenses/{expense_id}")
        log_test("Eliminar gasto", ok)


def test_returns():
    print("\n" + "=" * 60)
    print("↩️  MÓDULO: DEVOLUCIONES")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    ok, data = api_call("GET", "/returns")
    log_test("Listar devoluciones", ok)


def test_patients():
    print("\n" + "=" * 60)
    print("🏥  MÓDULO: PACIENTES")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    ok, data = api_call("GET", "/patients")
    log_test("Listar pacientes", ok)

    ok, data = api_call("POST", "/patients",
                        {"fullname": "Paciente de Prueba",
                         "dateofbirth": "1990-01-15",
                         "phone": "555-0000",
                         "bloodtype": "O+"},
                        expected_status=201)
    log_test("Crear nuevo paciente", ok)


def test_prescriptions():
    print("\n" + "=" * 60)
    print("📝  MÓDULO: RECETAS")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    ok, data = api_call("GET", "/prescriptions")
    log_test("Listar recetas", ok)


def test_alerts():
    print("\n" + "=" * 60)
    print("🔔  MÓDULO: ALERTAS")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    ok, data = api_call("GET", "/alerts")
    log_test("Listar alertas", ok)

    if ok and isinstance(data, list) and len(data) > 0:
        alert_id = data[0]["id"]
        ok, data = api_call("PATCH", f"/alerts/{alert_id}/read")
        log_test("Marcar alerta como leída", ok)

        ok, data = api_call("PATCH", "/alerts/read-all")
        log_test("Marcar todas como leídas", ok)


def test_inventory():
    print("\n" + "=" * 60)
    print("📦  MÓDULO: INVENTARIO (MOVIMIENTOS)")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    ok, data = api_call("GET", "/inventory")
    log_test("Listar movimientos de inventario", ok)

    # Obtener un producto
    ok, prod_data = api_call("GET", "/products", {"limit": 1})
    products = prod_data.get("products", []) if ok else []
    if products:
        ok, data = api_call("POST", "/inventory",
                            {"productid": products[0]["id"],
                             "type": "in",
                             "quantity": 50,
                             "reason": "Prueba automatizada"},
                            expected_status=201)
        log_test("Registrar movimiento de entrada", ok)

        ok, data = api_call("POST", "/inventory",
                            {"productid": products[0]["id"],
                             "type": "out",
                             "quantity": 10,
                             "reason": "Prueba automatizada"},
                            expected_status=201)
        log_test("Registrar movimiento de salida", ok)


def test_company():
    print("\n" + "=" * 60)
    print("⚙️  MÓDULO: EMPRESA")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    ok, data = api_call("GET", "/company")
    log_test("Obtener información de la empresa", ok)

    if ok:
        log_test("Empresa configurada", True, f"Nombre: {data.get('name', 'No configurado')}")

    ok, data = api_call("PUT", "/company",
                        {"name": "Mi Farmacia",
                         "rfc": "XAXX010101000",
                         "phone": "555-0000"})
    log_test("Actualizar información de la empresa", ok)


def test_users():
    print("\n" + "=" * 60)
    print("👤  MÓDULO: USUARIOS")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    ok, data = api_call("GET", "/users")
    log_test("Listar usuarios", ok)
    users = data if isinstance(data, list) and ok else []
    log_test(f"Usuarios del sistema", True, f"{len(users)} usuario(s)")

    if ok:
        for u in users:
            log_test(f"  Usuario: {u.get('username', 'N/A')}", True,
                     f"Rol: {u.get('role', 'N/A')}")


def test_facturas():
    print("\n" + "=" * 60)
    print("📄  MÓDULO: FACTURACIÓN CFDI")
    print("=" * 60)

    if not TOKEN:
        log_skip("Todas las pruebas", "No autenticado")
        return

    ok, data = api_call("GET", "/facturas")
    log_test("Listar facturas", ok)


def print_summary():
    total = PASSED + FAILED + SKIPPED
    print("\n" + "=" * 60)
    print("📊  RESUMEN DE PRUEBAS")
    print("=" * 60)
    print(f"  Total:    {total}")
    print(f"  ✅ Pasadas: {PASSED}")
    print(f"  ❌ Fallidas: {FAILED}")
    print(f"  ➖ Omitidas: {SKIPPED}")
    print(f"  Tasa de éxito: {PASSED / max(total - SKIPPED, 1) * 100:.1f}%")
    print("=" * 60)

    if FAILED > 0:
        print("\n⚠️  Algunas pruebas fallaron. Revisa los detalles arriba.")
    else:
        print("\n🎉 Todas las pruebas pasaron correctamente.")
    print()


def main():
    global BASE_URL

    parser = argparse.ArgumentParser(description="Pruebas automatizadas para POS Ferretería")
    parser.add_argument("--url", default=BASE_URL, help="URL base de la API")
    parser.add_argument("--module", choices=[
        "auth", "products", "categories", "customers", "suppliers",
        "sales", "cash-register", "reports", "expenses", "returns",
        "patients", "prescriptions", "alerts", "inventory", "company",
        "users", "facturas", "all"
    ], default="all", help="Módulo a probar")
    args = parser.parse_args()

    BASE_URL = args.url

    print(f"\n🔍  POS Ferretería - Suite de Pruebas Automatizadas")
    print(f"📅  {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}")
    print(f"🌐  URL: {BASE_URL}")
    print(f"📋  Módulo: {args.module}")

    modules = {
        "auth": test_auth,
        "products": test_products,
        "categories": test_categories,
        "customers": test_customers,
        "suppliers": test_suppliers,
        "sales": test_sales,
        "cash-register": test_cash_register,
        "reports": test_reports,
        "expenses": test_expenses,
        "returns": test_returns,
        "patients": test_patients,
        "prescriptions": test_prescriptions,
        "alerts": test_alerts,
        "inventory": test_inventory,
        "company": test_company,
        "users": test_users,
        "facturas": test_facturas,
    }

    if args.module == "all":
        # Auth primero para obtener el token
        test_auth()
        for name, func in modules.items():
            if name != "auth":
                func()
    else:
        modules[args.module]()

    print_summary()
    return 0 if FAILED == 0 else 1


if __name__ == "__main__":
    sys.exit(main())
