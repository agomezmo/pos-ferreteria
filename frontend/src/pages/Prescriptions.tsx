import { useState, useEffect } from 'react';
import { prescriptionsApi } from '../services/api';
import CsvImportModal from '../components/CsvImportModal';

export default function Prescriptions() {
  const [prescriptions, setPrescriptions] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [detail, setDetail] = useState<any>(null);
  const [showImport, setShowImport] = useState(false);

  const handleCsvImport = async (rows: Record<string, string>[]) => {
    let success = 0;
    const errors: { row: number; message: string }[] = [];
    for (let i = 0; i < rows.length; i++) {
      const r = rows[i];
      try {
        await prescriptionsApi.create({
          patient_name: r.patient_name || r.patientname || '',
          doctor_name: r.doctor_name || r.doctorname || '',
          license_number: r.license_number || r.licensenumber || '',
          diagnosis: r.diagnosis || '',
          issuedate: r.issue_date || r.issuedate || new Date().toISOString().split('T')[0],
          expirydate: r.expiry_date || r.expirydate || '',
          notes: r.notes || '',
          items: r.medications ? r.medications.split(';').map((m: string) => {
            const parts = m.split('|');
            return { product_name: parts[0] || '', dosage: parts[1] || '', frequency: parts[2] || '', duration: parts[3] || '', quantity: parseInt(parts[4]) || 1 };
          }) : [],
        });
        success++;
      } catch (err: any) {
        errors.push({ row: i + 2, message: err.response?.data?.error?.message || err.message || 'Error' });
      }
    }
    return { success, errors };
  };

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
        <button className="btn-secondary" onClick={() => setShowImport(true)}>📥 Importar CSV</button>
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
      <CsvImportModal
        show={showImport}
        onClose={() => { setShowImport(false); fetchPrescriptions(); }}
        title="Recetas"
        sampleCsv="patient_name,doctor_name,license_number,diagnosis,issue_date,expiry_date,notes,medications
Ana Martinez,Dr. Lopez,MED12345,Hipertension arterial,2026-05-01,2026-08-01,Tomar con alimentos,Enalapril 10mg|1 cada 12h|Cada 12 horas|30 dias|60
Pedro Sanchez,Dr. Garcia,MED67890,Infeccion respiratoria,2026-05-15,2026-06-15,,Amoxicilina 500mg|1 cada 8h|Cada 8 horas|7 dias|21;Ibuprofeno 400mg|1 cada 12h|Cada 12 horas|5 dias|10
Rosa Jimenez,Dr. Hernandez,MED11111,Diabetes tipo 2,2026-05-20,2026-11-20,No suspender tratamiento,Metformina 850mg|1 cada 12h|Cada 12 horas|60 dias|120"
        onImport={handleCsvImport}
      />
    </div>
  );
}
