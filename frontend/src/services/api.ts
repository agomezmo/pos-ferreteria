import axios, { AxiosError, InternalAxiosRequestConfig } from 'axios';

const api = axios.create({
  baseURL: '/api',
  headers: { 'Content-Type': 'application/json' },
});

api.interceptors.request.use((config: InternalAxiosRequestConfig) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

api.interceptors.response.use(
  (response) => response,
  (error: AxiosError) => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

export default api;

/* ── Helpers to map backend field names ── */

function mapDailyReport(d: any) {
  return {
    total_sales: d.totalSales ?? d.TotalSales ?? 0,
    total_revenue: d.totalRevenue ?? d.TotalRevenue ?? 0,
    total_tax: d.totalTax ?? d.TotalTax ?? 0,
    total_discounts: d.totalDiscount ?? d.TotalDiscount ?? 0,
    unique_customers: d.uniqueCustomers ?? d.UniqueCustomers ?? 0,
    total_cash: d.totalCash ?? d.TotalCash ?? 0,
    total_card: d.totalCard ?? d.TotalCard ?? 0,
    total_transfer: d.totalTransfer ?? d.TotalTransfer ?? 0,
    total_expenses: d.totalExpenses ?? d.TotalExpenses ?? 0,
    total_products_sold: d.totalProductsSold ?? d.TotalProductsSold ?? 0,
    average_ticket: d.averageTicket ?? d.AverageTicket ?? 0,
  };
}

function mapInventoryStatus(d: any) {
  return {
    total_products: d.totalProducts ?? d.TotalProducts ?? 0,
    low_stock_count: d.lowStockProducts ?? d.LowStockProducts ?? 0,
    out_of_stock_count: d.outOfStockProducts ?? d.OutOfStockProducts ?? 0,
    inventory_value: d.totalInventoryValue ?? d.TotalInventoryValue ?? 0,
    low_stock_items: d.lowStockItems ?? d.LowStockItems ?? [],
  };
}

function mapTopProduct(p: any) {
  return {
    id: p.productId ?? p.ProductId ?? p.id,
    name: p.productName ?? p.ProductName ?? p.name,
    code: p.productCode ?? p.ProductCode ?? p.code,
    category_name: p.categoryName ?? p.CategoryName ?? p.category_name,
    total_quantity: p.totalQuantity ?? p.TotalQuantity ?? p.total_quantity ?? 0,
    total_revenue: p.totalRevenue ?? p.TotalRevenue ?? p.total_revenue ?? 0,
  };
}

/* Auth */
export const authApi = {
  login: (username: string, password: string) =>
    api.post('/auth/login', { username, password }),
  me: () => api.get('/auth/me'),
};

/* Products */
export const productsApi = {
  getAll: (params?: any) => api.get('/products', { params }),
  getById: (id: number) => api.get(`/products/${id}`),
  create: (data: any) => api.post('/products', data),
  update: (id: number, data: any) => api.put(`/products/${id}`, data),
  delete: (id: number) => api.delete(`/products/${id}`),
  getLowStock: (threshold?: number) => api.get('/products/low-stock', { params: { threshold } }),
  getByCategory: (categoryId: number) => api.get(`/products/category/${categoryId}`),
  toggleActive: (id: number) => api.patch(`/products/${id}/toggle-active`),
};

/* Categories */
export const categoriesApi = {
  getAll: () => api.get('/products/categories'),
  getById: (id: number) => api.get(`/products/categories`),
  create: (data: any) => api.post('/categories', data),
  update: (id: number, data: any) => api.put(`/categories/${id}`, data),
  delete: (id: number) => api.delete(`/categories/${id}`),
};

/* Customers */
export const customersApi = {
  getAll: (params?: any) => api.get('/customers', { params }),
  getById: (id: number) => api.get(`/customers/${id}`),
  create: (data: any) => api.post('/customers', data),
  update: (id: number, data: any) => api.put(`/customers/${id}`, data),
  delete: (id: number) => api.delete(`/customers/${id}`),
};

/* Suppliers */
export const suppliersApi = {
  getAll: () => api.get('/suppliers'),
  getById: (id: number) => api.get(`/suppliers/${id}`),
  create: (data: any) => api.post('/suppliers', data),
  update: (id: number, data: any) => api.put(`/suppliers/${id}`, data),
  delete: (id: number) => api.delete(`/suppliers/${id}`),
};

/* Sales */
export const salesApi = {
  getAll: (params?: any) => api.get('/sales', { params }),
  getById: (id: number) => api.get(`/sales/${id}`),
  create: (data: any) => api.post('/sales', data),
};

/* Cash Register */
export const cashRegisterApi = {
  getAll: () => api.get('/cashregister'),
  getSessions: (params?: any) => api.get('/cashregister/sessions', { params }),
  openSession: (data: any) => api.post('/cashregister/sessions/open', data),
  closeSession: (id: number, data: any) => api.post(`/cashregister/sessions/${id}/close`, data),
  getCurrentSession: (cashRegisterId: number) => api.get(`/cashregister/sessions/current/${cashRegisterId}`),
};

/* Returns */
export const returnsApi = {
  getAll: (params?: any) => api.get('/returns', { params }),
  getById: (id: number) => api.get(`/returns/${id}`),
  create: (data: any) => api.post('/returns', data),
};

/* Expenses */
export const expensesApi = {
  getAll: (params?: any) => api.get('/expenses', { params }),
  getById: (id: number) => api.get(`/expenses/${id}`),
  create: (data: any) => api.post('/expenses', data),
  update: (id: number, data: any) => api.put(`/expenses/${id}`, data),
  delete: (id: number) => api.delete(`/expenses/${id}`),
};

/* Patients */
export const patientsApi = {
  getAll: (params?: any) => api.get('/patients', { params }),
  getById: (id: number) => api.get(`/patients/${id}`),
  create: (data: any) => api.post('/patients', data),
  update: (id: number, data: any) => api.put(`/patients/${id}`, data),
  delete: (id: number) => api.delete(`/patients/${id}`),
};

/* Prescriptions */
export const prescriptionsApi = {
  getAll: (params?: any) => api.get('/prescriptions', { params }),
  getById: (id: number) => api.get(`/prescriptions/${id}`),
  create: (data: any) => api.post('/prescriptions', data),
};

/* Alerts */
export const alertsApi = {
  getAll: () => api.get('/alerts'),
  markAsRead: (id: number) => api.post(`/alerts/${id}/read`),
  markAllAsRead: () => api.post('/alerts/read-all'),
};

/* Inventory */
export const inventoryApi = {
  getAll: (params?: any) => api.get('/inventory', { params }),
  create: (data: any) => api.post('/inventory', data),
};

/* Company */
export const companyApi = {
  get: () => api.get('/company'),
  update: (data: any) => api.put('/company', data),
};

/* Users */
export const usersApi = {
  getAll: () => api.get('/users'),
  getById: (id: number) => api.get(`/users/${id}`),
  create: (data: any) => api.post('/users', data),
  update: (id: number, data: any) => api.put(`/users/${id}`, data),
  updatePassword: (id: number, data: any) => api.put(`/users/${id}/password`, data),
  delete: (id: number) => api.delete(`/users/${id}`),
};

/* Settings */
export const settingsApi = {
  getAll: () => api.get('/settings'),
  update: (key: string, value: string) => api.put('/settings', { key, value }),
};

/* Reports - URLs match .NET backend routes, with field mapping */
export const reportsApi = {
  getDailySummary: (date?: string) =>
    api.get('/reports/daily', { params: { date } }).then(r => ({ ...r, data: mapDailyReport(r.data) })),
  getTopProducts: (params?: any) => {
    // Map frontend 'limit' to backend 'top'
    const mapped = { ...params };
    if (mapped.limit !== undefined) { mapped.top = mapped.limit; delete mapped.limit; }
    return api.get('/reports/top-products', { params: mapped }).then(r => ({ ...r, data: (Array.isArray(r.data) ? r.data : []).map(mapTopProduct) }));
  },
  getInventoryStatus: () =>
    api.get('/reports/inventory').then(r => ({ ...r, data: mapInventoryStatus(r.data) })),
  getSalesByCategory: (params?: any) =>
    api.get('/reports/sales-by-category', { params }),
  getMonthlyComparison: (params?: any) =>
    api.get('/reports/monthly-comparison', { params }),
  getHourlySales: (params?: any) =>
    api.get('/reports/hourly-sales', { params }),
  getPaymentMethods: (params?: any) =>
    api.get('/reports/payment-methods', { params }),
  getTopCustomers: (params?: any) =>
    api.get('/reports/top-customers', { params }),
  getProfitMargin: (params?: any) =>
    api.get('/reports/profit-margin', { params }),
  getReturnRate: (params?: any) =>
    api.get('/reports/return-rate', { params }),
  getDayOfWeek: (params?: any) =>
    api.get('/reports/day-of-week', { params }),
};

/* Campaigns */
export const campaignsApi = {
  getAll: (params?: any) => api.get('/campaigns', { params }),
  getById: (id: number) => api.get(`/campaigns/${id}`),
  create: (data: any) => api.post('/campaigns', data),
  update: (id: number, data: any) => api.put(`/campaigns/${id}`, data),
  delete: (id: number) => api.delete(`/campaigns/${id}`),
  send: (id: number, channels: string[]) => api.post(`/campaigns/${id}/send`, { channels }),
  getAvailableCustomers: () => api.get('/campaigns/available-customers/list'),
  getExpiringProducts: (days?: number) => api.get('/campaigns/expiring-products/list', { params: { days } }),
};

/* WhatsApp */
export const whatsAppApi = {
  getStatus: () => api.get('/whatsapp/status'),
  getQr: () => api.get('/whatsapp/qr'),
  reconnect: () => api.post('/whatsapp/reconnect'),
};

/* CFDI Facturas */
export const facturasApi = {
  getAll: (params?: any) => api.get('/facturas', { params }),
  getById: (id: number) => api.get(`/facturas/${id}`),
  generate: (saleId: number) => api.post('/facturas/generate', { saleId }),
  cancel: (id: number, motivo: string) => api.post(`/facturas/${id}/cancel`, { motivo }),
};
