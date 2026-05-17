import { useState, useEffect } from 'react';
import { salesApi } from '../services/api';

export default function Sales() {
  const [sales, setSales] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [detail, setDetail] = useState<any>(null);
  const [startDate, setStartDate] = useState('');
  const [endDate, setEndDate] = useState('');

  const fetchSales = async () => {
    try {
      const params: any = { limit: 50 };
      if (startDate) params.startdate = startDate;
      if (endDate) params.enddate = endDate;
      const res = await salesApi.getAll(params);
      setSales(res.data.sales || []);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchSales(); }, []);

  const searchByDate = () => { setLoading(true); fetchSales(); };

  const viewDetail = async (id: number) => {
    try {
      const res = await salesApi.getById(id);
      setDetail(res.data);
    } catch (err) { console.error(err); }
  };

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Ventas</h1>
      </div>
      <div className="search-bar">
        <label>Desde:</label>
        <input type="date" value={startDate} onChange={e => setStartDate(e.target.value)} />
        <label>Hasta:</label>
        <input type="date" value={endDate} onChange={e => setEndDate(e.target.value)} />
        <button className="btn-primary" onClick={searchByDate}>Filtrar</button>
      </div>
      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th>Folio</th><th>Cliente</th><th>Usuario</th><th>Subtotal</th><th>IVA</th><th>Total</th><th>Método Pago</th><th>Fecha</th><th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {sales.map(s => (
              <tr key={s.id}>
                <td>{s.receiptnumber}</td>
                <td>{s.customer_name || 'Mostrador'}</td>
                <td>{s.user_name}</td>
                <td>${Number(s.subtotal).toFixed(2)}</td>
                <td>${Number(s.tax).toFixed(2)}</td>
                <td><strong>${Number(s.total).toFixed(2)}</strong></td>
                <td>{s.paymentmethod}</td>
                <td>{new Date(s.createdat).toLocaleDateString()}</td>
                <td><button className="btn-sm" onClick={() => viewDetail(s.id)}>Ver</button></td>
              </tr>
            ))}
            {sales.length === 0 && <tr><td colSpan={9} className="empty">No hay ventas</td></tr>}
          </tbody>
        </table>
      </div>

      {detail && (
        <div className="modal-overlay" onClick={() => setDetail(null)}>
          <div className="modal modal-lg" onClick={e => e.stopPropagation()}>
            <h2>Venta: {detail.receiptnumber}</h2>
            <div className="detail-grid">
              <div><strong>Cliente:</strong> {detail.customer_name || 'Mostrador'}</div>
              <div><strong>Usuario:</strong> {detail.user_name}</div>
              <div><strong>Fecha:</strong> {new Date(detail.createdat).toLocaleString()}</div>
              <div><strong>Método Pago:</strong> {detail.paymentmethod}</div>
            </div>
            <h3 style={{marginTop:'1rem',marginBottom:'0.5rem'}}>Artículos</h3>
            <table className="table">
              <thead>
                <tr><th>Producto</th><th>Código</th><th>Cant</th><th>Precio</th><th>Subtotal</th></tr>
              </thead>
              <tbody>
                {(detail.items || []).map((i: any) => (
                  <tr key={i.id}>
                    <td>{i.product_name}</td>
                    <td>{i.product_code}</td>
                    <td>{i.quantity}</td>
                    <td>${Number(i.unitprice).toFixed(2)}</td>
                    <td>${Number(i.subtotal).toFixed(2)}</td>
                  </tr>
                ))}
              </tbody>
              <tfoot>
                <tr><td colSpan={4}><strong>Subtotal</strong></td><td>${Number(detail.subtotal).toFixed(2)}</td></tr>
                <tr><td colSpan={4}><strong>IVA</strong></td><td>${Number(detail.tax).toFixed(2)}</td></tr>
                {detail.discount > 0 && <tr><td colSpan={4}><strong>Descuento</strong></td><td>-${Number(detail.discount).toFixed(2)}</td></tr>}
                <tr><td colSpan={4}><strong>Total</strong></td><td><strong>${Number(detail.total).toFixed(2)}</strong></td></tr>
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
