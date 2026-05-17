import { useState, useEffect, FormEvent } from 'react';
import { suppliersApi } from '../services/api';

export default function Suppliers() {
  const [suppliers, setSuppliers] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [editId, setEditId] = useState<number | null>(null);
  const [form, setForm] = useState({ name: '', contactname: '', phone: '', email: '', address: '' });

  const fetchSuppliers = async () => {
    try {
      const res = await suppliersApi.getAll();
      setSuppliers(res.data || []);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchSuppliers(); }, []);

  const openCreate = () => {
    setEditId(null);
    setForm({ name: '', contactname: '', phone: '', email: '', address: '' });
    setShowModal(true);
  };

  const openEdit = (s: any) => {
    setEditId(s.id);
    setForm({ name: s.name, contactname: s.contactname || '', phone: s.phone || '', email: s.email || '', address: s.address || '' });
    setShowModal(true);
  };

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    try {
      if (editId) {
        await suppliersApi.update(editId, form);
      } else {
        await suppliersApi.create(form);
      }
      setShowModal(false);
      fetchSuppliers();
    } catch (err: any) {
      alert(err.response?.data?.error?.message || 'Error al guardar');
    }
  };

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Proveedores</h1>
        <button className="btn-primary" onClick={openCreate}>+ Nuevo Proveedor</button>
      </div>
      <div className="table-container">
        <table className="table">
          <thead>
            <tr><th>Nombre</th><th>Contacto</th><th>Teléfono</th><th>Email</th><th>Dirección</th><th>Acciones</th></tr>
          </thead>
          <tbody>
            {suppliers.map(s => (
              <tr key={s.id}>
                <td>{s.name}</td>
                <td>{s.contactname || '-'}</td>
                <td>{s.phone || '-'}</td>
                <td>{s.email || '-'}</td>
                <td>{s.address || '-'}</td>
                <td className="actions">
                  <button className="btn-sm" onClick={() => openEdit(s)}>Editar</button>
                </td>
              </tr>
            ))}
            {suppliers.length === 0 && <tr><td colSpan={6} className="empty">No hay proveedores</td></tr>}
          </tbody>
        </table>
      </div>

      {showModal && (
        <div className="modal-overlay" onClick={() => setShowModal(false)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>{editId ? 'Editar Proveedor' : 'Nuevo Proveedor'}</h2>
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label>Nombre *</label>
                <input value={form.name} onChange={e => setForm({...form, name: e.target.value})} required />
              </div>
              <div className="form-group">
                <label>Nombre Contacto</label>
                <input value={form.contactname} onChange={e => setForm({...form, contactname: e.target.value})} />
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
              <div className="modal-actions">
                <button type="button" className="btn-secondary" onClick={() => setShowModal(false)}>Cancelar</button>
                <button type="submit" className="btn-primary">{editId ? 'Guardar Cambios' : 'Crear Proveedor'}</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
