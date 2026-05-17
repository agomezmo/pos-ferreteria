import { useState, useEffect } from 'react';
import { alertsApi } from '../services/api';
import { useNavigate } from 'react-router-dom';

export default function Alerts() {
  const [alerts, setAlerts] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  const fetchAlerts = async () => {
    try {
      const res = await alertsApi.getAll();
      setAlerts(Array.isArray(res.data) ? res.data : []);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchAlerts(); }, []);

  const markAsRead = async (id: number) => {
    try {
      await alertsApi.markAsRead(id);
      fetchAlerts();
    } catch (err) { console.error(err); }
  };

  const markAllAsRead = async () => {
    try {
      await alertsApi.markAllAsRead();
      fetchAlerts();
    } catch (err) { console.error(err); }
  };

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

  if (loading) return <div className="page-loading">Cargando...</div>;

  const unreadCount = alerts.filter(a => !a.isread).length;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Alertas {unreadCount > 0 && <span className="badge badge-danger">{unreadCount} nuevas</span>}</h1>
        {unreadCount > 0 && (
          <button className="btn-secondary" onClick={markAllAsRead}>Marcar todas como leídas</button>
        )}
      </div>

      <div className="dashboard-grid" style={{marginBottom:'1.5rem'}}>
        <div className="dashboard-card" onClick={() => navigate('/products?filter=low_stock')}>
          <h3>📦 Stock Bajo</h3>
          <p className="stat-number">{alerts.filter(a => a.type === 'low_stock' && !a.isread).length}</p>
        </div>
        <div className="dashboard-card" onClick={() => navigate('/products?filter=expiring')}>
          <h3>⏰ Por Vencer</h3>
          <p className="stat-number">{alerts.filter(a => a.type === 'expiry' && !a.isread).length}</p>
        </div>
      </div>

      <div className="alerts-list">
        {alerts.length === 0 ? (
          <div className="card" style={{textAlign:'center',padding:'2rem'}}>
            <p>No hay alertas pendientes</p>
          </div>
        ) : (
          alerts.map(a => (
            <div key={a.id} className={`alert-item ${a.isread ? 'read' : 'unread'}`} onClick={() => !a.isread && markAsRead(a.id)}>
              <div className="alert-icon">{getAlertIcon(a.type)}</div>
              <div className="alert-content">
                <div className="alert-header">
                  <strong>{a.title}</strong>
                  <span className={`badge ${getSeverityClass(a.severity)}`}>{a.severity}</span>
                </div>
                <p>{a.message}</p>
                <small>{new Date(a.createdat).toLocaleString()}</small>
              </div>
              {!a.isread && <div className="alert-unread-dot" />}
            </div>
          ))
        )}
      </div>
    </div>
  );
}
