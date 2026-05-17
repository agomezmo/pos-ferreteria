import { useState, useEffect, useRef, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import { productsApi, customersApi, salesApi } from '../services/api';

interface CartItem {
  productid: number;
  code: string;
  name: string;
  quantity: number;
  unitprice: number;
  subtotal: number;
  requiresprescription: boolean;
}

interface Customer { id: number; fullname: string; rfc: string; }

export default function NewSale() {
  const navigate = useNavigate();
  const [cart, setCart] = useState<CartItem[]>([]);
  const [search, setSearch] = useState('');
  const [products, setProducts] = useState<any[]>([]);
  const [showResults, setShowResults] = useState(false);
  const [customer, setCustomer] = useState<Customer | null>(null);
  const [customerSearch, setCustomerSearch] = useState('');
  const [customerResults, setCustomerResults] = useState<Customer[]>([]);
  const [showCustomerSearch, setShowCustomerSearch] = useState(false);
  const [paymentMethod, setPaymentMethod] = useState('Efectivo');
  const [amountReceived, setAmountReceived] = useState(0);
  const [discount, setDiscount] = useState(0);
  const [notes, setNotes] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState(false);
  const searchRef = useRef<HTMLInputElement>(null);
  const barcodeInput = useRef<HTMLInputElement>(null);
  const [barcode, setBarcode] = useState('');

  const subtotal = cart.reduce((sum, item) => sum + item.subtotal, 0);
  const tax = cart.reduce((sum, item) => sum + (item.subtotal * 0.16), 0);
  const total = subtotal + tax - discount;
  const change = Math.max(0, amountReceived - total);

  const searchProducts = useCallback(async (q: string) => {
    if (!q.trim()) { setProducts([]); return; }
    try {
      const res = await productsApi.getAll({ search: q, limit: 20 });
      setProducts(res.data.products || []);
      setShowResults(true);
    } catch { setProducts([]); }
  }, []);

  useEffect(() => {
    const timer = setTimeout(() => searchProducts(search), 300);
    return () => clearTimeout(timer);
  }, [search, searchProducts]);

  const searchCustomers = async (q: string) => {
    if (!q.trim()) { setCustomerResults([]); return; }
    try {
      const res = await customersApi.getAll({ search: q });
      setCustomerResults(res.data || []);
    } catch { setCustomerResults([]); }
  };

  useEffect(() => {
    const timer = setTimeout(() => searchCustomers(customerSearch), 300);
    return () => clearTimeout(timer);
  }, [customerSearch]);

  const addToCart = (product: any) => {
    setCart(prev => {
      const existing = prev.find(item => item.productid === product.id);
      if (existing) {
        return prev.map(item =>
          item.productid === product.id
            ? { ...item, quantity: item.quantity + 1, subtotal: (item.quantity + 1) * item.unitprice }
            : item
        );
      }
      return [...prev, {
        productid: product.id,
        code: product.code,
        name: product.name,
        quantity: 1,
        unitprice: product.saleprice,
        subtotal: product.saleprice,
        requiresprescription: product.requiresprescription,
      }];
    });
    setSearch('');
    setShowResults(false);
    searchRef.current?.focus();
  };

  const handleBarcode = async () => {
    if (!barcode.trim()) return;
    try {
      const res = await productsApi.getAll({ search: barcode, limit: 1 });
      const product = res.data.products?.[0];
      if (product) {
        addToCart(product);
        setBarcode('');
        barcodeInput.current?.focus();
      }
    } catch {}
  };

  const updateQuantity = (productId: number, qty: number) => {
    if (qty <= 0) {
      setCart(prev => prev.filter(item => item.productid !== productId));
      return;
    }
    setCart(prev => prev.map(item =>
      item.productid === productId
        ? { ...item, quantity: qty, subtotal: qty * item.unitprice }
        : item
    ));
  };

  const handleCheckout = async () => {
    if (cart.length === 0) { setError('Agrega productos al carrito'); return; }
    setLoading(true);
    setError('');
    try {
      await salesApi.create({
        customerid: customer?.id || undefined,
        items: cart.map(item => ({
          productid: item.productid,
          quantity: item.quantity,
          unitprice: item.unitprice,
        })),
        paymentmethod: paymentMethod,
        amountreceived: amountReceived,
        discount,
        notes,
      });
      setSuccess(true);
      setTimeout(() => {
        navigate('/sales');
      }, 2000);
    } catch (err: any) {
      setError(err.response?.data?.error?.message || 'Error al procesar la venta');
    } finally {
      setLoading(false);
    }
  };

  if (success) {
    return (
      <div className="page">
        <div className="success-screen">
          <div className="success-icon">✅</div>
          <h1>Venta Registrada Exitosamente</h1>
          <p>Redirigiendo al historial de ventas...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="page pos-page">
      <div className="page-header">
        <h1>Nueva Venta</h1>
        <button className="btn-secondary" onClick={() => navigate('/sales')}>
          ← Historial
        </button>
      </div>

      <div className="pos-layout">
        <div className="pos-left">
          {/* Búsqueda por código de barras */}
          <div className="barcode-section">
            <label>Código de Barras</label>
            <div className="barcode-input">
              <input
                ref={barcodeInput}
                type="text"
                value={barcode}
                onChange={e => setBarcode(e.target.value)}
                onKeyDown={e => e.key === 'Enter' && handleBarcode()}
                placeholder="Escanea o escribe código de barras..."
                autoFocus
              />
              <button className="btn-primary" onClick={handleBarcode}>+</button>
            </div>
          </div>

          {/* Buscador de productos */}
          <div className="product-search-section">
            <label>Buscar Producto</label>
            <input
              ref={searchRef}
              type="text"
              value={search}
              onChange={e => setSearch(e.target.value)}
              placeholder="Nombre, código o SKU..."
              onFocus={() => setShowResults(true)}
            />
            {showResults && products.length > 0 && (
              <div className="search-results">
                {products.map(p => (
                  <div key={p.id} className="search-result-item" onClick={() => addToCart(p)}>
                    <div className="srp-info">
                      <strong>{p.name}</strong>
                      <span className="srp-code">{p.code} {p.barcode ? `| ${p.barcode}` : ''}</span>
                    </div>
                    <div className="srp-details">
                      <span className="srp-price">${Number(p.saleprice).toFixed(2)}</span>
                      <span className="srp-stock">Stock: {p.stock} {p.unit}</span>
                    </div>
                  </div>
                ))}
              </div>
            )}
          </div>

          {/* Seleccionar cliente */}
          <div className="customer-section">
            <label>Cliente</label>
            {customer ? (
              <div className="selected-customer">
                <span>{customer.fullname} {customer.rfc ? `(${customer.rfc})` : ''}</span>
                <button className="btn-sm" onClick={() => setCustomer(null)}>Cambiar</button>
              </div>
            ) : (
              <div className="customer-search">
                <input
                  type="text"
                  value={customerSearch}
                  onChange={e => setCustomerSearch(e.target.value)}
                  placeholder="Buscar cliente por nombre o RFC..."
                  onFocus={() => setShowCustomerSearch(true)}
                />
                {showCustomerSearch && customerResults.length > 0 && (
                  <div className="search-results">
                    {customerResults.map(c => (
                      <div key={c.id} className="search-result-item"
                        onClick={() => { setCustomer(c); setShowCustomerSearch(false); setCustomerSearch(''); }}>
                        <strong>{c.fullname}</strong>
                        <span>{c.rfc || 'Sin RFC'}</span>
                      </div>
                    ))}
                  </div>
                )}
                <button className="btn-sm" onClick={() => navigate('/customers')}>+ Nuevo</button>
              </div>
            )}
          </div>
        </div>

        <div className="pos-right">
          {/* Carrito */}
          <div className="cart-section">
            <h2>Carrito ({cart.length} items)</h2>
            <div className="cart-items">
              {cart.length === 0 ? (
                <div className="empty-cart">
                  <p>Agrega productos al carrito</p>
                  <p className="hint">Usa el código de barras o busca productos</p>
                </div>
              ) : (
                <table className="table cart-table">
                  <thead>
                    <tr>
                      <th>Producto</th>
                      <th>Código</th>
                      <th>Cant</th>
                      <th>Precio</th>
                      <th>Subtotal</th>
                      <th></th>
                    </tr>
                  </thead>
                  <tbody>
                    {cart.map(item => (
                      <tr key={item.productid}>
                        <td>{item.name}</td>
                        <td>{item.code}</td>
                        <td>
                          <div className="qty-control">
                            <button onClick={() => updateQuantity(item.productid, item.quantity - 1)}>-</button>
                            <input
                              type="number"
                              value={item.quantity}
                              min="1"
                              onChange={e => updateQuantity(item.productid, parseInt(e.target.value) || 1)}
                            />
                            <button onClick={() => updateQuantity(item.productid, item.quantity + 1)}>+</button>
                          </div>
                        </td>
                        <td>${Number(item.unitprice).toFixed(2)}</td>
                        <td>${Number(item.subtotal).toFixed(2)}</td>
                        <td>
                          <button className="btn-sm btn-danger" onClick={() => updateQuantity(item.productid, 0)}>×</button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              )}
            </div>
          </div>

          {/* Resumen y pago */}
          <div className="checkout-section">
            <div className="totals">
              <div className="total-row"><span>Subtotal:</span><span>${subtotal.toFixed(2)}</span></div>
              <div className="total-row"><span>IVA (16%):</span><span>${tax.toFixed(2)}</span></div>
              <div className="total-row">
                <span>Descuento:</span>
                <input type="number" value={discount} min="0" step="0.01"
                  onChange={e => setDiscount(parseFloat(e.target.value) || 0)} />
              </div>
              <div className="total-row total-final"><span>Total:</span><span>${total.toFixed(2)}</span></div>
            </div>

            <div className="payment-section">
              <div className="form-group">
                <label>Método de Pago</label>
                <select value={paymentMethod} onChange={e => setPaymentMethod(e.target.value)}>
                  <option value="Efectivo">Efectivo</option>
                  <option value="Tarjeta">Tarjeta</option>
                  <option value="Transferencia">Transferencia</option>
                  <option value="Cheque">Cheque</option>
                  <option value="Credito">Crédito</option>
                </select>
              </div>
              {paymentMethod === 'Efectivo' && (
                <div className="form-group">
                  <label>Recibí</label>
                  <input type="number" value={amountReceived} min="0" step="0.01"
                    onChange={e => setAmountReceived(parseFloat(e.target.value) || 0)} />
                  {amountReceived > 0 && (
                    <div className="change-display">Cambio: ${change.toFixed(2)}</div>
                  )}
                </div>
              )}
              <div className="form-group">
                <label>Notas</label>
                <textarea value={notes} onChange={e => setNotes(e.target.value)} rows={2} />
              </div>
            </div>

            {error && <div className="error-message">{error}</div>}

            <button
              className="btn-primary btn-checkout"
              onClick={handleCheckout}
              disabled={loading || cart.length === 0}
            >
              {loading ? 'Procesando...' : `Cobrar $${total.toFixed(2)}`}
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
