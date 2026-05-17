--
-- PostgreSQL database dump
--

\restrict QrP7cybWTbfFirGdsJd5aKVKKiwN4EBl0OiVSxm8VzuMFYK9OdBe8s7PRJGlQlq

-- Dumped from database version 15.17 (Debian 15.17-1.pgdg13+1)
-- Dumped by pg_dump version 15.17 (Debian 15.17-1.pgdg13+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Data for Name: roles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.roles (id, name, description, createdat) FROM stdin;
1	Admin	Administrador del sistema	2026-05-15 21:10:51.795406
2	Cajero	Cajero - puede realizar ventas	2026-05-15 21:10:51.795406
3	Inventario	Encargado de inventario	2026-05-15 21:10:51.795406
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (id, username, passwordhash, email, fullname, roleid, isactive, lastlogin, createdat, updatedat) FROM stdin;
2	cajero	$2b$10$UILL0MQ0o1GzVfei2yNg3.VIC5NXDMb9LXiYsV6EOjcQAKWJuZzPG	cajero@ferreteria.com	Cajero Principal	2	t	\N	2026-05-15 21:10:51.804873	\N
1	admin	$2b$10$8Ob1aHw.9VHx0OfXK7nP5ebL0NMIBRPu/7dhlYFtnYf1X4UhqUdBS	admin@ferreteria.com	Administrador	1	t	2026-05-16 00:29:33.019992	2026-05-15 21:10:51.800869	\N
\.


--
-- Data for Name: alerts; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.alerts (id, type, title, message, referencetype, referenceid, isread, userid, createdat) FROM stdin;
\.


--
-- Data for Name: cashregisters; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.cashregisters (id, name, location, isactive, createdat) FROM stdin;
1	Caja Principal	Mostrador Principal	t	2026-05-15 21:10:51.807181
2	Caja Secundaria	Mostrador de Materiales	t	2026-05-15 21:10:51.807181
\.


--
-- Data for Name: cashregistersessions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.cashregistersessions (id, cashregisterid, userid, openingamount, closingamount, expectedamount, difference, openingnotes, closingnotes, openedat, closedat, status) FROM stdin;
\.


--
-- Data for Name: catclavesprodserv; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.catclavesprodserv (id, description, isactive) FROM stdin;
\.


--
-- Data for Name: catclavesunidad; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.catclavesunidad (id, description, isactive) FROM stdin;
\.


--
-- Data for Name: categories; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.categories (id, name, description, isactive, createdat) FROM stdin;
1	Herramientas Manuales	Martillos, desarmadores, llaves, pinzas, etc.	t	2026-05-15 21:10:51.823739
2	Herramientas Eléctricas	Taladros, esmeriles, sierras, etc.	t	2026-05-15 21:10:51.823739
3	Materiales de Construcción	Cemento, varilla, block, grava, arena	t	2026-05-15 21:10:51.823739
4	Plomería	Tuberías, conexiones, llaves de agua, etc.	t	2026-05-15 21:10:51.823739
5	Electricidad	Cables, interruptores, focos, contactos	t	2026-05-15 21:10:51.823739
6	Pinturas y Accesorios	Pinturas, brochas, rodillos, solventes	t	2026-05-15 21:10:51.823739
7	Fijaciones	Tornillos, clavos, taquetes, tuercas	t	2026-05-15 21:10:51.823739
8	Jardinería	Mangueras, aspersores, herramientas de jardín	t	2026-05-15 21:10:51.823739
9	Seguridad Industrial	Cascos, guantes, arneses, lentes	t	2026-05-15 21:10:51.823739
10	Ferretería General	Productos varios de ferretería	t	2026-05-15 21:10:51.823739
\.


--
-- Data for Name: catformaspago; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.catformaspago (id, description, isactive) FROM stdin;
\.


--
-- Data for Name: catmetodospago; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.catmetodospago (id, description, isactive) FROM stdin;
\.


--
-- Data for Name: catregimenesfiscales; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.catregimenesfiscales (id, description, isactive) FROM stdin;
\.


--
-- Data for Name: catusoscfdi; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.catusoscfdi (id, description, isactive) FROM stdin;
\.


--
-- Data for Name: companyinfo; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.companyinfo (id, name, businessname, address, phone, email, logourl, taxid, receiptfooter, slogan, regimenfiscalid) FROM stdin;
1	Mi Ferretería	Ferretería Ejemplo SA de CV	\N	\N	\N	\N	XXXX000000XXX	¡Gracias por su preferencia!	Todo en ferretería y materiales	\N
\.


--
-- Data for Name: customers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.customers (id, documenttype, documentnumber, fullname, phone, email, address, credit_limit, current_balance, is_credit_customer, regimenfiscalid, usocfdiid, createdat, updatedat) FROM stdin;
\.


--
-- Data for Name: expenses; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.expenses (id, category, description, amount, paymentmethod, reference, userid, createdat) FROM stdin;
\.


--
-- Data for Name: sales; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.sales (id, receiptnumber, userid, customerid, subtotal, tax, discount, total, paymentmethod, paymentstatus, sale_type, notes, cashregistersessionid, createdat) FROM stdin;
1	F-260515-0001	1	\N	6.50	1.04	0.00	7.54	Cash	Completed	Cash		\N	2026-05-15 21:47:19.894006
2	F-260515-0002	1	\N	649.00	103.84	0.00	752.84	Efectivo	Completed	Cash		\N	2026-05-15 22:30:44.784783
3	F-260515-0003	1	\N	287.00	45.92	0.00	332.92	Efectivo	Completed	Cash		\N	2026-05-15 22:49:28.736736
\.


--
-- Data for Name: facturas; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.facturas (id, uuid, serie, folio, saleid, customerid, subtotal, total, iva, ivaretenido, isrretenido, descuento, formapago, metodopago, usocfdi, lugarexpedicion, regimenfiscal, xmlcontent, pdfcontent, status, createdbyuserid, createdat, cancelledat) FROM stdin;
\.


--
-- Data for Name: suppliers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.suppliers (id, name, contactname, phone, email, address, rfc, createdat) FROM stdin;
1	Truper SA de CV	Carlos López	555-123-4567	ventas@truper.com	Av. Industrial 123, EdoMex	TRU850101XXX	2026-05-15 21:10:51.827494
2	Urrea SA de CV	María García	555-234-5678	ventas@urrea.com	Blvd. Herramientas 456, NL	URREA010101XXX	2026-05-15 21:10:51.827494
3	CEMEX SA de CV	Pedro Sánchez	555-345-6789	ventas@cemex.com	Carretera Nacional Km 15, NL	CEMEX010101XXX	2026-05-15 21:10:51.827494
4	Comex SA de CV	Ana Martínez	555-456-7890	ventas@comex.com	Av. Pintores 789, CDMX	COMEX010101XXX	2026-05-15 21:10:51.827494
5	Interlub SA de CV	José Hernández	555-567-8901	ventas@interlub.com	Calle Lubricantes 321, CDMX	INTER010101XXX	2026-05-15 21:10:51.827494
\.


--
-- Data for Name: products; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.products (id, code, barcode, name, description, categoryid, supplierid, purchaseprice, saleprice, wholesale_price, stock, minstock, unit, isactive, requires_tax, is_service, createdat, updatedat) FROM stdin;
1	HER-MAR-001	7501001212345	Martillo de Uña 16oz Truper	Martillo de uña con mango de madera, 16 onzas	1	1	85.00	149.00	125.00	50.00	10.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
2	HER-DES-001	7501001223456	Juego de Desarmadores 6pzas Truper	Juego de desarmadores planos y Phillips 6 piezas	1	1	95.00	179.00	150.00	30.00	5.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
3	HER-LLE-001	7501001234567	Llave Ajustable 10" Truper	Llave ajustable de acero al carbono 10 pulgadas	1	1	120.00	219.00	185.00	25.00	5.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
4	HER-PIN-001	7501001245678	Pinza de Corte 8" Truper	Pinza de corte diagonal 8 pulgadas	1	1	75.00	139.00	118.00	40.00	8.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
5	HEL-TAL-001	7501001312345	Taladro Percutor 1/2" 600W Truper	Taladro percutor con velocidad variable 600W	2	1	450.00	899.00	750.00	15.00	3.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
6	HEL-ESM-001	7501001323456	Esmeril Angular 4 1/2" Truper	Esmeril angular 850W con disco de corte	2	1	380.00	759.00	640.00	12.00	3.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
7	HEL-SIE-001	7501001334567	Sierra Caladora 500W Truper	Sierra caladora con velocidad variable 500W	2	1	320.00	659.00	550.00	8.00	2.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
8	HEL-LIJ-001	7501001345678	Lijadora Orbital 250W Truper	Lijadora orbital 1/4 de hoja 250W	2	1	290.00	589.00	490.00	10.00	2.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
10	MAT-VAR-001	7501001423456	Varilla Corrugada 3/8" 12m	Varilla de acero corrugada 3/8 pulgada 12 metros	3	3	85.00	155.00	130.00	150.00	30.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
11	MAT-BLO-001	7501001434567	Block Hueco 15x20x40	Block hueco de concreto 15x20x40 cm	3	3	4.50	8.50	7.00	500.00	100.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
12	MAT-GRA-001	7501001445678	Grava 1/2" Metro Cúbico	Grava de 1/2 pulgada para construcción	3	3	250.00	450.00	380.00	30.00	5.00	m3	t	t	f	2026-05-15 21:10:51.832248	\N
13	PLO-TUB-001	7501001512345	Tubo PVC 1/2" 6m	Tubo de PVC hidráulico 1/2 pulgada 6 metros	4	1	25.00	49.00	40.00	100.00	20.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
15	PLO-LLA-001	7501001534567	Llave de Agua Jardín 1/2"	Llave de agua para jardín con rosca 1/2 pulgada	4	1	45.00	89.00	75.00	60.00	10.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
16	PLO-TEE-001	7501001545678	Tee PVC 1/2"	Tee de PVC hidráulico 1/2 pulgada	4	1	3.50	7.00	5.50	180.00	30.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
18	ELE-FOC-001	7501001623456	Foco LED 12W Luz Blanca	Foco LED 12W luz blanca 6500K	5	1	18.00	39.00	32.00	80.00	15.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
19	ELE-INT-001	7501001634567	Interruptor Sencillo Volteck	Interruptor sencillo color blanco 10A	5	1	12.00	29.00	23.00	100.00	20.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
20	ELE-CON-001	7501001645678	Contacto Doble 15A Volteck	Contacto doble polarizado 15A color blanco	5	1	15.00	35.00	28.00	90.00	15.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
21	PIN-VIN-001	7501001712345	Pintura Vinílica Blanca 19L	Pintura vinílica blanca Comex 19 litros	6	4	350.00	699.00	590.00	25.00	5.00	l	t	t	f	2026-05-15 21:10:51.832248	\N
22	PIN-ESM-001	7501001723456	Esmalte Sintético Negro 1L	Esmalte sintético color negro 1 litro	6	4	65.00	139.00	115.00	40.00	8.00	l	t	t	f	2026-05-15 21:10:51.832248	\N
23	PIN-BRO-001	7501001734567	Brocha Plana 2" Truper	Brocha plana de cerdas sintéticas 2 pulgadas	6	1	15.00	35.00	28.00	60.00	10.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
24	PIN-ROD-001	7501001745678	Rodillo para Pintura 9"	Rodillo para pintura 9 pulgadas con mango	6	1	25.00	55.00	45.00	45.00	8.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
25	FIJ-TOR-001	7501001812345	Tornillo para Madera 2" Bolsa 50pzas	Tornillo para madera 2 pulgadas bolsa 50 piezas	7	1	12.00	29.00	24.00	150.00	30.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
27	FIJ-TAQ-001	7501001834567	Taquete Fischer 8mm Bolsa 20pzas	Taquete de expansión Fischer 8mm bolsa 20 piezas	7	1	8.00	19.00	15.00	200.00	40.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
28	FIJ-TUE-001	7501001845678	Tuerca Hexagonal 1/2" 10pzas	Tuerca hexagonal galvanizada 1/2 pulgada 10 piezas	7	1	5.00	13.00	10.00	180.00	35.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
29	JAR-MAN-001	7501001912345	Manguera Jardín 5/8" 15m	Manguera para jardín 5/8 pulgada 15 metros	8	1	85.00	169.00	140.00	30.00	5.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
30	JAR-ASP-001	7501001923456	Aspersor de Impacto Metal	Aspersor de impacto para jardín de metal	8	1	35.00	75.00	62.00	20.00	4.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
31	JAR-TIJ-001	7501001934567	Tijera para Podar 8"	Tijera de podar con mango ergonómico 8 pulgadas	8	1	55.00	109.00	90.00	25.00	5.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
33	SEG-GUA-001	7501002023456	Guantes de Carnaza	Guantes de carnaza para trabajo pesado	9	1	25.00	55.00	45.00	50.00	10.00	par	t	t	f	2026-05-15 21:10:51.832248	\N
34	SEG-LEN-001	7501002034567	Lentes de Seguridad Transparentes	Lentes de seguridad con protección UV	9	1	8.00	22.00	17.00	80.00	15.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
14	PLO-COB-001	7501001523456	Codo PVC 1/2" 90°	Codo de PVC hidráulico 1/2 pulgada 90 grados	4	1	3.00	6.50	5.00	199.00	40.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
17	ELE-CAB-001	7501001612345	Cable Eléctrico 12 AWG 100m	Cable de cobre THW 12 AWG 100 metros	5	1	350.00	649.00	550.00	19.00	5.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
9	MAT-CEM-001	7501001412345	Cemento Gris 50kg CEMEX	Cemento Portland gris 50 kg	3	3	95.00	169.00	145.00	199.00	50.00	kg	t	t	f	2026-05-15 21:10:51.832248	\N
26	FIJ-CLA-001	7501001823456	Clavo 2 1/2" 1kg	Clavo de acero 2 1/2 pulgadas 1 kilogramo	7	1	18.00	39.00	32.00	79.00	15.00	kg	t	t	f	2026-05-15 21:10:51.832248	\N
32	SEG-CAS-001	7501002012345	Casco de Seguridad Industrial	Casco de seguridad con suspensión ajustable	9	1	35.00	79.00	65.00	39.00	10.00	pza	t	t	f	2026-05-15 21:10:51.832248	\N
\.


--
-- Data for Name: facturaitems; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.facturaitems (id, facturaid, productoid, claveservicio, claveunidad, descripcion, cantidad, unidad, valorunitario, importe, descuento, iva, ivatasa) FROM stdin;
\.


--
-- Data for Name: facturarelaciones; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.facturarelaciones (id, facturaid, tiporelacion, uuidrelacionado) FROM stdin;
\.


--
-- Data for Name: inventorymovements; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.inventorymovements (id, productid, type, quantity, referencetype, referenceid, notes, userid, createdat) FROM stdin;
1	14	OUT	1.00	Sale	0	\N	1	2026-05-15 21:47:19.929782
2	17	OUT	1.00	Sale	0	\N	1	2026-05-15 22:30:44.796651
3	26	OUT	1.00	Sale	0	\N	1	2026-05-15 22:49:28.739765
4	9	OUT	1.00	Sale	0	\N	1	2026-05-15 22:49:28.742017
5	32	OUT	1.00	Sale	0	\N	1	2026-05-15 22:49:28.743508
\.


--
-- Data for Name: loginlogs; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.loginlogs (id, userid, ipaddress, action, createdat) FROM stdin;
1	1	\N	Login	2026-05-15 21:11:52.886917
2	1	\N	Login	2026-05-15 21:47:52.958678
3	1	\N	Login	2026-05-15 21:48:06.353567
4	1	\N	Login	2026-05-15 21:48:18.640119
5	1	\N	Login	2026-05-15 21:55:30.581173
6	1	\N	Login	2026-05-15 22:00:30.037487
7	1	\N	Login	2026-05-15 22:15:28.178681
8	1	\N	Login	2026-05-16 00:29:33.02744
\.


--
-- Data for Name: payments; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.payments (id, saleid, amount, paymentmethod, reference, createdat) FROM stdin;
1	1	7.54	Cash	\N	2026-05-15 21:47:19.967114
2	2	752.84	Efectivo	\N	2026-05-15 22:30:44.796948
3	3	332.92	Efectivo	\N	2026-05-15 22:49:28.743662
\.


--
-- Data for Name: returns; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.returns (id, saleid, userid, reason, total, createdat) FROM stdin;
\.


--
-- Data for Name: returnitems; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.returnitems (id, returnid, productid, quantity, unitprice, subtotal) FROM stdin;
\.


--
-- Data for Name: saleitems; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.saleitems (id, saleid, productid, quantity, unitprice, discount, subtotal) FROM stdin;
1	1	14	1.00	6.50	0.00	6.50
2	2	17	1.00	649.00	0.00	649.00
3	3	26	1.00	39.00	0.00	39.00
4	3	9	1.00	169.00	0.00	169.00
5	3	32	1.00	79.00	0.00	79.00
\.


--
-- Data for Name: systemsettings; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.systemsettings (id, key, value, description) FROM stdin;
1	store.name	Mi Ferretería	Nombre del negocio
2	store.address	Dirección del negocio	Dirección fiscal
3	store.phone	000-000-0000	Teléfono de contacto
4	store.tax_id	XXXX000000XXX	RFC del negocio
5	receipt.footer	¡Gracias por su preferencia!	Pie de página del ticket
6	pos.tax_rate	16	Tasa de impuesto predeterminada (%)
7	pos.currency	MXN	Moneda del sistema
8	inventory.low_stock_alert	10	Alerta de stock bajo
9	cfdi.serie	F	Serie para facturación CFDI
10	cfdi.regimen_fiscal	601	Régimen fiscal por defecto
11	cfdi.lugar_expedicion	Lugar de Expedición	Lugar de expedición del CFDI
\.


--
-- Data for Name: taxrates; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.taxrates (id, name, rate, isactive, createdat) FROM stdin;
1	IVA 16%	16.00	t	2026-05-15 21:10:51.81084
2	IVA 8%	8.00	t	2026-05-15 21:10:51.81084
3	Exento	0.00	t	2026-05-15 21:10:51.81084
\.


--
-- Name: alerts_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.alerts_id_seq', 1, false);


--
-- Name: cashregisters_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.cashregisters_id_seq', 2, true);


--
-- Name: cashregistersessions_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.cashregistersessions_id_seq', 1, false);


--
-- Name: categories_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.categories_id_seq', 10, true);


--
-- Name: companyinfo_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.companyinfo_id_seq', 1, true);


--
-- Name: customers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.customers_id_seq', 1, false);


--
-- Name: expenses_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.expenses_id_seq', 1, false);


--
-- Name: facturaitems_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.facturaitems_id_seq', 1, false);


--
-- Name: facturarelaciones_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.facturarelaciones_id_seq', 1, false);


--
-- Name: facturas_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.facturas_id_seq', 1, false);


--
-- Name: inventorymovements_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.inventorymovements_id_seq', 5, true);


--
-- Name: loginlogs_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.loginlogs_id_seq', 8, true);


--
-- Name: payments_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.payments_id_seq', 3, true);


--
-- Name: products_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.products_id_seq', 34, true);


--
-- Name: returnitems_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.returnitems_id_seq', 1, false);


--
-- Name: returns_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.returns_id_seq', 1, false);


--
-- Name: roles_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.roles_id_seq', 3, true);


--
-- Name: saleitems_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.saleitems_id_seq', 5, true);


--
-- Name: sales_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.sales_id_seq', 3, true);


--
-- Name: suppliers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.suppliers_id_seq', 5, true);


--
-- Name: systemsettings_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.systemsettings_id_seq', 11, true);


--
-- Name: taxrates_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.taxrates_id_seq', 3, true);


--
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_id_seq', 2, true);


--
-- PostgreSQL database dump complete
--

\unrestrict QrP7cybWTbfFirGdsJd5aKVKKiwN4EBl0OiVSxm8VzuMFYK9OdBe8s7PRJGlQlq

