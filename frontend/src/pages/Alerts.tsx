import { useState, useEffect } from 'react';
import { alertsApi, productsApi } from '../services/api';
import { useNavigate } from 'react-router-dom';

const STOCK_THRESHOLD = 5;

export default function Alerts() {
  const [backendAlerts, setBackendAlerts] = useState<any[]>([]);
  const [lowStockProducts, setLowStockProducts] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  const fetchData = async () => {
    setLoading(true);
    try {
      const [alertRes, prodRes] = await Promise.all([
        alertsApi.getAll().catch(() => ({ data: [] })),
        productsApi.getAll({ limit: 500 }).catch(() => ({ data: [] })),
      ]);
      const rawAlerts = alertRes.data;
      setBackendAlerts(Array.isArray(rawAlerts) ? rawAlerts : rawAlerts?.alerts || []);

      const rawProducts = prodRes.data;
      const products = Array.isArray(rawProducts) ? rawProducts : rawProducts?.products || [];
      const low = products.filter((p: any) => {
        const stock = p.stock ?? p.Stock ?? 999;
        return typeof stock === 'number' && stock <= STOCK_THRESHOLD;
      });
      setLowStockProducts(low);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchData(); }, []);

  const localAlerts = lowStockProducts.map((p, i) => ({
    id: `lowstock-${i}`,
    type: 'low_stock',
    title: 'Stock Bajo',
    message: `${p.name || p.Name || 'Producto'} — solo ${p.stock ?? p.Stock} unidad(es) en inventario (mín. ${p.minstock ?? p.minStock ?? '-'})`,
    severity: (p.stock ?? p.Stock) <= 2 ? 'high' : 'medium',
    isread: false,
    createdat: new Date().toISOString(),
  }));

  const allAlerts = [...localAlerts, ...backendAlerts];

  if (loading) return <div className="page-loading">Cargando...</div>;

  const unreadCount = allAlerts.filter(a => !a.isread).length;

  const getSeverityClass = (severity: string) => {
    switch (severity) {
      case 'high': return 'badge-danger';
      case 'medium': return 'badge-warning';
      case 'low': return 'badge-info';
      default: return 'badge-secondary';
    }
  };

  const getAlertIcon = (type: string) => {
    switch (type) {
      case 'low_stock': return '📦';
      case 'expiry': return '⏰';
      case 'system': return '🔧';
      default: return '🔔';
    }
  };

  return (
    <div className="page">
      <div className="page-header">
        <h1>Alertas {unreadCount > 0 && <span className="badge badge-danger">{unreadCount} nuevas</span>}</h1>
      </div>

      <div className="dashboard-grid" style={{marginBottom:'1.5rem'}}>
        <div className="dashboard-card" onClick={() => navigate('/products')}>
          <h3>📦 Stock Bajo (&le;{STOCK_THRESHOLD})</h3>
          <p className="stat-number">{lowStockProducts.length}</p>
        </div>
      </div>

      <div className="alerts-list">
        {allAlerts.length === 0 ? (
          <div className="card" style={{textAlign:'center',padding:'2rem'}}>
            <p>No hay alertas pendientes — todos los productos tienen stock suficiente (&gt;{STOCK_THRESHOLD}).</p>
          </div>
        ) : (
          allAlerts.map(a => (
            <div key={a.id} className={`alert-item ${a.isread ? 'read' : 'unread'}`}>
              <div className="alert-icon">{getAlertIcon(a.type)}</div>
              <div className="alert-content">
                <div className="alert-header">
                  <strong>{a.title}</strong>
                  <span className={`badge ${getSeverityClass(a.severity)}`}>{a.severity}</span>
                </div>
                <p>{a.message}</p>
                <small>{a.createdat ? new Date(a.createdat).toLocaleString() : ''}</small>
              </div>
              {!a.isread && <div className="alert-unread-dot" />}
            </div>
          ))
        )}
      </div>
    </div>
  );
}
