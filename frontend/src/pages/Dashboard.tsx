import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { reportsApi, alertsApi } from '../services/api';
import api from '../services/api';

/* ── Chart components ── */

function BarChart({ data, labelKey, valueKey, color, title, format }: {
  data: any[]; labelKey: string; valueKey: string; color: string; title: string; format?: (v: number) => string;
}) {
  if (!data.length) return <div className="card"><h2>{title}</h2><p style={{ color: 'var(--gray-400)', padding: '1rem 0' }}>Sin datos</p></div>;
  const max = Math.max(...data.map(d => Number(d[valueKey])), 1);
  const pad = data.length < 6 ? 30 : 10;
  const w = data.length * (40 + pad) + 40;
  return (
    <div className="card"><h2>{title}</h2>
      <svg width="100%" height="180" viewBox={`0 0 ${w} 160`} style={{ overflow: 'visible' }}>
        {data.map((d, i) => {
          const v = Number(d[valueKey]); const h = (v / max) * 120;
          const x = i * (40 + pad) + 20; const y = 140 - h;
          const label = d[labelKey]?.toString().length > 5 ? d[labelKey]?.toString().slice(0, 5) : (d[labelKey] || '');
          return (<g key={i}>
            <rect x={x} y={y} width={30} height={h} rx={4} fill={color} opacity={0.85}>
              <title>{`${d[labelKey]}: ${format ? format(v) : v}`}</title>
            </rect>
            <text x={x + 15} y={154} textAnchor="middle" fontSize={10} fill="var(--gray-500)">{label}</text>
            <text x={x + 15} y={y - 4} textAnchor="middle" fontSize={9} fill="var(--gray-600)">{format ? format(v) : v}</text>
          </g>);
        })}
      </svg>
    </div>
  );
}

function HBarChart({ data, labelKey, valueKey, valueLabel, title, color, format }: {
  data: any[]; labelKey: string; valueKey: string; valueLabel?: string; title: string; color: string; format?: (v: number) => string;
}) {
  if (!data.length) return <div className="card"><h2>{title}</h2><p style={{ color: 'var(--gray-400)', padding: '1rem 0' }}>Sin datos</p></div>;
  const max = Math.max(...data.map(d => Number(d[valueKey])), 1);
  const bh = 32; const gap = 8; const h = data.length * (bh + gap) + 20;
  const lw = 200; const cw = 600;
  return (
    <div className="card"><h2>{title}</h2>
      <div style={{ overflowX: 'auto' }}>
        <svg width="100%" height={h} viewBox={`0 0 ${lw + cw + 80} ${h}`} style={{ overflow: 'visible', minWidth: '500px' }}>
          {data.map((d, i) => {
            const v = Number(d[valueKey]); const bw = (v / max) * cw; const y = i * (bh + gap) + 10;
            const name = (d[labelKey] || '').toString(); const short = name.length > 28 ? name.slice(0, 26) + '...' : name;
            return (<g key={i}>
              <text x={0} y={y + bh / 2 + 5} fontSize={13} fill="var(--gray-700)" textAnchor="end" fontWeight="500">{short}</text>
              <rect x={lw + 4} y={y} width={Math.max(bw, 6)} height={bh} rx={4} fill={color} opacity={0.85}>
                <title>{`${name}\n${valueLabel || valueKey}: ${format ? format(v) : v}`}</title>
              </rect>
              <text x={lw + 10} y={y + bh / 2 + 5} fontSize={12} fill="#fff" fontWeight="bold">{format ? format(v) : v}</text>
            </g>);
          })}
        </svg>
      </div>
    </div>
  );
}

function PieChart({ data, labelKey, valueKey, title, format }: {
  data: any[]; labelKey: string; valueKey: string; title: string; format?: (v: number) => string;
}) {
  if (!data.length) return <div className="card"><h2>{title}</h2><p style={{ color: 'var(--gray-400)', padding: '1rem 0' }}>Sin datos</p></div>;
  const total = data.reduce((s, d) => s + Number(d[valueKey]), 0) || 1;
  const colors = ['#4f46e5', '#06b6d4', '#10b981', '#f59e0b', '#ef4444', '#8b5cf6', '#ec4899', '#14b8a6'];
  let acc = 0;
  const r = 55; const cx = 70; const cy = 60;
  const slices = data.map((d, i) => {
    const pct = Number(d[valueKey]) / total; const ang = pct * 360;
    const start = (acc / 360) * 2 * Math.PI; const end = ((acc + ang) / 360) * 2 * Math.PI;
    const x1 = cx + r * Math.sin(start); const y1 = cy - r * Math.cos(start);
    const x2 = cx + r * Math.sin(end); const y2 = cy - r * Math.cos(end);
    const large = ang > 180 ? 1 : 0;
    acc += ang;
    return { d: `M${cx},${cy} L${x1},${y1} A${r},${r} 0 ${large},1 ${x2},${y2} Z`, color: colors[i % colors.length], pct, label: d[labelKey], val: d[valueKey] };
  });
  return (
    <div className="card"><h2>{title}</h2>
      <div style={{ display: 'flex', alignItems: 'center', gap: '1rem', padding: '0.5rem 0' }}>
        <svg width="140" height="130" viewBox="0 0 140 130">
          {slices.map((s, i) => <path key={i} d={s.d} fill={s.color}><title>{`${s.label}: ${format ? format(s.val) : s.val} (${(s.pct * 100).toFixed(1)}%)`}</title></path>)}
        </svg>
        <div style={{ fontSize: '11px', lineHeight: '1.6' }}>
          {slices.map((s, i) => <div key={i} style={{ display: 'flex', alignItems: 'center', gap: '4px' }}>
            <span style={{ width: 10, height: 10, borderRadius: 2, background: s.color, display: 'inline-block' }}></span>
            <span>{s.label}: <strong>{format ? format(s.val) : s.val}</strong> ({(s.pct * 100).toFixed(1)}%)</span>
          </div>)}
        </div>
      </div>
    </div>
  );
}

/* ── Helpers ── */

const months = ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'];

function groupByMonth(items: any[], dateField: string, valueField: string, valMapper?: (v: number) => number): any[] {
  const groups: Record<string, { year: number; month: number; total: number }> = {};
  for (const item of items) {
    const d = new Date(item[dateField]);
    if (isNaN(d.getTime())) continue;
    const key = `${d.getFullYear()}-${d.getMonth()}`;
    if (!groups[key]) groups[key] = { year: d.getFullYear(), month: d.getMonth() + 1, total: 0 };
    groups[key].total += valMapper ? valMapper(Number(item[valueField] || 0)) : Number(item[valueField] || 0);
  }
  return Object.values(groups).sort((a, b) => a.year - b.year || a.month - b.month)
    .map(g => ({ label: `${months[g.month - 1]} ${g.year}`, ...g }));
}

function groupByKey(items: any[], key: string, valueKey: string, valMapper?: (v: number) => number): any[] {
  const groups: Record<string, number> = {};
  for (const item of items) {
    const k = item[key] || 'Otros';
    groups[k] = (groups[k] || 0) + (valMapper ? valMapper(Number(item[valueKey] || 0)) : Number(item[valueKey] || 0));
  }
  return Object.entries(groups)
    .map(([label, total]) => ({ label, total }))
    .sort((a, b) => b.total - a.total);
}

function mapSale(s: any) {
  return {
    ...s,
    createdAt: s.createdAt || s.createdat,
    total: Number(s.total || 0),
    paymentMethod: s.paymentMethod || s.paymentmethod || 'Efectivo',
    customerName: s.customerName || s.customer_name || 'Mostrador',
  };
}

function mapProduct(p: any) {
  return {
    ...p,
    categoryName: p.categoryName || p.category_name || 'Sin categoría',
    salePrice: Number(p.salePrice || p.saleprice || 0),
    purchasePrice: Number(p.purchasePrice || p.purchaseprice || 0),
    stock: Number(p.stock ?? p.stock ?? 0),
    minStock: Number(p.minStock ?? p.minstock ?? 0),
  };
}

/* ── Dashboard ── */

export default function Dashboard() {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [summary, setSummary] = useState<any>(null);
  const [invStatus, setInvStatus] = useState<any>(null);
  const [alerts, setAlerts] = useState<any[]>([]);
  const [recentSales, setRecentSales] = useState<any[]>([]);
  const [monthlySales, setMonthlySales] = useState<any[]>([]);
  const [monthlyExpenses, setMonthlyExpenses] = useState<any[]>([]);
  const [monthlyComparison, setMonthlyComparison] = useState<any[]>([]);
  const [lowStock, setLowStock] = useState<any[]>([]);
  const [expiringProducts, setExpiringProducts] = useState<any[]>([]);
  const [topProducts, setTopProducts] = useState<any[]>([]);
  const [salesByCategory, setSalesByCategory] = useState<any[]>([]);
  const [hourlySales, setHourlySales] = useState<any[]>([]);
  const [paymentMethods, setPaymentMethods] = useState<any[]>([]);
  const [dayOfWeek, setDayOfWeek] = useState<any[]>([]);
  const [profitMargin, setProfitMargin] = useState<any[]>([]);
  const [topCustomers, setTopCustomers] = useState<any[]>([]);
  const [returnRate, setReturnRate] = useState<any[]>([]);

  useEffect(() => {
    const today = new Date().toISOString().split('T')[0];

    reportsApi.getDailySummary(today).then(r => setSummary(r.data)).catch(() => {});
    reportsApi.getInventoryStatus().then(r => setInvStatus(r.data)).catch(() => {});

    alertsApi.getAll().then(r => {
      const data = Array.isArray(r.data) ? r.data : [];
      setAlerts(data.filter((a: any) => !a.isread).slice(0, 5));
    }).catch(() => {});

    api.get('/sales', { params: { limit: 5 } })
      .then(r => setRecentSales((r.data?.sales || r.data || []).map(mapSale)))
      .catch(() => {});

    // --- Compute analytics from raw data ---
    api.get('/sales', { params: { limit: 1000 } }).then(r => {
      const sales = ((r.data?.sales || r.data || []) as any[]).map(mapSale);
      if (!sales.length) return;

      // Monthly sales
      setMonthlySales(groupByMonth(sales, 'createdAt', 'total'));

      // Monthly comparison (revenue grouped by month)
      setMonthlyComparison(groupByMonth(sales, 'createdAt', 'total'));

      // Payment methods
      setPaymentMethods(groupByKey(sales, 'paymentMethod', 'total'));

      // Hourly sales
      const hours: Record<string, number> = {};
      for (const s of sales) {
        const d = new Date(s.createdAt);
        if (isNaN(d.getTime())) continue;
        const h = String(d.getHours()).padStart(2, '0');
        hours[h] = (hours[h] || 0) + s.total;
      }
      setHourlySales(Object.entries(hours)
        .map(([hour, total]) => ({ hour, total, sale_count: 0 }))
        .sort((a, b) => a.hour.localeCompare(b.hour)));

      // Day of week
      const days = ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'];
      const dayGroups: Record<string, number> = {};
      for (const s of sales) {
        const d = new Date(s.createdAt);
        if (isNaN(d.getTime())) continue;
        const dayName = days[d.getDay()];
        dayGroups[dayName] = (dayGroups[dayName] || 0) + s.total;
      }
      setDayOfWeek(days.filter(d => dayGroups[d]).map(d => ({ day_name: d, revenue: dayGroups[d] })));

      // Top customers
      setTopCustomers(groupByKey(sales, 'customerName', 'total').slice(0, 8)
        .map(c => ({ fullname: c.label, total_spent: c.total })));
    }).catch(() => {});

    api.get('/expenses', { params: { limit: 1000 } }).then(r => {
      const expenses = r.data?.data || r.data || [];
      if (Array.isArray(expenses)) {
        setMonthlyExpenses(groupByMonth(expenses, 'date', 'amount'));
      }
    }).catch(() => {});

    api.get('/products', { params: { limit: 500 } }).then(r => {
      const raw: any[] = r.data?.products || r.data || [];
      const prods = raw.map(mapProduct);
      if (!prods.length) return;

      setLowStock(prods.filter((p: any) => p.stock <= p.minStock).slice(0, 5));
      const now = new Date();
      const thirtyDays = new Date(now.getTime() + 30 * 24 * 60 * 60 * 1000);
      setExpiringProducts(prods
        .filter((p: any) => p.expiryDate || p.expiry_date)
        .filter((p: any) => new Date(p.expiryDate || p.expiry_date) <= thirtyDays)
        .slice(0, 5));

      // Sales by category (compute from sales items + product categories)
      // Profit margin per product
      const withMargin = prods
        .filter((p: any) => p.salePrice > 0)
        .map((p: any) => ({
          name: p.name,
          margin_pct: Math.round(((p.salePrice - p.purchasePrice) / p.salePrice) * 100),
        }))
        .sort((a: any, b: any) => b.margin_pct - a.margin_pct)
        .slice(0, 10);
      setProfitMargin(withMargin);
    }).catch(() => {});

    // Top products from backend endpoint
    reportsApi.getTopProducts({ limit: 10 })
      .then(r => {
        const d = Array.isArray(r.data) ? r.data : [];
        setTopProducts(d.map((p: any) => ({ name: p.productName || p.product_name || p.name, total_quantity: p.totalQuantity || p.total_quantity || p.quantity || 0 })));
      }).catch(() => {});

    // Sales by category — try backend, fall back to client-side via products + sales
    api.get('/sales', { params: { limit: 500 } }).then(async (r) => {
      const sales = ((r.data?.sales || r.data || []) as any[]).map(mapSale);
      if (!sales.length) return;

      // Get all sale items and compute category from product data
      try {
        const prodRes = await api.get('/products', { params: { limit: 500 } });
        const rawProds: any[] = prodRes.data?.products || prodRes.data || [];
        const prodMap = new Map(rawProds.map((p: any) => [p.id, mapProduct(p)]));

        // Fetch sale items for each sale
        const catRevenue: Record<string, number> = {};
        for (const sale of sales.slice(0, 50)) {
          try {
            const detailRes = await api.get(`/sales/${sale.id}`);
            const detail = detailRes.data;
            const items = detail.items || [];
            for (const item of items) {
              const prod = prodMap.get(item.productId);
              const cat = prod?.categoryName || 'Sin categoría';
              catRevenue[cat] = (catRevenue[cat] || 0) + Number(item.subtotal || 0);
            }
          } catch {}
        }
        setSalesByCategory(Object.entries(catRevenue)
          .map(([name, revenue]) => ({ name, revenue }))
          .sort((a, b) => b.revenue - a.revenue));
      } catch {}
    }).catch(() => {});

    // Return rate (client-side from returns vs sales)
    api.get('/returns', { params: { limit: 500 } }).then(r => {
      const returns = Array.isArray(r.data) ? r.data : (r.data?.data || r.data?.returns || []);
      if (!returns.length) return;
      setReturnRate(groupByMonth(returns, 'createdAt', 'total'));
    }).catch(() => {});
  }, []);

  const modules = [
    { title: '🛒 Nueva Venta', desc: 'Registrar una venta', path: '/sales/new', color: 'var(--success)' },
    { title: '💰 Caja', desc: 'Abrir/cerrar caja', path: '/cash-register', color: 'var(--warning)' },
    { title: '🔧 Productos', desc: 'Inventario de productos', path: '/products', color: 'var(--primary)' },
    { title: '👥 Clientes', desc: 'Administrar clientes', path: '/customers', color: 'var(--info)' },
    { title: '📄 Facturación', desc: 'CFDI y facturas', path: '/facturas', color: '#8b5cf6' },
    { title: '📦 Reportes', desc: 'Reportes y estadísticas', path: '/reports', color: '#ec4899' },
  ];

  return (
    <div className="dashboard">
      <div className="welcome-card">
        <h1>Bienvenido, {user?.fullname || user?.username}</h1>
        <p>{user?.role} · {new Date().toLocaleDateString('es-MX', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' })}</p>
      </div>

      {/* KPIs */}
      <div className="dashboard-grid">
        {summary && <>
          <div className="dashboard-card">
            <h3>💰 Ventas Hoy</h3>
            <p className="stat-number">${Number(summary.total_revenue || 0).toFixed(2)}</p>
            <p>{summary.total_sales || 0} transacciones</p>
          </div>
          <div className="dashboard-card">
            <h3>🧾 IVA del Día</h3>
            <p className="stat-number">${Number(summary.total_tax || 0).toFixed(2)}</p>
            <p>{summary.total_discounts > 0 ? `Descuentos: $${Number(summary.total_discounts).toFixed(2)}` : 'Sin descuentos'}</p>
          </div>
        </>}
        {invStatus && <>
          <div className="dashboard-card">
            <h3>📦 Productos</h3>
            <p className="stat-number">{invStatus.total_products}</p>
            <p>{invStatus.low_stock_count > 0 ? `${invStatus.low_stock_count} con stock bajo` : 'Stock saludable'}</p>
          </div>
          <div className="dashboard-card">
            <h3>💰 Valor Inventario</h3>
            <p className="stat-number">${Number(invStatus.inventory_value || 0).toFixed(2)}</p>
            <p>{invStatus.out_of_stock_count > 0 ? `${invStatus.out_of_stock_count} agotados` : 'Todo en stock'}</p>
          </div>
        </>}
      </div>

      {/* Fila 1: Ventas y Gastos mensuales */}
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem', marginBottom: '1.5rem' }}>
        <BarChart data={monthlySales} labelKey="label" valueKey="total" color="var(--success)" title="📈 Ventas Mensuales" format={v => `$${Number(v).toFixed(0)}`} />
        <BarChart data={monthlyExpenses} labelKey="label" valueKey="total" color="var(--danger)" title="📉 Gastos Mensuales" format={v => `$${Number(v).toFixed(0)}`} />
      </div>

      {/* Fila 2: Comparativa mensual */}
      {monthlyComparison.length > 0 && (
        <div style={{ marginBottom: '1.5rem' }}>
          <BarChart data={monthlyComparison} labelKey="label" valueKey="total" color="var(--info)" title="📊 Comparativa Mensual" format={v => `$${Number(v).toFixed(0)}`} />
        </div>
      )}

      {/* Fila 3: Productos más vendidos */}
      {topProducts.length > 0 && (
        <div style={{ marginBottom: '1.5rem' }}>
          <HBarChart data={topProducts} labelKey="name" valueKey="total_quantity" title="🏆 Productos Más Vendidos" color="var(--primary)" format={v => String(v)} />
        </div>
      )}

      {/* Fila 4: Ventas por Categoría */}
      {salesByCategory.length > 0 && (
        <div style={{ marginBottom: '1.5rem' }}>
          <HBarChart data={salesByCategory} labelKey="name" valueKey="revenue" title="📂 Ventas por Categoría" color="#8b5cf6" format={v => `$${Number(v).toFixed(0)}`} />
        </div>
      )}

      {/* Fila 5: Ventas por Hora / Día */}
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem', marginBottom: '1.5rem' }}>
        <BarChart data={hourlySales} labelKey="hour" valueKey="total" title="⏰ Ventas por Hora" color="#06b6d4" format={v => `$${Number(v).toFixed(0)}`} />
        <BarChart data={dayOfWeek} labelKey="day_name" valueKey="revenue" title="📅 Ventas por Día" color="#f59e0b" format={v => `$${Number(v).toFixed(0)}`} />
      </div>

      {/* Fila 6: Métodos de Pago */}
      {paymentMethods.length > 0 && (
        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem', marginBottom: '1.5rem' }}>
          <PieChart data={paymentMethods} labelKey="label" valueKey="total" title="💳 Métodos de Pago" format={v => `$${Number(v).toFixed(0)}`} />
        </div>
      )}

      {/* Fila 7: Margen de Ganancia */}
      {profitMargin.length > 0 && (
        <div style={{ marginBottom: '1.5rem' }}>
          <HBarChart data={profitMargin} labelKey="name" valueKey="margin_pct" valueLabel="Margen %" title="💰 Margen de Ganancia" color="#10b981" format={v => `${v}%`} />
        </div>
      )}

      {/* Fila 8: Clientes Frecuentes */}
      {topCustomers.length > 0 && (
        <div style={{ marginBottom: '1.5rem' }}>
          <HBarChart data={topCustomers} labelKey="fullname" valueKey="total_spent" title="👥 Clientes Frecuentes" color="#ec4899" format={v => `$${Number(v).toFixed(0)}`} />
        </div>
      )}

      {/* Fila 9: Tasa de Devolución */}
      {returnRate.length > 0 && (
        <div style={{ marginBottom: '1.5rem' }}>
          <BarChart data={returnRate} labelKey="label" valueKey="total" color="#ef4444" title="🔄 Devoluciones Mensuales" format={v => `$${Number(v).toFixed(0)}`} />
        </div>
      )}

      {/* Stock bajo y próximos a caducar */}
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem', marginBottom: '1.5rem' }}>
        {lowStock.length > 0 && (
          <div className="card card-danger">
            <h2>⚠️ Stock Bajo</h2>
            {lowStock.map((p: any) => (
              <div key={p.id} style={{ display: 'flex', justifyContent: 'space-between', padding: '0.35rem 0', borderBottom: '1px solid var(--gray-100)' }}>
                <span><strong>{p.name}</strong></span>
                <span style={{ color: 'var(--danger)' }}>{p.stock} / {p.minStock} {p.unit}</span>
              </div>
            ))}
            <button className="btn-sm" style={{ marginTop: '0.5rem' }} onClick={() => navigate('/products?filter=low_stock')}>Ver todos</button>
          </div>
        )}
        {expiringProducts.length > 0 && (
          <div className="card card-warning">
            <h2>⏰ Próximos a Caducar</h2>
            {expiringProducts.map((p: any) => (
              <div key={p.id} style={{ display: 'flex', justifyContent: 'space-between', padding: '0.35rem 0', borderBottom: '1px solid var(--gray-100)' }}>
                <span><strong>{p.name}</strong></span>
                <span style={{ color: 'var(--warning)' }}>{p.expiryDate || p.expiry_date ? new Date(p.expiryDate || p.expiry_date).toLocaleDateString() : ''}</span>
              </div>
            ))}
            <button className="btn-sm" style={{ marginTop: '0.5rem' }} onClick={() => navigate('/products?filter=expiring')}>Ver todos</button>
          </div>
        )}
      </div>

      {/* Acceso rápido */}
      <h2 style={{ marginBottom: '0.75rem', fontSize: '1.1rem' }}>Acceso Rápido</h2>
      <div className="dashboard-grid">
        {modules.map(m => (
          <div key={m.path} className="dashboard-card" onClick={() => navigate(m.path)} style={{ borderLeft: `3px solid ${m.color}` }}>
            <h3>{m.title}</h3><p>{m.desc}</p>
          </div>
        ))}
      </div>

      {/* Últimas ventas y alertas */}
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem', marginTop: '1rem' }}>
        {recentSales.length > 0 && (
          <div className="card">
            <h2>Últimas Ventas</h2>
            <table className="table">
              <thead><tr><th>Folio</th><th>Total</th><th>Hora</th></tr></thead>
              <tbody>
                {recentSales.map((s: any) => (
                  <tr key={s.id} style={{ cursor: 'pointer' }} onClick={() => navigate('/sales')}>
                    <td>{s.receiptNumber || `#${s.id}`}</td>
                    <td><strong>${Number(s.total).toFixed(2)}</strong></td>
                    <td>{new Date(s.createdAt).toLocaleTimeString()}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
        {alerts.length > 0 && (
          <div className="card">
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
              <h2>Alertas Pendientes</h2>
              <button className="btn-sm" onClick={() => navigate('/alerts')}>Ver todas</button>
            </div>
            {alerts.map((a: any) => (
              <div key={a.id} className="alert-item unread" style={{ marginBottom: '0.35rem', padding: '0.5rem' }}>
                <strong>{a.title}</strong>
                <p style={{ fontSize: '0.82rem' }}>{a.message}</p>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}
