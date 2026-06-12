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

function fDate(d: string) { return d ? new Date(d).toLocaleDateString() : '-'; }
function fCur(n: number) { return `$${Number(n).toFixed(2)}`; }

function Badge({ status }: { status: string }) {
  const m: Record<string, { bg: string; color: string; label: string }> = {
    draft: { bg: '#f3f4f6', color: '#4b5563', label: 'Borrador' },
    active: { bg: '#dbeafe', color: '#1d4ed8', label: 'Activa' },
    completed: { bg: '#d1fae5', color: '#047857', label: 'Enviada' },
    cancelled: { bg: '#fee2e2', color: '#b91c1c', label: 'Cancelada' },
  };
  const s = m[status] || m.draft;
  return <span style={{ padding: '2px 10px', borderRadius: 999, fontSize: 12, fontWeight: 600, background: s.bg, color: s.color }}>{s.label}</span>;
}

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

  const categories = useMemo(() =>
    Array.from(new Set(products.map(p => p.category_name || 'Sin categoría'))).sort(),
  [products]);

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
    return customers.filter(c =>
      c.fullname.toLowerCase().includes(s) ||
      (c.email || '').toLowerCase().includes(s) ||
      (c.phone || '').includes(s));
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

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <div>
          <h1>Campañas</h1>
          <p style={{ color: 'var(--gray-500)', fontSize: 14, marginTop: 2 }}>Crea y envía ofertas a tus clientes</p>
        </div>
        <button onClick={openCreate} className="btn-primary">
          + Nueva Campaña
        </button>
      </div>

      {/* ── Lista de campañas ── */}
      <div className="card" style={{ padding: 0, overflow: 'hidden' }}>
        {campaigns.length === 0 ? (
          <p style={{ textAlign: 'center', padding: '3rem 0', color: 'var(--gray-400)', fontSize: 14 }}>No hay campañas. Crea la primera.</p>
        ) : (
          <table className="table" style={{ boxShadow: 'none' }}>
            <thead>
              <tr>
                <th>Nombre</th><th>Estado</th><th style={{ textAlign: 'center' }}>Prod.</th>
                <th style={{ textAlign: 'center' }}>Clientes</th><th style={{ textAlign: 'center' }}>Env.</th>
                <th>Creada</th><th style={{ textAlign: 'center' }}></th>
              </tr>
            </thead>
            <tbody>
              {campaigns.map(c => (
                <tr key={c.id} onClick={() => viewDetail(c)}
                  style={{ cursor: 'pointer' }}>
                  <td><strong>{c.name}</strong></td>
                  <td><Badge status={c.status} /></td>
                  <td style={{ textAlign: 'center' }}>{c.product_count}</td>
                  <td style={{ textAlign: 'center' }}>{c.customer_count}</td>
                  <td style={{ textAlign: 'center' }}>{c.sent_count}</td>
                  <td style={{ color: 'var(--gray-500)', fontSize: 13 }}>{fDate(c.created_at)}</td>
                  <td style={{ textAlign: 'center' }}>
                    {c.status === 'draft' && (
                      <button onClick={e => { e.stopPropagation(); handleSend(c.id, ['email']); }}
                        className="btn-sm" style={{ background: '#d1fae5', color: '#047857', fontWeight: 600 }}>
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

      {/* ── Modal: Nueva Campaña ── */}
      {showForm && (
        <div className="modal-overlay" onClick={e => { if (e.target === e.currentTarget) setShowForm(false); }}
          style={{ alignItems: 'flex-start', paddingTop: '1.5rem', overflowY: 'auto' }}>
          <div className="modal modal-lg" onClick={e => e.stopPropagation()}
            style={{ maxWidth: 1100, maxHeight: '95vh' }}>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', marginBottom: '1.5rem' }}>
              <div>
                <h2>Nueva campaña</h2>
                <p style={{ color: 'var(--gray-500)', fontSize: 13, marginTop: 2 }}>Completa los datos para crear la promoción</p>
              </div>
              <button onClick={() => setShowForm(false)}
                style={{ background: 'none', border: 'none', fontSize: 24, cursor: 'pointer', color: 'var(--gray-400)', padding: '0 4px' }}>&times;</button>
            </div>

            <form onSubmit={handleCreate}>
              <div style={{ display: 'grid', gridTemplateColumns: '2fr 1fr', gap: '1rem', marginBottom: '1rem' }}>
                <div className="form-group">
                  <label>Nombre de la campaña *</label>
                  <input required value={form.name}
                    onChange={e => setForm({ ...form, name: e.target.value })}
                    placeholder="Ej: Ofertas de temporada" />
                </div>
                <div className="form-group">
                  <label>Tipo de oferta</label>
                  <select value={form.offer_type}
                    onChange={e => setForm({ ...form, offer_type: e.target.value })}>
                    {OFFER_TYPES.map(t => <option key={t.value} value={t.value}>{t.label}</option>)}
                  </select>
                </div>
              </div>

              <div style={{ display: 'grid', gridTemplateColumns: form.offer_type === 'cost_price' ? '1fr 1fr 1fr' : '1fr 1fr 1fr 1fr', gap: '1rem', marginBottom: '1rem' }}>
                {form.offer_type !== 'cost_price' && (
                  <div className="form-group">
                    <label>{form.offer_type === 'percentage' ? 'Descuento %' : 'Precio fijo ($)'}</label>
                    <input type="number" step="0.01" min="0" value={form.offer_value}
                      onChange={e => setForm({ ...form, offer_value: Number(e.target.value) })} />
                  </div>
                )}
                <div className="form-group">
                  <label>Días mín. para caducar</label>
                  <input type="number" min="1" value={form.min_expiry_days}
                    onChange={e => setForm({ ...form, min_expiry_days: Number(e.target.value) })} />
                </div>
                <div className="form-group">
                  <label>Días máx. para caducar</label>
                  <input type="number" min="1" value={form.max_expiry_days}
                    onChange={e => setForm({ ...form, max_expiry_days: Number(e.target.value) })} />
                </div>
                <div className="form-group">
                  <label>Notas</label>
                  <input value={form.notes}
                    onChange={e => setForm({ ...form, notes: e.target.value })}
                    placeholder="Opcional" />
                </div>
              </div>

              <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem', marginBottom: '1.5rem' }}>
                {/* Productos */}
                <div className="card" style={{ padding: '1rem', marginBottom: 0 }}>
                  <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '0.75rem' }}>
                    <span style={{ fontWeight: 600, fontSize: 14 }}>Productos</span>
                    <span style={{ fontSize: 12, color: 'var(--gray-400)' }}>{form.product_ids.length}/{products.length} seleccionados</span>
                  </div>
                  <input placeholder="Buscar producto por nombre..."
                    value={productSearch}
                    onChange={e => { setProductSearch(e.target.value); setActiveCategory(''); }}
                    className="search-bar" style={{ width: '100%', marginBottom: '0.5rem' }} />
                  <div style={{ display: 'flex', flexWrap: 'wrap', gap: 4, marginBottom: '0.5rem' }}>
                    <button type="button" onClick={selectAllProducts}
                      style={btnSmall('#d1fae5', '#047857')}>Todos</button>
                    <button type="button" onClick={() => selectExpiringSoon(7)}
                      style={btnSmall('#fee2e2', '#b91c1c')}>Próximos 7 días</button>
                    <button type="button" onClick={() => selectExpiringSoon(15)}
                      style={btnSmall('#ffedd5', '#c2410c')}>15 días</button>
                    <button type="button" onClick={() => selectExpiringSoon(30)}
                      style={btnSmall('#fef3c7', '#b45309')}>30 días</button>
                    <button type="button" onClick={deselectAllProducts}
                      style={btnSmall('#f3f4f6', '#6b7280')}>Limpiar</button>
                  </div>
                  <div style={{ display: 'flex', gap: 4, marginBottom: '0.5rem', overflowX: 'auto', paddingBottom: 4 }}>
                    <button type="button" onClick={() => setActiveCategory('')}
                      style={activeCategory === '' ? btnSmall('#374151', '#fff') : btnSmall('#f3f4f6', '#6b7280')}>Todas</button>
                    {categories.map(cat => (
                      <button key={cat} type="button" onClick={() => { setActiveCategory(cat); setProductSearch(''); }}
                        style={activeCategory === cat ? btnSmall('#6b7280', '#fff') : btnSmall('#f3f4f6', '#6b7280')}>
                        {cat} {categoryCounts[cat] || 0}
                      </button>
                    ))}
                  </div>
                  <div style={{ border: '1px solid var(--gray-200)', borderRadius: 8, overflow: 'hidden', maxHeight: 280, overflowY: 'auto' }}>
                    {filteredProducts.length === 0 ? (
                      <p style={{ textAlign: 'center', padding: '2rem', color: 'var(--gray-300)', fontSize: 13 }}>Sin resultados</p>
                    ) : (
                      filteredProducts.map(p => {
                        const days = p.expiry_date ? Math.ceil((new Date(p.expiry_date).getTime() - Date.now()) / 86400000) : 0;
                        const sel = form.product_ids.includes(p.id);
                        return (
                          <div key={p.id} onClick={() => toggleProduct(p.id)}
                            style={{
                              display: 'flex', alignItems: 'center', gap: 8, padding: '8px 12px',
                              cursor: 'pointer', borderBottom: '1px solid var(--gray-100)', fontSize: 13,
                              background: sel ? '#f0fdf4' : 'transparent',
                            }}>
                            <input type="checkbox" checked={sel} readOnly style={{ accentColor: '#16a34a', width: 16, height: 16, flexShrink: 0 }} />
                            <div style={{ flex: 1, minWidth: 0 }}>
                              <span style={{ display: 'block', overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap', fontWeight: sel ? 600 : 400 }}>{p.name}</span>
                              <span style={{ fontSize: 12, color: 'var(--gray-400)', fontFamily: 'monospace' }}>{p.code}</span>
                            </div>
                            <div style={{ display: 'flex', alignItems: 'center', gap: 6, flexShrink: 0 }}>
                              {days > 0 ? (
                                <span style={{
                                  fontSize: 12, fontWeight: 700, padding: '2px 6px', borderRadius: 4,
                                  background: days <= 7 ? '#fee2e2' : days <= 15 ? '#ffedd5' : '#fef3c7',
                                  color: days <= 7 ? '#b91c1c' : days <= 15 ? '#c2410c' : '#b45309',
                                }}>{days}d</span>
                              ) : (
                                <span style={{ fontSize: 12, color: '#f87171' }}>Vencido</span>
                              )}
                              <span style={{ fontSize: 12, color: 'var(--gray-400)', textDecoration: 'line-through' }}>{fCur(p.sale_price)}</span>
                              <span style={{ fontSize: 12, fontWeight: 700, color: '#16a34a' }}>{fCur(p.purchase_price)}</span>
                            </div>
                          </div>
                        );
                      })
                    )}
                  </div>
                </div>

                {/* Clientes */}
                <div className="card" style={{ padding: '1rem', marginBottom: 0 }}>
                  <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '0.75rem' }}>
                    <span style={{ fontWeight: 600, fontSize: 14 }}>Clientes</span>
                    <span style={{ fontSize: 12, color: 'var(--gray-400)' }}>{form.customer_ids.length}/{customers.length} seleccionados</span>
                  </div>
                  <input placeholder="Buscar cliente por nombre..."
                    value={customerSearch}
                    onChange={e => setCustomerSearch(e.target.value)}
                    className="search-bar" style={{ width: '100%', marginBottom: '0.5rem' }} />
                  <div style={{ display: 'flex', gap: 8, marginBottom: '0.5rem' }}>
                    <div style={statBox}>{customers.filter(c => c.email).length}<span style={{ display: 'block', fontSize: 11, color: 'var(--gray-400)' }}>Con email</span></div>
                    <div style={statBox}>{customers.filter(c => c.phone).length}<span style={{ display: 'block', fontSize: 11, color: 'var(--gray-400)' }}>Con teléfono</span></div>
                    <div style={statBox}>{customers.length}<span style={{ display: 'block', fontSize: 11, color: 'var(--gray-400)' }}>Total</span></div>
                  </div>
                  <div style={{ display: 'flex', flexWrap: 'wrap', gap: 4, marginBottom: '0.5rem' }}>
                    <button type="button" onClick={selectAllCustomers} style={btnSmall('#d1fae5', '#047857')}>Todos</button>
                    <button type="button" onClick={selectWithEmail} style={btnSmall('#dbeafe', '#1d4ed8')}>Con email</button>
                    <button type="button" onClick={selectWithPhone} style={btnSmall('#d1fae5', '#047857')}>Con teléfono</button>
                    <button type="button" onClick={deselectAllCustomers} style={btnSmall('#f3f4f6', '#6b7280')}>Limpiar</button>
                  </div>
                  <div style={{ border: '1px solid var(--gray-200)', borderRadius: 8, overflow: 'hidden', maxHeight: 280, overflowY: 'auto' }}>
                    {filteredCustomers.length === 0 ? (
                      <p style={{ textAlign: 'center', padding: '2rem', color: 'var(--gray-300)', fontSize: 13 }}>Sin resultados</p>
                    ) : (
                      filteredCustomers.map(c => {
                        const sel = form.customer_ids.includes(c.id);
                        return (
                          <div key={c.id} onClick={() => toggleCustomer(c.id)}
                            style={{
                              display: 'flex', alignItems: 'center', gap: 8, padding: '8px 12px',
                              cursor: 'pointer', borderBottom: '1px solid var(--gray-100)', fontSize: 13,
                              background: sel ? '#eff6ff' : 'transparent',
                            }}>
                            <input type="checkbox" checked={sel} readOnly style={{ accentColor: '#2563eb', width: 16, height: 16, flexShrink: 0 }} />
                            <div style={{ flex: 1, minWidth: 0 }}>
                              <span style={{ display: 'block', overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap', fontWeight: sel ? 600 : 400 }}>{c.fullname}</span>
                            </div>
                            <div style={{ display: 'flex', gap: 4, flexShrink: 0 }}>
                              {c.email && <span style={{ fontSize: 11, background: '#dbeafe', color: '#1d4ed8', padding: '2px 6px', borderRadius: 4, fontWeight: 500 }}>
                                {c.email.length > 20 ? c.email.substring(0, 18) + '…' : c.email}
                              </span>}
                              {c.phone && <span style={{ fontSize: 11, background: '#d1fae5', color: '#047857', padding: '2px 6px', borderRadius: 4, fontWeight: 500 }}>{c.phone}</span>}
                            </div>
                          </div>
                        );
                      })
                    )}
                  </div>
                </div>
              </div>

              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', paddingTop: '1rem', borderTop: '1px solid var(--gray-200)' }}>
                <span style={{ fontSize: 13, color: 'var(--gray-400)' }}>
                  {form.name && form.product_ids.length > 0 && form.customer_ids.length > 0
                    ? '✓ Completo — puedes crear la campaña'
                    : 'Faltan campos por llenar'}
                </span>
                <div style={{ display: 'flex', gap: 8 }}>
                  <button type="button" className="btn-secondary" onClick={() => setShowForm(false)}>Cancelar</button>
                  <button type="submit"
                    disabled={!form.name || form.product_ids.length === 0 || form.customer_ids.length === 0}
                    className="btn-primary" style={{ opacity: (!form.name || form.product_ids.length === 0 || form.customer_ids.length === 0) ? 0.5 : 1 }}>
                    Crear campaña
                  </button>
                </div>
              </div>
            </form>
          </div>
        </div>
      )}

      {/* ── Modal: Detalle de Campaña ── */}
      {showDetail && (
        <div className="modal-overlay" onClick={e => { if (e.target === e.currentTarget) setShowDetail(null); }}
          style={{ alignItems: 'flex-start', paddingTop: '1.5rem', overflowY: 'auto' }}>
          <div className="modal modal-lg" onClick={e => e.stopPropagation()}
            style={{ maxWidth: 900, maxHeight: '95vh' }}>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', marginBottom: '1.5rem' }}>
              <div>
                <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
                  <h2>{detailData.name}</h2>
                  <Badge status={detailData.status} />
                </div>
                <p style={{ color: 'var(--gray-500)', fontSize: 13, marginTop: 2 }}>{detailData.description || 'Sin descripción'}</p>
              </div>
              <button onClick={() => setShowDetail(null)}
                style={{ background: 'none', border: 'none', fontSize: 24, cursor: 'pointer', color: 'var(--gray-400)', padding: '0 4px' }}>&times;</button>
            </div>

            <div style={{ display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gap: '1rem', marginBottom: '1.5rem' }}>
              {[
                { label: 'Tipo', value: OFFER_TYPES.find(t => t.value === detailData.offer_type)?.label || detailData.offer_type },
                ...(detailData.offer_value > 0 ? [{ label: 'Valor', value: detailData.offer_type === 'percentage' ? `${detailData.offer_value}%` : `$${detailData.offer_value}` }] : []),
                { label: 'Vencimiento', value: `${detailData.min_expiry_days}–${detailData.max_expiry_days} días` },
                { label: 'Creada por', value: `${detailData.created_by_name} · ${fDate(detailData.created_at)}` },
              ].map((item, i) => (
                <div key={i} className="detail-grid-item" style={{ background: 'var(--gray-50)', padding: '10px 14px', borderRadius: 8, border: '1px solid var(--gray-100)' }}>
                  <p style={{ fontSize: 11, color: 'var(--gray-500)', fontWeight: 600, textTransform: 'uppercase', marginBottom: 4 }}>{item.label}</p>
                  <p style={{ fontSize: 13, fontWeight: 600 }}>{item.value}</p>
                </div>
              ))}
            </div>

            {detailData.notes && (
              <div style={{ background: 'var(--gray-50)', padding: '10px 14px', borderRadius: 8, border: '1px solid var(--gray-100)', marginBottom: '1.5rem' }}>
                <p style={{ fontSize: 11, color: 'var(--gray-500)', fontWeight: 600, textTransform: 'uppercase', marginBottom: 4 }}>Notas</p>
                <p style={{ fontSize: 13 }}>{detailData.notes}</p>
              </div>
            )}

            <div style={{ marginBottom: '1.5rem' }}>
              <h3 style={{ fontSize: 14, marginBottom: '0.75rem' }}>Productos ({detailData.products?.length || 0})</h3>
              <div style={{ border: '1px solid var(--gray-200)', borderRadius: 8, overflow: 'hidden' }}>
                <table className="table" style={{ boxShadow: 'none' }}>
                  <thead>
                    <tr><th>Producto</th><th style={{ textAlign: 'center' }}>Precio</th><th style={{ textAlign: 'center' }}>Oferta</th><th style={{ textAlign: 'center' }}>Caduca</th></tr>
                  </thead>
                  <tbody>
                    {detailData.products?.map((p: any) => (
                      <tr key={p.id}>
                        <td>{p.product_name}</td>
                        <td style={{ textAlign: 'center', color: 'var(--gray-400)', textDecoration: 'line-through' }}>{fCur(p.original_price)}</td>
                        <td style={{ textAlign: 'center', color: '#16a34a', fontWeight: 700 }}>{fCur(p.offer_price)}</td>
                        <td style={{ textAlign: 'center' }}>{p.expiry_date ? fDate(p.expiry_date) : '—'}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>

            <div style={{ marginBottom: '1.5rem' }}>
              <h3 style={{ fontSize: 14, marginBottom: '0.75rem' }}>Clientes ({detailData.customers?.length || 0})</h3>
              <div style={{ border: '1px solid var(--gray-200)', borderRadius: 8, overflow: 'hidden' }}>
                <table className="table" style={{ boxShadow: 'none' }}>
                  <thead>
                    <tr><th>Nombre</th><th>Email</th><th>Teléfono</th></tr>
                  </thead>
                  <tbody>
                    {detailData.customers?.map((c: any) => (
                      <tr key={c.id}>
                        <td>{c.customer_name || c.fullname}</td>
                        <td style={{ color: '#2563eb' }}>{c.contact_email || c.email || '—'}</td>
                        <td style={{ color: '#059669' }}>{c.contact_phone || c.phone || '—'}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>

            {detailData.logs?.length > 0 && (
              <div style={{ marginBottom: '1.5rem' }}>
                <h3 style={{ fontSize: 14, marginBottom: '0.75rem' }}>Bitácora de envíos ({detailData.logs.length})</h3>
                <div style={{ border: '1px solid var(--gray-200)', borderRadius: 8, overflow: 'hidden' }}>
                  <table className="table" style={{ boxShadow: 'none' }}>
                    <thead>
                      <tr><th>Cliente</th><th style={{ textAlign: 'center' }}>Canal</th><th>Destino</th><th style={{ textAlign: 'center' }}>Estado</th><th style={{ textAlign: 'center' }}>Fecha</th></tr>
                    </thead>
                    <tbody>
                      {detailData.logs?.map((l: any) => (
                        <tr key={l.id}>
                          <td>{l.customer_name || '—'}</td>
                          <td style={{ textAlign: 'center' }}>
                            <span style={{
                              fontSize: 11, fontWeight: 600, padding: '2px 8px', borderRadius: 999,
                              background: l.channel === 'email' ? '#dbeafe' : '#d1fae5',
                              color: l.channel === 'email' ? '#1d4ed8' : '#047857',
                            }}>{l.channel === 'email' ? 'Email' : 'WhatsApp'}</span>
                          </td>
                          <td style={{ fontSize: 13 }}>{l.recipient}</td>
                          <td style={{ textAlign: 'center' }}>
                            {l.status === 'sent' ? <span style={{ color: '#16a34a', fontWeight: 600 }}>✓ Enviado</span> :
                             l.status === 'failed' ? <span style={{ color: '#dc2626', fontWeight: 600 }} title={l.error_message || ''}>✗ Falló</span> :
                             <span style={{ color: 'var(--gray-400)' }}>— Pendiente</span>}
                          </td>
                          <td style={{ textAlign: 'center', fontSize: 13 }}>{l.sent_at ? fDate(l.sent_at) : '—'}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>
            )}

            <div style={{ display: 'flex', gap: 8, flexWrap: 'wrap', paddingTop: '1rem', borderTop: '1px solid var(--gray-200)' }}>
              {detailData.status === 'draft' && (
                <>
                  <button onClick={() => handleSend(detailData.id, ['email'])} disabled={sending}
                    className="btn-primary">📧 {sending ? 'Enviando...' : 'Enviar por Email'}</button>
                  <button onClick={() => handleSend(detailData.id, ['whatsapp'])} disabled={sending}
                    className="btn-primary" style={{ background: '#25D366' }}>💬 {sending ? 'Enviando...' : 'Enviar por WhatsApp'}</button>
                  <button onClick={() => handleSend(detailData.id, ['email', 'whatsapp'])} disabled={sending}
                    className="btn-secondary">📨 Ambos</button>
                  <button onClick={() => handleDelete(detailData.id)}
                    className="btn-danger">Eliminar</button>
                </>
              )}
              <button onClick={() => setShowDetail(null)}
                className="btn-secondary">Cerrar</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

/* ── Helpers for inline button styles ── */
function btnSmall(bg: string, color: string) {
  return {
    fontSize: 12, fontWeight: 600, padding: '4px 12px', borderRadius: 8,
    background: bg, color, border: 'none', cursor: 'pointer',
    whiteSpace: 'nowrap' as const,
  };
}

const statBox: React.CSSProperties = {
  flex: 1, background: '#f9fafb', borderRadius: 8, padding: '8px 12px',
  textAlign: 'center', border: '1px solid var(--gray-100)',
  fontSize: 14, fontWeight: 700, color: '#2563eb',
};
