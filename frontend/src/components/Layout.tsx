import { Outlet, NavLink, useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

export default function Layout() {
  const { logout, user } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <div className="app-layout">
      <nav className="sidebar">
        <div className="sidebar-header">
          <h2>POS Ferretería</h2>
        </div>
        <div className="sidebar-menu">
          <NavLink to="/" end className={({ isActive }) => isActive ? 'nav-item active' : 'nav-item'}>Dashboard</NavLink>
          <NavLink to="/products" className="nav-item">Productos</NavLink>
          <NavLink to="/sales" className="nav-item">Ventas</NavLink>
          <NavLink to="/customers" className="nav-item">Clientes</NavLink>
          <NavLink to="/categories" className="nav-item">Categorías</NavLink>
          <NavLink to="/suppliers" className="nav-item">Proveedores</NavLink>
          <NavLink to="/reports" className="nav-item">Reportes</NavLink>
        </div>
        <div className="sidebar-footer">
          <span>{user?.fullname || user?.username}</span>
          <button onClick={handleLogout} className="btn-logout">Cerrar Sesión</button>
        </div>
      </nav>
      <main className="main-content">
        <Outlet />
      </main>
    </div>
  );
}
