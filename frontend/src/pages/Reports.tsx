import { useState, useEffect } from 'react';
import { reportsApi, expensesApi } from '../services/api';
import api from '../services/api';

export default function Reports() {
  const [date, setDate] = useState(new Date().toISOString().split('T')[0]);
  const [summary, setSummary] = useState<any>(null);
  const [topProducts, setTopProducts] = useState<any[]>([]);
  const [invStatus, setInvStatus] = useState<any>(null);
  const [expenses, setExpenses] = useState<any[]>([]);
  const [activeTab, setActiveTab] = useState('resumen');

  const tabs = [
    { id: 'resumen', label: 'Resumen Diario' },
    { id: 'productos', label: 'Productos' },
    { id: 'inventario', label: 'Inventario' },
    { id: 'gastos', label: 'Gastos' },
  ];

  useEffect(() => {
    reportsApi.getDailySummary(date).then(r => setSummary(r.data)).catch(() => {});
    reportsApi.getTopProducts({ limit: 15, startDate: date }).then(r => setTopProducts(r.data || [])).catch(() => {});
    reportsApi.getInventoryStatus().then(r => setInvStatus(r.data)).catch(() => {});
    expensesApi.getAll({ limit: 50 }).then(r => setExpenses(Array.isArray(r.data) ? r.data : [])).catch(() => {});
  }, [date]);

  const totalExpenses = expenses.reduce((sum, e) => sum + Number(e.amount), 0);
  const netIncome = summary ? (Number(summary.total_revenue) - totalExpenses) : 0;

  /* Quick chart using CSS bars */
  const maxQty = Math.max(...topProducts.map(p => Number(p.total_quantity)), 1);

  return (
    <div className="page">
      <div className="page-header">
        <h1>Reportes</h1>
        <div className="search-bar" style={{ margin: 0 }}>
          <label>Fecha:</label>
          <input type="date" value={date} onChange={e => setDate(e.target.value)}
            style={{ minWidth: 'auto', flex: 'none' }} />
        </div>
      </div>

      <div className="tabs" style={{ display: 'flex', gap: '0.25rem', marginBottom: '1.5rem' }}>
        {tabs.map(t => (
          <button key={t.id}
            className={activeTab === t.id ? 'btn-primary' : 'btn-secondary'}
            onClick={() => setActiveTab(t.id)}
            style={{ borderRadius: 0 }}>
            {t.label}
          </button>
        ))}
      </div>

      {activeTab === 'resumen' && (
        <>
          <div className="dashboard-grid">
            {summary && (
              <>
                <div className="dashboard-card">
                  <h3>Ventas</h3>
                  <p className="stat-number">{summary.total_sales}</p>
                </div>
                <div className="dashboard-card">
                  <h3>Ingresos</h3>
                  <p className="stat-number">${Number(summary.total_revenue).toFixed(2)}</p>
                </div>
                <div className="dashboard-card">
                  <h3>IVA</h3>
                  <p className="stat-number">${Number(summary.total_tax).toFixed(2)}</p>
                </div>
                <div className="dashboard-card">
                  <h3>Descuentos</h3>
                  <p className="stat-number">${Number(summary.total_discounts || 0).toFixed(2)}</p>
                </div>
                <div className="dashboard-card">
                  <h3>Clientes Atendidos</h3>
                  <p className="stat-number">{summary.unique_customers || 0}</p>
                </div>
                <div className="dashboard-card" style={{ borderLeft: `4px solid ${netIncome >= 0 ? 'var(--success)' : 'var(--danger)'}` }}>
                  <h3>Utilidad Neta</h3>
                  <p className="stat-number">${netIncome.toFixed(2)}</p>
                  <p>Gastos: ${totalExpenses.toFixed(2)}</p>
                </div>
              </>
            )}
          </div>

          {/* Bar chart - Ventas por hora simulated */}
          <div className="card">
            <h2>Productos Más Vendidos</h2>
            <div className="bar-chart">
              {topProducts.slice(0, 10).map((p, i) => (
                <div key={p.id} className="bar-item">
                  <div className="bar-label">
                    <span>{i + 1}. {p.name}</span>
                    <span>{p.total_quantity} uds</span>
                  </div>
                  <div className="bar-track">
                    <div className="bar-fill" style={{
                      width: `${(Number(p.total_quantity) / maxQty) * 100}%`,
                      background: `hsl(${i * 35}, 70%, 50%)`
                    }} />
                  </div>
                </div>
              ))}
            </div>
          </div>
        </>
      )}

      {activeTab === 'productos' && (
        <div className="card">
          <h2>Top Productos por Ingresos</h2>
          <table className="table">
            <thead>
              <tr><th>#</th><th>Producto</th><th>Código</th><th>Cantidad Vendida</th><th>Ingresos</th></tr>
            </thead>
            <tbody>
              {topProducts.map((p: any, i: number) => (
                <tr key={p.id}>
                  <td>{i + 1}</td>
                  <td><strong>{p.name}</strong></td>
                  <td>{p.code}</td>
                  <td>{p.total_quantity}</td>
                  <td><strong>${Number(p.total_revenue).toFixed(2)}</strong></td>
                </tr>
              ))}
              {topProducts.length === 0 && <tr><td colSpan={5} className="empty">Sin datos para esta fecha</td></tr>}
            </tbody>
          </table>
        </div>
      )}

      {activeTab === 'inventario' && invStatus && (
        <div className="dashboard-grid">
          <div className="dashboard-card">
            <h3>Total Productos</h3>
            <p className="stat-number">{invStatus.total_products}</p>
          </div>
          <div className="dashboard-card" style={{ borderLeft: `4px solid ${invStatus.low_stock_count > 0 ? 'var(--warning)' : 'var(--success)'}` }}>
            <h3>Stock Bajo</h3>
            <p className="stat-number">{invStatus.low_stock_count}</p>
          </div>
          <div className="dashboard-card" style={{ borderLeft: `4px solid ${invStatus.out_of_stock_count > 0 ? 'var(--danger)' : 'var(--success)'}` }}>
            <h3>Sin Stock</h3>
            <p className="stat-number">{invStatus.out_of_stock_count}</p>
          </div>
          <div className="dashboard-card">
            <h3>Valor del Inventario</h3>
            <p className="stat-number">${Number(invStatus.inventory_value).toFixed(2)}</p>
          </div>
        </div>
      )}

      {activeTab === 'gastos' && (
        <>
          <div className="dashboard-grid">
            <div className="dashboard-card">
              <h3>Total Gastos</h3>
              <p className="stat-number">${totalExpenses.toFixed(2)}</p>
            </div>
            <div className="dashboard-card">
              <h3>Registros</h3>
              <p className="stat-number">{expenses.length}</p>
            </div>
            {summary && (
              <div className="dashboard-card">
                <h3>Gastos vs Ingresos</h3>
                <p className="stat-number" style={{ fontSize: '1rem !important', color: totalExpenses > 0 ? `var(--warning)` : 'var(--success)' }}>
                  {totalExpenses > 0 ? `${((totalExpenses / Number(summary.total_revenue)) * 100).toFixed(1)}%` : '0%'}
                </p>
                <p>de los ingresos</p>
              </div>
            )}
          </div>

          <div className="card">
            <h2>Listado de Gastos</h2>
            <table className="table">
              <thead>
                <tr><th>Descripción</th><th>Categoría</th><th>Monto</th><th>Fecha</th></tr>
              </thead>
              <tbody>
                {expenses.map(e => (
                  <tr key={e.id}>
                    <td>{e.description}</td>
                    <td><span className="badge badge-info">{e.category}</span></td>
                    <td><strong>${Number(e.amount).toFixed(2)}</strong></td>
                    <td>{new Date(e.createdat).toLocaleDateString()}</td>
                  </tr>
                ))}
                {expenses.length === 0 && <tr><td colSpan={4} className="empty">Sin gastos registrados</td></tr>}
              </tbody>
            </table>
          </div>
        </>
      )}

      <style>{`
        .bar-chart { display: flex; flex-direction: column; gap: 0.5rem; }
        .bar-item { }
        .bar-label { display: flex; justify-content: space-between; font-size: 0.85rem; margin-bottom: 0.2rem; }
        .bar-track { height: 24px; background: var(--gray-100); border-radius: 4px; overflow: hidden; }
        .bar-fill { height: 100%; border-radius: 4px; transition: width 0.5s; min-width: 2%; }
        .tabs .btn-primary, .tabs .btn-secondary { border-radius: 0; }
        .tabs .btn-primary:first-child, .tabs .btn-secondary:first-child { border-radius: 6px 0 0 6px; }
        .tabs .btn-primary:last-child, .tabs .btn-secondary:last-child { border-radius: 0 6px 6px 0; }
      `}</style>
    </div>
  );
}
