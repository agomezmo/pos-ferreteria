import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { reportsApi, alertsApi } from '../services/api';
import api from '../services/api';

export default function Dashboard() {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [summary, setSummary] = useState<any>(null);
  const [invStatus, setInvStatus] = useState<any>(null);
  const [alerts, setAlerts] = useState<any[]>([]);
  const [recentSales, setRecentSales] = useState<any[]>([]);

  useEffect(() => {
    const today = new Date().toISOString().split('T')[0];
    reportsApi.getDailySummary(today).then(r => setSummary(r.data)).catch(() => {});
    reportsApi.getInventoryStatus().then(r => setInvStatus(r.data)).catch(() => {});
    alertsApi.getAll().then(r => {
      const data = Array.isArray(r.data) ? r.data : [];
      setAlerts(data.filter((a: any) => !a.isread).slice(0, 5));
    }).catch(() => {});
    api.get('/sales', { params: { limit: 5 } })
      .then(r => setRecentSales(r.data?.sales || []))
      .catch(() => {});
  }, []);

  const modules = [
    { title: '🛒 Nueva Venta', desc: 'Registrar una venta', path: '/sales/new', color: 'var(--success)' },
    { title: '💰 Caja', desc: 'Abrir/cerrar caja', path: '/cash-register', color: 'var(--warning)' },
    { title: '💊 Productos', desc: 'Inventario de productos', path: '/products', color: 'var(--primary)' },
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
                    <td>{s.receiptnumber}</td>
                    <td><strong>${Number(s.total).toFixed(2)}</strong></td>
                    <td>{new Date(s.createdat).toLocaleTimeString()}</td>
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
