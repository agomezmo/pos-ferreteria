import { useState, useEffect, useMemo } from 'react';
import api from '../services/api';

interface Campaign {
  id: number; name: string; description: string; status: string;
  offer_type: string; offer_value: number; min_expiry_days: number; max_expiry_days: number;
  notes: string; created_by_name: string; created_at: string; sent_at: string;
  product_count: number; customer_count: number; sent_count: number;
}

interface Product {
  id: number; name: string; code: string; barcode: string;
  category_name: string; sale_price: number; purchase_price: number; expiry_date: string;
}

interface Customer {
  id: number; fullname: string; email: string; phone: string; document_number: string;
}

const OFFER_TYPES = [
  { value: 'cost_price', label: 'Precio de costo' },
  { value: 'percentage', label: 'Descuento %' },
  { value: 'fixed', label: 'Precio fijo' },
] as const;

export default function Campaigns() {
  const [campaigns, setCampaigns] = useState<Campaign[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [showDetail, setShowDetail] = useState<Campaign | null>(null);
  const [detailData, setDetailData] = useState<any>(null);
  const [products, setProducts] = useState<Product[]>([]);
  const [customers, setCustomers] = useState<Customer[]>([]);
  const [sending, setSending] = useState(false);
  const [productSearch, setProductSearch] = useState('');
  const [customerSearch, setCustomerSearch] = useState('');
  const [activeCategory, setActiveCategory] = useState('');

  const [form, setForm] = useState({
    name: '', description: '', offer_type: 'cost_price', offer_value: 0,
    min_expiry_days: 30, max_expiry_days: 90, notes: '',
    product_ids: [] as number[], customer_ids: [] as number[],
  });

  useEffect(() => { loadCampaigns(); }, []);

  const loadCampaigns = async () => {
    try {
      const res = await api.get('/campaigns');
      setCampaigns(res.data);
    } catch (err) { console.error(err); } finally { setLoading(false); }
  };

  const categories = useMemo(() => Array.from(new Set(products.map(p => p.category_name || 'Sin categoría'))).sort(), [products]);
  const categoryCounts = useMemo(() => {
    const map: Record<string, number> = {};
    for (const p of products) { const cat = p.category_name || 'Sin categoría'; map[cat] = (map[cat] || 0) + 1; }
    return map;
  }, [products]);

  const filteredProducts = useMemo(() => {
    const s = productSearch.toLowerCase().trim();
    return products.filter(p => {
      if (activeCategory && (p.category_name || 'Sin categoría') !== activeCategory) return false;
      return !s || p.name.toLowerCase().includes(s) || p.code.toLowerCase().includes(s);
    });
  }, [products, productSearch, activeCategory]);

  const filteredCustomers = useMemo(() => {
    const s = customerSearch.toLowerCase().trim();
    if (!s) return customers;
    return customers.filter(c => c.fullname.toLowerCase().includes(s) || (c.email || '').toLowerCase().includes(s) || (c.phone || '').includes(s));
  }, [customers, customerSearch]);

  const openCreate = async () => {
    try {
      const [pr, cr] = await Promise.all([
        api.get('/campaigns/expiring-products/list', { params: { days: 90 } }),
        api.get('/campaigns/available-customers/list'),
      ]);
      setProducts(pr.data); setCustomers(cr.data);
    } catch (err) { console.error(err); }
    setForm({ name: '', description: '', offer_type: 'cost_price', offer_value: 0, min_expiry_days: 30, max_expiry_days: 90, notes: '', product_ids: [], customer_ids: [] });
    setProductSearch(''); setCustomerSearch(''); setActiveCategory('');
    setShowForm(true);
  };

  const handleCreate = async (e: React.FormEvent) => {
    e.preventDefault();
    try { await api.post('/campaigns', form); setShowForm(false); loadCampaigns(); }
    catch (err: any) { alert(err.response?.data?.error?.message || err.response?.data?.error || 'Error al crear campaña'); }
  };

  const viewDetail = async (c: Campaign) => {
    try { const r = await api.get(`/campaigns/${c.id}`); setDetailData(r.data); setShowDetail(r.data); }
    catch (err) { console.error(err); }
  };

  const handleSend = async (id: number, channels: string[]) => {
    if (!confirm('Enviar esta campaña?')) return;
    setSending(true);
    try {
      const r = await api.post(`/campaigns/${id}/send`, { channels });
      alert(r.data.message); viewDetail(showDetail!); loadCampaigns();
    } catch (err: any) { alert(err.response?.data?.error?.message || err.response?.data?.error || 'Error al enviar'); }
    finally { setSending(false); }
  };

  const handleDelete = async (id: number) => {
    if (!confirm('Eliminar campaña?')) return;
    try { await api.delete(`/campaigns/${id}`); setShowDetail(null); loadCampaigns(); }
    catch (err: any) { alert(err.response?.data?.error?.message || err.response?.data?.error || 'Error al eliminar'); }
  };

  const toggleProduct = (id: number) => setForm(p => ({ ...p, product_ids: p.product_ids.includes(id) ? p.product_ids.filter(x => x !== id) : [...p.product_ids, id] }));
  const toggleCustomer = (id: number) => setForm(p => ({ ...p, customer_ids: p.customer_ids.includes(id) ? p.customer_ids.filter(x => x !== id) : [...p.customer_ids, id] }));

  const selectAllProducts = () => setForm(p => ({ ...p, product_ids: products.map(x => x.id) }));
  const deselectAllProducts = () => setForm(p => ({ ...p, product_ids: [] }));
  const selectExpiringSoon = (n: number) => {
    const now = Date.now();
    setForm(p => ({ ...p, product_ids: products.filter(x => { if (!x.expiry_date) return false; const d = Math.ceil((new Date(x.expiry_date).getTime() - now) / 86400000); return d > 0 && d <= n; }).map(x => x.id) }));
  };
  const selectAllCustomers = () => setForm(p => ({ ...p, customer_ids: customers.map(x => x.id) }));
  const selectWithEmail = () => setForm(p => ({ ...p, customer_ids: customers.filter(x => x.email).map(x => x.id) }));
  const selectWithPhone = () => setForm(p => ({ ...p, customer_ids: customers.filter(x => x.phone).map(x => x.id) }));
  const deselectAllCustomers = () => setForm(p => ({ ...p, customer_ids: [] }));

  const fDate = (d: string) => d ? new Date(d).toLocaleDateString() : '-';
  const fCur = (n: number) => `$${Number(n).toFixed(2)}`;

  const badge = (s: string) => {
    const m: Record<string, string> = { draft: 'bg-gray-100 text-gray-600', active: 'bg-blue-100 text-blue-700', completed: 'bg-green-100 text-green-700', cancelled: 'bg-red-100 text-red-700' };
    const l: Record<string, string> = { draft: 'Borrador', active: 'Activa', completed: 'Enviada', cancelled: 'Cancelada' };
    return <span className={`px-2 py-0.5 rounded-full text-xs font-semibold ${m[s] || 'bg-gray-100 text-gray-600'}`}>{l[s] || s}</span>;
  };

  if (loading) return <div className="flex items-center justify-center h-64"><div className="animate-spin rounded-full h-12 w-12 border-b-2 border-green-600" /></div>;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-800">Campañas</h1>
          <p className="text-sm text-gray-500 mt-0.5">Crea y envía ofertas a tus clientes</p>
        </div>
        <button onClick={openCreate}
          className="btn-primary flex items-center gap-1.5 text-sm font-semibold px-4 py-2">
          + Nueva Campaña
        </button>
      </div>

      <div className="bg-white rounded-xl border border-gray-200 overflow-hidden">
        {campaigns.length === 0 ? (
          <p className="text-center py-12 text-gray-400 text-sm">No hay campañas. Crea la primera.</p>
        ) : (
          <table className="w-full text-sm">
            <thead>
              <tr className="bg-gray-50 border-b border-gray-200">
                <th className="text-left py-3 px-4 text-gray-500 font-semibold text-xs uppercase">Nombre</th>
                <th className="text-left py-3 px-4 text-gray-500 font-semibold text-xs uppercase">Estado</th>
                <th className="text-center py-3 px-4 text-gray-500 font-semibold text-xs uppercase">Prod.</th>
                <th className="text-center py-3 px-4 text-gray-500 font-semibold text-xs uppercase">Clientes</th>
                <th className="text-center py-3 px-4 text-gray-500 font-semibold text-xs uppercase">Env.</th>
                <th className="text-left py-3 px-4 text-gray-500 font-semibold text-xs uppercase">Creada</th>
                <th className="text-center py-3 px-4 text-gray-500 font-semibold text-xs uppercase"></th>
              </tr>
            </thead>
            <tbody>
              {campaigns.map(c => (
                <tr key={c.id} onClick={() => viewDetail(c)}
                  className="border-b border-gray-100 hover:bg-gray-50 cursor-pointer last:border-0 transition-colors">
                  <td className="py-3 px-4 font-medium text-gray-800">{c.name}</td>
                  <td className="py-3 px-4">{badge(c.status)}</td>
                  <td className="py-3 px-4 text-center text-gray-700">{c.product_count}</td>
                  <td className="py-3 px-4 text-center text-gray-700">{c.customer_count}</td>
                  <td className="py-3 px-4 text-center text-gray-700">{c.sent_count}</td>
                  <td className="py-3 px-4 text-gray-500 text-sm">{fDate(c.created_at)}</td>
                  <td className="py-3 px-4 text-center">
                    {c.status === 'draft' && (
                      <button onClick={e => { e.stopPropagation(); handleSend(c.id, ['email']); }}
                        className="text-xs font-semibold text-green-600 bg-green-50 hover:bg-green-100 px-2.5 py-1.5 rounded-lg transition-colors">
                        Enviar
                      </button>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>

      {showForm && (
        <div className="fixed inset-0 bg-black/40 z-[1000] flex items-start justify-center overflow-y-auto py-6"
          onClick={e => { if (e.target === e.currentTarget) setShowForm(false); }}>
          <div className="bg-white rounded-xl shadow-2xl w-full max-w-6xl mx-4"
            onClick={e => e.stopPropagation()}>

            <div className="flex items-center justify-between px-6 py-4 border-b border-gray-200">
              <div>
                <h2 className="text-xl font-bold text-gray-800">Nueva campaña</h2>
                <p className="text-sm text-gray-500 mt-0.5">Completa los datos para crear la promoción</p>
              </div>
              <button onClick={() => setShowForm(false)}
                className="w-8 h-8 rounded-lg hover:bg-gray-100 flex items-center justify-center text-gray-400 hover:text-gray-600 text-xl leading-none">&times;</button>
            </div>

            <form onSubmit={handleCreate}>
              <div className="px-6 py-5 space-y-6">
                <div className="grid grid-cols-3 gap-5">
                  <div className="col-span-2">
                    <label className="block text-sm font-semibold text-gray-700 mb-1.5">Nombre de la campaña *</label>
                    <input required value={form.name}
                      onChange={e => setForm({ ...form, name: e.target.value })}
                      placeholder="Ej: Ofertas de temporada"
                      className="w-full px-4 py-2.5 border border-gray-300 rounded-lg text-sm focus:ring-2 focus:ring-green-500/20 focus:border-green-500 outline-none transition-all" />
                  </div>
                  <div>
                    <label className="block text-sm font-semibold text-gray-700 mb-1.5">Tipo de oferta</label>
                    <select value={form.offer_type}
                      onChange={e => setForm({ ...form, offer_type: e.target.value })}
                      className="w-full px-4 py-2.5 border border-gray-300 rounded-lg text-sm bg-white focus:ring-2 focus:ring-green-500/20 focus:border-green-500 outline-none">
                      {OFFER_TYPES.map(t => <option key={t.value} value={t.value}>{t.label}</option>)}
                    </select>
                  </div>
                </div>

                <div className="grid grid-cols-4 gap-5">
                  {form.offer_type !== 'cost_price' && (
                    <div>
                      <label className="block text-sm font-semibold text-gray-700 mb-1.5">
                        {form.offer_type === 'percentage' ? 'Descuento %' : 'Precio fijo ($)'}
                      </label>
                      <input type="number" step="0.01" min="0" value={form.offer_value}
                        onChange={e => setForm({ ...form, offer_value: Number(e.target.value) })}
                        className="w-full px-4 py-2.5 border border-gray-300 rounded-lg text-sm focus:ring-2 focus:ring-green-500/20 focus:border-green-500 outline-none" />
                    </div>
                  )}
                  <div className={form.offer_type === 'cost_price' ? 'col-span-2' : ''}>
                    <label className="block text-sm font-semibold text-gray-700 mb-1.5">Días para caducar</label>
                    <div className="flex items-center gap-2">
                      <input type="number" min="1" value={form.min_expiry_days}
                        onChange={e => setForm({ ...form, min_expiry_days: Number(e.target.value) })}
                        className="w-full px-4 py-2.5 border border-gray-300 rounded-lg text-sm focus:ring-2 focus:ring-green-500/20 focus:border-green-500 outline-none" />
                      <span className="text-gray-400 font-medium">—</span>
                      <input type="number" min="1" value={form.max_expiry_days}
                        onChange={e => setForm({ ...form, max_expiry_days: Number(e.target.value) })}
                        className="w-full px-4 py-2.5 border border-gray-300 rounded-lg text-sm focus:ring-2 focus:ring-green-500/20 focus:border-green-500 outline-none" />
                    </div>
                  </div>
                  <div>
                    <label className="block text-sm font-semibold text-gray-700 mb-1.5">Notas</label>
                    <input value={form.notes}
                      onChange={e => setForm({ ...form, notes: e.target.value })}
                      placeholder="Opcional"
                      className="w-full px-4 py-2.5 border border-gray-300 rounded-lg text-sm focus:ring-2 focus:ring-green-500/20 focus:border-green-500 outline-none" />
                  </div>
                </div>

                <div className="grid grid-cols-2 gap-6">
                  <div className="card !p-4 !mb-0">
                    <div className="flex items-center justify-between mb-3">
                      <span className="text-sm font-bold text-gray-700">Productos</span>
                      <span className="text-xs text-gray-400 font-medium">{form.product_ids.length}/{products.length} seleccionados</span>
                    </div>
                    <div className="relative mb-3">
                      <input placeholder="Buscar producto por nombre..." value={productSearch}
                        onChange={e => { setProductSearch(e.target.value); setActiveCategory(''); }}
                        className="w-full px-3 py-2 border border-gray-300 rounded-lg text-sm focus:ring-2 focus:ring-green-500/20 focus:border-green-500 outline-none" />
                    </div>
                    <div className="flex flex-wrap gap-1.5 mb-3">
                      <button type="button" onClick={selectAllProducts}
                        className="text-xs font-semibold px-3 py-1.5 rounded-lg bg-green-50 text-green-700 hover:bg-green-100 border border-green-200 transition-colors">Todos</button>
                      <button type="button" onClick={() => selectExpiringSoon(7)}
                        className="text-xs font-semibold px-3 py-1.5 rounded-lg bg-red-50 text-red-600 hover:bg-red-100 border border-red-200 transition-colors">Próximos 7 días</button>
                      <button type="button" onClick={() => selectExpiringSoon(15)}
                        className="text-xs font-semibold px-3 py-1.5 rounded-lg bg-orange-50 text-orange-600 hover:bg-orange-100 border border-orange-200 transition-colors">15 días</button>
                      <button type="button" onClick={() => selectExpiringSoon(30)}
                        className="text-xs font-semibold px-3 py-1.5 rounded-lg bg-amber-50 text-amber-600 hover:bg-amber-100 border border-amber-200 transition-colors">30 días</button>
                      <button type="button" onClick={deselectAllProducts}
                        className="text-xs font-semibold px-3 py-1.5 rounded-lg bg-gray-50 text-gray-500 hover:bg-gray-100 border border-gray-200 transition-colors">Limpiar</button>
                    </div>
                    <div className="flex gap-1.5 mb-3 overflow-x-auto pb-1">
                      <button type="button" onClick={() => setActiveCategory('')}
                        className={`whitespace-nowrap text-xs font-semibold px-3 py-1 rounded-full transition-colors ${!activeCategory ? 'bg-gray-800 text-white' : 'bg-gray-100 text-gray-500 hover:bg-gray-200'}`}>Todas</button>
                      {categories.map(cat => (
                        <button key={cat} type="button" onClick={() => { setActiveCategory(cat); setProductSearch(''); }}
                          className={`whitespace-nowrap text-xs font-semibold px-3 py-1 rounded-full transition-colors ${activeCategory === cat ? 'text-white' : 'bg-gray-100 text-gray-500 hover:bg-gray-200'}`}
                          style={activeCategory === cat ? { backgroundColor: '#6b7280' } : {}}>
                          {cat} {categoryCounts[cat] || 0}
                        </button>
                      ))}
                    </div>
                    <div className="border border-gray-200 rounded-lg overflow-hidden max-h-72 overflow-y-auto">
                      {filteredProducts.length === 0 ? (
                        <p className="text-center py-8 text-gray-300 text-sm">Sin resultados</p>
                      ) : (
                        filteredProducts.map(p => {
                          const days = p.expiry_date ? Math.ceil((new Date(p.expiry_date).getTime() - Date.now()) / 86400000) : 0;
                          const sel = form.product_ids.includes(p.id);
                          return (
                            <div key={p.id} onClick={() => toggleProduct(p.id)}
                              className={`flex items-center gap-3 px-3.5 py-2.5 cursor-pointer border-b border-gray-100 last:border-0 text-sm transition-colors ${sel ? 'bg-green-50' : 'hover:bg-gray-50'}`}>
                              <input type="checkbox" checked={sel} readOnly className="accent-green-600 w-4 h-4 pointer-events-none shrink-0" />
                              <div className="flex-1 min-w-0">
                                <span className={`block truncate ${sel ? 'font-semibold text-gray-800' : 'text-gray-700'}`}>{p.name}</span>
                                <span className="text-xs text-gray-400 font-mono">{p.code}</span>
                              </div>
                              <div className="flex items-center gap-2 shrink-0">
                                {days > 0 ? (
                                  <span className={`text-xs font-bold px-1.5 py-0.5 rounded ${days <= 7 ? 'bg-red-50 text-red-600' : days <= 15 ? 'bg-orange-50 text-orange-600' : 'bg-amber-50 text-amber-600'}`}>{days}d</span>
                                ) : (
                                  <span className="text-xs font-medium text-red-400">Vencido</span>
                                )}
                                <span className="text-xs text-gray-400 line-through">{fCur(p.sale_price)}</span>
                                <span className="text-xs font-bold text-green-600">{fCur(p.purchase_price)}</span>
                              </div>
                            </div>
                          );
                        })
                      )}
                    </div>
                  </div>

                  <div className="card !p-4 !mb-0">
                    <div className="flex items-center justify-between mb-3">
                      <span className="text-sm font-bold text-gray-700">Clientes</span>
                      <span className="text-xs text-gray-400 font-medium">{form.customer_ids.length}/{customers.length} seleccionados</span>
                    </div>
                    <div className="relative mb-3">
                      <input placeholder="Buscar cliente por nombre..." value={customerSearch}
                        onChange={e => setCustomerSearch(e.target.value)}
                        className="w-full px-3 py-2 border border-gray-300 rounded-lg text-sm focus:ring-2 focus:ring-green-500/20 focus:border-green-500 outline-none" />
                    </div>
                    <div className="flex gap-2 mb-3">
                      <div className="flex-1 bg-gray-50 rounded-lg px-3 py-2 text-center border border-gray-100">
                        <p className="text-sm font-bold text-blue-600">{customers.filter(c => c.email).length}</p>
                        <p className="text-xs text-gray-400 font-medium">Con email</p>
                      </div>
                      <div className="flex-1 bg-gray-50 rounded-lg px-3 py-2 text-center border border-gray-100">
                        <p className="text-sm font-bold text-emerald-600">{customers.filter(c => c.phone).length}</p>
                        <p className="text-xs text-gray-400 font-medium">Con teléfono</p>
                      </div>
                      <div className="flex-1 bg-gray-50 rounded-lg px-3 py-2 text-center border border-gray-100">
                        <p className="text-sm font-bold text-purple-600">{customers.length}</p>
                        <p className="text-xs text-gray-400 font-medium">Total</p>
                      </div>
                    </div>
                    <div className="flex flex-wrap gap-1.5 mb-3">
                      <button type="button" onClick={selectAllCustomers}
                        className="text-xs font-semibold px-3 py-1.5 rounded-lg bg-green-50 text-green-700 hover:bg-green-100 border border-green-200 transition-colors">Todos</button>
                      <button type="button" onClick={selectWithEmail}
                        className="text-xs font-semibold px-3 py-1.5 rounded-lg bg-blue-50 text-blue-600 hover:bg-blue-100 border border-blue-200 transition-colors">Con email</button>
                      <button type="button" onClick={selectWithPhone}
                        className="text-xs font-semibold px-3 py-1.5 rounded-lg bg-emerald-50 text-emerald-600 hover:bg-emerald-100 border border-emerald-200 transition-colors">Con teléfono</button>
                      <button type="button" onClick={deselectAllCustomers}
                        className="text-xs font-semibold px-3 py-1.5 rounded-lg bg-gray-50 text-gray-500 hover:bg-gray-100 border border-gray-200 transition-colors">Limpiar</button>
                    </div>
                    <div className="border border-gray-200 rounded-lg overflow-hidden max-h-72 overflow-y-auto">
                      {filteredCustomers.length === 0 ? (
                        <p className="text-center py-8 text-gray-300 text-sm">Sin resultados</p>
                      ) : (
                        filteredCustomers.map(c => {
                          const sel = form.customer_ids.includes(c.id);
                          return (
                            <div key={c.id} onClick={() => toggleCustomer(c.id)}
                              className={`flex items-center gap-3 px-3.5 py-2.5 cursor-pointer border-b border-gray-100 last:border-0 text-sm transition-colors ${sel ? 'bg-blue-50' : 'hover:bg-gray-50'}`}>
                              <input type="checkbox" checked={sel} readOnly className="accent-blue-600 w-4 h-4 pointer-events-none shrink-0" />
                              <div className="flex-1 min-w-0">
                                <span className={`block truncate ${sel ? 'font-semibold text-gray-800' : 'text-gray-700'}`}>{c.fullname}</span>
                              </div>
                              <div className="flex items-center gap-1.5 shrink-0">
                                {c.email && (
                                  <span className="text-xs bg-blue-50 text-blue-600 px-1.5 py-0.5 rounded font-medium">
                                    {c.email.length > 20 ? c.email.substring(0, 18) + '…' : c.email}
                                  </span>
                                )}
                                {c.phone && (
                                  <span className="text-xs bg-emerald-50 text-emerald-600 px-1.5 py-0.5 rounded font-medium">{c.phone}</span>
                                )}
                              </div>
                            </div>
                          );
                        })
                      )}
                    </div>
                  </div>
                </div>
              </div>

              <div className="flex items-center justify-between px-6 py-4 border-t border-gray-200 bg-gray-50">
                <span className="text-sm text-gray-400">
                  {form.name && form.product_ids.length > 0 && form.customer_ids.length > 0
                    ? '✓ Completo — puedes crear la campaña'
                    : 'Faltan campos por llenar'}
                </span>
                <div className="flex gap-3">
                  <button type="button" onClick={() => setShowForm(false)}
                    className="px-5 py-2 text-sm font-semibold text-gray-600 hover:bg-gray-100 rounded-lg transition-colors">Cancelar</button>
                  <button type="submit"
                    disabled={!form.name || form.product_ids.length === 0 || form.customer_ids.length === 0}
                    className="px-6 py-2.5 text-sm font-bold text-white bg-green-600 hover:bg-green-700 rounded-lg transition-colors disabled:opacity-40 disabled:cursor-not-allowed shadow-sm">
                    Crear campaña
                  </button>
                </div>
              </div>
            </form>
          </div>
        </div>
      )}

      {showDetail && (
        <div className="fixed inset-0 bg-black/40 z-[1000] flex items-start justify-center overflow-y-auto py-6"
          onClick={e => { if (e.target === e.currentTarget) setShowDetail(null); }}>
          <div className="bg-white rounded-xl shadow-2xl w-full max-w-4xl mx-4"
            onClick={e => e.stopPropagation()}>

            <div className="flex items-center justify-between px-6 py-4 border-b border-gray-200">
              <div>
                <div className="flex items-center gap-3">
                  <h2 className="text-xl font-bold text-gray-800">{detailData.name}</h2>
                  {badge(detailData.status)}
                </div>
                <p className="text-sm text-gray-500 mt-0.5">{detailData.description || 'Sin descripción'}</p>
              </div>
              <button onClick={() => setShowDetail(null)}
                className="w-8 h-8 rounded-lg hover:bg-gray-100 flex items-center justify-center text-gray-400 hover:text-gray-600 text-xl leading-none">&times;</button>
            </div>

            <div className="px-6 py-5 space-y-6">
              <div className="grid grid-cols-4 gap-4">
                <div className="bg-gray-50 rounded-lg px-4 py-3 border border-gray-100">
                  <p className="text-xs text-gray-500 font-semibold uppercase tracking-wider">Tipo</p>
                  <p className="text-sm font-semibold text-gray-800 mt-1">{OFFER_TYPES.find(t => t.value === detailData.offer_type)?.label || detailData.offer_type}</p>
                </div>
                {detailData.offer_value > 0 && (
                  <div className="bg-gray-50 rounded-lg px-4 py-3 border border-gray-100">
                    <p className="text-xs text-gray-500 font-semibold uppercase tracking-wider">Valor</p>
                    <p className="text-sm font-semibold text-gray-800 mt-1">{detailData.offer_type === 'percentage' ? `${detailData.offer_value}%` : `$${detailData.offer_value}`}</p>
                  </div>
                )}
                <div className="bg-gray-50 rounded-lg px-4 py-3 border border-gray-100">
                  <p className="text-xs text-gray-500 font-semibold uppercase tracking-wider">Vencimiento</p>
                  <p className="text-sm font-semibold text-gray-800 mt-1">{detailData.min_expiry_days}–{detailData.max_expiry_days} días</p>
                </div>
                <div className="bg-gray-50 rounded-lg px-4 py-3 border border-gray-100">
                  <p className="text-xs text-gray-500 font-semibold uppercase tracking-wider">Creada por</p>
                  <p className="text-sm font-semibold text-gray-800 mt-1">{detailData.created_by_name}</p>
                  <p className="text-xs text-gray-400 mt-0.5">{fDate(detailData.created_at)}</p>
                </div>
              </div>

              {detailData.notes && (
                <div className="bg-gray-50 rounded-lg px-4 py-3 border border-gray-100">
                  <p className="text-xs text-gray-500 font-semibold uppercase tracking-wider mb-1">Notas</p>
                  <p className="text-sm text-gray-700">{detailData.notes}</p>
                </div>
              )}

              <div>
                <h3 className="text-sm font-bold text-gray-700 mb-3">Productos ({detailData.products?.length || 0})</h3>
                <div className="border border-gray-200 rounded-lg overflow-hidden">
                  <table className="w-full text-sm">
                    <thead>
                      <tr className="bg-gray-50 border-b border-gray-200">
                        <th className="text-left py-2.5 px-4 text-gray-500 font-semibold text-xs uppercase">Producto</th>
                        <th className="text-center py-2.5 px-4 text-gray-500 font-semibold text-xs uppercase">Precio</th>
                        <th className="text-center py-2.5 px-4 text-gray-500 font-semibold text-xs uppercase">Oferta</th>
                        <th className="text-center py-2.5 px-4 text-gray-500 font-semibold text-xs uppercase">Caduca</th>
                      </tr>
                    </thead>
                    <tbody>
                      {detailData.products?.map((p: any) => (
                        <tr key={p.id} className="border-b border-gray-100 last:border-0">
                          <td className="py-2.5 px-4 text-gray-700 text-sm">{p.product_name}</td>
                          <td className="py-2.5 px-4 text-center text-gray-400 line-through text-sm">{fCur(p.original_price)}</td>
                          <td className="py-2.5 px-4 text-center text-green-600 font-bold text-sm">{fCur(p.offer_price)}</td>
                          <td className="py-2.5 px-4 text-center text-gray-500 text-sm">{p.expiry_date ? fDate(p.expiry_date) : '—'}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>

              <div>
                <h3 className="text-sm font-bold text-gray-700 mb-3">Clientes ({detailData.customers?.length || 0})</h3>
                <div className="border border-gray-200 rounded-lg overflow-hidden">
                  <table className="w-full text-sm">
                    <thead>
                      <tr className="bg-gray-50 border-b border-gray-200">
                        <th className="text-left py-2.5 px-4 text-gray-500 font-semibold text-xs uppercase">Nombre</th>
                        <th className="text-left py-2.5 px-4 text-gray-500 font-semibold text-xs uppercase">Email</th>
                        <th className="text-left py-2.5 px-4 text-gray-500 font-semibold text-xs uppercase">Teléfono</th>
                      </tr>
                    </thead>
                    <tbody>
                      {detailData.customers?.map((c: any) => (
                        <tr key={c.id} className="border-b border-gray-100 last:border-0">
                          <td className="py-2.5 px-4 text-gray-700 text-sm">{c.customer_name}</td>
                          <td className="py-2.5 px-4 text-blue-600 text-sm">{c.contact_email || '—'}</td>
                          <td className="py-2.5 px-4 text-emerald-600 text-sm">{c.contact_phone || '—'}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>

              {detailData.logs?.length > 0 && (
                <div>
                  <h3 className="text-sm font-bold text-gray-700 mb-3">Bitácora de envíos ({detailData.logs.length})</h3>
                  <div className="border border-gray-200 rounded-lg overflow-hidden">
                    <table className="w-full text-sm">
                      <thead>
                        <tr className="bg-gray-50 border-b border-gray-200">
                          <th className="text-left py-2.5 px-4 text-gray-500 font-semibold text-xs uppercase">Cliente</th>
                          <th className="text-center py-2.5 px-4 text-gray-500 font-semibold text-xs uppercase">Canal</th>
                          <th className="text-left py-2.5 px-4 text-gray-500 font-semibold text-xs uppercase">Destino</th>
                          <th className="text-center py-2.5 px-4 text-gray-500 font-semibold text-xs uppercase">Estado</th>
                          <th className="text-center py-2.5 px-4 text-gray-500 font-semibold text-xs uppercase">Fecha</th>
                        </tr>
                      </thead>
                      <tbody>
                        {detailData.logs?.map((l: any) => (
                          <tr key={l.id} className="border-b border-gray-100 last:border-0 text-sm">
                            <td className="py-2.5 px-4 text-gray-700">{l.customer_name || '—'}</td>
                            <td className="py-2.5 px-4 text-center">
                              <span className={`text-xs font-semibold px-2 py-0.5 rounded-full ${l.channel === 'email' ? 'bg-blue-50 text-blue-600' : 'bg-green-50 text-green-600'}`}>{l.channel === 'email' ? 'Email' : 'WhatsApp'}</span>
                            </td>
                            <td className="py-2.5 px-4 text-gray-500 text-sm">{l.recipient}</td>
                            <td className="py-2.5 px-4 text-center">
                              {l.status === 'sent' ? <span className="text-green-600 font-semibold text-sm">✓ Enviado</span> :
                               l.status === 'failed' ? <span className="text-red-500 font-semibold text-sm" title={l.error_message || ''}>✗ Falló</span> :
                               <span className="text-gray-400 text-sm">— Pendiente</span>}
                            </td>
                            <td className="py-2.5 px-4 text-center text-gray-500 text-sm">{l.sent_at ? fDate(l.sent_at) : '—'}</td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                </div>
              )}

              <div className="flex flex-wrap gap-3 pt-4 border-t border-gray-200">
                {detailData.status === 'draft' && (
                  <>
                    <button onClick={() => handleSend(detailData.id, ['email'])} disabled={sending}
                      className="flex-1 min-w-[140px] px-5 py-2.5 text-sm font-bold text-white bg-blue-600 hover:bg-blue-700 rounded-lg transition-colors disabled:opacity-40 shadow-sm">
                      {sending ? 'Enviando...' : '📧 Enviar por Email'}
                    </button>
                    <button onClick={() => handleSend(detailData.id, ['whatsapp'])} disabled={sending}
                      className="flex-1 min-w-[140px] px-5 py-2.5 text-sm font-bold text-white rounded-lg transition-colors disabled:opacity-40 shadow-sm"
                      style={{ background: '#25D366' }}>
                      {sending ? 'Enviando...' : '💬 Enviar por WhatsApp'}
                    </button>
                    <button onClick={() => handleSend(detailData.id, ['email', 'whatsapp'])} disabled={sending}
                      className="flex-1 min-w-[140px] px-5 py-2.5 text-sm font-semibold text-gray-700 bg-gray-100 hover:bg-gray-200 rounded-lg transition-colors">
                      📨 Ambos
                    </button>
                    <button onClick={() => handleDelete(detailData.id)}
                      className="px-4 py-2.5 text-sm font-semibold text-red-500 hover:bg-red-50 rounded-lg transition-colors">
                      Eliminar
                    </button>
                  </>
                )}
                <button onClick={() => setShowDetail(null)}
                  className="px-5 py-2.5 text-sm font-semibold text-gray-600 hover:bg-gray-100 rounded-lg transition-colors">
                  Cerrar
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
