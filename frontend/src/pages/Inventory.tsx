import { useState, useEffect, FormEvent } from 'react';
import { inventoryApi, productsApi } from '../services/api';

export default function Inventory() {
  const [movements, setMovements] = useState<any[]>([]);
  const [products, setProducts] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [filterProduct, setFilterProduct] = useState('');
  const [filterType, setFilterType] = useState('');
  const [form, setForm] = useState({ productid: '', type: 'in', quantity: 1, reason: '', reference: '' });
  const [error, setError] = useState('');

  const fetchMovements = async () => {
    try {
      const params: any = {};
      if (filterProduct) params.productId = filterProduct;
      if (filterType) params.type = filterType;
      const [movRes, prodRes] = await Promise.all([
        inventoryApi.getAll(params),
        productsApi.getAll({ limit: 500 }),
      ]);
      setMovements(movRes.data?.movements || []);
      setProducts(prodRes.data?.products || []);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchMovements(); }, [filterProduct, filterType]);

  const openCreate = () => {
    setForm({ productid: '', type: 'in', quantity: 1, reason: '', reference: '' });
    setShowModal(true);
  };

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setError('');
    try {
      await inventoryApi.create(form);
      setShowModal(false);
      fetchMovements();
    } catch (err: any) {
      setError(err.response?.data?.error?.message || 'Error al registrar movimiento');
    }
  };

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Movimientos de Inventario</h1>
        <button className="btn-primary" onClick={openCreate}>+ Nuevo Movimiento</button>
      </div>

      <div className="search-bar">
        <select value={filterProduct} onChange={e => setFilterProduct(e.target.value)}>
          <option value="">Todos los productos</option>
          {products.map(p => <option key={p.id} value={p.id}>{p.name}</option>)}
        </select>
        <select value={filterType} onChange={e => setFilterType(e.target.value)}>
          <option value="">Todos los tipos</option>
          <option value="in">Entrada</option>
          <option value="out">Salida</option>
        </select>
      </div>

      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th>Producto</th><th>Tipo</th><th>Cantidad</th><th>Motivo</th><th>Referencia</th><th>Usuario</th><th>Fecha</th>
            </tr>
          </thead>
          <tbody>
            {movements.map(m => (
              <tr key={m.id}>
                <td>{m.product_name}</td>
                <td>
                  <span className={`badge badge-${m.type === 'in' ? 'success' : 'danger'}`}>
                    {m.type === 'in' ? 'Entrada' : 'Salida'}
                  </span>
                </td>
                <td><strong>{m.type === 'in' ? '+' : '-'}{m.quantity}</strong></td>
                <td>{m.reason || '-'}</td>
                <td>{m.reference || '-'}</td>
                <td>{m.user_name}</td>
                <td>{new Date(m.createdat).toLocaleString()}</td>
              </tr>
            ))}
            {movements.length === 0 && <tr><td colSpan={7} className="empty">No hay movimientos</td></tr>}
          </tbody>
        </table>
      </div>

      {showModal && (
        <div className="modal-overlay" onClick={() => setShowModal(false)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>Nuevo Movimiento</h2>
            {error && <div className="error-message">{error}</div>}
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label>Producto *</label>
                <select value={form.productid} onChange={e => setForm({...form, productid: e.target.value})} required>
                  <option value="">Seleccionar...</option>
                  {products.map(p => <option key={p.id} value={p.id}>{p.name} (Stock: {p.stock})</option>)}
                </select>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Tipo *</label>
                  <select value={form.type} onChange={e => setForm({...form, type: e.target.value})}>
                    <option value="in">Entrada</option>
                    <option value="out">Salida</option>
                  </select>
                </div>
                <div className="form-group">
                  <label>Cantidad *</label>
                  <input type="number" min="1" value={form.quantity} onChange={e => setForm({...form, quantity: parseInt(e.target.value) || 1})} required />
                </div>
              </div>
              <div className="form-group">
                <label>Motivo</label>
                <input value={form.reason} onChange={e => setForm({...form, reason: e.target.value})} placeholder="Compra, ajuste, merma, etc." />
              </div>
              <div className="form-group">
                <label>Referencia</label>
                <input value={form.reference} onChange={e => setForm({...form, reference: e.target.value})} placeholder="Factura, orden, etc." />
              </div>
              <div className="modal-actions">
                <button className="btn-secondary" onClick={() => setShowModal(false)}>Cancelar</button>
                <button className="btn-primary">Registrar Movimiento</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
