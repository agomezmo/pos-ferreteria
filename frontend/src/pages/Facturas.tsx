import { useState, useEffect } from 'react';
import { facturasApi } from '../services/api';

export default function Facturas() {
  const [facturas, setFacturas] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [detail, setDetail] = useState<any>(null);

  const fetchFacturas = async () => {
    try {
      const res = await facturasApi.getAll();
      setFacturas(Array.isArray(res.data) ? res.data : []);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchFacturas(); }, []);

  const viewDetail = async (id: number) => {
    try {
      const res = await facturasApi.getById(id);
      setDetail(res.data);
    } catch (err) { console.error(err); }
  };

  const handleCancel = async (id: number) => {
    const motivo = prompt('Motivo de cancelación:');
    if (!motivo) return;
    try {
      await facturasApi.cancel(id, motivo);
      fetchFacturas();
    } catch (err: any) {
      alert(err.response?.data?.error?.message || 'Error al cancelar factura');
    }
  };

  const getStatusBadge = (status: string) => {
    switch (status) {
      case 'Active': return <span className="badge badge-success">Activa</span>;
      case 'Cancelled': return <span className="badge badge-danger">Cancelada</span>;
      default: return <span className="badge badge-secondary">{status}</span>;
    }
  };

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Facturación CFDI</h1>
      </div>
      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th>UUID</th><th>Serie</th><th>Folio</th><th>RFC</th><th>Razón Social</th><th>Total</th><th>Estado</th><th>Fecha</th><th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {facturas.map(f => (
              <tr key={f.id}>
                <td title={f.uuid}>{f.uuid?.substring(0, 12)}...</td>
                <td>{f.serie || '-'}</td>
                <td>{f.folio || '-'}</td>
                <td>{f.rfc}</td>
                <td>{f.razonsocial}</td>
                <td><strong>${Number(f.total).toFixed(2)}</strong></td>
                <td>{getStatusBadge(f.status)}</td>
                <td>{new Date(f.createdat).toLocaleDateString()}</td>
                <td className="actions">
                  <button className="btn-sm" onClick={() => viewDetail(f.id)}>Ver</button>
                  {f.status === 'Active' && (
                    <button className="btn-sm btn-danger" onClick={() => handleCancel(f.id)}>Cancelar</button>
                  )}
                </td>
              </tr>
            ))}
            {facturas.length === 0 && <tr><td colSpan={9} className="empty">No hay facturas generadas</td></tr>}
          </tbody>
        </table>
      </div>

      {detail && (
        <div className="modal-overlay" onClick={() => setDetail(null)}>
          <div className="modal modal-lg" onClick={e => e.stopPropagation()}>
            <h2>Factura: {detail.uuid}</h2>
            <div className="detail-grid">
              <div><strong>Serie:</strong> {detail.serie} <strong>Folio:</strong> {detail.folio}</div>
              <div><strong>RFC:</strong> {detail.rfc}</div>
              <div><strong>Razón Social:</strong> {detail.razonsocial}</div>
              <div><strong>Total:</strong> ${Number(detail.total).toFixed(2)}</div>
              <div><strong>Estado:</strong> {getStatusBadge(detail.status)}</div>
              <div><strong>Venta:</strong> #{detail.saleid}</div>
            </div>
            <div className="modal-actions">
              <button className="btn-secondary" onClick={() => setDetail(null)}>Cerrar</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
