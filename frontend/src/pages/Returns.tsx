import { useState, useEffect } from 'react';
import { returnsApi } from '../services/api';

export default function Returns() {
  const [returns, setReturns] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [detail, setDetail] = useState<any>(null);

  const fetchReturns = async () => {
    try {
      const res = await returnsApi.getAll();
      setReturns(Array.isArray(res.data) ? res.data : []);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchReturns(); }, []);

  const viewDetail = async (id: number) => {
    try {
      const res = await returnsApi.getById(id);
      setDetail(res.data);
    } catch (err) { console.error(err); }
  };

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Devoluciones</h1>
      </div>
      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th>#</th><th>Venta</th><th>Usuario</th><th>Motivo</th><th>Total</th><th>Fecha</th><th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {returns.map((r, i) => (
              <tr key={r.id}>
                <td>{i + 1}</td>
                <td>{r.receiptnumber || r.saleid}</td>
                <td>{r.user_name}</td>
                <td>{r.reason}</td>
                <td>${Number(r.total).toFixed(2)}</td>
                <td>{new Date(r.createdat).toLocaleDateString()}</td>
                <td><button className="btn-sm" onClick={() => viewDetail(r.id)}>Ver</button></td>
              </tr>
            ))}
            {returns.length === 0 && <tr><td colSpan={7} className="empty">No hay devoluciones</td></tr>}
          </tbody>
        </table>
      </div>

      {detail && (
        <div className="modal-overlay" onClick={() => setDetail(null)}>
          <div className="modal modal-lg" onClick={e => e.stopPropagation()}>
            <h2>Devolución de Venta: {detail.receiptnumber || '#' + detail.saleid}</h2>
            <div className="detail-grid">
              <div><strong>Usuario:</strong> {detail.user_name}</div>
              <div><strong>Motivo:</strong> {detail.reason}</div>
              <div><strong>Fecha:</strong> {new Date(detail.createdat).toLocaleString()}</div>
            </div>
            <h3 style={{marginTop:'1rem'}}>Artículos Devueltos</h3>
            <table className="table">
              <thead>
                <tr><th>Producto</th><th>Cant</th><th>Precio</th><th>Subtotal</th></tr>
              </thead>
              <tbody>
                {(detail.items || []).map((i: any) => (
                  <tr key={i.id}>
                    <td>{i.product_name}</td>
                    <td>{i.quantity}</td>
                    <td>${Number(i.unitprice).toFixed(2)}</td>
                    <td>${Number(i.subtotal).toFixed(2)}</td>
                  </tr>
                ))}
              </tbody>
              <tfoot>
                <tr><td colSpan={3}><strong>Total</strong></td><td><strong>${Number(detail.total).toFixed(2)}</strong></td></tr>
              </tfoot>
            </table>
            <div className="modal-actions">
              <button className="btn-secondary" onClick={() => setDetail(null)}>Cerrar</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
