import { useState, useEffect } from 'react';
import { salesApi } from '../services/api';

function fmt(n: any): string {
  const v = Number(n);
  return !isNaN(v) && n != null ? `$${v.toFixed(2)}` : '—';
}

export default function Sales() {
  const [sales, setSales] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [detail, setDetail] = useState<any>(null);
  const [startDate, setStartDate] = useState(() => new Date().toISOString().split('T')[0]);
  const [endDate, setEndDate] = useState(() => new Date().toISOString().split('T')[0]);
  const [dateError, setDateError] = useState('');

  const fetchSales = async () => {
    try {
      const params: any = { limit: 50 };
      if (startDate) params.startdate = `${startDate}T00:00:00`;
      if (endDate) params.enddate = `${endDate}T23:59:59`;
      const res = await salesApi.getAll(params);
      const data = res.data;
      let list = Array.isArray(data) ? data : data.sales || [];

      // SaleListDTO does not include subtotal/tax; batch-fetch details to fill them
      if (list.length > 0 && (list[0].subtotal === undefined || list[0].tax === undefined)) {
        const BATCH = 10;
        const populated: any[] = [];
        for (let i = 0; i < list.length; i += BATCH) {
          const batch = list.slice(i, i + BATCH);
          const details = await Promise.allSettled(
            batch.map((s: any) => salesApi.getById(s.id).then(r => r.data))
          );
          batch.forEach((s: any, j: number) => {
            const d = details[j].status === 'fulfilled' ? details[j].value : null;
            if (d) { s.subtotal = d.subtotal; s.tax = d.tax; }
            populated.push(s);
          });
        }
        list = populated;
      }

      setSales(list);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchSales(); }, []);

  const searchByDate = () => {
    if (startDate && endDate && startDate > endDate) {
      setDateError('La fecha Desde debe ser menor o igual a la fecha Hasta');
      return;
    }
    setDateError('');
    setLoading(true);
    fetchSales();
  };

  const handleStartDateChange = (value: string) => {
    setStartDate(value);
    if (value && endDate && value > endDate) {
      setDateError('La fecha Desde debe ser menor o igual a la fecha Hasta');
    } else {
      setDateError('');
    }
  };

  const handleEndDateChange = (value: string) => {
    setEndDate(value);
    if (startDate && value && startDate > value) {
      setDateError('La fecha Desde debe ser menor o igual a la fecha Hasta');
    } else {
      setDateError('');
    }
  };

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
        <input type="date" value={startDate} onChange={e => handleStartDateChange(e.target.value)} />
        <label>Hasta:</label>
        <input type="date" value={endDate} onChange={e => handleEndDateChange(e.target.value)} />
        <button className="btn-primary" onClick={searchByDate}>Filtrar</button>
        {dateError && <span style={{ color: 'var(--danger)', fontSize: '0.85rem', marginLeft: '0.5rem' }}>{dateError}</span>}
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
                <td>{s.receiptNumber ?? s.receiptnumber}</td>
                <td>{s.customerName ?? s.customer_name ?? 'Mostrador'}</td>
                <td>{s.userName ?? s.user_name}</td>
                <td>{fmt(s.subtotal)}</td>
                <td>{fmt(s.tax)}</td>
                <td><strong>{fmt(s.total)}</strong></td>
                <td>{s.paymentMethod ?? s.paymentmethod}</td>
                <td>{new Date(s.createdAt ?? s.createdat).toLocaleDateString()}</td>
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
            <h2>Venta: {detail.receiptNumber ?? detail.receiptnumber}</h2>
            <div className="detail-grid">
              <div><strong>Cliente:</strong> {detail.customerName ?? detail.customer_name ?? 'Mostrador'}</div>
              <div><strong>Usuario:</strong> {detail.userName ?? detail.user_name}</div>
              <div><strong>Fecha:</strong> {new Date(detail.createdAt ?? detail.createdat).toLocaleString()}</div>
              <div><strong>Método Pago:</strong> {detail.paymentMethod ?? detail.paymentmethod}</div>
            </div>
            <h3 style={{marginTop:'1rem',marginBottom:'0.5rem'}}>Artículos</h3>
            <table className="table">
              <thead>
                <tr><th>Producto</th><th>Código</th><th>Cant</th><th>Precio</th><th>Subtotal</th></tr>
              </thead>
              <tbody>
                {(detail.items || []).map((i: any) => (
                  <tr key={i.id}>
                    <td>{i.productName ?? i.product_name}</td>
                    <td>{i.productCode ?? i.product_code}</td>
                    <td>{i.quantity}</td>
                    <td>{fmt(i.unitPrice ?? i.unitprice)}</td>
                    <td>{fmt(i.subtotal)}</td>
                  </tr>
                ))}
              </tbody>
              <tfoot>
                <tr><td colSpan={4}><strong>Subtotal</strong></td><td>{fmt(detail.subtotal)}</td></tr>
                <tr><td colSpan={4}><strong>IVA</strong></td><td>{fmt(detail.tax)}</td></tr>
                {detail.discount > 0 && <tr><td colSpan={4}><strong>Descuento</strong></td><td>-{fmt(detail.discount).replace('$', '')}</td></tr>}
                <tr><td colSpan={4}><strong>Total</strong></td><td><strong>{fmt(detail.total)}</strong></td></tr>
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
