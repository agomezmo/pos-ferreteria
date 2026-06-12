import { useState, useEffect, useMemo } from 'react';
import { reportsApi, expensesApi } from '../services/api';
import api from '../services/api';

/* ── Chart helpers (inline SVG) ── */

function BarChart({ data, labelKey, valueKey, color, title, format }: {
  data: any[]; labelKey: string; valueKey: string; color: string; title: string; format?: (v: number) => string;
}) {
  if (!data.length) return null;
  const max = Math.max(...data.map(d => Number(d[valueKey])), 1);
  const pad = data.length < 6 ? 30 : 10;
  const w = data.length * (40 + pad) + 40;
  return (
    <div className="card"><h2>{title}</h2>
      <svg width="100%" height="180" viewBox={`0 0 ${w} 160`} style={{ overflow: 'visible' }}>
        {data.map((d, i) => {
          const v = Number(d[valueKey]); const h = (v / max) * 120;
          const x = i * (40 + pad) + 20; const y = 140 - h;
          const label = (d[labelKey]?.toString() || '').length > 6 ? d[labelKey]?.toString().slice(0, 5) + '…' : (d[labelKey]?.toString() || '');
          return (<g key={i}>
            <rect x={x} y={y} width={30} height={h} rx={4} fill={color} opacity={0.85}>
              <title>{`${d[labelKey]}: ${format ? format(v) : v}`}</title>
            </rect>
            <text x={x + 15} y={154} textAnchor="middle" fontSize={10} fill="var(--gray-500)">{label || ''}</text>
            <text x={x + 15} y={y - 4} textAnchor="middle" fontSize={9} fill="var(--gray-600)">{format ? format(v) : v}</text>
          </g>);
        })}
      </svg>
    </div>
  );
}

function HBarChart({ data, labelKey, valueKey, title, color, format }: {
  data: any[]; labelKey: string; valueKey: string; title: string; color: string; format?: (v: number) => string;
}) {
  if (!data.length) return null;
  const max = Math.max(...data.map(d => Number(d[valueKey])), 1);
  const bh = 32; const gap = 8; const h = data.length * (bh + gap) + 20;
  const lw = 200; const cw = 600;
  return (
    <div className="card"><h2>{title}</h2>
      <div style={{ overflowX: 'auto' }}>
        <svg width="100%" height={h} viewBox={`0 0 ${lw + cw + 80} ${h}`} style={{ overflow: 'visible', minWidth: '500px' }}>
          {data.map((d, i) => {
            const v = Number(d[valueKey]); const bw = (v / max) * cw; const y = i * (bh + gap) + 10;
            const name = (d[labelKey] || '').toString(); const short = name.length > 28 ? name.slice(0, 26) + '…' : name;
            return (<g key={i}>
              <text x={lw - 6} y={y + bh / 2 + 5} fontSize={13} fill="var(--gray-700)" textAnchor="end" fontWeight="500">{short}</text>
              <rect x={lw + 4} y={y} width={Math.max(bw, 6)} height={bh} rx={4} fill={color} opacity={0.85}>
                <title>{name + ': ' + (format ? format(v) : v)}</title>
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
  if (!data.length) return null;
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
          {slices.map((s, i) => <path key={i} d={s.d} fill={s.color}>
            <title>{`${s.label}: ${format ? format(s.val) : s.val} (${(s.pct * 100).toFixed(1)}%)`}</title>
          </path>)}
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
const days = ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'];

function groupByMonth(items: any[], dateField: string, valueField: string): any[] {
  const groups: Record<string, { year: number; month: number; total: number }> = {};
  for (const item of items) {
    const d = new Date(item[dateField]);
    if (isNaN(d.getTime())) continue;
    const key = `${d.getFullYear()}-${d.getMonth()}`;
    if (!groups[key]) groups[key] = { year: d.getFullYear(), month: d.getMonth() + 1, total: 0 };
    groups[key].total += Number(item[valueField] || 0);
  }
  return Object.values(groups).sort((a, b) => a.year - b.year || a.month - b.month)
    .map(g => ({ label: `${months[g.month - 1]} ${g.year}`, ...g }));
}

function groupByKey(items: any[], key: string, valueKey: string): any[] {
  const groups: Record<string, number> = {};
  for (const item of items) {
    const k = item[key] || 'Otros';
    groups[k] = (groups[k] || 0) + Number(item[valueKey] || 0);
  }
  return Object.entries(groups)
    .map(([label, total]) => ({ label, total }))
    .sort((a, b) => b.total - a.total);
}

function mapSale(s: any) {
  return {
    ...s,
    createdAt: s.createdAt || s.createdat || s.CreatedAt,
    total: Number(s.total || s.Total || 0),
    paymentMethod: s.paymentMethod || s.paymentmethod || s.PaymentMethod || 'Efectivo',
    customerName: s.customerName || s.customer_name || s.CustomerName || 'Mostrador',
  };
}

function mapProduct(p: any) {
  return {
    ...p,
    categoryName: p.categoryName || p.category_name || p.CategoryName || 'Sin categoría',
    salePrice: Number(p.salePrice || p.saleprice || p.SalePrice || 0),
    purchasePrice: Number(p.purchasePrice || p.purchaseprice || p.PurchasePrice || 0),
    stock: Number(p.stock ?? p.Stock ?? 0),
    minStock: Number(p.minStock ?? p.minstock ?? p.MinStock ?? 0),
  };
}

export default function Reports() {
  const [dateRange, setDateRange] = useState({
    startDate: new Date(new Date().getFullYear(), new Date().getMonth(), 1).toISOString().split('T')[0],
    endDate: new Date().toISOString().split('T')[0],
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  /* Data */
  const [summary, setSummary] = useState<any>(null);
  const [invStatus, setInvStatus] = useState<any>(null);
  const [topProducts, setTopProducts] = useState<any[]>([]);
  const [expenses, setExpenses] = useState<any[]>([]);
  const [sales, setSales] = useState<any[]>([]);
  const [products, setProducts] = useState<any[]>([]);

  const fetchData = async () => {
    setLoading(true);
    setError('');
    try {
      const today = new Date().toISOString().split('T')[0];
      const results = await Promise.allSettled([
        reportsApi.getDailySummary(today),
        reportsApi.getTopProducts({ limit: 15, startDate: dateRange.startDate, endDate: dateRange.endDate }),
        reportsApi.getInventoryStatus(),
        expensesApi.getAll({ limit: 200 }),
        api.get('/sales', { params: { limit: 1000 } }),
        api.get('/products', { params: { limit: 500 } }),
      ]);

      results.forEach((r, i) => {
        if (r.status === 'fulfilled') {
          const data = r.value.data;
          if (i === 0) setSummary(data);
          else if (i === 1) setTopProducts(Array.isArray(data) ? data : []);
          else if (i === 2) setInvStatus(data);
          else if (i === 3) setExpenses(Array.isArray(data) ? data : []);
          else if (i === 4) setSales(((data?.sales || data || []) as any[]).map(mapSale));
          else if (i === 5) {
            const raw: any[] = data?.products || data || [];
            setProducts(raw.map(mapProduct));
          }
        }
      });
    } catch (err) {
      setError('Error al cargar datos');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { fetchData(); }, []);

  /* ── Computed stats ── */
  const totalExpenses = useMemo(() => expenses.reduce((s, e) => s + Number(e.amount || e.Amount || 0), 0), [expenses]);
  const netIncome = summary ? Number(summary.total_revenue) - totalExpenses : 0;
  const maxQty = Math.max(...topProducts.map(p => Number(p.total_quantity)), 1);

  const monthlySales = useMemo(() => groupByMonth(sales, 'createdAt', 'total'), [sales]);

  const hourlySales = useMemo(() => {
    const hours: Record<string, number> = {};
    for (const s of sales) {
      const d = new Date(s.createdAt);
      if (isNaN(d.getTime())) continue;
      const h = String(d.getHours()).padStart(2, '0');
      hours[h] = (hours[h] || 0) + s.total;
    }
    return Object.entries(hours).map(([hour, total]) => ({ hour, total })).sort((a, b) => a.hour.localeCompare(b.hour));
  }, [sales]);

  const dayOfWeek = useMemo(() => {
    const dayGroups: Record<string, number> = {};
    for (const s of sales) {
      const d = new Date(s.createdAt);
      if (isNaN(d.getTime())) continue;
      const dayName = days[d.getDay()];
      dayGroups[dayName] = (dayGroups[dayName] || 0) + s.total;
    }
    return days.filter(d => dayGroups[d]).map(d => ({ day_name: d, revenue: dayGroups[d] }));
  }, [sales]);

  const paymentMethods = useMemo(() => groupByKey(sales, 'paymentMethod', 'total'), [sales]);

  const topCustomers = useMemo(() =>
    groupByKey(sales, 'customerName', 'total').slice(0, 8)
      .map(c => ({ fullname: c.label, total_spent: c.total })),
  [sales]);

  const profitMargin = useMemo(() =>
    products
      .filter((p: any) => p.salePrice > 0)
      .map((p: any) => ({ name: p.name, margin_pct: Math.round(((p.salePrice - p.purchasePrice) / p.salePrice) * 100) }))
      .sort((a: any, b: any) => b.margin_pct - a.margin_pct)
      .slice(0, 10),
  [products]);

  const returnRate = useMemo(() => {
    // Simple version: just show monthly sales count
    return groupByMonth(sales, 'createdAt', 'total').slice(-12);
  }, [sales]);

  const [activeTab, setActiveTab] = useState('resumen');

  const tabs = [
    { id: 'resumen', label: 'Resumen' },
    { id: 'ventas', label: 'Ventas' },
    { id: 'productos', label: 'Productos' },
    { id: 'inventario', label: 'Inventario' },
    { id: 'gastos', label: 'Gastos' },
    { id: 'clientes', label: 'Clientes' },
  ];

  if (loading) return <div className="page-loading">Cargando estadísticas...</div>;

  return (
    <div className="page" style={{ maxWidth: '100%' }}>
      <div className="page-header" style={{ flexWrap: 'wrap', gap: '0.75rem' }}>
        <h1>Reportes y Estadísticas</h1>
        <div className="search-bar" style={{ margin: 0, flexWrap: 'nowrap' }}>
          <label>Desde:</label>
          <input type="date" value={dateRange.startDate}
            onChange={e => setDateRange(p => ({ ...p, startDate: e.target.value }))}
            style={{ minWidth: '140px', flex: 'none' }} />
          <label>Hasta:</label>
          <input type="date" value={dateRange.endDate}
            onChange={e => setDateRange(p => ({ ...p, endDate: e.target.value }))}
            style={{ minWidth: '140px', flex: 'none' }} />
          <button className="btn-primary" onClick={fetchData} style={{ whiteSpace: 'nowrap' }}>
            Actualizar
          </button>
        </div>
      </div>

      {error && <div className="error-message">{error}</div>}

      <div className="tabs" style={{ display: 'flex', gap: '0.25rem', marginBottom: '1.5rem', flexWrap: 'wrap' }}>
        {tabs.map(t => (
          <button key={t.id}
            className={activeTab === t.id ? 'btn-primary' : 'btn-secondary'}
            onClick={() => setActiveTab(t.id)}
            style={{ borderRadius: 0, fontSize: '0.8rem' }}>
            {t.label}
          </button>
        ))}
      </div>

      {/* ═══════════════ TAB: RESUMEN ═══════════════ */}
      {activeTab === 'resumen' && (
        <>
          <div className="dashboard-grid">
            {summary && <>
              <div className="dashboard-card" style={{ borderLeft: '4px solid var(--success)' }}>
                <h3>💰 Ventas del Día</h3>
                <p className="stat-number">{summary.total_sales}</p>
                <p>${Number(summary.total_revenue).toFixed(2)} ingresos</p>
              </div>
              <div className="dashboard-card">
                <h3>🧾 IVA</h3>
                <p className="stat-number">${Number(summary.total_tax).toFixed(2)}</p>
                <p>Descuentos: ${Number(summary.total_discounts || 0).toFixed(2)}</p>
              </div>
              <div className="dashboard-card">
                <h3>👥 Clientes Atendidos</h3>
                <p className="stat-number">{summary.unique_customers || 0}</p>
                <p>Ticket prom.: ${Number(summary.average_ticket || 0).toFixed(2)}</p>
              </div>
              <div className="dashboard-card" style={{ borderLeft: `4px solid ${netIncome >= 0 ? 'var(--success)' : 'var(--danger)'}` }}>
                <h3>📊 Utilidad Neta</h3>
                <p className="stat-number">${netIncome.toFixed(2)}</p>
                <p>Gastos: ${totalExpenses.toFixed(2)}</p>
              </div>
            </>}
            {invStatus && <>
              <div className="dashboard-card">
                <h3>📦 Inventario</h3>
                <p className="stat-number">{invStatus.total_products}</p>
                <p>Valor: ${Number(invStatus.inventory_value).toFixed(2)}</p>
              </div>
              <div className="dashboard-card" style={{ borderLeft: `4px solid ${invStatus.low_stock_count > 0 ? 'var(--warning)' : 'var(--success)'}` }}>
                <h3>⚠️ Stock Bajo</h3>
                <p className="stat-number">{invStatus.low_stock_count}</p>
                <p>{invStatus.out_of_stock_count} agotados</p>
              </div>
            </>}
          </div>

          <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem', marginBottom: '1.5rem' }}>
            <BarChart data={monthlySales} labelKey="label" valueKey="total" color="var(--success)" title="📈 Ventas Mensuales" format={v => `$${Number(v).toFixed(0)}`} />
            <BarChart data={hourlySales} labelKey="hour" valueKey="total" color="#06b6d4" title="⏰ Ventas por Hora" format={v => `$${Number(v).toFixed(0)}`} />
          </div>

          <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem', marginBottom: '1.5rem' }}>
            <BarChart data={dayOfWeek} labelKey="day_name" valueKey="revenue" title="📅 Ventas por Día" color="#f59e0b" format={v => `$${Number(v).toFixed(0)}`} />
            <PieChart data={paymentMethods} labelKey="label" valueKey="total" title="💳 Métodos de Pago" format={v => `$${Number(v).toFixed(0)}`} />
          </div>

          {topProducts.length > 0 && (
            <div style={{ marginBottom: '1.5rem' }}>
              <HBarChart data={topProducts.slice(0, 10)} labelKey="name" valueKey="total_quantity" title="🏆 Productos Más Vendidos" color="var(--primary)" format={v => String(v)} />
            </div>
          )}

          {profitMargin.length > 0 && (
            <div style={{ marginBottom: '1.5rem' }}>
              <HBarChart data={profitMargin} labelKey="name" valueKey="margin_pct" title="💰 Margen de Ganancia" color="#10b981" format={v => `${v}%`} />
            </div>
          )}

          {topCustomers.length > 0 && (
            <div style={{ marginBottom: '1.5rem' }}>
              <HBarChart data={topCustomers} labelKey="fullname" valueKey="total_spent" title="👥 Clientes Frecuentes" color="#ec4899" format={v => `$${Number(v).toFixed(0)}`} />
            </div>
          )}
        </>
      )}

      {/* ═══════════════ TAB: VENTAS ═══════════════ */}
      {activeTab === 'ventas' && (
        <>
          <div className="dashboard-grid">
            {summary && <>
              <div className="dashboard-card">
                <h3>Ventas Hoy</h3>
                <p className="stat-number">{summary.total_sales}</p>
              </div>
              <div className="dashboard-card">
                <h3>Ingresos</h3>
                <p className="stat-number">${Number(summary.total_revenue || 0).toFixed(2)}</p>
              </div>
              <div className="dashboard-card">
                <h3>IVA Total</h3>
                <p className="stat-number">${Number(summary.total_tax || 0).toFixed(2)}</p>
              </div>
              <div className="dashboard-card">
                <h3>Descuentos</h3>
                <p className="stat-number">${Number(summary.total_discounts || 0).toFixed(2)}</p>
              </div>
              <div className="dashboard-card">
                <h3>Efectivo</h3>
                <p className="stat-number">${Number(summary.total_cash || 0).toFixed(2)}</p>
              </div>
              <div className="dashboard-card">
                <h3>Tarjeta</h3>
                <p className="stat-number">${Number(summary.total_card || 0).toFixed(2)}</p>
              </div>
              <div className="dashboard-card">
                <h3>Transferencia</h3>
                <p className="stat-number">${Number(summary.total_transfer || 0).toFixed(2)}</p>
              </div>
              <div className="dashboard-card">
                <h3>Ticket Promedio</h3>
                <p className="stat-number">${Number(summary.average_ticket || 0).toFixed(2)}</p>
              </div>
            </>}
          </div>

          <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem', marginBottom: '1.5rem' }}>
            <BarChart data={monthlySales} labelKey="label" valueKey="total" color="var(--info)" title="📊 Ventas Mensuales" format={v => `$${Number(v).toFixed(0)}`} />
            <BarChart data={hourlySales} labelKey="hour" valueKey="total" color="#06b6d4" title="⏰ Ventas por Hora" format={v => `$${Number(v).toFixed(0)}`} />
          </div>

          <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem', marginBottom: '1.5rem' }}>
            <BarChart data={dayOfWeek} labelKey="day_name" valueKey="revenue" title="📅 Por Día de la Semana" color="#f59e0b" format={v => `$${Number(v).toFixed(0)}`} />
            <PieChart data={paymentMethods} labelKey="label" valueKey="total" title="💳 Métodos de Pago" format={v => `$${Number(v).toFixed(0)}`} />
          </div>

          <div className="card">
            <h2>📋 Últimas Ventas</h2>
            <div className="table-container">
              <table className="table">
                <thead>
                  <tr><th>Folio</th><th>Cliente</th><th>Total</th><th>Método</th><th>Fecha</th></tr>
                </thead>
                <tbody>
                  {sales.slice(0, 20).map((s: any) => (
                    <tr key={s.id}>
                      <td><strong>{s.receiptnumber || s.receiptNumber || `#${s.id}`}</strong></td>
                      <td>{s.customerName || 'Mostrador'}</td>
                      <td><strong>${Number(s.total).toFixed(2)}</strong></td>
                      <td><span className="badge badge-info">{s.paymentMethod}</span></td>
                      <td>{s.createdAt ? new Date(s.createdAt).toLocaleString() : '-'}</td>
                    </tr>
                  ))}
                  {sales.length === 0 && <tr><td colSpan={5} className="empty">Sin ventas</td></tr>}
                </tbody>
              </table>
            </div>
          </div>
        </>
      )}

      {/* ═══════════════ TAB: PRODUCTOS ═══════════════ */}
      {activeTab === 'productos' && (
        <>
          <div className="card">
            <h2>🏆 Top Productos más Vendidos</h2>
            <div className="bar-chart">
              {topProducts.slice(0, 10).map((p, i) => (
                <div key={p.id || i} className="bar-item">
                  <div className="bar-label">
                    <span>{i + 1}. {p.name}</span>
                    <span>{p.total_quantity} uds - ${Number(p.total_revenue).toFixed(2)}</span>
                  </div>
                  <div className="bar-track">
                    <div className="bar-fill" style={{
                      width: `${(Number(p.total_quantity) / maxQty) * 100}%`,
                      background: `hsl(${i * 35}, 70%, 50%)`
                    }} />
                  </div>
                </div>
              ))}
              {topProducts.length === 0 && <p className="empty" style={{ padding: '1rem' }}>Sin datos</p>}
            </div>
          </div>

          <div className="card">
            <h2>💰 Margen de Ganancia por Producto</h2>
            <div className="table-container">
              <table className="table">
                <thead>
                  <tr><th>#</th><th>Producto</th><th>Margen %</th></tr>
                </thead>
                <tbody>
                  {profitMargin.map((p: any, i: number) => (
                    <tr key={i}>
                      <td>{i + 1}</td>
                      <td><strong>{p.name}</strong></td>
                      <td><span className={`badge ${p.margin_pct >= 50 ? 'badge-success' : p.margin_pct >= 20 ? 'badge-warning' : 'badge-danger'}`}>{p.margin_pct}%</span></td>
                    </tr>
                  ))}
                  {profitMargin.length === 0 && <tr><td colSpan={3} className="empty">Sin datos</td></tr>}
                </tbody>
              </table>
            </div>
          </div>

          <div className="card">
            <h2>📋 Detalle de Productos Vendidos</h2>
            <table className="table">
              <thead>
                <tr><th>#</th><th>Producto</th><th>Código</th><th>Cantidad</th><th>Ingresos</th></tr>
              </thead>
              <tbody>
                {topProducts.map((p: any, i: number) => (
                  <tr key={p.id || i}>
                    <td>{i + 1}</td>
                    <td><strong>{p.name}</strong></td>
                    <td>{p.code || '-'}</td>
                    <td>{p.total_quantity}</td>
                    <td><strong>${Number(p.total_revenue).toFixed(2)}</strong></td>
                  </tr>
                ))}
                {topProducts.length === 0 && <tr><td colSpan={5} className="empty">Sin datos</td></tr>}
              </tbody>
            </table>
          </div>
        </>
      )}

      {/* ═══════════════ TAB: INVENTARIO ═══════════════ */}
      {activeTab === 'inventario' && invStatus && (
        <>
          <div className="dashboard-grid">
            <div className="dashboard-card">
              <h3>Total Productos</h3>
              <p className="stat-number">{invStatus.total_products}</p>
            </div>
            <div className="dashboard-card" style={{ borderLeft: `4px solid ${invStatus.low_stock_count > 0 ? 'var(--warning)' : 'var(--success)'}` }}>
              <h3>Stock Bajo</h3>
              <p className="stat-number">{invStatus.low_stock_count}</p>
            </div>
            <div className="dashboard-card" style={{ borderLeft: `4px solid ${invStatus.out_of_stock_count > 0 ? 'var(--danger)' : 'var(--success)'}` }}>
              <h3>Sin Stock</h3>
              <p className="stat-number">{invStatus.out_of_stock_count}</p>
            </div>
            <div className="dashboard-card">
              <h3>Valor del Inventario</h3>
              <p className="stat-number">${Number(invStatus.inventory_value).toFixed(2)}</p>
            </div>
          </div>

          <div className="card">
            <h2>⚠️ Productos con Stock Bajo</h2>
            <div className="table-container">
              <table className="table">
                <thead>
                  <tr><th>Producto</th><th>Código</th><th>Stock</th><th>Stock Mín</th><th>Precio</th></tr>
                </thead>
                <tbody>
                  {(invStatus.low_stock_items || []).map((p: any) => (
                    <tr key={p.id}>
                      <td><strong>{p.name || p.Name}</strong></td>
                      <td>{p.code || p.Code}</td>
                      <td style={{ color: 'var(--danger)' }}>{p.stock ?? p.Stock}</td>
                      <td>{p.minStock ?? p.MinStock}</td>
                      <td>${Number(p.salePrice ?? p.SalePrice).toFixed(2)}</td>
                    </tr>
                  ))}
                  {(!invStatus.low_stock_items || invStatus.low_stock_items.length === 0) && (
                    <tr><td colSpan={5} className="empty">Sin productos con stock bajo</td></tr>
                  )}
                </tbody>
              </table>
            </div>
          </div>
        </>
      )}

      {/* ═══════════════ TAB: GASTOS ═══════════════ */}
      {activeTab === 'gastos' && (
        <>
          <div className="dashboard-grid">
            <div className="dashboard-card" style={{ borderLeft: '4px solid var(--danger)' }}>
              <h3>Total Gastos</h3>
              <p className="stat-number">${totalExpenses.toFixed(2)}</p>
            </div>
            <div className="dashboard-card">
              <h3>Registros</h3>
              <p className="stat-number">{expenses.length}</p>
            </div>
            {summary && Number(summary.total_revenue) > 0 && (
              <div className="dashboard-card">
                <h3>Gastos vs Ingresos</h3>
                <p className="stat-number" style={{ color: totalExpenses > Number(summary.total_revenue) * 0.5 ? 'var(--danger)' : 'var(--success)' }}>
                  {((totalExpenses / Number(summary.total_revenue)) * 100).toFixed(1)}%
                </p>
                <p>de los ingresos</p>
              </div>
            )}
          </div>

          <div className="card">
            <h2>📋 Listado de Gastos</h2>
            <div className="table-container">
              <table className="table">
                <thead>
                  <tr><th>Descripción</th><th>Categoría</th><th>Monto</th><th>Método</th><th>Fecha</th></tr>
                </thead>
                <tbody>
                  {expenses.slice(0, 50).map((e: any) => (
                    <tr key={e.id || e.Id}>
                      <td>{e.description || e.Description}</td>
                      <td><span className="badge badge-info">{e.category || e.Category}</span></td>
                      <td><strong>${Number(e.amount || e.Amount).toFixed(2)}</strong></td>
                      <td>{e.paymentmethod || e.PaymentMethod || '-'}</td>
                      <td>{new Date(e.createdat || e.CreatedAt || e.date || e.Date).toLocaleDateString()}</td>
                    </tr>
                  ))}
                  {expenses.length === 0 && <tr><td colSpan={5} className="empty">Sin gastos registrados</td></tr>}
                </tbody>
              </table>
            </div>
          </div>
        </>
      )}

      {/* ═══════════════ TAB: CLIENTES ═══════════════ */}
      {activeTab === 'clientes' && (
        <>
          <div className="dashboard-grid">
            <div className="dashboard-card">
              <h3>Clientes Atendidos</h3>
              <p className="stat-number">{summary?.unique_customers || 0}</p>
              <p>Hoy</p>
            </div>
            <div className="dashboard-card">
              <h3>Ticket Promedio</h3>
              <p className="stat-number">${Number(summary?.average_ticket || 0).toFixed(2)}</p>
            </div>
            <div className="dashboard-card">
              <h3>Productos Vendidos</h3>
              <p className="stat-number">{summary?.total_products_sold || 0}</p>
            </div>
          </div>

          {topCustomers.length > 0 && (
            <div style={{ marginBottom: '1.5rem' }}>
              <HBarChart data={topCustomers} labelKey="fullname" valueKey="total_spent" title="👑 Top Clientes" color="#8b5cf6" format={v => `$${Number(v).toFixed(0)}`} />
            </div>
          )}
        </>
      )}

      <style>{`
        .bar-chart { display: flex; flex-direction: column; gap: 0.5rem; }
        .bar-label { display: flex; justify-content: space-between; font-size: 0.85rem; margin-bottom: 0.2rem; }
        .bar-track { height: 24px; background: var(--gray-100); border-radius: 4px; overflow: hidden; }
        .bar-fill { height: 100%; border-radius: 4px; transition: width 0.5s; min-width: 2%; }
        .tabs .btn-primary, .tabs .btn-secondary { border-radius: 0; }
        .tabs .btn-primary:first-child, .tabs .btn-secondary:first-child { border-radius: 6px 0 0 6px; }
        .tabs .btn-primary:last-child, .tabs .btn-secondary:last-child { border-radius: 0 6px 6px 0; }
      `}</style>
    </div>
  );
}
