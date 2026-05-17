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
  getExpiringSoon: (days?: number) => api.get('/products/expiring-soon', { params: { days } }),
  toggleActive: (id: number) => api.patch(`/products/${id}/toggle-active`),
};

/* Categories */
export const categoriesApi = {
  getAll: () => api.get('/categories'),
  getById: (id: number) => api.get(`/categories/${id}`),
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
};

/* Suppliers */
export const suppliersApi = {
  getAll: () => api.get('/suppliers'),
  getById: (id: number) => api.get(`/suppliers/${id}`),
  create: (data: any) => api.post('/suppliers', data),
  update: (id: number, data: any) => api.put(`/suppliers/${id}`, data),
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
  openSession: (data: any) => api.post('/cashregister/open', data),
  closeSession: (data: any) => api.post('/cashregister/close', data),
  getActiveSession: () => api.get('/cashregister/active'),
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
  markAsRead: (id: number) => api.patch(`/alerts/${id}/read`),
  markAllAsRead: () => api.patch('/alerts/read-all'),
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
  create: (data: any) => api.post('/users', data),
  update: (id: number, data: any) => api.put(`/users/${id}`, data),
  updatePassword: (id: number, data: any) => api.put(`/users/${id}/password`, data),
};

/* Settings */
export const settingsApi = {
  getAll: () => api.get('/settings'),
  update: (key: string, value: string) => api.put('/settings', { key, value }),
};

/* Reports */
export const reportsApi = {
  getDailySummary: (date?: string) => api.get('/reports/daily-summary', { params: { date } }),
  getTopProducts: (params?: any) => api.get('/reports/top-products', { params }),
  getInventoryStatus: () => api.get('/reports/inventory-status'),
  getSalesByCategory: (params?: any) => api.get('/reports/sales-by-category', { params }),
  getMonthlyComparison: (params?: any) => api.get('/reports/monthly-comparison', { params }),
  getHourlySales: (params?: any) => api.get('/reports/hourly-sales', { params }),
};

/* CFDI Facturas */
export const facturasApi = {
  getAll: (params?: any) => api.get('/facturas', { params }),
  getById: (id: number) => api.get(`/facturas/${id}`),
  generate: (saleId: number) => api.post('/facturas/generate', { saleId }),
  cancel: (id: number, motivo: string) => api.post(`/facturas/${id}/cancel`, { motivo }),
};
