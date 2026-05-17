import { useState, useEffect } from 'react';
import { cashRegisterApi } from '../services/api';

export default function CashRegister() {
  const [sessions, setSessions] = useState<any[]>([]);
  const [activeSession, setActiveSession] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [showOpenModal, setShowOpenModal] = useState(false);
  const [showCloseModal, setShowCloseModal] = useState(false);
  const [openingBalance, setOpeningBalance] = useState(0);
  const [closingData, setClosingData] = useState({ closingbalance: 0, notes: '' });
  const [error, setError] = useState('');

  const fetchData = async () => {
    try {
      const [sessRes, activeRes] = await Promise.all([
        cashRegisterApi.getSessions({ limit: 50 }),
        cashRegisterApi.getActiveSession().catch(() => null),
      ]);
      setSessions(sessRes.data || []);
      setActiveSession(activeRes?.data || null);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchData(); }, []);

  const openSession = async () => {
    if (openingBalance <= 0) { setError('El monto inicial debe ser mayor a 0'); return; }
    try {
      await cashRegisterApi.openSession({ openingbalance: openingBalance });
      setShowOpenModal(false);
      setOpeningBalance(0);
      setError('');
      fetchData();
    } catch (err: any) {
      setError(err.response?.data?.error?.message || 'Error al abrir caja');
    }
  };

  const closeSession = async () => {
    if (closingData.closingbalance <= 0) { setError('El monto final debe ser mayor a 0'); return; }
    try {
      await cashRegisterApi.closeSession(closingData);
      setShowCloseModal(false);
      setClosingData({ closingbalance: 0, notes: '' });
      setError('');
      fetchData();
    } catch (err: any) {
      setError(err.response?.data?.error?.message || 'Error al cerrar caja');
    }
  };

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Caja</h1>
      </div>

      {activeSession ? (
        <div className="card card-success">
          <div className="session-active-header">
            <div>
              <h2>Sesión Activa</h2>
              <p>Abierta: {new Date(activeSession.openingdate).toLocaleString()}</p>
              <p>Saldo Inicial: ${Number(activeSession.openingbalance).toFixed(2)}</p>
              {activeSession.total_sales > 0 && (
                <>
                  <p>Ventas: {activeSession.total_sales} | Efectivo: ${Number(activeSession.total_cash || 0).toFixed(2)}</p>
                  <p>Tarjeta: ${Number(activeSession.total_card || 0).toFixed(2)} | Transferencia: ${Number(activeSession.total_transfer || 0).toFixed(2)}</p>
                </>
              )}
            </div>
            <button className="btn-danger" onClick={() => setShowCloseModal(true)}>
              Cerrar Caja
            </button>
          </div>
        </div>
      ) : (
        <div className="card">
          <div className="session-inactive">
            <h2>No hay sesión activa</h2>
            <p>Abre una sesión de caja para comenzar a registrar ventas</p>
            <button className="btn-primary" onClick={() => setShowOpenModal(true)}>
              Abrir Caja
            </button>
          </div>
        </div>
      )}

      <div className="card" style={{ marginTop: '1.5rem' }}>
        <h2>Historial de Sesiones</h2>
        <div className="table-container">
          <table className="table">
            <thead>
              <tr>
                <th>#</th><th>Usuario</th><th>Apertura</th><th>Cierre</th>
                <th>Inicial</th><th>Final</th><th>Ventas</th><th>Estado</th>
              </tr>
            </thead>
            <tbody>
              {sessions.map((s, i) => (
                <tr key={s.id}>
                  <td>{i + 1}</td>
                  <td>{s.user_name}</td>
                  <td>{new Date(s.openingdate).toLocaleString()}</td>
                  <td>{s.closingdate ? new Date(s.closingdate).toLocaleString() : '-'}</td>
                  <td>${Number(s.openingbalance).toFixed(2)}</td>
                  <td>{s.closingbalance ? `$${Number(s.closingbalance).toFixed(2)}` : '-'}</td>
                  <td>{s.total_sales || 0}</td>
                  <td><span className={`badge badge-${s.status === 'Open' ? 'success' : 'secondary'}`}>{s.status}</span></td>
                </tr>
              ))}
              {sessions.length === 0 && <tr><td colSpan={8} className="empty">Sin sesiones</td></tr>}
            </tbody>
          </table>
        </div>
      </div>

      {showOpenModal && (
        <div className="modal-overlay" onClick={() => setShowOpenModal(false)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>Abrir Caja</h2>
            {error && <div className="error-message">{error}</div>}
            <div className="form-group">
              <label>Monto Inicial *</label>
              <input type="number" step="0.01" value={openingBalance}
                onChange={e => setOpeningBalance(parseFloat(e.target.value) || 0)} autoFocus />
            </div>
            <div className="modal-actions">
              <button className="btn-secondary" onClick={() => setShowOpenModal(false)}>Cancelar</button>
              <button className="btn-primary" onClick={openSession}>Abrir Caja</button>
            </div>
          </div>
        </div>
      )}

      {showCloseModal && (
        <div className="modal-overlay" onClick={() => setShowCloseModal(false)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>Cerrar Caja</h2>
            {error && <div className="error-message">{error}</div>}
            <div className="form-group">
              <label>Monto Final *</label>
              <input type="number" step="0.01" value={closingData.closingbalance}
                onChange={e => setClosingData({...closingData, closingbalance: parseFloat(e.target.value) || 0})} autoFocus />
            </div>
            <div className="form-group">
              <label>Notas</label>
              <textarea value={closingData.notes} onChange={e => setClosingData({...closingData, notes: e.target.value})} />
            </div>
            <div className="modal-actions">
              <button className="btn-secondary" onClick={() => setShowCloseModal(false)}>Cancelar</button>
              <button className="btn-danger" onClick={closeSession}>Cerrar Caja</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
