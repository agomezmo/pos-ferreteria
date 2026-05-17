export interface User {
  id: number;
  username: string;
  email: string;
  fullname: string;
  role: string;
  isactive: boolean;
  lastlogin: string;
  createdat: string;
}

export interface AuthResponse {
  token: string;
  user: User;
}

export interface Product {
  id: number;
  code: string;
  barcode: string;
  name: string;
  description: string;
  categoryid: number;
  category_name: string;
  supplierid: number;
  supplier_name: string;
  purchaseprice: number;
  saleprice: number;
  stock: number;
  minstock: number;
  unit: string;
  isactive: boolean;
  requiresprescription: boolean;
  wholesale_price: number;
  expiry_date: string;
  requires_tax: boolean;
  createdat: string;
  updatedat: string;
}

export interface Category {
  id: number;
  name: string;
  description: string;
  isactive: boolean;
}

export interface Customer {
  id: number;
  documenttype: string;
  documentnumber: string;
  fullname: string;
  phone: string;
  email: string;
  address: string;
  rfc: string;
  razonsocial: string;
  codigopostal: string;
  regimenfiscalid: number;
  regimen_fiscal_desc: string;
  usocfdiid: number;
  uso_cfdi_desc: string;
}

export interface Supplier {
  id: number;
  name: string;
  contactname: string;
  phone: string;
  email: string;
  address: string;
}

export interface Sale {
  id: number;
  receiptnumber: string;
  userid: number;
  user_name: string;
  customerid: number;
  customer_name: string;
  subtotal: number;
  tax: number;
  discount: number;
  total: number;
  paymentmethod: string;
  paymentstatus: string;
  notes: string;
  amountreceived: number;
  change: number;
  createdat: string;
  items: SaleItem[];
  payments: Payment[];
}

export interface SaleItem {
  id: number;
  saleid: number;
  productid: number;
  product_name: string;
  product_code: string;
  quantity: number;
  unitprice: number;
  discount: number;
  subtotal: number;
}

export interface Payment {
  id: number;
  saleid: number;
  amount: number;
  paymentmethod: string;
  reference: string;
  createdat: string;
}

export interface CreateSaleDto {
  customerid?: number;
  items: CreateSaleItemDto[];
  paymentmethod: string;
  amountreceived: number;
  discount: number;
  notes: string;
}

export interface CreateSaleItemDto {
  productid: number;
  quantity: number;
  unitprice: number;
}

export interface CashRegister {
  id: number;
  name: string;
  isactive: boolean;
}

export interface CashRegisterSession {
  id: number;
  cashregisterid: number;
  cashregister_name: string;
  userid: number;
  user_name: string;
  openingbalance: number;
  closingbalance: number;
  openingdate: string;
  closingdate: string;
  status: string;
  notes: string;
  total_sales: number;
  total_cash: number;
  total_card: number;
  total_transfer: number;
}

export interface Return {
  id: number;
  saleid: number;
  receiptnumber: string;
  userid: number;
  user_name: string;
  reason: string;
  total: number;
  createdat: string;
  items: ReturnItem[];
}

export interface ReturnItem {
  id: number;
  returnid: number;
  productid: number;
  product_name: string;
  quantity: number;
  unitprice: number;
  subtotal: number;
}

export interface Expense {
  id: number;
  description: string;
  amount: number;
  category: string;
  paymentmethod: string;
  reference: string;
  notes: string;
  userid: number;
  user_name: string;
  createdat: string;
}

export interface Patient {
  id: number;
  customerid: number;
  customer_name: string;
  fullname: string;
  dateofbirth: string;
  phone: string;
  email: string;
  address: string;
  bloodtype: string;
  allergies: string;
  medicalnotes: string;
  createdat: string;
}

export interface Prescription {
  id: number;
  patientid: number;
  patient_name: string;
  doctor_name: string;
  license_number: string;
  diagnosis: string;
  issuedate: string;
  expirydate: string;
  notes: string;
  isactive: boolean;
  items: PrescriptionItem[];
}

export interface PrescriptionItem {
  id: number;
  prescriptionid: number;
  productid: number;
  product_name: string;
  dosage: string;
  frequency: string;
  duration: string;
  quantity: number;
}

export interface Alert {
  id: number;
  type: string;
  title: string;
  message: string;
  severity: string;
  isread: boolean;
  createdat: string;
}

export interface InventoryMovement {
  id: number;
  productid: number;
  product_name: string;
  type: string;
  quantity: number;
  reason: string;
  reference: string;
  userid: number;
  user_name: string;
  createdat: string;
}

export interface CompanyInfo {
  id: number;
  name: string;
  rfc: string;
  address: string;
  phone: string;
  email: string;
  logo_url: string;
}

export interface SystemSetting {
  id: number;
  key: string;
  value: string;
  description: string;
}

export interface Role {
  id: number;
  name: string;
  description: string;
}

export interface CatRegimenFiscal {
  id: number;
  codigo: string;
  descripcion: string;
}

export interface CatUsoCfdi {
  id: number;
  codigo: string;
  descripcion: string;
}

export interface CatFormaPago {
  id: number;
  codigo: string;
  descripcion: string;
}

export interface CatMetodoPago {
  id: number;
  codigo: string;
  descripcion: string;
}

export interface Factura {
  id: number;
  saleid: number;
  uuid: string;
  serie: string;
  folio: string;
  rfc: string;
  razonsocial: string;
  total: number;
  status: string;
  createdat: string;
}
