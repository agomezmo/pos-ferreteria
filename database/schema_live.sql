--
-- PostgreSQL database dump
--

\restrict lUCq3IVUc9q68dNjXoXoi7FvgJBe2X5uBYjKRgtBQLlRhymkGymxYvXWk2RPcgb

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

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: alerts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.alerts (
    id integer NOT NULL,
    type character varying(30) NOT NULL,
    title character varying(200) NOT NULL,
    message character varying(500),
    referencetype character varying(50),
    referenceid integer,
    isread boolean DEFAULT false,
    userid integer,
    createdat timestamp without time zone DEFAULT now()
);


ALTER TABLE public.alerts OWNER TO postgres;

--
-- Name: alerts_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.alerts_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.alerts_id_seq OWNER TO postgres;

--
-- Name: alerts_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.alerts_id_seq OWNED BY public.alerts.id;


--
-- Name: cashregisters; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.cashregisters (
    id integer NOT NULL,
    name character varying(100) NOT NULL,
    location character varying(200),
    isactive boolean DEFAULT true,
    createdat timestamp without time zone DEFAULT now()
);


ALTER TABLE public.cashregisters OWNER TO postgres;

--
-- Name: cashregisters_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.cashregisters_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.cashregisters_id_seq OWNER TO postgres;

--
-- Name: cashregisters_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.cashregisters_id_seq OWNED BY public.cashregisters.id;


--
-- Name: cashregistersessions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.cashregistersessions (
    id integer NOT NULL,
    cashregisterid integer NOT NULL,
    userid integer NOT NULL,
    openingamount numeric(18,2) DEFAULT 0,
    closingamount numeric(18,2),
    expectedamount numeric(18,2) DEFAULT 0,
    difference numeric(18,2) DEFAULT 0,
    openingnotes character varying(200),
    closingnotes character varying(200),
    openedat timestamp without time zone DEFAULT now(),
    closedat timestamp without time zone,
    status character varying(20) DEFAULT 'Open'::character varying NOT NULL
);


ALTER TABLE public.cashregistersessions OWNER TO postgres;

--
-- Name: cashregistersessions_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.cashregistersessions_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.cashregistersessions_id_seq OWNER TO postgres;

--
-- Name: cashregistersessions_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.cashregistersessions_id_seq OWNED BY public.cashregistersessions.id;


--
-- Name: catclavesprodserv; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.catclavesprodserv (
    id character varying(10) NOT NULL,
    description character varying(300) NOT NULL,
    isactive boolean DEFAULT true
);


ALTER TABLE public.catclavesprodserv OWNER TO postgres;

--
-- Name: catclavesunidad; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.catclavesunidad (
    id character varying(10) NOT NULL,
    description character varying(200) NOT NULL,
    isactive boolean DEFAULT true
);


ALTER TABLE public.catclavesunidad OWNER TO postgres;

--
-- Name: categories; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.categories (
    id integer NOT NULL,
    name character varying(100) NOT NULL,
    description character varying(200),
    isactive boolean DEFAULT true,
    createdat timestamp without time zone DEFAULT now()
);


ALTER TABLE public.categories OWNER TO postgres;

--
-- Name: categories_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.categories_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.categories_id_seq OWNER TO postgres;

--
-- Name: categories_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.categories_id_seq OWNED BY public.categories.id;


--
-- Name: catformaspago; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.catformaspago (
    id character varying(10) NOT NULL,
    description character varying(200) NOT NULL,
    isactive boolean DEFAULT true
);


ALTER TABLE public.catformaspago OWNER TO postgres;

--
-- Name: catmetodospago; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.catmetodospago (
    id character varying(10) NOT NULL,
    description character varying(200) NOT NULL,
    isactive boolean DEFAULT true
);


ALTER TABLE public.catmetodospago OWNER TO postgres;

--
-- Name: catregimenesfiscales; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.catregimenesfiscales (
    id character varying(10) NOT NULL,
    description character varying(200) NOT NULL,
    isactive boolean DEFAULT true
);


ALTER TABLE public.catregimenesfiscales OWNER TO postgres;

--
-- Name: catusoscfdi; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.catusoscfdi (
    id character varying(10) NOT NULL,
    description character varying(200) NOT NULL,
    isactive boolean DEFAULT true
);


ALTER TABLE public.catusoscfdi OWNER TO postgres;

--
-- Name: companyinfo; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.companyinfo (
    id integer NOT NULL,
    name character varying(200) NOT NULL,
    businessname character varying(200),
    address character varying(200),
    phone character varying(20),
    email character varying(100),
    logourl character varying(500),
    taxid character varying(20),
    receiptfooter character varying(300),
    slogan character varying(100),
    regimenfiscalid character varying(10)
);


ALTER TABLE public.companyinfo OWNER TO postgres;

--
-- Name: companyinfo_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.companyinfo_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.companyinfo_id_seq OWNER TO postgres;

--
-- Name: companyinfo_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.companyinfo_id_seq OWNED BY public.companyinfo.id;


--
-- Name: customers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.customers (
    id integer NOT NULL,
    documenttype character varying(20) DEFAULT 'DNI'::character varying NOT NULL,
    documentnumber character varying(20) NOT NULL,
    fullname character varying(100) NOT NULL,
    phone character varying(20),
    email character varying(100),
    address character varying(200),
    credit_limit numeric(18,2) DEFAULT 0,
    current_balance numeric(18,2) DEFAULT 0,
    is_credit_customer boolean DEFAULT false,
    regimenfiscalid character varying(10),
    usocfdiid character varying(10),
    createdat timestamp without time zone DEFAULT now(),
    updatedat timestamp without time zone
);


ALTER TABLE public.customers OWNER TO postgres;

--
-- Name: customers_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.customers_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.customers_id_seq OWNER TO postgres;

--
-- Name: customers_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.customers_id_seq OWNED BY public.customers.id;


--
-- Name: expenses; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.expenses (
    id integer NOT NULL,
    category character varying(100) NOT NULL,
    description character varying(500) NOT NULL,
    amount numeric(18,2) NOT NULL,
    paymentmethod character varying(30),
    reference character varying(100),
    userid integer,
    createdat timestamp without time zone DEFAULT now()
);


ALTER TABLE public.expenses OWNER TO postgres;

--
-- Name: expenses_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.expenses_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.expenses_id_seq OWNER TO postgres;

--
-- Name: expenses_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.expenses_id_seq OWNED BY public.expenses.id;


--
-- Name: facturaitems; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.facturaitems (
    id integer NOT NULL,
    facturaid integer NOT NULL,
    productoid integer,
    claveservicio character varying(10) NOT NULL,
    claveunidad character varying(10) NOT NULL,
    descripcion character varying(200) NOT NULL,
    cantidad numeric(18,6) NOT NULL,
    unidad character varying(20),
    valorunitario numeric(18,2) NOT NULL,
    importe numeric(18,2) NOT NULL,
    descuento numeric(18,2) DEFAULT 0,
    iva numeric(18,2) DEFAULT 0,
    ivatasa numeric(18,2) DEFAULT 0
);


ALTER TABLE public.facturaitems OWNER TO postgres;

--
-- Name: facturaitems_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.facturaitems_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.facturaitems_id_seq OWNER TO postgres;

--
-- Name: facturaitems_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.facturaitems_id_seq OWNED BY public.facturaitems.id;


--
-- Name: facturarelaciones; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.facturarelaciones (
    id integer NOT NULL,
    facturaid integer NOT NULL,
    tiporelacion character varying(10) NOT NULL,
    uuidrelacionado character varying(50) NOT NULL
);


ALTER TABLE public.facturarelaciones OWNER TO postgres;

--
-- Name: facturarelaciones_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.facturarelaciones_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.facturarelaciones_id_seq OWNER TO postgres;

--
-- Name: facturarelaciones_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.facturarelaciones_id_seq OWNED BY public.facturarelaciones.id;


--
-- Name: facturas; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.facturas (
    id integer NOT NULL,
    uuid character varying(50) NOT NULL,
    serie character varying(20) NOT NULL,
    folio character varying(20) NOT NULL,
    saleid integer NOT NULL,
    customerid integer,
    subtotal numeric(18,2) DEFAULT 0,
    total numeric(18,2) DEFAULT 0,
    iva numeric(18,2) DEFAULT 0,
    ivaretenido numeric(18,2) DEFAULT 0,
    isrretenido numeric(18,2) DEFAULT 0,
    descuento numeric(18,2) DEFAULT 0,
    formapago character varying(10) NOT NULL,
    metodopago character varying(10) NOT NULL,
    usocfdi character varying(10) NOT NULL,
    lugarexpedicion character varying(200) NOT NULL,
    regimenfiscal character varying(10) NOT NULL,
    xmlcontent character varying(500),
    pdfcontent character varying(500),
    status character varying(20) DEFAULT 'Pending'::character varying NOT NULL,
    createdbyuserid integer,
    createdat timestamp without time zone DEFAULT now(),
    cancelledat timestamp without time zone
);


ALTER TABLE public.facturas OWNER TO postgres;

--
-- Name: facturas_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.facturas_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.facturas_id_seq OWNER TO postgres;

--
-- Name: facturas_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.facturas_id_seq OWNED BY public.facturas.id;


--
-- Name: inventorymovements; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.inventorymovements (
    id integer NOT NULL,
    productid integer NOT NULL,
    type character varying(10) NOT NULL,
    quantity numeric(18,2) NOT NULL,
    referencetype character varying(50),
    referenceid integer,
    notes character varying(500),
    userid integer,
    createdat timestamp without time zone DEFAULT now()
);


ALTER TABLE public.inventorymovements OWNER TO postgres;

--
-- Name: inventorymovements_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.inventorymovements_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.inventorymovements_id_seq OWNER TO postgres;

--
-- Name: inventorymovements_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.inventorymovements_id_seq OWNED BY public.inventorymovements.id;


--
-- Name: loginlogs; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.loginlogs (
    id integer NOT NULL,
    userid integer,
    ipaddress character varying(50),
    action character varying(20) NOT NULL,
    createdat timestamp without time zone DEFAULT now()
);


ALTER TABLE public.loginlogs OWNER TO postgres;

--
-- Name: loginlogs_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.loginlogs_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.loginlogs_id_seq OWNER TO postgres;

--
-- Name: loginlogs_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.loginlogs_id_seq OWNED BY public.loginlogs.id;


--
-- Name: payments; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.payments (
    id integer NOT NULL,
    saleid integer NOT NULL,
    amount numeric(18,2) NOT NULL,
    paymentmethod character varying(30) NOT NULL,
    reference character varying(100),
    createdat timestamp without time zone DEFAULT now()
);


ALTER TABLE public.payments OWNER TO postgres;

--
-- Name: payments_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.payments_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.payments_id_seq OWNER TO postgres;

--
-- Name: payments_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.payments_id_seq OWNED BY public.payments.id;


--
-- Name: products; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.products (
    id integer NOT NULL,
    code character varying(50) NOT NULL,
    barcode character varying(50),
    name character varying(200) NOT NULL,
    description character varying(500),
    categoryid integer NOT NULL,
    supplierid integer,
    purchaseprice numeric(18,2) DEFAULT 0,
    saleprice numeric(18,2) DEFAULT 0,
    wholesale_price numeric(18,2) DEFAULT 0,
    stock numeric(18,2) DEFAULT 0,
    minstock numeric(18,2) DEFAULT 0,
    unit character varying(20) DEFAULT 'pza'::character varying,
    isactive boolean DEFAULT true,
    requires_tax boolean DEFAULT true,
    is_service boolean DEFAULT false,
    createdat timestamp without time zone DEFAULT now(),
    updatedat timestamp without time zone
);


ALTER TABLE public.products OWNER TO postgres;

--
-- Name: products_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.products_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.products_id_seq OWNER TO postgres;

--
-- Name: products_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.products_id_seq OWNED BY public.products.id;


--
-- Name: returnitems; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.returnitems (
    id integer NOT NULL,
    returnid integer NOT NULL,
    productid integer NOT NULL,
    quantity numeric(18,2) NOT NULL,
    unitprice numeric(18,2) DEFAULT 0,
    subtotal numeric(18,2) DEFAULT 0
);


ALTER TABLE public.returnitems OWNER TO postgres;

--
-- Name: returnitems_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.returnitems_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.returnitems_id_seq OWNER TO postgres;

--
-- Name: returnitems_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.returnitems_id_seq OWNED BY public.returnitems.id;


--
-- Name: returns; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.returns (
    id integer NOT NULL,
    saleid integer NOT NULL,
    userid integer,
    reason character varying(500) NOT NULL,
    total numeric(18,2) DEFAULT 0,
    createdat timestamp without time zone DEFAULT now()
);


ALTER TABLE public.returns OWNER TO postgres;

--
-- Name: returns_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.returns_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.returns_id_seq OWNER TO postgres;

--
-- Name: returns_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.returns_id_seq OWNED BY public.returns.id;


--
-- Name: roles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.roles (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    description character varying(200),
    createdat timestamp without time zone DEFAULT now()
);


ALTER TABLE public.roles OWNER TO postgres;

--
-- Name: roles_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.roles_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.roles_id_seq OWNER TO postgres;

--
-- Name: roles_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.roles_id_seq OWNED BY public.roles.id;


--
-- Name: saleitems; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.saleitems (
    id integer NOT NULL,
    saleid integer NOT NULL,
    productid integer NOT NULL,
    quantity numeric(18,2) NOT NULL,
    unitprice numeric(18,2) DEFAULT 0,
    discount numeric(18,2) DEFAULT 0,
    subtotal numeric(18,2) DEFAULT 0
);


ALTER TABLE public.saleitems OWNER TO postgres;

--
-- Name: saleitems_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.saleitems_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.saleitems_id_seq OWNER TO postgres;

--
-- Name: saleitems_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.saleitems_id_seq OWNED BY public.saleitems.id;


--
-- Name: sales; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.sales (
    id integer NOT NULL,
    receiptnumber character varying(20) NOT NULL,
    userid integer NOT NULL,
    customerid integer,
    subtotal numeric(18,2) DEFAULT 0,
    tax numeric(18,2) DEFAULT 0,
    discount numeric(18,2) DEFAULT 0,
    total numeric(18,2) DEFAULT 0,
    paymentmethod character varying(30) NOT NULL,
    paymentstatus character varying(20) DEFAULT 'Completed'::character varying NOT NULL,
    sale_type character varying(20) DEFAULT 'Cash'::character varying,
    notes character varying(500),
    cashregistersessionid integer,
    createdat timestamp without time zone DEFAULT now()
);


ALTER TABLE public.sales OWNER TO postgres;

--
-- Name: sales_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.sales_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.sales_id_seq OWNER TO postgres;

--
-- Name: sales_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.sales_id_seq OWNED BY public.sales.id;


--
-- Name: suppliers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.suppliers (
    id integer NOT NULL,
    name character varying(100) NOT NULL,
    contactname character varying(100),
    phone character varying(20),
    email character varying(100),
    address character varying(200),
    rfc character varying(20),
    createdat timestamp without time zone DEFAULT now()
);


ALTER TABLE public.suppliers OWNER TO postgres;

--
-- Name: suppliers_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.suppliers_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.suppliers_id_seq OWNER TO postgres;

--
-- Name: suppliers_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.suppliers_id_seq OWNED BY public.suppliers.id;


--
-- Name: systemsettings; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.systemsettings (
    id integer NOT NULL,
    key character varying(100) NOT NULL,
    value text NOT NULL,
    description character varying(200)
);


ALTER TABLE public.systemsettings OWNER TO postgres;

--
-- Name: systemsettings_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.systemsettings_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.systemsettings_id_seq OWNER TO postgres;

--
-- Name: systemsettings_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.systemsettings_id_seq OWNED BY public.systemsettings.id;


--
-- Name: taxrates; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.taxrates (
    id integer NOT NULL,
    name character varying(100) NOT NULL,
    rate numeric(5,2) NOT NULL,
    isactive boolean DEFAULT true,
    createdat timestamp without time zone DEFAULT now()
);


ALTER TABLE public.taxrates OWNER TO postgres;

--
-- Name: taxrates_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.taxrates_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.taxrates_id_seq OWNER TO postgres;

--
-- Name: taxrates_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.taxrates_id_seq OWNED BY public.taxrates.id;


--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    id integer NOT NULL,
    username character varying(50) NOT NULL,
    passwordhash text NOT NULL,
    email character varying(100),
    fullname character varying(100) NOT NULL,
    roleid integer NOT NULL,
    isactive boolean DEFAULT true,
    lastlogin timestamp without time zone,
    createdat timestamp without time zone DEFAULT now(),
    updatedat timestamp without time zone
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_id_seq OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;


--
-- Name: alerts id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.alerts ALTER COLUMN id SET DEFAULT nextval('public.alerts_id_seq'::regclass);


--
-- Name: cashregisters id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.cashregisters ALTER COLUMN id SET DEFAULT nextval('public.cashregisters_id_seq'::regclass);


--
-- Name: cashregistersessions id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.cashregistersessions ALTER COLUMN id SET DEFAULT nextval('public.cashregistersessions_id_seq'::regclass);


--
-- Name: categories id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categories ALTER COLUMN id SET DEFAULT nextval('public.categories_id_seq'::regclass);


--
-- Name: companyinfo id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.companyinfo ALTER COLUMN id SET DEFAULT nextval('public.companyinfo_id_seq'::regclass);


--
-- Name: customers id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customers ALTER COLUMN id SET DEFAULT nextval('public.customers_id_seq'::regclass);


--
-- Name: expenses id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.expenses ALTER COLUMN id SET DEFAULT nextval('public.expenses_id_seq'::regclass);


--
-- Name: facturaitems id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.facturaitems ALTER COLUMN id SET DEFAULT nextval('public.facturaitems_id_seq'::regclass);


--
-- Name: facturarelaciones id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.facturarelaciones ALTER COLUMN id SET DEFAULT nextval('public.facturarelaciones_id_seq'::regclass);


--
-- Name: facturas id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.facturas ALTER COLUMN id SET DEFAULT nextval('public.facturas_id_seq'::regclass);


--
-- Name: inventorymovements id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inventorymovements ALTER COLUMN id SET DEFAULT nextval('public.inventorymovements_id_seq'::regclass);


--
-- Name: loginlogs id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.loginlogs ALTER COLUMN id SET DEFAULT nextval('public.loginlogs_id_seq'::regclass);


--
-- Name: payments id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.payments ALTER COLUMN id SET DEFAULT nextval('public.payments_id_seq'::regclass);


--
-- Name: products id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products ALTER COLUMN id SET DEFAULT nextval('public.products_id_seq'::regclass);


--
-- Name: returnitems id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.returnitems ALTER COLUMN id SET DEFAULT nextval('public.returnitems_id_seq'::regclass);


--
-- Name: returns id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.returns ALTER COLUMN id SET DEFAULT nextval('public.returns_id_seq'::regclass);


--
-- Name: roles id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.roles ALTER COLUMN id SET DEFAULT nextval('public.roles_id_seq'::regclass);


--
-- Name: saleitems id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.saleitems ALTER COLUMN id SET DEFAULT nextval('public.saleitems_id_seq'::regclass);


--
-- Name: sales id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sales ALTER COLUMN id SET DEFAULT nextval('public.sales_id_seq'::regclass);


--
-- Name: suppliers id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.suppliers ALTER COLUMN id SET DEFAULT nextval('public.suppliers_id_seq'::regclass);


--
-- Name: systemsettings id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.systemsettings ALTER COLUMN id SET DEFAULT nextval('public.systemsettings_id_seq'::regclass);


--
-- Name: taxrates id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.taxrates ALTER COLUMN id SET DEFAULT nextval('public.taxrates_id_seq'::regclass);


--
-- Name: users id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);


--
-- Name: alerts alerts_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.alerts
    ADD CONSTRAINT alerts_pkey PRIMARY KEY (id);


--
-- Name: cashregisters cashregisters_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.cashregisters
    ADD CONSTRAINT cashregisters_pkey PRIMARY KEY (id);


--
-- Name: cashregistersessions cashregistersessions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.cashregistersessions
    ADD CONSTRAINT cashregistersessions_pkey PRIMARY KEY (id);


--
-- Name: catclavesprodserv catclavesprodserv_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.catclavesprodserv
    ADD CONSTRAINT catclavesprodserv_pkey PRIMARY KEY (id);


--
-- Name: catclavesunidad catclavesunidad_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.catclavesunidad
    ADD CONSTRAINT catclavesunidad_pkey PRIMARY KEY (id);


--
-- Name: categories categories_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categories
    ADD CONSTRAINT categories_pkey PRIMARY KEY (id);


--
-- Name: catformaspago catformaspago_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.catformaspago
    ADD CONSTRAINT catformaspago_pkey PRIMARY KEY (id);


--
-- Name: catmetodospago catmetodospago_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.catmetodospago
    ADD CONSTRAINT catmetodospago_pkey PRIMARY KEY (id);


--
-- Name: catregimenesfiscales catregimenesfiscales_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.catregimenesfiscales
    ADD CONSTRAINT catregimenesfiscales_pkey PRIMARY KEY (id);


--
-- Name: catusoscfdi catusoscfdi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.catusoscfdi
    ADD CONSTRAINT catusoscfdi_pkey PRIMARY KEY (id);


--
-- Name: companyinfo companyinfo_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.companyinfo
    ADD CONSTRAINT companyinfo_pkey PRIMARY KEY (id);


--
-- Name: customers customers_documentnumber_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_documentnumber_key UNIQUE (documentnumber);


--
-- Name: customers customers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (id);


--
-- Name: expenses expenses_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.expenses
    ADD CONSTRAINT expenses_pkey PRIMARY KEY (id);


--
-- Name: facturaitems facturaitems_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.facturaitems
    ADD CONSTRAINT facturaitems_pkey PRIMARY KEY (id);


--
-- Name: facturarelaciones facturarelaciones_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.facturarelaciones
    ADD CONSTRAINT facturarelaciones_pkey PRIMARY KEY (id);


--
-- Name: facturas facturas_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.facturas
    ADD CONSTRAINT facturas_pkey PRIMARY KEY (id);


--
-- Name: inventorymovements inventorymovements_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inventorymovements
    ADD CONSTRAINT inventorymovements_pkey PRIMARY KEY (id);


--
-- Name: loginlogs loginlogs_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.loginlogs
    ADD CONSTRAINT loginlogs_pkey PRIMARY KEY (id);


--
-- Name: payments payments_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.payments
    ADD CONSTRAINT payments_pkey PRIMARY KEY (id);


--
-- Name: products products_code_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_code_key UNIQUE (code);


--
-- Name: products products_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_pkey PRIMARY KEY (id);


--
-- Name: returnitems returnitems_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.returnitems
    ADD CONSTRAINT returnitems_pkey PRIMARY KEY (id);


--
-- Name: returns returns_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.returns
    ADD CONSTRAINT returns_pkey PRIMARY KEY (id);


--
-- Name: roles roles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (id);


--
-- Name: saleitems saleitems_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.saleitems
    ADD CONSTRAINT saleitems_pkey PRIMARY KEY (id);


--
-- Name: sales sales_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sales
    ADD CONSTRAINT sales_pkey PRIMARY KEY (id);


--
-- Name: sales sales_receiptnumber_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sales
    ADD CONSTRAINT sales_receiptnumber_key UNIQUE (receiptnumber);


--
-- Name: suppliers suppliers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.suppliers
    ADD CONSTRAINT suppliers_pkey PRIMARY KEY (id);


--
-- Name: systemsettings systemsettings_key_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.systemsettings
    ADD CONSTRAINT systemsettings_key_key UNIQUE (key);


--
-- Name: systemsettings systemsettings_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.systemsettings
    ADD CONSTRAINT systemsettings_pkey PRIMARY KEY (id);


--
-- Name: taxrates taxrates_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.taxrates
    ADD CONSTRAINT taxrates_pkey PRIMARY KEY (id);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- Name: alerts alerts_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.alerts
    ADD CONSTRAINT alerts_userid_fkey FOREIGN KEY (userid) REFERENCES public.users(id);


--
-- Name: cashregistersessions cashregistersessions_cashregisterid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.cashregistersessions
    ADD CONSTRAINT cashregistersessions_cashregisterid_fkey FOREIGN KEY (cashregisterid) REFERENCES public.cashregisters(id);


--
-- Name: cashregistersessions cashregistersessions_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.cashregistersessions
    ADD CONSTRAINT cashregistersessions_userid_fkey FOREIGN KEY (userid) REFERENCES public.users(id);


--
-- Name: expenses expenses_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.expenses
    ADD CONSTRAINT expenses_userid_fkey FOREIGN KEY (userid) REFERENCES public.users(id);


--
-- Name: facturaitems facturaitems_facturaid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.facturaitems
    ADD CONSTRAINT facturaitems_facturaid_fkey FOREIGN KEY (facturaid) REFERENCES public.facturas(id) ON DELETE CASCADE;


--
-- Name: facturaitems facturaitems_productoid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.facturaitems
    ADD CONSTRAINT facturaitems_productoid_fkey FOREIGN KEY (productoid) REFERENCES public.products(id);


--
-- Name: facturarelaciones facturarelaciones_facturaid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.facturarelaciones
    ADD CONSTRAINT facturarelaciones_facturaid_fkey FOREIGN KEY (facturaid) REFERENCES public.facturas(id) ON DELETE CASCADE;


--
-- Name: facturas facturas_createdbyuserid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.facturas
    ADD CONSTRAINT facturas_createdbyuserid_fkey FOREIGN KEY (createdbyuserid) REFERENCES public.users(id);


--
-- Name: facturas facturas_customerid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.facturas
    ADD CONSTRAINT facturas_customerid_fkey FOREIGN KEY (customerid) REFERENCES public.customers(id);


--
-- Name: facturas facturas_saleid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.facturas
    ADD CONSTRAINT facturas_saleid_fkey FOREIGN KEY (saleid) REFERENCES public.sales(id);


--
-- Name: inventorymovements inventorymovements_productid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inventorymovements
    ADD CONSTRAINT inventorymovements_productid_fkey FOREIGN KEY (productid) REFERENCES public.products(id);


--
-- Name: inventorymovements inventorymovements_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inventorymovements
    ADD CONSTRAINT inventorymovements_userid_fkey FOREIGN KEY (userid) REFERENCES public.users(id);


--
-- Name: loginlogs loginlogs_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.loginlogs
    ADD CONSTRAINT loginlogs_userid_fkey FOREIGN KEY (userid) REFERENCES public.users(id);


--
-- Name: payments payments_saleid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.payments
    ADD CONSTRAINT payments_saleid_fkey FOREIGN KEY (saleid) REFERENCES public.sales(id) ON DELETE CASCADE;


--
-- Name: products products_categoryid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_categoryid_fkey FOREIGN KEY (categoryid) REFERENCES public.categories(id);


--
-- Name: products products_supplierid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_supplierid_fkey FOREIGN KEY (supplierid) REFERENCES public.suppliers(id);


--
-- Name: returnitems returnitems_productid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.returnitems
    ADD CONSTRAINT returnitems_productid_fkey FOREIGN KEY (productid) REFERENCES public.products(id);


--
-- Name: returnitems returnitems_returnid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.returnitems
    ADD CONSTRAINT returnitems_returnid_fkey FOREIGN KEY (returnid) REFERENCES public.returns(id) ON DELETE CASCADE;


--
-- Name: returns returns_saleid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.returns
    ADD CONSTRAINT returns_saleid_fkey FOREIGN KEY (saleid) REFERENCES public.sales(id);


--
-- Name: returns returns_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.returns
    ADD CONSTRAINT returns_userid_fkey FOREIGN KEY (userid) REFERENCES public.users(id);


--
-- Name: saleitems saleitems_productid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.saleitems
    ADD CONSTRAINT saleitems_productid_fkey FOREIGN KEY (productid) REFERENCES public.products(id);


--
-- Name: saleitems saleitems_saleid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.saleitems
    ADD CONSTRAINT saleitems_saleid_fkey FOREIGN KEY (saleid) REFERENCES public.sales(id) ON DELETE CASCADE;


--
-- Name: sales sales_cashregistersessionid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sales
    ADD CONSTRAINT sales_cashregistersessionid_fkey FOREIGN KEY (cashregistersessionid) REFERENCES public.cashregistersessions(id);


--
-- Name: sales sales_customerid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sales
    ADD CONSTRAINT sales_customerid_fkey FOREIGN KEY (customerid) REFERENCES public.customers(id);


--
-- Name: sales sales_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sales
    ADD CONSTRAINT sales_userid_fkey FOREIGN KEY (userid) REFERENCES public.users(id);


--
-- Name: users users_roleid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_roleid_fkey FOREIGN KEY (roleid) REFERENCES public.roles(id);


--
-- PostgreSQL database dump complete
--

\unrestrict lUCq3IVUc9q68dNjXoXoi7FvgJBe2X5uBYjKRgtBQLlRhymkGymxYvXWk2RPcgb

