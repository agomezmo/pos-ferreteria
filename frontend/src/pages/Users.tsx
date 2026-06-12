import { useState, useEffect, FormEvent } from 'react';
import { usersApi } from '../services/api';

export default function Users() {
  const [users, setUsers] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [editId, setEditId] = useState<number | null>(null);
  const [form, setForm] = useState({ username: '', password: '', email: '', fullname: '', roleid: '2', isactive: true });
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const [deleteConfirm, setDeleteConfirm] = useState<number | null>(null);

  const normalizeUser = (u: any) => ({
    id: u.id,
    username: u.username || u.Username,
    email: u.email || u.Email,
    fullname: u.fullname || u.fullName || u.FullName,
    role: u.role || u.roleName || u.RoleName || u.Role,
    roleid: u.roleid ?? u.roleId ?? u.RoleId ?? '',
    isactive: u.isactive ?? u.isActive ?? u.IsActive ?? true,
    lastlogin: u.lastlogin || u.lastLogin || u.LastLogin,
    createdat: u.createdat || u.createdAt || u.CreatedAt,
  });

  const fetchUsers = async () => {
    try {
      const res = await usersApi.getAll();
      const data = Array.isArray(res.data) ? res.data : [];
      setUsers(data.map(normalizeUser));
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchUsers(); }, []);

  const openCreate = () => {
    setEditId(null);
    setForm({ username: '', password: '', email: '', fullname: '', roleid: '2', isactive: true });
    setShowModal(true);
  };

  const openEdit = (u: any) => {
    setEditId(u.id);
    setForm({
      username: u.username || '',
      password: '',
      email: u.email || '',
      fullname: u.fullname || '',
      roleid: String(u.roleid || '2'),
      isactive: u.isactive !== false,
    });
    setShowModal(true);
  };

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setError('');
    setSuccess('');
    try {
      if (editId) {
        await usersApi.update(editId, {
          email: form.email,
          fullname: form.fullname,
          roleid: parseInt(form.roleid),
          isactive: form.isactive,
        });
        setSuccess('Usuario actualizado correctamente');
      } else {
        await usersApi.create(form);
        setSuccess('Usuario creado correctamente');
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

  const handleDelete = async (id: number) => {
    try {
      await usersApi.delete(id);
      setDeleteConfirm(null);
      setSuccess('Usuario desactivado');
      fetchUsers();
    } catch (err: any) {
      alert(err.response?.data?.error?.message || 'Error al eliminar');
      setDeleteConfirm(null);
    }
  };

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Usuarios / Cuentas</h1>
        <button className="btn-primary" onClick={openCreate}>+ Nuevo Usuario</button>
      </div>

      {success && <div className="success-message">{success}</div>}

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
                <td>{u.fullname || '-'}</td>
                <td>{u.email || '-'}</td>
                <td>{getRoleBadge(u.role)}</td>
                <td>{u.isactive ? '✅' : '❌'}</td>
                <td>{u.lastlogin ? new Date(u.lastlogin).toLocaleString() : 'Nunca'}</td>
                <td className="actions">
                  <button className="btn-sm" onClick={() => openEdit(u)}>Editar</button>
                  <button className="btn-sm btn-danger" onClick={() => setDeleteConfirm(u.id)}>
                    {u.isactive ? 'Desactivar' : 'Activar'}
                  </button>
                </td>
              </tr>
            ))}
            {users.length === 0 && <tr><td colSpan={7} className="empty">No hay usuarios</td></tr>}
          </tbody>
        </table>
      </div>

      {deleteConfirm && (
        <div className="modal-overlay" onClick={() => setDeleteConfirm(null)}>
          <div className="modal modal-sm" onClick={e => e.stopPropagation()}>
            <h2>Confirmar</h2>
            <p>¿Desactivar este usuario? Podrás reactivarlo después.</p>
            <div className="modal-actions">
              <button className="btn-secondary" onClick={() => setDeleteConfirm(null)}>Cancelar</button>
              <button className="btn-danger" onClick={() => handleDelete(deleteConfirm)}>Desactivar</button>
            </div>
          </div>
        </div>
      )}

      {showModal && (
        <div className="modal-overlay" onClick={() => { if (!success) setShowModal(false); }}>
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
              {editId && (
                <div className="form-group">
                  <label>Estado</label>
                  <select value={form.isactive ? 'true' : 'false'} onChange={e => setForm({...form, isactive: e.target.value === 'true'})}>
                    <option value="true">Activo</option>
                    <option value="false">Inactivo</option>
                  </select>
                </div>
              )}
              <div className="modal-actions">
                <button type="button" className="btn-secondary" onClick={() => setShowModal(false)}>Cancelar</button>
                <button type="submit" className="btn-primary">{editId ? 'Guardar Cambios' : 'Crear Usuario'}</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
