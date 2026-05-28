import { useState, useEffect, FormEvent } from 'react';
import { patientsApi, customersApi } from '../services/api';
import CsvImportModal from '../components/CsvImportModal';

export default function Patients() {
  const [patients, setPatients] = useState<any[]>([]);
  const [customers, setCustomers] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [editId, setEditId] = useState<number | null>(null);
  const [form, setForm] = useState({ customerid: '', fullname: '', dateofbirth: '', phone: '', email: '', address: '', bloodtype: '', allergies: '', medicalnotes: '' });
  const [error, setError] = useState('');
  const [deleteConfirm, setDeleteConfirm] = useState<number | null>(null);
  const [showImport, setShowImport] = useState(false);

  const handleCsvImport = async (rows: Record<string, string>[]) => {
    let success = 0;
    const errors: { row: number; message: string }[] = [];
    for (let i = 0; i < rows.length; i++) {
      const r = rows[i];
      try {
        await patientsApi.create({
          fullname: r.full_name || r.fullname || r.name || '',
          dateofbirth: r.date_of_birth || r.dateofbirth || '',
          phone: r.phone || '',
          email: r.email || '',
          address: r.address || '',
          bloodtype: r.blood_type || r.bloodtype || '',
          allergies: r.allergies || '',
          medicalnotes: r.medical_notes || r.medicalnotes || '',
          customerid: r.customer_id || r.customerid || null,
        });
        success++;
      } catch (err: any) {
        errors.push({ row: i + 2, message: err.response?.data?.error?.message || err.message || 'Error' });
      }
    }
    return { success, errors };
  };

  const fetchPatients = async () => {
    try {
      const [patRes, custRes] = await Promise.all([
        patientsApi.getAll().catch(() => ({ data: [] })),
        customersApi.getAll().catch(() => ({ data: [] })),
      ]);
      setPatients(Array.isArray(patRes.data) ? patRes.data : []);
      setCustomers(Array.isArray(custRes.data) ? custRes.data : []);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchPatients(); }, []);

  const openCreate = () => {
    setEditId(null);
    setForm({ customerid: '', fullname: '', dateofbirth: '', phone: '', email: '', address: '', bloodtype: '', allergies: '', medicalnotes: '' });
    setShowModal(true);
  };

  const openEdit = (p: any) => {
    setEditId(p.id);
    setForm({ customerid: p.customerid || '', fullname: p.fullname, dateofbirth: p.dateofbirth?.split('T')[0] || '', phone: p.phone || '', email: p.email || '', address: p.address || '', bloodtype: p.bloodtype || '', allergies: p.allergies || '', medicalnotes: p.medicalnotes || '' });
    setShowModal(true);
  };

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setError('');
    try {
      if (editId) {
        await patientsApi.update(editId, form);
      } else {
        await patientsApi.create(form);
      }
      setShowModal(false);
      fetchPatients();
    } catch (err: any) {
      setError(err.response?.data?.error?.message || 'Error al guardar');
    }
  };

  const getAge = (dob: string) => {
    if (!dob) return '-';
    const birth = new Date(dob);
    const today = new Date();
    let age = today.getFullYear() - birth.getFullYear();
    const m = today.getMonth() - birth.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birth.getDate())) age--;
    return age;
  };

  const handleDelete = async (id: number) => {
    try {
      await patientsApi.delete(id);
      setDeleteConfirm(null);
      fetchPatients();
    } catch (err: any) {
      alert(err.response?.data?.error?.message || 'Error al eliminar');
      setDeleteConfirm(null);
    }
  };

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Pacientes</h1>
        <div style={{ display: 'flex', gap: '0.5rem' }}>
          <button className="btn-secondary" onClick={() => setShowImport(true)}>📥 Importar CSV</button>
          <button className="btn-primary" onClick={openCreate}>+ Nuevo Paciente</button>
        </div>
      </div>
      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th>Nombre</th><th>Edad</th><th>Teléfono</th><th>Tipo Sangre</th><th>Alergias</th><th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {patients.map(p => (
              <tr key={p.id}>
                <td><strong>{p.fullname}</strong></td>
                <td>{getAge(p.dateofbirth)} años</td>
                <td>{p.phone || '-'}</td>
                <td><span className="badge badge-info">{p.bloodtype || '-'}</span></td>
                <td>{p.allergies || '-'}</td>
                <td className="actions">
                  <button className="btn-sm" onClick={() => openEdit(p)}>Editar</button>
                  <button className="btn-sm btn-danger" onClick={() => setDeleteConfirm(p.id)}>Eliminar</button>
                </td>
              </tr>
            ))}
            {patients.length === 0 && <tr><td colSpan={6} className="empty">No hay pacientes</td></tr>}
          </tbody>
        </table>
      </div>

      {deleteConfirm && (
        <div className="modal-overlay" onClick={() => setDeleteConfirm(null)}>
          <div className="modal modal-sm" onClick={e => e.stopPropagation()}>
            <h2>Confirmar Eliminación</h2>
            <p>¿Eliminar este paciente? Esta acción no se puede deshacer.</p>
            <div className="modal-actions">
              <button className="btn-secondary" onClick={() => setDeleteConfirm(null)}>Cancelar</button>
              <button className="btn-danger" onClick={() => handleDelete(deleteConfirm)}>Eliminar</button>
            </div>
          </div>
        </div>
      )}

      {showModal && (
        <div className="modal-overlay" onClick={() => setShowModal(false)}>
          <div className="modal modal-lg" onClick={e => e.stopPropagation()}>
            <h2>{editId ? 'Editar Paciente' : 'Nuevo Paciente'}</h2>
            {error && <div className="error-message">{error}</div>}
            <form onSubmit={handleSubmit}>
              <div className="form-row">
                <div className="form-group">
                  <label>Cliente Asociado</label>
                  <select value={form.customerid} onChange={e => setForm({...form, customerid: e.target.value})}>
                    <option value="">Sin asociación</option>
                    {customers.map(c => <option key={c.id} value={c.id}>{c.fullName ?? c.fullname}</option>)}
                  </select>
                </div>
                <div className="form-group">
                  <label>Nombre Completo *</label>
                  <input value={form.fullname} onChange={e => setForm({...form, fullname: e.target.value})} required />
                </div>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Fecha de Nacimiento</label>
                  <input type="date" value={form.dateofbirth} onChange={e => setForm({...form, dateofbirth: e.target.value})} />
                </div>
                <div className="form-group">
                  <label>Tipo de Sangre</label>
                  <select value={form.bloodtype} onChange={e => setForm({...form, bloodtype: e.target.value})}>
                    <option value="">Seleccionar</option>
                    <option value="A+">A+</option><option value="A-">A-</option>
                    <option value="B+">B+</option><option value="B-">B-</option>
                    <option value="AB+">AB+</option><option value="AB-">AB-</option>
                    <option value="O+">O+</option><option value="O-">O-</option>
                  </select>
                </div>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Teléfono</label>
                  <input value={form.phone} onChange={e => setForm({...form, phone: e.target.value})} />
                </div>
                <div className="form-group">
                  <label>Email</label>
                  <input type="email" value={form.email} onChange={e => setForm({...form, email: e.target.value})} />
                </div>
              </div>
              <div className="form-group">
                <label>Dirección</label>
                <input value={form.address} onChange={e => setForm({...form, address: e.target.value})} />
              </div>
              <div className="form-group">
                <label>Alergias</label>
                <textarea value={form.allergies} onChange={e => setForm({...form, allergies: e.target.value})} placeholder="Lista de alergias conocidas..." />
              </div>
              <div className="form-group">
                <label>Notas Médicas</label>
                <textarea value={form.medicalnotes} onChange={e => setForm({...form, medicalnotes: e.target.value})} />
              </div>
              <div className="modal-actions">
                <button className="btn-secondary" onClick={() => setShowModal(false)}>Cancelar</button>
                <button className="btn-primary">{editId ? 'Guardar' : 'Crear Paciente'}</button>
              </div>
            </form>
          </div>
        </div>
      )}
      <CsvImportModal
        show={showImport}
        onClose={() => { setShowImport(false); fetchPatients(); }}
        title="Pacientes"
        sampleCsv="full_name,date_of_birth,phone,email,address,blood_type,allergies,medical_notes
Ana Martinez,1990-05-15,555-1111,ana@email.com,Calle Salud 123,O+,Ninguna,
Pedro Sanchez,1985-08-22,555-2222,pedro@email.com,Avenida Bienestar 456,A-,Penicilina,Diabetes tipo 2
Rosa Jimenez,1975-12-03,555-3333,rosa@email.com,Boulevard Vida 789,AB+,Sulfa,Hipertension arterial"
        onImport={handleCsvImport}
      />
    </div>
  );
}
