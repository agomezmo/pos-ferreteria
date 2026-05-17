import { useState } from 'react';
import { Outlet, NavLink, useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

export default function Layout() {
  const { logout, user } = useAuth();
  const navigate = useNavigate();
  const [sidebarOpen, setSidebarOpen] = useState(false);

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <div className="app-layout">
      <nav className={`sidebar ${sidebarOpen ? 'open' : ''}`}>
        <div className="sidebar-header">
          <h2>POS Farmacia</h2>
          <button className="sidebar-close" onClick={() => setSidebarOpen(false)}>×</button>
        </div>
        <div className="sidebar-menu">
          <NavLink to="/" end className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">📊</span> Dashboard
          </NavLink>

          <div className="nav-section">PUNTO DE VENTA</div>
          <NavLink to="/sales/new" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">🛒</span> Nueva Venta
          </NavLink>
          <NavLink to="/sales" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">📋</span> Historial Ventas
          </NavLink>
          <NavLink to="/cash-register" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">💰</span> Caja
          </NavLink>
          <NavLink to="/returns" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">↩️</span> Devoluciones
          </NavLink>
          <NavLink to="/facturas" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">📄</span> Facturación CFDI
          </NavLink>

          <div className="nav-section">INVENTARIO</div>
          <NavLink to="/products" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">💊</span> Productos
          </NavLink>
          <NavLink to="/categories" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">📂</span> Categorías
          </NavLink>
          <NavLink to="/suppliers" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">🚚</span> Proveedores
          </NavLink>
          <NavLink to="/inventory" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">📦</span> Movimientos
          </NavLink>
          <NavLink to="/alerts" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">🔔</span> Alertas
          </NavLink>

          <div className="nav-section">CLIENTES</div>
          <NavLink to="/customers" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">👥</span> Clientes
          </NavLink>
          <NavLink to="/patients" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">🏥</span> Pacientes
          </NavLink>
          <NavLink to="/prescriptions" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">📝</span> Recetas
          </NavLink>

          <div className="nav-section">ADMINISTRACIÓN</div>
          <NavLink to="/expenses" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">💸</span> Gastos
          </NavLink>
          <NavLink to="/reports" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">📈</span> Reportes
          </NavLink>
          <NavLink to="/users" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">👤</span> Usuarios
          </NavLink>
          <NavLink to="/company" className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}
            onClick={() => setSidebarOpen(false)}>
            <span className="nav-icon">⚙️</span> Empresa
          </NavLink>
        </div>
        <div className="sidebar-footer">
          <div className="user-info">
            <span className="user-name">{user?.fullname || user?.username}</span>
            <span className="user-role">{user?.role}</span>
          </div>
          <button onClick={handleLogout} className="btn-logout">Cerrar Sesión</button>
        </div>
      </nav>

      <div className="sidebar-overlay" onClick={() => setSidebarOpen(false)} />
      <button className="menu-toggle" onClick={() => setSidebarOpen(true)}>☰</button>

      <main className="main-content">
        <Outlet />
      </main>
    </div>
  );
}
