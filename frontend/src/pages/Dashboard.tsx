import { useAuth } from '../context/AuthContext';

export default function Dashboard() {
  const { user } = useAuth();

  return (
    <div className="dashboard">
      <div className="welcome-card">
        <h1>Bienvenido, {user?.fullname || user?.username}</h1>
        <p>Rol: {user?.role}</p>
      </div>
      <div className="dashboard-grid">
        <div className="dashboard-card">
          <h3>Productos</h3>
          <p>Gestión de inventario de ferretería</p>
        </div>
        <div className="dashboard-card">
          <h3>Ventas</h3>
          <p>Registro y consulta de ventas</p>
        </div>
        <div className="dashboard-card">
          <h3>Clientes</h3>
          <p>Administración de clientes</p>
        </div>
        <div className="dashboard-card">
          <h3>Reportes</h3>
          <p>Reportes de ventas e inventario</p>
        </div>
      </div>
    </div>
  );
}
