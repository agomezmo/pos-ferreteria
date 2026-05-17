import { useState, useEffect, FormEvent } from 'react';
import { usersApi } from '../services/api';

export default function Users() {
  const [users, setUsers] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [editId, setEditId] = useState<number | null>(null);
  const [form, setForm] = useState({ username: '', password: '', email: '', fullname: '', roleid: '' });
  const [error, setError] = useState('');

  const fetchUsers = async () => {
    try {
      const res = await usersApi.getAll();
      setUsers(Array.isArray(res.data) ? res.data : []);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchUsers(); }, []);

  const openCreate = () => {
    setEditId(null);
    setForm({ username: '', password: '', email: '', fullname: '', roleid: '2' });
    setShowModal(true);
  };

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setError('');
    try {
      if (editId) {
        await usersApi.update(editId, { email: form.email, fullname: form.fullname, roleid: form.roleid });
      } else {
        await usersApi.create(form);
      }
      setShowModal(false);
      fetchUsers();
    } catch (err: any) {
      setError(err.response?.data?.error?.message || 'Error al guardar');
    }
  };

  const getRoleBadge = (role: string) => {
    const colors: any = { Admin: 'badge-danger', 'Cajero': 'badge-info', 'Farmacéutico': 'badge-success' };
    return <span className={`badge ${colors[role] || 'badge-secondary'}`}>{role}</span>;
  };

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Usuarios</h1>
        <button className="btn-primary" onClick={openCreate}>+ Nuevo Usuario</button>
      </div>
      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th>Usuario</th><th>Nombre</th><th>Email</th><th>Rol</th><th>Activo</th><th>Último Acceso</th><th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {users.map(u => (
              <tr key={u.id}>
                <td><strong>{u.username}</strong></td>
                <td>{u.fullname}</td>
                <td>{u.email || '-'}</td>
                <td>{getRoleBadge(u.role)}</td>
                <td>{u.isactive ? '✅' : '❌'}</td>
                <td>{u.lastlogin ? new Date(u.lastlogin).toLocaleString() : 'Nunca'}</td>
                <td className="actions">
                  <button className="btn-sm" onClick={() => {
                    setEditId(u.id);
                    setForm({ username: u.username, password: '', email: u.email || '', fullname: u.fullname, roleid: u.roleid });
                    setShowModal(true);
                  }}>Editar</button>
                </td>
              </tr>
            ))}
            {users.length === 0 && <tr><td colSpan={7} className="empty">No hay usuarios</td></tr>}
          </tbody>
        </table>
      </div>

      {showModal && (
        <div className="modal-overlay" onClick={() => setShowModal(false)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>{editId ? 'Editar Usuario' : 'Nuevo Usuario'}</h2>
            {error && <div className="error-message">{error}</div>}
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label>Usuario *</label>
                <input value={form.username} onChange={e => setForm({...form, username: e.target.value})} required disabled={!!editId} />
              </div>
              {!editId && (
                <div className="form-group">
                  <label>Contraseña *</label>
                  <input type="password" value={form.password} onChange={e => setForm({...form, password: e.target.value})}
                    required={!editId} minLength={6} />
                </div>
              )}
              <div className="form-group">
                <label>Nombre Completo *</label>
                <input value={form.fullname} onChange={e => setForm({...form, fullname: e.target.value})} required />
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Email</label>
                  <input type="email" value={form.email} onChange={e => setForm({...form, email: e.target.value})} />
                </div>
                <div className="form-group">
                  <label>Rol</label>
                  <select value={form.roleid} onChange={e => setForm({...form, roleid: e.target.value})}>
                    <option value="1">Admin</option>
                    <option value="2">Cajero</option>
                    <option value="3">Farmacéutico</option>
                  </select>
                </div>
              </div>
              <div className="modal-actions">
                <button className="btn-secondary" onClick={() => setShowModal(false)}>Cancelar</button>
                <button className="btn-primary">{editId ? 'Guardar Cambios' : 'Crear Usuario'}</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
