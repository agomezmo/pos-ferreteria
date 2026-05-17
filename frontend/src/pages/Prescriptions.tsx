import { useState, useEffect } from 'react';
import { prescriptionsApi } from '../services/api';

export default function Prescriptions() {
  const [prescriptions, setPrescriptions] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [detail, setDetail] = useState<any>(null);

  const fetchPrescriptions = async () => {
    try {
      const res = await prescriptionsApi.getAll();
      setPrescriptions(Array.isArray(res.data) ? res.data : []);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchPrescriptions(); }, []);

  const viewDetail = async (id: number) => {
    try {
      const res = await prescriptionsApi.getById(id);
      setDetail(res.data);
    } catch (err) { console.error(err); }
  };

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Recetas Médicas</h1>
      </div>
      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th>Paciente</th><th>Médico</th><th>Cédula</th><th>Diagnóstico</th><th>Emisión</th><th>Vencimiento</th><th>Estado</th><th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {prescriptions.map(p => (
              <tr key={p.id}>
                <td><strong>{p.patient_name}</strong></td>
                <td>{p.doctor_name}</td>
                <td>{p.license_number}</td>
                <td>{p.diagnosis?.substring(0, 50)}...</td>
                <td>{new Date(p.issuedate).toLocaleDateString()}</td>
                <td>{p.expirydate ? new Date(p.expirydate).toLocaleDateString() : '-'}</td>
                <td><span className={`badge badge-${p.isactive ? 'success' : 'secondary'}`}>{p.isactive ? 'Vigente' : 'Vencida'}</span></td>
                <td><button className="btn-sm" onClick={() => viewDetail(p.id)}>Ver</button></td>
              </tr>
            ))}
            {prescriptions.length === 0 && <tr><td colSpan={8} className="empty">No hay recetas</td></tr>}
          </tbody>
        </table>
      </div>

      {detail && (
        <div className="modal-overlay" onClick={() => setDetail(null)}>
          <div className="modal modal-lg" onClick={e => e.stopPropagation()}>
            <h2>Receta - {detail.patient_name}</h2>
            <div className="detail-grid">
              <div><strong>Médico:</strong> {detail.doctor_name} ({detail.license_number})</div>
              <div><strong>Diagnóstico:</strong> {detail.diagnosis}</div>
              <div><strong>Emisión:</strong> {new Date(detail.issuedate).toLocaleDateString()}</div>
              <div><strong>Vence:</strong> {detail.expirydate ? new Date(detail.expirydate).toLocaleDateString() : 'No especificado'}</div>
            </div>
            <h3 style={{marginTop:'1rem'}}>Medicamentos</h3>
            <table className="table">
              <thead>
                <tr><th>Producto</th><th>Dosis</th><th>Frecuencia</th><th>Duración</th><th>Cantidad</th></tr>
              </thead>
              <tbody>
                {(detail.items || []).map((i: any) => (
                  <tr key={i.id}>
                    <td>{i.product_name}</td>
                    <td>{i.dosage}</td>
                    <td>{i.frequency}</td>
                    <td>{i.duration}</td>
                    <td>{i.quantity}</td>
                  </tr>
                ))}
              </tbody>
            </table>
            {detail.notes && <p style={{marginTop:'1rem'}}><strong>Notas:</strong> {detail.notes}</p>}
            <div className="modal-actions">
              <button className="btn-secondary" onClick={() => setDetail(null)}>Cerrar</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
