#!/usr/bin/env python3
"""
Suite de Pruebas Integral — POS Ferretería
API: http://localhost:5002/api
"""

import sys, json, random, string, os
from datetime import datetime, timedelta
from urllib.request import Request, urlopen, HTTPError
from urllib.parse import urlencode

BASE = "http://localhost:5002/api"
PASS = "✅"
FAIL = "❌"
SKIP = "⏸️"

def rand_name():
    prefixes = ["Ferreteria", "Construrama", "Tornillos", "Materiales", "Tool"]
    suffixes = ["Express", "Plus", "Max", "Pro", "Center", "Del Norte", "Del Sur"]
    return random.choice(prefixes) + " " + random.choice(suffixes) + " " + str(random.randint(100,999))

def rand_phone():
    return f"55{random.randint(10000000,99999999)}"

def rand_email(name):
    clean = name.lower().replace(" ", "").replace("ñ","n")[:10]
    return f"{clean}{random.randint(1,999)}@test.com"

def rand_id(prefix, length=6):
    return prefix + ''.join(random.choices(string.digits, k=length))

# ── HTTP helper ──
def api(method, path, data=None, token=None):
    url = f"{BASE}{path}"
    headers = {"Content-Type": "application/json"}
    if token:
        headers["Authorization"] = f"Bearer {token}"
    body = json.dumps(data).encode() if data else None
    req = Request(url, data=body, headers=headers, method=method)
    try:
        with urlopen(req, timeout=15) as resp:
            raw = resp.read().decode()
            return json.loads(raw) if raw else {}
    except HTTPError as e:
        raw = e.read().decode()
        try: return json.loads(raw)
        except: return {"error": raw, "status": e.code}
    except Exception as e:
        return {"error": str(e)}

# ── Test runner ──
results = []
def test(module, name, fn):
    try:
        ok, msg = fn()
        symbol = PASS if ok else FAIL
        results.append((module, name, ok))
        print(f"  {symbol} {name}" + (f"\n    → {msg}" if msg else ""))
    except Exception as e:
        results.append((module, name, False))
        print(f"  {FAIL} {name}\n    → EXCEPCIÓN: {e}")

# ── Global state ──
token = None
product_ids = []
customer_ids = []
session_id = None

def rand_price(base=10, max_v=500):
    return round(random.uniform(base, max_v), 2)

# ── Modules ──

def test_auth():
    def login_ok():
        global token
        resp = api("POST", "/auth/login", {"username": "admin", "password": "admin123"})
        token = resp.get("token")
        ok = token is not None
        return ok, f"token obtenido ({token[:20]}...)" if ok else str(resp)
    test("Auth", "Inicio de sesión", login_ok)

    def me_ok():
        if not token: return False, "no token"
        # The /auth/me endpoint is served by auth-me service on port 3050
        try:
            from urllib.request import Request, urlopen
            url = "http://localhost:3050/api/auth/me"
            req = Request(url, headers={"Authorization": f"Bearer {token}"})
            with urlopen(req, timeout=10) as resp:
                data = json.loads(resp.read().decode())
                fullname = data.get("fullname") or data.get("fullName") or data.get("full_name","?")
                ok = data.get("id") is not None
                return ok, f"usuario: {fullname}"
        except Exception as e:
            return False, str(e)
    test("Auth", "Obtener perfil", me_ok)

def test_products():
    def list_ok():
        resp = api("GET", "/products", token=token)
        items = resp if isinstance(resp, list) else []
        ok = len(items) > 0
        return ok, f"{len(items)} productos"
    test("Productos", "Listar productos", list_ok)

    def create_ok():
        name = f"TestProduct_{rand_id('P', 4)}"
        cat_resp = api("GET", "/products/categories", token=token)
        cats = cat_resp if isinstance(cat_resp, list) else []
        cat_id = cats[0]["id"] if cats else 1
        data = {"name": name, "code": rand_id("COD", 6), "categoryId": cat_id,
                "purchasePrice": rand_price(5, 50), "salePrice": rand_price(20, 150),
                "stock": random.randint(10, 100), "minStock": 5, "unit": "Pieza"}
        resp = api("POST", "/products", data, token=token)
        pid = resp.get("id")
        if pid: product_ids.append(pid)
        ok = pid is not None
        return ok, f"id={pid}" if ok else str(resp.get("error","?"))
    test("Productos", "Crear producto", create_ok)

    def search_ok():
        resp = api("GET", "/products/search?q=Martillo", token=token)
        items = resp if isinstance(resp, list) else []
        ok = len(items) > 0
        return ok, f"{len(items)} resultados para 'Martillo'"
    test("Productos", "Buscar producto", search_ok)

    if product_ids:
        pid = product_ids[0]
        def edit_ok():
            # First get full product data to send complete entity
            full = api("GET", f"/products/{pid}", token=token)
            if full.get("id"):
                full["salePrice"] = 99.99
                resp = api("PUT", f"/products/{pid}", full, token=token)
                ok = resp.get("id") == pid
                return ok, f"id={pid}" if ok else str(resp.get("error","?"))
            return False, "producto no encontrado"
        test("Productos", "Editar producto", edit_ok)

def test_categories():
    def cat_ok():
        resp = api("GET", "/products/categories", token=token)
        items = resp if isinstance(resp, list) else []
        ok = len(items) > 0
        return ok, f"{len(items)} categorías"
    test("Categorías", "Listar categorías", cat_ok)

    def count_ok():
        resp = api("GET", "/categories/product-count", token=token)
        ok = isinstance(resp, list)
        return ok, f"{len(resp)} categorías con conteo" if ok else str(resp)
    test("Categorías", "Conteo de productos por categoría", count_ok)

def test_suppliers():
    def sup_ok():
        resp = api("GET", "/products/suppliers", token=token)
        items = resp if isinstance(resp, list) else []
        ok = len(items) > 0
        return ok, f"{len(items)} proveedores"
    test("Proveedores", "Listar proveedores", sup_ok)

def test_customers():
    def list_ok():
        resp = api("GET", "/customers", token=token)
        items = resp if isinstance(resp, list) else []
        ok = len(items) > 0
        return ok, f"{len(items)} clientes"
    test("Clientes", "Listar clientes", list_ok)

    def create_ok():
        name = rand_name()
        data = {"fullName": name, "phone": rand_phone(), "email": rand_email(name),
                "documentType": "INE", "documentNumber": f"TEST{random.randint(100000,999999)}"}
        resp = api("POST", "/customers", data, token=token)
        cid = resp.get("id")
        if cid: customer_ids.append(cid)
        ok = cid is not None
        return ok, f"id={cid}" if ok else str(resp.get("error","?"))
    test("Clientes", "Crear cliente", create_ok)

    if customer_ids:
        def search_ok():
            c = customer_ids[0]
            resp = api("GET", f"/customers/{c}", token=token)
            ok = resp.get("id") == c
            return ok, f"cliente #{c} encontrado"
        test("Clientes", "Obtener cliente por ID", search_ok)

def test_sales():
    def list_ok():
        resp = api("GET", "/sales", token=token)
        items = resp if isinstance(resp, list) else resp.get("sales", [])
        ok = len(items) > 0
        return ok, f"{len(items)} ventas"
    test("Ventas", "Listar ventas", list_ok)

    def create_ok():
        prods = api("GET", "/products", token=token)
        all_prods = prods if isinstance(prods, list) else []
        if len(all_prods) < 2:
            return False, "no hay suficientes productos"
        selected = random.sample(all_prods, min(2, len(all_prods)))
        items = [{"productId": p["id"], "quantity": random.randint(1,3), "unitPrice": p["salePrice"]} for p in selected]
        total = sum(i["quantity"] * i["unitPrice"] for i in items)
        pay_methods = ["Efectivo", "Tarjeta", "Transferencia"]
        method = random.choice(pay_methods)
        data = {"items": items, "paymentMethod": method, "amountReceived": round(total * 1.2, 2),
                "discount": 0, "notes": "Venta generada por test automático"}
        if customer_ids:
            data["customerId"] = random.choice(customer_ids)
        if session_id:
            data["cashRegisterSessionId"] = session_id
        resp = api("POST", "/sales", data, token=token)
        sid = resp.get("id") or resp.get("sale", {}).get("id")
        ok = sid is not None
        return ok, f"id={sid} ({method})" if ok else str(resp.get("error","?"))
    test("Ventas", "Crear venta", create_ok)

def test_cash_register():
    def list_ok():
        resp = api("GET", "/cashregister", token=token)
        items = resp if isinstance(resp, list) else []
        ok = len(items) > 0
        return ok, f"{len(items)} cajas"
    test("Caja", "Listar cajas", list_ok)

    def open_session():
        global session_id
        # Check for existing active session first
        regs = api("GET", "/cashregister", token=token)
        regs_list = regs if isinstance(regs, list) else []
        if not regs_list:
            return False, "no hay cajas disponibles"
        reg_id = regs_list[0]["id"]
        # Try to get current session
        current = api("GET", f"/cashregister/sessions/current/{reg_id}", token=token)
        if current.get("id"):
            session_id = current["id"]
            return True, f"sesión ya activa #{session_id}"
        resp = api("POST", "/cashregister/sessions/open", {"cashRegisterId": reg_id, "openingBalance": 500}, token=token)
        sid = resp.get("id")
        if sid:
            session_id = sid
            return True, f"sesión #{sid}"
        return False, str(resp.get("error","?"))
    test("Caja", "Abrir sesión", open_session)

def test_reports():
    today = datetime.now().strftime("%Y-%m-%d")

    def daily_ok():
        resp = api("GET", f"/reports/daily?date={today}", token=token)
        ok = resp.get("totalSales") is not None
        return ok, f"{resp.get('totalSales')} ventas, ${resp.get('totalRevenue',0):.2f}"
    test("Reportes", "Resumen diario", daily_ok)

    def top_ok():
        resp = api("GET", "/reports/top-products?top=5", token=token)
        items = resp if isinstance(resp, list) else []
        ok = len(items) > 0
        return ok, f"{len(items)} productos"
    test("Reportes", "Top productos", top_ok)

    def inv_ok():
        resp = api("GET", "/reports/inventory", token=token)
        ok = resp.get("totalProducts") is not None
        return ok, f"{resp.get('totalProducts',0)} productos, valor ${resp.get('inventoryValue',0):.2f}"
    test("Reportes", "Estado del inventario", inv_ok)

def test_expenses():
    def list_ok():
        resp = api("GET", "/expenses", token=token)
        items = resp if isinstance(resp, list) else []
        ok = isinstance(resp, list)
        return ok, f"{len(items)} gastos"
    test("Gastos", "Listar gastos", list_ok)

    def create_ok():
        data = {"category": "Servicios", "description": f"Gasto test {rand_id('G',4)}",
                "amount": round(random.uniform(50, 2000), 2), "paymentMethod": "Efectivo"}
        resp = api("POST", "/expenses", data, token=token)
        ok = resp.get("id") is not None
        return ok, f"id={resp.get('id')} ${data['amount']}" if ok else str(resp.get("error","?"))
    test("Gastos", "Crear gasto", create_ok)

def test_alerts():
    def list_ok():
        resp = api("GET", "/alerts", token=token)
        items = resp if isinstance(resp, list) else []
        ok = isinstance(resp, list)
        return ok, f"{len(items)} alertas"
    test("Alertas", "Listar alertas", list_ok)

def test_inventory():
    def list_ok():
        resp = api("GET", "/inventory/movements", token=token)
        items = resp if isinstance(resp, list) else []
        ok = isinstance(resp, list)
        return ok, f"{len(items)} movimientos"
    test("Inventario", "Listar movimientos", list_ok)

    if product_ids:
        def create_ok():
            pid = random.choice(product_ids)
            data = {"productId": pid, "type": "IN", "quantity": 10, "reason": "Entrada de prueba", "reference": rand_id("REF",4)}
            resp = api("POST", "/inventory/movements", data, token=token)
            ok = resp.get("id") is not None
            return ok, f"id={resp.get('id')}" if ok else str(resp.get("error","?"))
        test("Inventario", "Registrar movimiento", create_ok)

def test_company():
    def get_ok():
        resp = api("GET", "/company", token=token)
        ok = resp.get("name") is not None
        return ok, f"Empresa: {resp.get('name','?')}"
    test("Empresa", "Obtener información", get_ok)

def test_settings():
    def list_ok():
        resp = api("GET", "/settings", token=token)
        items = resp if isinstance(resp, list) else []
        ok = isinstance(resp, list) and len(items) > 0
        return ok, f"{len(items)} configuraciones"
    test("Configuración", "Listar configuraciones", list_ok)

def test_facturas():
    def list_ok():
        resp = api("GET", "/facturas", token=token)
        items = resp if isinstance(resp, list) else []
        ok = isinstance(resp, list)
        return ok, f"{len(items)} facturas"
    test("Facturas", "Listar facturas", list_ok)

def test_returns():
    def list_ok():
        resp = api("GET", "/returns", token=token)
        items = resp if isinstance(resp, list) else []
        ok = isinstance(resp, list)
        return ok, f"{len(items)} devoluciones"
    test("Devoluciones", "Listar devoluciones", list_ok)

def test_users():
    def list_ok():
        resp = api("GET", "/auth/users", token=token)
        items = resp if isinstance(resp, list) else []
        ok = isinstance(resp, list) and len(items) > 0
        return ok, f"{len(items)} usuarios"
    test("Usuarios", "Listar usuarios", list_ok)

    def roles_ok():
        resp = api("GET", "/auth/roles", token=token)
        items = resp if isinstance(resp, list) else []
        ok = isinstance(resp, list) and len(items) > 0
        return ok, f"{len(items)} roles"
    test("Usuarios", "Listar roles", roles_ok)


# ── Main ──
if __name__ == "__main__":
    print("#" * 60)
    print("  SISTEMA POS FERRETERÍA — SUITE DE PRUEBAS INTEGRAL")
    print(f"  {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}")
    print(f"  API: {BASE}")
    print("#" * 60)

    # Login first
    test_auth()
    if not token:
        print(f"\n  {FAIL} No se pudo obtener token. Abortando.")
        sys.exit(1)
    print()

    modules = [
        ("Productos", test_products),
        ("Categorías", test_categories),
        ("Proveedores", test_suppliers),
        ("Clientes", test_customers),
        ("Ventas", test_sales),
        ("Caja", test_cash_register),
        ("Reportes", test_reports),
        ("Gastos", test_expenses),
        ("Alertas", test_alerts),
        ("Inventario", test_inventory),
        ("Empresa", test_company),
        ("Configuración", test_settings),
        ("Facturas", test_facturas),
        ("Devoluciones", test_returns),
        ("Usuarios", test_users),
    ]

    for mod_name, mod_fn in modules:
        print(f"{'='*60}")
        print(f"  Módulo: {mod_name}")
        print(f"{'='*60}")
        mod_fn()
        mod_tests = [r for r in results if r[0] == mod_name]
        total = len(mod_tests)
        passed = sum(1 for r in mod_tests if r[2])
        pct = (passed / total * 100) if total > 0 else 0
        print(f"  {'─'*40}")
        print(f"  {'✅' if total==passed else '❌'} {passed}/{total} pruebas pasadas ({pct:.0f}%)")
        print()

    # Final summary
    print("#" * 60)
    print("  RESUMEN FINAL")
    print("#" * 60)
    all_mods = sorted(set(r[0] for r in results))
    print(f"  {'Módulo':<20} {'Pruebas':<8} {'Pasadas':<8} {'Tasa':<8}")
    print(f"  {'─'*20} {'─'*8} {'─'*8} {'─'*8}")
    total_t = len(results)
    total_p = sum(1 for r in results if r[2])
    for mod in all_mods:
        mod_t = [r for r in results if r[0] == mod]
        p = sum(1 for r in mod_t if r[2])
        pct = (p / len(mod_t) * 100) if mod_t else 0
        print(f"  {mod:<20} {len(mod_t):<8} {p:<8} {pct:>6.0f}%")
    print(f"  {'─'*20} {'─'*8} {'─'*8} {'─'*8}")
    print(f"  {'TOTAL':<20} {total_t:<8} {total_p:<8} {(total_p/total_t*100):>6.0f}%")

    if total_p == total_t:
        print(f"\n  {PASS} Tasa de aceptación: 100% ({total_p}/{total_t})")
    else:
        print(f"\n  {PASS} Tasa de aceptación: {(total_p/total_t*100):.0f}% ({total_p}/{total_t})")
        print(f"  {FAIL} Tasa de errores: {((total_t-total_p)/total_t*100):.0f}% ({total_t-total_p}/{total_t})")
    print("#" * 60)
