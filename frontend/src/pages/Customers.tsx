import { useState, useEffect, FormEvent } from 'react';
import { customersApi } from '../services/api';
import CsvImportModal from '../components/CsvImportModal';

export default function Customers() {
  const [customers, setCustomers] = useState<any[]>([]);
  const [search, setSearch] = useState('');
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [editId, setEditId] = useState<number | null>(null);
  const [form, setForm] = useState<any>({});
  const [deleteConfirm, setDeleteConfirm] = useState<number | null>(null);
  const [showImport, setShowImport] = useState(false);

  const handleCsvImport = async (rows: Record<string, string>[]) => {
    let success = 0;
    const errors: { row: number; message: string }[] = [];
    for (let i = 0; i < rows.length; i++) {
      const r = rows[i];
      try {
        await customersApi.create({
          documenttype: r.document_type || r.documenttype || 'INE',
          documentnumber: r.document_number || r.documentnumber || '',
          fullname: r.full_name || r.fullname || r.name || '',
          phone: r.phone || '',
          email: r.email || '',
          address: r.address || '',
          rfc: r.rfc || '',
          razonsocial: r.razonsocial || r.business_name || '',
          codigopostal: r.codigopostal || r.zip || '',
          regimenfiscalid: r.regimenfiscalid || r.tax_regime || '',
          usocfdiid: r.usocfdiid || r.cfdi_usage || '',
        });
        success++;
      } catch (err: any) {
        errors.push({ row: i + 2, message: err.response?.data?.error?.message || err.message || 'Error' });
      }
    }
    return { success, errors };
  };

  const fetchCustomers = async () => {
    try {
      const params: any = {};
      if (search) params.search = search;
      const res = await customersApi.getAll(params);
      setCustomers(res.data || []);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchCustomers(); }, []);

  useEffect(() => { fetchCustomers(); }, [search]);

  const openCreate = () => {
    setEditId(null);
    setForm({ documenttype: 'INE', documentnumber: '', fullname: '', phone: '', email: '', address: '', rfc: '', razonsocial: '', codigopostal: '', regimenfiscalid: '', usocfdiid: '' });
    setShowModal(true);
  };

  const openEdit = (c: any) => {
    setEditId(c.id);
    setForm({ ...c });
    setShowModal(true);
  };

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    try {
      if (editId) {
        await customersApi.update(editId, form);
      } else {
        await customersApi.create(form);
      }
      setShowModal(false);
      fetchCustomers();
    } catch (err: any) {
      alert(err.response?.data?.error?.message || 'Error al guardar');
    }
  };

  const handleDelete = async (id: number) => {
    try {
      await customersApi.delete(id);
      setDeleteConfirm(null);
      fetchCustomers();
    } catch (err: any) {
      alert(err.response?.data?.error?.message || 'Error al eliminar');
      setDeleteConfirm(null);
    }
  };

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Clientes</h1>
        <div style={{ display: 'flex', gap: '0.5rem' }}>
          <button className="btn-secondary" onClick={() => setShowImport(true)}>📥 Importar CSV</button>
          <button className="btn-primary" onClick={openCreate}>+ Nuevo Cliente</button>
        </div>
      </div>
      <div className="search-bar">
        <input type="text" placeholder="Buscar por nombre, RFC o documento..." value={search} onChange={e => setSearch(e.target.value)} />
      </div>
      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th>Nombre</th><th>Documento</th><th>RFC</th><th>Teléfono</th><th>Email</th><th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {customers.map(c => (
              <tr key={c.id}>
                <td>{c.fullname}</td>
                <td>{c.documenttype} {c.documentnumber}</td>
                <td>{c.rfc || '-'}</td>
                <td>{c.phone || '-'}</td>
                <td>{c.email || '-'}</td>
                <td className="actions">
                  <button className="btn-sm" onClick={() => openEdit(c)}>Editar</button>
                  <button className="btn-sm btn-danger" onClick={() => setDeleteConfirm(c.id)}>Eliminar</button>
                </td>
              </tr>
            ))}
            {customers.length === 0 && <tr><td colSpan={6} className="empty">No hay clientes</td></tr>}
          </tbody>
        </table>
      </div>

      {deleteConfirm && (
        <div className="modal-overlay" onClick={() => setDeleteConfirm(null)}>
          <div className="modal modal-sm" onClick={e => e.stopPropagation()}>
            <h2>Confirmar Eliminación</h2>
            <p>¿Eliminar este cliente? Esta acción no se puede deshacer.</p>
            <div className="modal-actions">
              <button className="btn-secondary" onClick={() => setDeleteConfirm(null)}>Cancelar</button>
              <button className="btn-danger" onClick={() => handleDelete(deleteConfirm)}>Eliminar</button>
            </div>
          </div>
        </div>
      )}

      {showModal && (
        <div className="modal-overlay" onClick={() => setShowModal(false)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>{editId ? 'Editar Cliente' : 'Nuevo Cliente'}</h2>
            <form onSubmit={handleSubmit}>
              <div className="form-row">
                <div className="form-group">
                  <label>Tipo Documento *</label>
                  <select value={form.documenttype || 'INE'} onChange={e => setForm({...form, documenttype: e.target.value})} required>
                    <option value="INE">INE</option>
                    <option value="Pasaporte">Pasaporte</option>
                    <option value="Cédula">Cédula</option>
                  </select>
                </div>
                <div className="form-group">
                  <label>Núm. Documento *</label>
                  <input value={form.documentnumber || ''} onChange={e => setForm({...form, documentnumber: e.target.value})} required />
                </div>
              </div>
              <div className="form-group">
                <label>Nombre Completo *</label>
                <input value={form.fullname || ''} onChange={e => setForm({...form, fullname: e.target.value})} required />
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Teléfono</label>
                  <input value={form.phone || ''} onChange={e => setForm({...form, phone: e.target.value})} />
                </div>
                <div className="form-group">
                  <label>Email</label>
                  <input type="email" value={form.email || ''} onChange={e => setForm({...form, email: e.target.value})} />
                </div>
              </div>
              <div className="form-group">
                <label>Dirección</label>
                <input value={form.address || ''} onChange={e => setForm({...form, address: e.target.value})} />
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>RFC</label>
                  <input value={form.rfc || ''} onChange={e => setForm({...form, rfc: e.target.value})} />
                </div>
                <div className="form-group">
                  <label>Razón Social</label>
                  <input value={form.razonsocial || ''} onChange={e => setForm({...form, razonsocial: e.target.value})} />
                </div>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Código Postal</label>
                  <input value={form.codigopostal || ''} onChange={e => setForm({...form, codigopostal: e.target.value})} />
                </div>
                <div className="form-group">
                  <label>Régimen Fiscal</label>
                  <input value={form.regimenfiscalid || ''} onChange={e => setForm({...form, regimenfiscalid: e.target.value})} />
                </div>
                <div className="form-group">
                  <label>Uso CFDI</label>
                  <input value={form.usocfdiid || ''} onChange={e => setForm({...form, usocfdiid: e.target.value})} />
                </div>
              </div>
              <div className="modal-actions">
                <button type="button" className="btn-secondary" onClick={() => setShowModal(false)}>Cancelar</button>
                <button type="submit" className="btn-primary">{editId ? 'Guardar Cambios' : 'Crear Cliente'}</button>
              </div>
            </form>
          </div>
        </div>
      )}
      <CsvImportModal
        show={showImport}
        onClose={() => { setShowImport(false); fetchCustomers(); }}
        title="Clientes"
        sampleCsv="full_name,document_type,document_number,phone,email,address,rfc,razonsocial,zip
Juan Perez,INE,JUANP950101HDF,555-1234,juan@email.com,Calle Principal 123,JUAP950101XXX,
Maria Garcia,Pasaporte,MG123456,555-5678,maria@email.com,Avenida Central 456,MAGA850101XXX,Maria Garcia SA de CV,77000
Carlos Lopez,INE,CARL920202MDF,555-9012,carlos@email.com,Boulevard Norte 789,,,"
        onImport={handleCsvImport}
      />
    </div>
  );
}
