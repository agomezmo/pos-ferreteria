import { useState, useEffect } from 'react';
import { returnsApi, salesApi } from '../services/api';

export default function Returns() {
  const [returns, setReturns] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [detail, setDetail] = useState<any>(null);
  const [showCreate, setShowCreate] = useState(false);
  const [saleSearch, setSaleSearch] = useState('');
  const [saleResults, setSaleResults] = useState<any[]>([]);
  const [selectedSale, setSelectedSale] = useState<any>(null);
  const [returnItems, setReturnItems] = useState<any[]>([]);
  const [reason, setReason] = useState('');
  const [error, setError] = useState('');
  const [searching, setSearching] = useState(false);

  const fetchReturns = async () => {
    try {
      const res = await returnsApi.getAll();
      setReturns(Array.isArray(res.data) ? res.data : []);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  };

  useEffect(() => { fetchReturns(); }, []);

  const searchSales = async () => {
    if (!saleSearch.trim()) return;
    setSearching(true);
    try {
      const res = await salesApi.getAll({ search: saleSearch, limit: 10 });
      setSaleResults(res.data?.sales || res.data || []);
    } catch { setSaleResults([]); }
    finally { setSearching(false); }
  };

  const selectSale = async (sale: any) => {
    setSelectedSale(sale);
    setSaleSearch('');
    setSaleResults([]);
    try {
      const res = await salesApi.getById(sale.id);
      const items = res.data?.items || [];
      setReturnItems(items.map((i: any) => {
        const id = i.id || i.productId;
        const qty = i.quantity || i.quantity;
        const unitPrice = i.unitprice ?? i.unitPrice ?? 0;
        const productName = i.product_name || i.productName || i.name || '';
        return { ...i, id, quantity: qty, unitprice: unitPrice, product_name: productName, return_qty: 0 };
      }));
    } catch { setReturnItems([]); }
  };

  const toggleItem = (item: any) => {
    setReturnItems(prev =>
      prev.map(i =>
        i.id === item.id ? { ...i, return_qty: i.return_qty > 0 ? 0 : i.quantity } : i
      )
    );
  };

  const updateReturnQty = (itemId: number, qty: number) => {
    setReturnItems(prev =>
      prev.map(i =>
        i.id === itemId ? { ...i, return_qty: Math.min(Math.max(0, qty), i.quantity) } : i
      )
    );
  };

  const handleCreateReturn = async () => {
    if (!selectedSale) { setError('Selecciona una venta'); return; }
    if (!reason.trim()) { setError('Indica el motivo de la devolución'); return; }
    const items = returnItems.filter(i => i.return_qty > 0);
    if (items.length === 0) { setError('Selecciona al menos un artículo'); return; }
    setError('');
    try {
      await returnsApi.create({
        saleId: selectedSale.id,
        reason: reason.trim(),
        items: items.map(i => ({
          productId: i.productid ?? i.productId ?? i.productid,
          quantity: i.return_qty,
          unitPrice: Number(i.unitprice),
        })),
      });
      setShowCreate(false);
      setSelectedSale(null);
      setReturnItems([]);
      setReason('');
      fetchReturns();
    } catch (err: any) {
      setError(err.response?.data?.error?.message || 'Error al crear devolución');
    }
  };

  const viewDetail = async (id: number) => {
    try {
      const res = await returnsApi.getById(id);
      setDetail(res.data);
    } catch (err) { console.error(err); }
  };

  const totalReturn = returnItems.reduce((s, i) => s + i.return_qty * Number(i.unitprice || 0), 0);

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Devoluciones</h1>
        <button className="btn-primary" onClick={() => { setShowCreate(true); setError(''); }}>+ Nueva Devolución</button>
      </div>
      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th>#</th><th>Venta</th><th>Usuario</th><th>Motivo</th><th>Total</th><th>Fecha</th><th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {returns.map((r, i) => (
              <tr key={r.id}>
                <td>{i + 1}</td>
                <td>{r.receiptNumber || r.receiptnumber || r.saleId}</td>
                <td>{r.userName || r.user_name}</td>
                <td>{r.reason}</td>
                <td>${Number(r.total).toFixed(2)}</td>
                <td>{new Date(r.createdAt || r.createdat).toLocaleDateString()}</td>
                <td><button className="btn-sm" onClick={() => viewDetail(r.id)}>Ver</button></td>
              </tr>
            ))}
            {returns.length === 0 && <tr><td colSpan={7} className="empty">No hay devoluciones</td></tr>}
          </tbody>
        </table>
      </div>

      {/* Create Return Modal */}
      {showCreate && (
        <div className="modal-overlay" onClick={() => setShowCreate(false)}>
          <div className="modal modal-lg" onClick={e => e.stopPropagation()}>
            <h2>Nueva Devolución</h2>
            {error && <div className="error-message">{error}</div>}

            {!selectedSale ? (
              <div>
                <div className="form-group">
                  <label>Buscar Venta (folio o ID)</label>
                  <div style={{ display: 'flex', gap: '0.5rem' }}>
                    <input value={saleSearch} onChange={e => setSaleSearch(e.target.value)}
                      onKeyDown={e => e.key === 'Enter' && searchSales()}
                      placeholder="Folio de venta..." />
                    <button className="btn-primary" onClick={searchSales} disabled={searching}>
                      {searching ? 'Buscando...' : 'Buscar'}
                    </button>
                  </div>
                </div>
                {saleResults.length > 0 && (
                  <div className="search-results" style={{ maxHeight: '200px', overflowY: 'auto', border: '1px solid var(--gray-200)', borderRadius: '6px' }}>
                    {saleResults.map(s => (
                      <div key={s.id} className="search-result-item" onClick={() => selectSale(s)}
                        style={{ cursor: 'pointer', padding: '0.5rem', borderBottom: '1px solid var(--gray-100)' }}>
                        <strong>{s.receiptNumber || s.receiptnumber || '#' + s.id}</strong>
                        <span style={{ marginLeft: '1rem' }}>${Number(s.total).toFixed(2)}</span>
                        <span style={{ marginLeft: '1rem', fontSize: '0.85rem', color: 'var(--gray-500)' }}>
                          {new Date(s.createdAt || s.createdat).toLocaleDateString()}
                        </span>
                      </div>
                    ))}
                  </div>
                )}
                {saleResults.length === 0 && saleSearch && !searching && (
                  <p style={{ color: 'var(--gray-400)', marginTop: '0.5rem' }}>Sin resultados</p>
                )}
              </div>
            ) : (
              <div>
                <div className="detail-grid" style={{ marginBottom: '1rem' }}>
                  <div><strong>Venta:</strong> {selectedSale.receiptNumber || selectedSale.receiptnumber || '#' + selectedSale.id}</div>
                  <div><strong>Total:</strong> ${Number(selectedSale.total).toFixed(2)}</div>
                  <div><strong>Fecha:</strong> {new Date(selectedSale.createdAt || selectedSale.createdat).toLocaleDateString()}</div>
                  <button className="btn-sm" onClick={() => { setSelectedSale(null); setReturnItems([]); }}>Cambiar venta</button>
                </div>

                <div className="form-group">
                  <label>Motivo de Devolución *</label>
                  <textarea value={reason} onChange={e => setReason(e.target.value)}
                    placeholder="Ej: Producto dañado, error en el pedido..."
                    rows={2} required />
                </div>

                <h3 style={{ margin: '1rem 0 0.5rem' }}>Artículos de la Venta</h3>
                <table className="table">
                  <thead>
                    <tr><th>Producto</th><th>Cant.</th><th>Precio</th><th>Devolver</th><th>Subtotal</th></tr>
                  </thead>
                  <tbody>
                    {returnItems.map((item: any) => (
                      <tr key={item.id}>
                        <td>{item.product_name || item.productName || item.name}</td>
                        <td>{item.quantity}</td>
                        <td>${Number(item.unitprice).toFixed(2)}</td>
                        <td>
                          <div style={{ display: 'flex', alignItems: 'center', gap: '0.25rem' }}>
                            <input type="checkbox" checked={item.return_qty > 0}
                              onChange={() => toggleItem(item)} />
                            {item.return_qty > 0 && (
                              <input type="number" value={item.return_qty} min="1" max={item.quantity}
                                onChange={e => updateReturnQty(item.id, parseInt(e.target.value) || 0)}
                                style={{ width: '60px' }} />
                            )}
                          </div>
                        </td>
                        <td>{item.return_qty > 0 ? `$${(item.return_qty * Number(item.unitprice)).toFixed(2)}` : '-'}</td>
                      </tr>
                    ))}
                  </tbody>
                  <tfoot>
                    <tr><td colSpan={4}><strong>Total a devolver</strong></td>
                      <td><strong>${totalReturn.toFixed(2)}</strong></td></tr>
                  </tfoot>
                </table>

                <div className="modal-actions" style={{ marginTop: '1rem' }}>
                  <button className="btn-secondary" onClick={() => { setShowCreate(false); setSelectedSale(null); setReturnItems([]); }}>Cancelar</button>
                  <button className="btn-danger" onClick={handleCreateReturn}>Registrar Devolución</button>
                </div>
              </div>
            )}
          </div>
        </div>
      )}

      {/* Detail Modal */}
      {detail && (
        <div className="modal-overlay" onClick={() => setDetail(null)}>
          <div className="modal modal-lg" onClick={e => e.stopPropagation()}>
            <h2>Devolución de Venta: {detail.receiptNumber || detail.receiptnumber || '#' + detail.saleId}</h2>
            <div className="detail-grid">
              <div><strong>Usuario:</strong> {detail.userName || detail.user_name}</div>
              <div><strong>Motivo:</strong> {detail.reason}</div>
              <div><strong>Fecha:</strong> {new Date(detail.createdAt || detail.createdat).toLocaleString()}</div>
            </div>
            <h3 style={{marginTop:'1rem'}}>Artículos Devueltos</h3>
            <table className="table">
              <thead>
                <tr><th>Producto</th><th>Cant</th><th>Precio</th><th>Subtotal</th></tr>
              </thead>
              <tbody>
                {(detail.items || []).map((i: any) => (
                  <tr key={i.id}>
                    <td>{i.productName || i.product_name}</td>
                    <td>{i.quantity}</td>
                    <td>${Number(i.unitPrice ?? i.unitprice).toFixed(2)}</td>
                    <td>${Number(i.subtotal).toFixed(2)}</td>
                  </tr>
                ))}
              </tbody>
              <tfoot>
                <tr><td colSpan={3}><strong>Total</strong></td><td><strong>${Number(detail.total).toFixed(2)}</strong></td></tr>
              </tfoot>
            </table>
            <div className="modal-actions">
              <button className="btn-secondary" onClick={() => setDetail(null)}>Cerrar</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
