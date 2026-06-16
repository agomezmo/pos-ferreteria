import { useState, useEffect } from 'react';
import { cashRegisterApi, salesApi } from '../services/api';
import api from '../services/api';

export default function CashRegister() {
  const [sessions, setSessions] = useState<any[]>([]);
  const [activeSession, setActiveSession] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [showOpenModal, setShowOpenModal] = useState(false);
  const [showCloseModal, setShowCloseModal] = useState(false);
  const [openingBalance, setOpeningBalance] = useState('');
  const [closingData, setClosingData] = useState({ closingAmount: '', closingNotes: '' });
  const [error, setError] = useState('');
  const [sessionSales, setSessionSales] = useState<any>(null);

  const CASH_REGISTER_ID = 1;

  const fetchData = async () => {
    try {
      const [sessRes, activeRes] = await Promise.all([
        cashRegisterApi.getSessions({ limit: 50 }),
        cashRegisterApi.getCurrentSession(CASH_REGISTER_ID).catch(() => null),
      ]);
      const active = activeRes?.data || null;
      setSessions(sessRes.data || []);
      setActiveSession(active);
      if (active) {
        const openedAt = new Date(active.openedAt);
        const salesRes = await api.get('/sales', { params: { limit: 1000 } });
        const allSales = salesRes.data?.sales || salesRes.data || [];
        const sinceOpen = allSales.filter((s: any) => new Date(s.createdAt) >= openedAt);
        const cash = sinceOpen.filter((s: any) => s.paymentMethod === 'Efectivo' || s.paymentMethod === 'Cash');
        const card = sinceOpen.filter((s: any) => s.paymentMethod === 'Tarjeta');
        const transfer = sinceOpen.filter((s: any) => s.paymentMethod === 'Transferencia');
        const totalCash = cash.reduce((sum: number, s: any) => sum + Number(s.total || 0), 0);
        setSessionSales({
          total_sales: sinceOpen.length,
          total_cash: totalCash,
          total_card: card.reduce((sum: number, s: any) => sum + Number(s.total || 0), 0),
          total_transfer: transfer.reduce((sum: number, s: any) => sum + Number(s.total || 0), 0),
          expected: Number(active.openingAmount || 0) + totalCash,
        });
      } else {
        setSessionSales(null);
      }
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchData(); }, []);

  const openSession = async () => {
    const amount = parseFloat(openingBalance);
    if (!openingBalance || isNaN(amount) || amount < 0) { setError('Ingresa un monto inicial válido'); return; }
    try {
      await cashRegisterApi.openSession({ cashRegisterId: CASH_REGISTER_ID, openingAmount: amount, openingNotes: '' });
      setShowOpenModal(false);
      setOpeningBalance('');
      setError('');
      fetchData();
    } catch (err: any) {
      setError(err.response?.data?.error || err.response?.data?.message || 'Error al abrir caja');
    }
  };

  const closeSession = async () => {
    if (!activeSession) return;
    const amount = parseFloat(closingData.closingAmount);
    if (!closingData.closingAmount || isNaN(amount) || amount < 0) { setError('Ingresa un monto final válido'); return; }
    try {
      await cashRegisterApi.closeSession(activeSession.id, {
        closingAmount: amount,
        closingNotes: closingData.closingNotes || '',
      });
      setShowCloseModal(false);
      setClosingData({ closingAmount: '', closingNotes: '' });
      setError('');
      fetchData();
    } catch (err: any) {
      setError(err.response?.data?.error || err.response?.data?.message || 'Error al cerrar caja');
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
              <p>Abierta: {new Date(activeSession.openedAt).toLocaleString()}</p>
              <p>Usuario: {activeSession.userName}</p>
              <p>Saldo Inicial: ${Number(activeSession.openingAmount).toFixed(2)}</p>
              {sessionSales && (
                <>
                  <p>Ventas: {sessionSales.total_sales} | Efectivo: ${sessionSales.total_cash.toFixed(2)}</p>
                  <p>Tarjeta: ${sessionSales.total_card.toFixed(2)} | Transferencia: ${sessionSales.total_transfer.toFixed(2)}</p>
                  <p><strong>Efectivo Esperado: ${sessionSales.expected.toFixed(2)}</strong></p>
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
                <th>Inicial</th><th>Final</th><th>Esperado</th><th>Diferencia</th><th>Estado</th>
              </tr>
            </thead>
            <tbody>
              {sessions.map((s, i) => {
                const expected = Number(s.openingAmount) + Number(s.expectedAmount || 0);
                const diff = s.closingAmount != null ? Number(s.closingAmount) - expected : 0;
                return (
                  <tr key={s.id}>
                    <td>{i + 1}</td>
                    <td>{s.userName}</td>
                    <td>{new Date(s.openedAt).toLocaleString()}</td>
                    <td>{s.closedAt ? new Date(s.closedAt).toLocaleString() : '-'}</td>
                    <td>${Number(s.openingAmount).toFixed(2)}</td>
                    <td>{s.closingAmount != null ? `$${Number(s.closingAmount).toFixed(2)}` : '-'}</td>
                    <td>${expected.toFixed(2)}</td>
                    <td>{s.closingAmount != null ? `$${diff.toFixed(2)}` : '-'}</td>
                    <td><span className={`badge badge-${s.status === 'Open' ? 'success' : 'secondary'}`}>{s.status}</span></td>
                  </tr>
                );
              })}
              {sessions.length === 0 && <tr><td colSpan={9} className="empty">Sin sesiones</td></tr>}
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
                onChange={e => setOpeningBalance(e.target.value)} placeholder="0.00" autoFocus />
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
            {sessionSales && (
              <div style={{ marginBottom: '1rem', padding: '0.75rem', background: '#f0fdf4', borderRadius: 6, border: '1px solid #bbf7d0' }}>
                <p><strong>Efectivo Esperado: ${sessionSales.expected.toFixed(2)}</strong></p>
                <p style={{ fontSize: '0.85rem', color: '#666' }}>
                  ({sessionSales.total_sales} ventas · ${sessionSales.total_cash.toFixed(2)} efectivo + ${Number(activeSession?.openingAmount || 0).toFixed(2)} inicial)
                </p>
              </div>
            )}
            <div className="form-group">
              <label>Monto Final *</label>
              <input type="number" step="0.01" value={closingData.closingAmount}
                onChange={e => setClosingData({...closingData, closingAmount: e.target.value})} placeholder="0.00" autoFocus />
            </div>
            <div className="form-group">
              <label>Notas</label>
              <textarea value={closingData.closingNotes} onChange={e => setClosingData({...closingData, closingNotes: e.target.value})} />
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
