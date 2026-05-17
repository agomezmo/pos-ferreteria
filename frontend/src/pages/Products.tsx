import { useState, useEffect, FormEvent } from 'react';
import { productsApi, categoriesApi, suppliersApi } from '../services/api';

interface Product {
  id: number; code: string; barcode: string; name: string; description: string;
  categoryid: number; category_name: string; supplierid: number; supplier_name: string;
  purchaseprice: number; saleprice: number; stock: number; minstock: number;
  unit: string; isactive: boolean; requiresprescription: boolean;
  wholesale_price: number; expiry_date: string; requires_tax: boolean;
}
interface Category { id: number; name: string; }
interface Supplier { id: number; name: string; }

export default function Products() {
  const [products, setProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [suppliers, setSuppliers] = useState<Supplier[]>([]);
  const [search, setSearch] = useState('');
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [editId, setEditId] = useState<number | null>(null);
  const [form, setForm] = useState<any>({});
  const [error, setError] = useState('');

  const fetchProducts = async () => {
    try {
      const res = await productsApi.getAll({ search, limit: 200 });
      setProducts(res.data.products || []);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => {
    fetchProducts();
    categoriesApi.getAll().then(r => setCategories(r.data)).catch(() => {});
    suppliersApi.getAll().then(r => setSuppliers(r.data)).catch(() => {});
  }, []);

  useEffect(() => { fetchProducts(); }, [search]);

  const openCreate = () => {
    setEditId(null);
    setForm({ name: '', code: '', barcode: '', description: '', categoryid: '', supplierid: '', purchaseprice: 0, saleprice: 0, stock: 0, minstock: 0, unit: 'pza', requiresprescription: false, wholesale_price: 0, expiry_date: '', requires_tax: true });
    setShowModal(true);
  };

  const openEdit = (p: Product) => {
    setEditId(p.id);
    setForm({ ...p, categoryid: p.categoryid || '', supplierid: p.supplierid || '' });
    setShowModal(true);
  };

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setError('');
    try {
      if (editId) {
        await productsApi.update(editId, form);
      } else {
        await productsApi.create(form);
      }
      setShowModal(false);
      fetchProducts();
    } catch (err: any) {
      setError(err.response?.data?.error?.message || 'Error al guardar');
    }
  };

  const handleDelete = async (id: number) => {
    if (!confirm('¿Desactivar este producto?')) return;
    try {
      await productsApi.delete(id);
      fetchProducts();
    } catch (err) { console.error(err); }
  };

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Productos</h1>
        <button className="btn-primary" onClick={openCreate}>+ Nuevo Producto</button>
      </div>
      <div className="search-bar">
        <input type="text" placeholder="Buscar por nombre, código o código de barras..." value={search} onChange={e => setSearch(e.target.value)} />
      </div>
      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th>Código</th><th>Nombre</th><th>Categoría</th><th>Precio Venta</th><th>Stock</th><th>Stock Mín</th><th>Unidad</th><th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {products.map(p => (
              <tr key={p.id} className={p.stock <= p.minstock ? 'row-warning' : ''}>
                <td>{p.code}</td>
                <td>{p.name}</td>
                <td>{p.category_name}</td>
                <td>${Number(p.saleprice).toFixed(2)}</td>
                <td>{p.stock}</td>
                <td>{p.minstock}</td>
                <td>{p.unit}</td>
                <td className="actions">
                  <button className="btn-sm" onClick={() => openEdit(p)}>Editar</button>
                  <button className="btn-sm btn-danger" onClick={() => handleDelete(p.id)}>Desactivar</button>
                </td>
              </tr>
            ))}
            {products.length === 0 && <tr><td colSpan={8} className="empty">No hay productos</td></tr>}
          </tbody>
        </table>
      </div>

      {showModal && (
        <div className="modal-overlay" onClick={() => setShowModal(false)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>{editId ? 'Editar Producto' : 'Nuevo Producto'}</h2>
            {error && <div className="error-message">{error}</div>}
            <form onSubmit={handleSubmit}>
              <div className="form-row">
                <div className="form-group">
                  <label>Código *</label>
                  <input value={form.code || ''} onChange={e => setForm({...form, code: e.target.value})} required />
                </div>
                <div className="form-group">
                  <label>Código de Barras</label>
                  <input value={form.barcode || ''} onChange={e => setForm({...form, barcode: e.target.value})} />
                </div>
              </div>
              <div className="form-group">
                <label>Nombre *</label>
                <input value={form.name || ''} onChange={e => setForm({...form, name: e.target.value})} required />
              </div>
              <div className="form-group">
                <label>Descripción</label>
                <textarea value={form.description || ''} onChange={e => setForm({...form, description: e.target.value})} />
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Categoría *</label>
                  <select value={form.categoryid || ''} onChange={e => setForm({...form, categoryid: e.target.value})} required>
                    <option value="">Seleccionar...</option>
                    {categories.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
                  </select>
                </div>
                <div className="form-group">
                  <label>Proveedor</label>
                  <select value={form.supplierid || ''} onChange={e => setForm({...form, supplierid: e.target.value})}>
                    <option value="">Seleccionar...</option>
                    {suppliers.map(s => <option key={s.id} value={s.id}>{s.name}</option>)}
                  </select>
                </div>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Precio Compra</label>
                  <input type="number" step="0.01" value={form.purchaseprice || 0} onChange={e => setForm({...form, purchaseprice: parseFloat(e.target.value) || 0})} />
                </div>
                <div className="form-group">
                  <label>Precio Venta *</label>
                  <input type="number" step="0.01" value={form.saleprice || 0} onChange={e => setForm({...form, saleprice: parseFloat(e.target.value) || 0})} required />
                </div>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Stock</label>
                  <input type="number" value={form.stock || 0} onChange={e => setForm({...form, stock: parseInt(e.target.value) || 0})} />
                </div>
                <div className="form-group">
                  <label>Stock Mínimo</label>
                  <input type="number" value={form.minstock || 0} onChange={e => setForm({...form, minstock: parseInt(e.target.value) || 0})} />
                </div>
                <div className="form-group">
                  <label>Unidad</label>
                  <input value={form.unit || 'pza'} onChange={e => setForm({...form, unit: e.target.value})} />
                </div>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Precio Mayoreo</label>
                  <input type="number" step="0.01" value={form.wholesale_price || 0} onChange={e => setForm({...form, wholesale_price: parseFloat(e.target.value) || 0})} />
                </div>
                <div className="form-group">
                  <label>Fecha Caducidad</label>
                  <input type="date" value={form.expiry_date || ''} onChange={e => setForm({...form, expiry_date: e.target.value})} />
                </div>
              </div>
              <div className="form-row checkbox-row">
                <label>
                  <input type="checkbox" checked={form.requiresprescription || false} onChange={e => setForm({...form, requiresprescription: e.target.checked})} />
                  Requiere Receta
                </label>
                <label>
                  <input type="checkbox" checked={form.requires_tax ?? true} onChange={e => setForm({...form, requires_tax: e.target.checked})} />
                  Aplica IVA
                </label>
              </div>
              <div className="modal-actions">
                <button type="button" className="btn-secondary" onClick={() => setShowModal(false)}>Cancelar</button>
                <button type="submit" className="btn-primary">{editId ? 'Guardar Cambios' : 'Crear Producto'}</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
