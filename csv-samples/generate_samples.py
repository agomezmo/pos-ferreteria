#!/usr/bin/env python3
"""Generate CSV sample files with 100+ records for Ferreteria POS."""

import csv, random, os

CATEGORIES = ["Tubería", "Eléctrico", "Ferretería", "Pinturas", "Jardín",
              "Herramientas", "Seguridad", "Fontanería", "Construcción", "Automotriz"]
ADJ = ["Galvanizado", "PVC", "Acero Inoxidable", "Reforzado", "Profesional",
       "Industrial", "Económico", "Premium", "Básico", "Pesado"]
NOUNS = [
    "Tubo 1/2\"", "Codo 90°", "Válvula de Compuerta", "Llave Inglesa 10\"",
    "Martillo de Uña", "Destornillador Plano", "Cinta Aislante", "Broca 1/4\"",
    "Disco de Corte 7\"", "Lija al Agua #180", "Pintura Esmalte Blanco",
    "Sellador de Silicon", "Candado 40mm", "Cerradura para Puerta",
    "Manguera 1/2\" x 15m", "Conector Rápido", "Adaptador Universal",
    "Cable THW #12", "Interruptor Sencillo", "Contacto Polarizado",
    "Foco LED 12W", "Lámpara de Emergencia", "Extensión Eléctrica 5m",
    "Multímetro Digital", "Taladro Percutor", "Esmeril Angular 4 1/2\"",
    "Sierra Caladora", "Cinta Métrica 5m", "Nivel de Aluminio 24\"",
    "Pala Cuadrada", "Pico de Punta", "Carretilla 6.5ft³",
    "Cubeta 19L", "Brocha 2\"", "Rodillo para Pintura 9\"",
    "Clavo 2 1/2\" Caja 1kg", "Tornillo P/Tablaroca 1 5/8\" Caja 100",
    "Taquete 1/4\" Caja 50", "Arandela 1/2\" Caja 100", "Tuerca 1/2\" Caja 50",
]


def generate_products(n=120):
    rows = []
    seen = set()
    while len(rows) < n:
        adj = random.choice(ADJ)
        noun = random.choice(NOUNS)
        name = f"{adj} {noun}"
        if name in seen:
            continue
        seen.add(name)
        code = f"FER-{random.randint(10000, 99999)}"
        cat = random.choice(CATEGORIES)
        purchase = round(random.uniform(5, 5000), 2)
        price = round(purchase * random.uniform(1.2, 1.8), 2)
        wholesale = round(price * 0.88, 2)
        stock = random.choices(
            [random.randint(0, 5), random.randint(5, 50), random.randint(50, 500)],
            weights=[15, 40, 45],
        )[0]
        min_stock = random.choice([1, 2, 3, 5, 10, 15])
        unit = random.choice(["PZA", "KG", "M", "CAJA", "LT", "MT"])
        active = random.choices(["1", "0"], weights=[90, 10])[0]
        rows.append([
            code, name, cat, f"{purchase:.2f}", f"{price:.2f}", f"{wholesale:.2f}",
            str(stock), str(min_stock), unit, active,
        ])
    return rows


FIRST_NAMES = [
    "Juan", "María", "Carlos", "Ana", "Luis", "Sofía", "José", "Laura",
    "Pedro", "Fernanda", "Miguel", "Isabel", "Francisco", "Gabriela",
    "Jorge", "Verónica", "Ricardo", "Patricia", "Daniel", "Carmen",
    "Alejandro", "Rosa", "Manuel", "Elena", "Javier", "Diana",
    "Alberto", "Claudia", "Rafael", "Silvia",
]
LAST_NAMES = [
    "García", "López", "Martínez", "Rodríguez", "Hernández", "Pérez",
    "González", "Mendoza", "Castillo", "Ramos", "Cruz", "Ortega",
    "Vargas", "Reyes", "Guzmán", "Morales", "Ortiz", "Delgado",
    "Flores", "Sánchez", "Torres", "Rivera", "Díaz", "Chávez", "Ruiz",
]
DOC_TYPES = ["INE", "Pasaporte", "Cédula"]


def generate_customers(n=120):
    rows = []
    seen = set()
    while len(rows) < n:
        first = random.choice(FIRST_NAMES)
        last1 = random.choice(LAST_NAMES)
        last2 = random.choice(LAST_NAMES)
        name = f"{first} {last1} {last2}"
        if name in seen:
            continue
        seen.add(name)
        rfc_letters = (last1[0:2] + first[0] + last2[0]).upper()
        rfc_date = f"{random.randint(50, 99)}{random.randint(1, 12):02d}{random.randint(1, 28):02d}"
        rfc = f"{rfc_letters}{rfc_date}{random.choice(['XXX', 'HDF', 'MDF', 'ABC', 'XYZ'])}"
        doc_type = random.choice(DOC_TYPES)
        doc_num = f"{random.choice([chr(c) for c in range(65, 91)])}{random.randint(100000000, 999999999)}"
        phone = f"555-{random.randint(1000, 9999)}"
        email = f"{first.lower()}.{last1.lower()}@email.com"
        address = f"Calle {random.choice(['Principal', 'Central', 'Norte', 'Sur', 'Oriente', 'Poniente'])} #{random.randint(1, 999)}"
        rows.append([name, doc_type, doc_num, phone, email, address, rfc])
    return rows


def write_csv(path, headers, data):
    os.makedirs(os.path.dirname(path) or ".", exist_ok=True)
    with open(path, "w", newline="") as f:
        w = csv.writer(f)
        w.writerow(headers)
        w.writerows(data)
    print(f"  ✓ {path} ({len(data)} records)")


if __name__ == "__main__":
    base = os.path.dirname(os.path.abspath(__file__))

    print("Generating productos.csv …")
    prods = generate_products(120)
    write_csv(os.path.join(base, "productos.csv"),
              ["code", "name", "category", "purchase_price", "sale_price",
               "wholesale_price", "stock", "min_stock", "unit", "is_active"],
              prods)

    print("Generating clientes.csv …")
    custs = generate_customers(120)
    write_csv(os.path.join(base, "clientes.csv"),
              ["full_name", "document_type", "document_number", "phone",
               "email", "address", "rfc"],
              custs)
