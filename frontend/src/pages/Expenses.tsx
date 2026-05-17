import { useState, useEffect, FormEvent } from 'react';
import { expensesApi } from '../services/api';

const EXPENSE_CATEGORIES = ['Renta', 'Servicios', 'Nómina', 'Insumos', 'Mantenimiento', 'Publicidad', 'Impuestos', 'Otros'];

export default function Expenses() {
  const [expenses, setExpenses] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [editId, setEditId] = useState<number | null>(null);
  const [form, setForm] = useState({ description: '', amount: 0, category: 'Otros', paymentmethod: 'Efectivo', reference: '', notes: '' });
  const [error, setError] = useState('');

  const fetchExpenses = async () => {
    try {
      const res = await expensesApi.getAll({ limit: 100 });
      setExpenses(Array.isArray(res.data) ? res.data : []);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchExpenses(); }, []);

  const openCreate = () => {
    setEditId(null);
    setForm({ description: '', amount: 0, category: 'Otros', paymentmethod: 'Efectivo', reference: '', notes: '' });
    setShowModal(true);
  };

  const openEdit = (e: any) => {
    setEditId(e.id);
    setForm({ description: e.description, amount: e.amount, category: e.category, paymentmethod: e.paymentmethod, reference: e.reference || '', notes: e.notes || '' });
    setShowModal(true);
  };

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setError('');
    try {
      if (editId) {
        await expensesApi.update(editId, form);
      } else {
        await expensesApi.create(form);
      }
      setShowModal(false);
      fetchExpenses();
    } catch (err: any) {
      setError(err.response?.data?.error?.message || 'Error al guardar');
    }
  };

  const handleDelete = async (id: number) => {
    if (!confirm('¿Eliminar este gasto?')) return;
    try { await expensesApi.delete(id); fetchExpenses(); }
    catch (err) { console.error(err); }
  };

  const totalExpenses = expenses.reduce((sum, e) => sum + Number(e.amount), 0);

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Gastos</h1>
        <button className="btn-primary" onClick={openCreate}>+ Nuevo Gasto</button>
      </div>

      <div className="dashboard-grid" style={{marginBottom:'1.5rem'}}>
        <div className="dashboard-card">
          <h3>Total Gastos</h3>
          <p className="stat-number">${totalExpenses.toFixed(2)}</p>
        </div>
        <div className="dashboard-card">
          <h3>Registros</h3>
          <p className="stat-number">{expenses.length}</p>
        </div>
      </div>

      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th>Descripción</th><th>Categoría</th><th>Monto</th><th>Método Pago</th><th>Referencia</th><th>Fecha</th><th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {expenses.map(e => (
              <tr key={e.id}>
                <td>{e.description}</td>
                <td><span className="badge badge-info">{e.category}</span></td>
                <td><strong>${Number(e.amount).toFixed(2)}</strong></td>
                <td>{e.paymentmethod}</td>
                <td>{e.reference || '-'}</td>
                <td>{new Date(e.createdat).toLocaleDateString()}</td>
                <td className="actions">
                  <button className="btn-sm" onClick={() => openEdit(e)}>Editar</button>
                  <button className="btn-sm btn-danger" onClick={() => handleDelete(e.id)}>Eliminar</button>
                </td>
              </tr>
            ))}
            {expenses.length === 0 && <tr><td colSpan={7} className="empty">No hay gastos registrados</td></tr>}
          </tbody>
        </table>
      </div>

      {showModal && (
        <div className="modal-overlay" onClick={() => setShowModal(false)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>{editId ? 'Editar Gasto' : 'Nuevo Gasto'}</h2>
            {error && <div className="error-message">{error}</div>}
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label>Descripción *</label>
                <input value={form.description} onChange={e => setForm({...form, description: e.target.value})} required />
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Categoría</label>
                  <select value={form.category} onChange={e => setForm({...form, category: e.target.value})}>
                    {EXPENSE_CATEGORIES.map(c => <option key={c} value={c}>{c}</option>)}
                  </select>
                </div>
                <div className="form-group">
                  <label>Monto *</label>
                  <input type="number" step="0.01" value={form.amount} onChange={e => setForm({...form, amount: parseFloat(e.target.value) || 0})} required />
                </div>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Método Pago</label>
                  <select value={form.paymentmethod} onChange={e => setForm({...form, paymentmethod: e.target.value})}>
                    <option value="Efectivo">Efectivo</option>
                    <option value="Tarjeta">Tarjeta</option>
                    <option value="Transferencia">Transferencia</option>
                  </select>
                </div>
                <div className="form-group">
                  <label>Referencia</label>
                  <input value={form.reference} onChange={e => setForm({...form, reference: e.target.value})} />
                </div>
              </div>
              <div className="form-group">
                <label>Notas</label>
                <textarea value={form.notes} onChange={e => setForm({...form, notes: e.target.value})} />
              </div>
              <div className="modal-actions">
                <button className="btn-secondary" onClick={() => setShowModal(false)}>Cancelar</button>
                <button className="btn-primary">{editId ? 'Guardar' : 'Crear Gasto'}</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
