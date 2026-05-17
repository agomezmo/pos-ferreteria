import { useState, useEffect, FormEvent } from 'react';
import { customersApi } from '../services/api';

export default function Customers() {
  const [customers, setCustomers] = useState<any[]>([]);
  const [search, setSearch] = useState('');
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [editId, setEditId] = useState<number | null>(null);
  const [form, setForm] = useState<any>({});

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

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Clientes</h1>
        <button className="btn-primary" onClick={openCreate}>+ Nuevo Cliente</button>
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
                </td>
              </tr>
            ))}
            {customers.length === 0 && <tr><td colSpan={6} className="empty">No hay clientes</td></tr>}
          </tbody>
        </table>
      </div>

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
    </div>
  );
}
