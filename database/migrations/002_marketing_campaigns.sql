-- Migration: Marketing Campaigns
-- Adds tables for promotional campaigns and WhatsApp integration

CREATE TABLE IF NOT EXISTS public.promo_campaigns (
    id integer NOT NULL,
    name character varying(200) NOT NULL,
    description character varying(500),
    status character varying(20) DEFAULT 'draft'::character varying NOT NULL,
    offer_type character varying(20) NOT NULL,
    offer_value numeric(18,2),
    min_expiry_days integer DEFAULT 30,
    max_expiry_days integer DEFAULT 90,
    notes character varying(500),
    created_by integer,
    created_at timestamp without time zone DEFAULT now(),
    updated_at timestamp without time zone,
    sent_at timestamp without time zone
);

ALTER TABLE public.promo_campaigns OWNER TO postgres;

CREATE SEQUENCE IF NOT EXISTS public.promo_campaigns_id_seq
    AS integer START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE CACHE 1;

ALTER SEQUENCE public.promo_campaigns_id_seq OWNED BY public.promo_campaigns.id;

ALTER TABLE ONLY public.promo_campaigns ALTER COLUMN id SET DEFAULT nextval('public.promo_campaigns_id_seq'::regclass);
ALTER TABLE ONLY public.promo_campaigns ADD CONSTRAINT promo_campaigns_pkey PRIMARY KEY (id);
ALTER TABLE ONLY public.promo_campaigns ADD CONSTRAINT promo_campaigns_created_by_fkey FOREIGN KEY (created_by) REFERENCES public.users(id);

CREATE TABLE IF NOT EXISTS public.promo_campaign_products (
    id integer NOT NULL,
    campaign_id integer NOT NULL,
    product_id integer NOT NULL,
    offer_price numeric(18,2) NOT NULL,
    original_price numeric(18,2) NOT NULL,
    expiry_date date
);

ALTER TABLE public.promo_campaign_products OWNER TO postgres;

CREATE SEQUENCE IF NOT EXISTS public.promo_campaign_products_id_seq
    AS integer START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE CACHE 1;

ALTER SEQUENCE public.promo_campaign_products_id_seq OWNED BY public.promo_campaign_products.id;

ALTER TABLE ONLY public.promo_campaign_products ALTER COLUMN id SET DEFAULT nextval('public.promo_campaign_products_id_seq'::regclass);
ALTER TABLE ONLY public.promo_campaign_products ADD CONSTRAINT promo_campaign_products_pkey PRIMARY KEY (id);
ALTER TABLE ONLY public.promo_campaign_products ADD CONSTRAINT promo_campaign_products_campaign_id_fkey FOREIGN KEY (campaign_id) REFERENCES public.promo_campaigns(id) ON DELETE CASCADE;
ALTER TABLE ONLY public.promo_campaign_products ADD CONSTRAINT promo_campaign_products_product_id_fkey FOREIGN KEY (product_id) REFERENCES public.products(id);

CREATE TABLE IF NOT EXISTS public.promo_campaign_customers (
    id integer NOT NULL,
    campaign_id integer NOT NULL,
    customer_id integer NOT NULL,
    contact_email character varying(100),
    contact_phone character varying(20)
);

ALTER TABLE public.promo_campaign_customers OWNER TO postgres;

CREATE SEQUENCE IF NOT EXISTS public.promo_campaign_customers_id_seq
    AS integer START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE CACHE 1;

ALTER SEQUENCE public.promo_campaign_customers_id_seq OWNED BY public.promo_campaign_customers.id;

ALTER TABLE ONLY public.promo_campaign_customers ALTER COLUMN id SET DEFAULT nextval('public.promo_campaign_customers_id_seq'::regclass);
ALTER TABLE ONLY public.promo_campaign_customers ADD CONSTRAINT promo_campaign_customers_pkey PRIMARY KEY (id);
ALTER TABLE ONLY public.promo_campaign_customers ADD CONSTRAINT promo_campaign_customers_campaign_id_fkey FOREIGN KEY (campaign_id) REFERENCES public.promo_campaigns(id) ON DELETE CASCADE;
ALTER TABLE ONLY public.promo_campaign_customers ADD CONSTRAINT promo_campaign_customers_customer_id_fkey FOREIGN KEY (customer_id) REFERENCES public.customers(id);

CREATE TABLE IF NOT EXISTS public.promo_campaign_log (
    id integer NOT NULL,
    campaign_id integer NOT NULL,
    customer_id integer,
    channel character varying(20) NOT NULL,
    recipient character varying(100),
    subject character varying(500),
    message text,
    status character varying(20) DEFAULT 'pending'::character varying NOT NULL,
    error_message character varying(500),
    sent_at timestamp without time zone DEFAULT now(),
    created_at timestamp without time zone DEFAULT now()
);

ALTER TABLE public.promo_campaign_log OWNER TO postgres;

CREATE SEQUENCE IF NOT EXISTS public.promo_campaign_log_id_seq
    AS integer START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE CACHE 1;

ALTER SEQUENCE public.promo_campaign_log_id_seq OWNED BY public.promo_campaign_log.id;

ALTER TABLE ONLY public.promo_campaign_log ALTER COLUMN id SET DEFAULT nextval('public.promo_campaign_log_id_seq'::regclass);
ALTER TABLE ONLY public.promo_campaign_log ADD CONSTRAINT promo_campaign_log_pkey PRIMARY KEY (id);
ALTER TABLE ONLY public.promo_campaign_log ADD CONSTRAINT promo_campaign_log_campaign_id_fkey FOREIGN KEY (campaign_id) REFERENCES public.promo_campaigns(id) ON DELETE CASCADE;
ALTER TABLE ONLY public.promo_campaign_log ADD CONSTRAINT promo_campaign_log_customer_id_fkey FOREIGN KEY (customer_id) REFERENCES public.customers(id);
