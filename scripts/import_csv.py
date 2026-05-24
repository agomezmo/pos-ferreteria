#!/usr/bin/env python3
"""
Importador masivo de productos desde archivo CSV.
Usa el entorno virtual .venv/ si existe.

Uso:
  ../tests/.venv/bin/python import_csv.py --csv ../data/productos.csv --url http://localhost:4000

Formato CSV esperado:
  code,name,description,category_id,purchase_price,sale_price,stock,min_stock,unit,requires_prescription,requires_tax,expiration_date,supplier_name
  PAR-001,Paracetamol 500mg,...,1,5.50,12.00,100,10,pza,false,true,2027-06-15,Distribuidora X
"""

import csv
import sys
import json
import time
import argparse

try:
    import requests
except ImportError:
    print("Error: pip install requests")
    sys.exit(1)


def login(url, username, password):
    r = requests.post(f"{url}/api/auth/login",
                      json={"username": username, "password": password},
                      timeout=10)
    if r.status_code == 200:
        data = r.json()
        return data.get("token")
    print(f"  Error de login: {r.status_code} {r.text[:100]}")
    return None


def get_products(url, token):
    r = requests.get(f"{url}/api/products",
                     headers={"Authorization": f"Bearer {token}"},
                     timeout=10)
    if r.status_code == 200:
        data = r.json()
        if isinstance(data, list):
            return data
        return data.get("products", [])
    return []


def delete_product(url, token, product_id):
    r = requests.delete(f"{url}/api/products/{product_id}",
                        headers={"Authorization": f"Bearer {token}"},
                        timeout=10)
    return r.status_code in (200, 204)


def create_product(url, token, product):
    r = requests.post(f"{url}/api/products",
                      json=product,
                      headers={
                          "Authorization": f"Bearer {token}",
                          "Content-Type": "application/json"
                      },
                      timeout=10)
    if r.status_code in (200, 201):
        return True, r.json()
    return False, r.text[:200]


def main():
    parser = argparse.ArgumentParser(description="Importar productos desde CSV")
    parser.add_argument("--csv", required=True, help="Ruta al archivo CSV")
    parser.add_argument("--url", default="http://localhost:4000", help="URL base de la API")
    parser.add_argument("--user", default="admin", help="Usuario")
    parser.add_argument("--password", default="admin123", help="Contraseña")
    parser.add_argument("--clear", action="store_true", help="Eliminar productos existentes antes de importar")
    parser.add_argument("--delimiter", default=",", help="Delimitador CSV")
    parser.add_argument("--batch", type=int, default=10, help="Pausa cada N productos")
    args = parser.parse_args()

    print(f"🔑 Iniciando sesión en {args.url}...")
    token = login(args.url, args.user, args.password)
    if not token:
        sys.exit(1)
    print("  ✅ Login exitoso")

    # Leer CSV
    products = []
    with open(args.csv, newline="", encoding="utf-8") as f:
        reader = csv.DictReader(f, delimiter=args.delimiter)
        for row in reader:
            # Limpiar espacios en nombres de columna
            clean = {}
            for k, v in row.items():
                clean[k.strip()] = v.strip() if v else ""
            products.append(clean)

    print(f"📄 Leídos {len(products)} productos del CSV")

    # Opcional: eliminar existentes
    if args.clear:
        print("🗑️  Eliminando productos existentes...")
        existing = get_products(args.url, token)
        count = 0
        for p in existing:
            pid = p.get("id")
            if pid and delete_product(args.url, token, pid):
                count += 1
        print(f"  ✅ Eliminados {count} productos")
        # Pequeña pausa para que el API se estabilice
        time.sleep(1)

    # Importar
    imported = 0
    errors = 0
    total = len(products)

    print(f"\n🚀 Importando {total} productos...")
    for i, prod in enumerate(products, 1):
        # Construir payload
        payload = {
            "code": prod.get("code", "").strip(),
            "name": prod.get("name", "").strip(),
            "description": prod.get("description", "").strip(),
            "categoryid": int(prod.get("category_id", 0) or 0),
            "purchaseprice": float(prod.get("purchase_price", 0) or 0),
            "saleprice": float(prod.get("sale_price", 0) or 0),
            "stock": int(float(prod.get("stock", 0) or 0)),
            "minstock": int(float(prod.get("min_stock", 0) or 0)),
            "unit": prod.get("unit", "pza").strip() or "pza",
            "requiresprescription": prod.get("requires_prescription", "false").strip().lower() == "true",
            "requires_tax": prod.get("requires_tax", "true").strip().lower() != "false",
        }

        # Expiration date (solo si está presente)
        exp = prod.get("expiration_date", "").strip()
        if exp:
            payload["expirationdate"] = exp

        ok, result = create_product(args.url, token, payload)
        if ok:
            imported += 1
            suffix = f"ID={result.get('id', '?')}" if isinstance(result, dict) else ""
            print(f"  ✅ [{i}/{total}] {payload['code']} - {payload['name']} {suffix}")
        else:
            errors += 1
            print(f"  ❌ [{i}/{total}] {payload['code']} - {payload['name']}: {result[:80]}")

        # Pausa cada batch para no sobrecargar
        if i % args.batch == 0 and i < total:
            time.sleep(0.5)

    print(f"\n{'='*50}")
    print(f"📊 RESULTADO:")
    print(f"  ✅ Importados: {imported}")
    print(f"  ❌ Errores:    {errors}")
    print(f"  📄 Total CSV:  {total}")
    print(f"{'='*50}")

    return 0 if errors == 0 else 1


if __name__ == "__main__":
    sys.exit(main())
