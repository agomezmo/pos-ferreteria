import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { reportsApi, alertsApi } from '../services/api';
import api from '../services/api';

const months = ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'];

function BarChart({ data, labelKey, valueKey, color, title, format }: {
  data: any[]; labelKey: string; valueKey: string; color: string; title: string; format?: (v: number) => string;
}) {
  if (!data.length) return <div className="card"><h2>{title}</h2><p style={{ color: 'var(--gray-400)', padding: '1rem 0' }}>Sin datos</p></div>;
  const max = Math.max(...data.map(d => Number(d[valueKey])), 1);
  const pad = data.length < 6 ? 30 : 10;
  return (
    <div className="card">
      <h2>{title}</h2>
      <svg width="100%" height="180" viewBox={`0 0 ${data.length * (40 + pad) + 40} 160`} style={{ overflow: 'visible' }}>
        {data.map((d, i) => {
          const v = Number(d[valueKey]);
          const h = (v / max) * 120;
          const x = i * (40 + pad) + 20;
          const y = 140 - h;
          const label = d[labelKey]?.toString().length > 5 ? d[labelKey]?.toString().slice(0, 5) : (d[labelKey] || '');
          return (
            <g key={i}>
              <rect x={x} y={y} width={30} height={h} rx={4} fill={color} opacity={0.85}>
                <title>{`${d[labelKey]}: ${format ? format(v) : v}`}</title>
              </rect>
              <text x={x + 15} y={154} textAnchor="middle" fontSize={10} fill="var(--gray-500)">{label}</text>
              <text x={x + 15} y={y - 4} textAnchor="middle" fontSize={9} fill="var(--gray-600)">
                {format ? format(v) : v}
              </text>
            </g>
          );
        })}
      </svg>
    </div>
  );
}

function groupByMonth(items: any[], dateField: string, valueField: string): any[] {
  const groups: Record<string, { year: number; month: number; total: number; count: number }> = {};
  for (const item of items) {
    const d = new Date(item[dateField] || item.createdat || item.date);
    const key = `${d.getFullYear()}-${d.getMonth()}`;
    if (!groups[key]) groups[key] = { year: d.getFullYear(), month: d.getMonth() + 1, total: 0, count: 0 };
    groups[key].total += Number(item[valueField] || 0);
    groups[key].count += 1;
  }
  return Object.values(groups).sort((a, b) => a.year - b.year || a.month - b.month)
    .map(g => ({ label: `${months[g.month - 1]} ${g.year}`, ...g }));
}

export default function Dashboard() {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [summary, setSummary] = useState<any>(null);
  const [invStatus, setInvStatus] = useState<any>(null);
  const [alerts, setAlerts] = useState<any[]>([]);
  const [recentSales, setRecentSales] = useState<any[]>([]);
  const [monthlySales, setMonthlySales] = useState<any[]>([]);
  const [monthlyExpenses, setMonthlyExpenses] = useState<any[]>([]);
  const [lowStock, setLowStock] = useState<any[]>([]);
  const [expiringProducts, setExpiringProducts] = useState<any[]>([]);

  useEffect(() => {
    const today = new Date().toISOString().split('T')[0];
    reportsApi.getDailySummary(today).then(r => setSummary(r.data)).catch(() => {});
    reportsApi.getInventoryStatus().then(r => setInvStatus(r.data)).catch(() => {});
    alertsApi.getAll().then(r => {
      const data = Array.isArray(r.data) ? r.data : [];
      setAlerts(data.filter((a: any) => !a.isread).slice(0, 5));
    }).catch(() => {});
    api.get('/sales', { params: { limit: 5 } })
      .then(r => setRecentSales(r.data?.sales || r.data || []))
      .catch(() => {});

    // Client-side monthly aggregation (ferretería API has no monthly endpoints)
    api.get('/sales', { params: { limit: 500 } }).then(r => {
      const sales = r.data?.sales || r.data || [];
      if (Array.isArray(sales)) setMonthlySales(groupByMonth(sales, 'createdAt', 'total'));
    }).catch(() => {});
    api.get('/expenses', { params: { limit: 500 } }).then(r => {
      const expenses = r.data?.data || r.data || [];
      if (Array.isArray(expenses)) setMonthlyExpenses(groupByMonth(expenses, 'date', 'amount'));
    }).catch(() => {});

    // Low stock & expiring products
    api.get('/products', { params: { limit: 500 } }).then(r => {
      const raw: any[] = r.data?.products || r.data || [];
      const prods = raw.map((p: any) => ({
        ...p,
        stock: p.stock ?? p.stock,
        minstock: p.minstock ?? p.minStock,
        expiry_date: p.expiry_date ?? p.expiryDate,
      }));
      setLowStock(prods.filter((p: any) => p.stock <= p.minstock).slice(0, 5));
      const now = new Date();
      const thirtyDays = new Date(now.getTime() + 30 * 24 * 60 * 60 * 1000);
      setExpiringProducts(prods.filter((p: any) => p.expiry_date && new Date(p.expiry_date) <= thirtyDays).slice(0, 5));
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

      <div className="dashboard-grid">
        {summary && (
          <>
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
          </>
        )}
        {invStatus && (
          <>
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
          </>
        )}
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem', marginBottom: '1.5rem' }}>
        <BarChart
          data={monthlySales}
          labelKey="label" valueKey="total"
          color="var(--success)" title="📈 Ventas Mensuales"
          format={v => `$${Number(v).toFixed(0)}`}
        />
        <BarChart
          data={monthlyExpenses}
          labelKey="label" valueKey="total"
          color="var(--danger)" title="📉 Gastos Mensuales"
          format={v => `$${Number(v).toFixed(0)}`}
        />
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem', marginBottom: '1.5rem' }}>
        {lowStock.length > 0 && (
          <div className="card card-danger">
            <h2>⚠️ Stock Bajo</h2>
            {lowStock.map((p: any) => (
              <div key={p.id} style={{ display: 'flex', justifyContent: 'space-between', padding: '0.35rem 0', borderBottom: '1px solid var(--gray-100)' }}>
                <span><strong>{p.name}</strong></span>
                <span style={{ color: 'var(--danger)' }}>{p.stock} / {p.minstock} {p.unit}</span>
              </div>
            ))}
            <button className="btn-sm" style={{ marginTop: '0.5rem' }} onClick={() => navigate('/products')}>
              Ver todos
            </button>
          </div>
        )}
        {expiringProducts.length > 0 && (
          <div className="card card-warning">
            <h2>⏰ Próximos a Caducar</h2>
            {expiringProducts.map((p: any) => (
              <div key={p.id} style={{ display: 'flex', justifyContent: 'space-between', padding: '0.35rem 0', borderBottom: '1px solid var(--gray-100)' }}>
                <span><strong>{p.name}</strong></span>
                <span style={{ color: 'var(--warning)' }}>
                  {p.expiry_date ? new Date(p.expiry_date).toLocaleDateString() : ''}
                </span>
              </div>
            ))}
          </div>
        )}
      </div>

      <h2 style={{ marginBottom: '0.75rem', fontSize: '1.1rem' }}>Acceso Rápido</h2>
      <div className="dashboard-grid">
        {modules.map(m => (
          <div key={m.path} className="dashboard-card" onClick={() => navigate(m.path)}
            style={{ borderLeft: `3px solid ${m.color}` }}>
            <h3>{m.title}</h3>
            <p>{m.desc}</p>
          </div>
        ))}
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem', marginTop: '1rem' }}>
        {recentSales.length > 0 && (
          <div className="card">
            <h2>Últimas Ventas</h2>
            <table className="table">
              <thead>
                <tr><th>Folio</th><th>Total</th><th>Hora</th></tr>
              </thead>
              <tbody>
                {recentSales.map((s: any) => (
                  <tr key={s.id} style={{ cursor: 'pointer' }} onClick={() => navigate('/sales')}>
                    <td>{s.receiptnumber || `#${s.id}`}</td>
                    <td><strong>${Number(s.total).toFixed(2)}</strong></td>
                    <td>{new Date(s.createdAt || s.createdat).toLocaleTimeString()}</td>
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
            <div>
              {alerts.map((a: any) => (
                <div key={a.id} className="alert-item unread" style={{ marginBottom: '0.35rem', padding: '0.5rem' }}>
                  <div className="alert-content">
                    <strong>{a.title}</strong>
                    <p style={{ fontSize: '0.82rem' }}>{a.message}</p>
                  </div>
                </div>
              ))}
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
