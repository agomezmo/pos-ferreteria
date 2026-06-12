import { useState, useEffect, useMemo, FormEvent } from 'react';
import { productsApi, categoriesApi, suppliersApi } from '../services/api';
import CsvImportModal from '../components/CsvImportModal';

interface Product {
  id: number; code: string; barcode: string; name: string; description: string;
  categoryid: number; category_name: string; supplierid: number; supplier_name: string;
  purchaseprice: number; saleprice: number; stock: number; minstock: number;
  unit: string; isactive: boolean; requiresprescription: boolean;
  wholesale_price: number; expiry_date: string; requires_tax: boolean;
}
interface Category { id: number; name: string; }
interface Supplier { id: number; name: string; }

const emptyForm = () => ({
  name: '', code: '', barcode: '', description: '', categoryid: '', supplierid: '',
  purchaseprice: 0, saleprice: 0, stock: 0, minstock: 0, unit: 'pza',
  requiresprescription: false, wholesale_price: 0, expiry_date: '', requires_tax: true,
  isactive: true,
});

export default function Products() {
  const [allProducts, setAllProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [suppliers, setSuppliers] = useState<Supplier[]>([]);
  const [search, setSearch] = useState('');
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [editId, setEditId] = useState<number | null>(null);
  const [form, setForm] = useState<any>(emptyForm());
  const [error, setError] = useState('');
  const [showImport, setShowImport] = useState(false);
  const [statusMsg, setStatusMsg] = useState('');

  const handleCsvImport = async (rows: Record<string, string>[]) => {
    let success = 0;
    const errors: { row: number; message: string }[] = [];
    for (let i = 0; i < rows.length; i++) {
      const r = rows[i];
      try {
        await productsApi.create({
          code: r.code, barcode: r.barcode || '', name: r.name,
          description: r.description || '', categoryid: r.category_id || r.categoryid || null,
          supplierid: r.supplier_id || r.supplierid || null,
          purchaseprice: parseFloat(r.purchase_price || r.purchaseprice) || 0,
          saleprice: parseFloat(r.sale_price || r.saleprice) || 0,
          stock: parseInt(r.stock) || 0, minstock: parseInt(r.min_stock || r.minstock) || 0,
          unit: r.unit || 'pza', wholesale_price: parseFloat(r.wholesale_price || '0') || 0,
          expiry_date: r.expiry_date || r.expirydate || '',
          requiresprescription: (r.requires_prescription || '').toUpperCase() === 'TRUE',
          requires_tax: (r.requires_tax || '').toUpperCase() !== 'FALSE',
        });
        success++;
      } catch (err: any) {
        errors.push({ row: i + 2, message: err.response?.data?.error?.message || err.message || 'Error' });
      }
    }
    return { success, errors };
  };

  const products = useMemo(() => {
    if (!search.trim()) return allProducts;
    const q = search.toLowerCase();
    return allProducts.filter(p =>
      p.name.toLowerCase().includes(q) ||
      (p.code && p.code.toLowerCase().includes(q)) ||
      (p.barcode && p.barcode.toLowerCase().includes(q))
    );
  }, [allProducts, search]);

  const normalizeProduct = (p: any): Product => ({
    id: p.id, code: p.code, barcode: p.barcode, name: p.name, description: p.description,
    categoryid: p.categoryid ?? p.categoryId ?? p.CategoryId,
    category_name: p.category_name ?? p.categoryName ?? p.CategoryName,
    supplierid: p.supplierid ?? p.supplierId ?? p.SupplierId,
    supplier_name: p.supplier_name ?? p.supplierName ?? p.SupplierName,
    purchaseprice: p.purchaseprice ?? p.purchasePrice ?? p.PurchasePrice ?? 0,
    saleprice: p.saleprice ?? p.salePrice ?? p.SalePrice ?? 0,
    stock: p.stock ?? p.Stock ?? 0,
    minstock: p.minstock ?? p.minStock ?? p.MinStock ?? 0,
    unit: p.unit ?? p.Unit ?? 'pza',
    isactive: p.isactive ?? p.isActive ?? p.IsActive ?? true,
    requiresprescription: p.requiresprescription ?? false,
    wholesale_price: p.wholesale_price ?? p.wholesalePrice ?? p.WholesalePrice ?? 0,
    expiry_date: p.expiry_date ?? p.expiryDate ?? p.ExpiryDate ?? '',
    requires_tax: p.requires_tax ?? p.requiresTax ?? p.RequiresTax ?? true,
  });

  const fetchProducts = async () => {
    try {
      const res = await productsApi.getAll({ limit: 500 });
      const raw = res.data?.products || res.data || [];
      setAllProducts(Array.isArray(raw) ? raw.map(normalizeProduct) : []);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => {
    fetchProducts();
    categoriesApi.getAll().then(r => setCategories(Array.isArray(r.data) ? r.data : [])).catch(() => {});
    suppliersApi.getAll().then(r => setSuppliers(Array.isArray(r.data) ? r.data : [])).catch(() => {});
  }, []);

  const openCreate = () => {
    setEditId(null);
    setForm(emptyForm());
    setShowModal(true);
  };

  const openEdit = (p: Product) => {
    setEditId(p.id);
    setForm({
      ...p,
      categoryid: p.categoryid || '',
      supplierid: p.supplierid || '',
    });
    setShowModal(true);
  };

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setError('');
    setStatusMsg('');
    try {
      if (editId) {
        await productsApi.update(editId, form);
        setStatusMsg('Producto actualizado correctamente');
      } else {
        await productsApi.create(form);
        setStatusMsg('Producto creado correctamente');
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
    <div className="page" style={{ height: 'calc(100vh - 80px)', display: 'flex', flexDirection: 'column', maxWidth: '100%' }}>
      <div className="page-header" style={{ flexShrink: 0 }}>
        <h1>Productos</h1>
        <div style={{ display: 'flex', gap: '0.5rem' }}>
          <button className="btn-secondary" onClick={() => setShowImport(true)}>📥 Importar CSV</button>
          <button className="btn-primary" onClick={openCreate}>+ Nuevo Producto</button>
        </div>
      </div>

      <div className="search-bar" style={{ flexShrink: 0 }}>
        <input type="text" placeholder="Buscar por nombre, código o código de barras..." value={search} onChange={e => setSearch(e.target.value)} />
        <span style={{ fontSize: 13, color: 'var(--gray-400)', whiteSpace: 'nowrap' }}>{products.length} productos</span>
      </div>

      <div className="table-container" style={{ flex: 1, overflow: 'auto', minHeight: 0 }}>
        <table className="table">
          <thead>
            <tr>
              <th>Código</th><th>Nombre</th><th>Categoría</th><th>Precio Venta</th>
              <th>Stock</th><th>Stock Mín</th><th>Unidad</th><th>Activo</th><th>Acciones</th>
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
                <td>{p.isactive ? '✅' : '❌'}</td>
                <td className="actions">
                  <button className="btn-sm" onClick={() => openEdit(p)}>Editar</button>
                  <button className="btn-sm btn-danger" onClick={() => handleDelete(p.id)}>{p.isactive ? 'Desactivar' : 'Activar'}</button>
                </td>
              </tr>
            ))}
            {products.length === 0 && <tr><td colSpan={9} className="empty">No hay productos</td></tr>}
          </tbody>
        </table>
      </div>

      {showModal && (
        <div className="modal-overlay" onClick={() => setShowModal(false)}>
          <div className="modal modal-lg" onClick={e => e.stopPropagation()} style={{ maxWidth: 800, maxHeight: '90vh', overflowY: 'auto' }}>
            <h2>{editId ? 'Editar Producto' : 'Nuevo Producto'}</h2>
            {error && <div className="error-message">{error}</div>}
            {statusMsg && <div className="success-message">{statusMsg}</div>}
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
                <div className="form-group">
                  <label>Precio Mayoreo</label>
                  <input type="number" step="0.01" value={form.wholesale_price || 0} onChange={e => setForm({...form, wholesale_price: parseFloat(e.target.value) || 0})} />
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
                  <label>Fecha Caducidad</label>
                  <input type="date" value={form.expiry_date || ''} onChange={e => setForm({...form, expiry_date: e.target.value})} />
                </div>
                <div className="form-group">
                  <label>Estado</label>
                  <select value={form.isactive !== false ? 'true' : 'false'} onChange={e => setForm({...form, isactive: e.target.value === 'true'})}>
                    <option value="true">Activo</option>
                    <option value="false">Inactivo</option>
                  </select>
                </div>
              </div>

              <div className="form-row checkbox-row" style={{ marginTop: '0.5rem' }}>
                <label>
                  <input type="checkbox" checked={form.requires_tax ?? true} onChange={e => setForm({...form, requires_tax: e.target.checked})} />
                  Aplica IVA
                </label>
                <label>
                  <input type="checkbox" checked={form.requiresprescription || false} onChange={e => setForm({...form, requiresprescription: e.target.checked})} />
                  Requiere Receta
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

      <CsvImportModal
        show={showImport}
        onClose={() => { setShowImport(false); fetchProducts(); }}
        title="Productos"
        sampleCsv="code,name,description,category_id,supplier_id,purchase_price,sale_price,stock,min_stock,unit,wholesale_price,expiry_date,requires_prescription,requires_tax
FERR-001,Tornillo 1/2 pulgada,Tornillo galvanizado 1/2 pulgada,1,1,5.50,12.00,100,10,pza,10.00,2026-12-31,FALSE,TRUE
FERR-002,Martillo 16oz,Martillo de uña 16 onzas,2,,45.00,85.00,50,5,pza,,,FALSE,TRUE
FERR-003,Cinta metrica 5m,Cinta metrica metalica 5 metros,3,2,25.00,55.00,80,8,pza,,,FALSE,TRUE
FERR-004,Pintura blanca 1gal,Pintura vinilica blanca 1 galon,4,1,80.00,150.00,30,5,gal,135.00,2026-06-30,FALSE,TRUE
FERR-005,Taladro inalambrico,Taladro percutor 18V con bateria,5,3,450.00,899.00,15,3,pza,799.00,,FALSE,FALSE"
        onImport={handleCsvImport}
      />
    </div>
  );
}
