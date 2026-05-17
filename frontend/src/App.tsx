import { Routes, Route, Navigate } from 'react-router-dom';
import { useAuth } from './context/AuthContext';
import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import Products from './pages/Products';
import Sales from './pages/Sales';
import NewSale from './pages/NewSale';
import Customers from './pages/Customers';
import Categories from './pages/Categories';
import Suppliers from './pages/Suppliers';
import Reports from './pages/Reports';
import CashRegister from './pages/CashRegister';
import Returns from './pages/Returns';
import Expenses from './pages/Expenses';
import Patients from './pages/Patients';
import Prescriptions from './pages/Prescriptions';
import Alerts from './pages/Alerts';
import Inventory from './pages/Inventory';
import Company from './pages/Company';
import Users from './pages/Users';
import Facturas from './pages/Facturas';
import Layout from './components/Layout';

function PrivateRoute({ children }: { children: React.ReactNode }) {
  const { isAuthenticated } = useAuth();
  return isAuthenticated ? <>{children}</> : <Navigate to="/login" />;
}

export default function App() {
  const { isAuthenticated } = useAuth();
  return (
    <Routes>
      <Route path="/login" element={isAuthenticated ? <Navigate to="/" /> : <Login />} />
      <Route path="/" element={<PrivateRoute><Layout /></PrivateRoute>}>
        <Route index element={<Dashboard />} />
        <Route path="products" element={<Products />} />
        <Route path="sales" element={<Sales />} />
        <Route path="sales/new" element={<NewSale />} />
        <Route path="customers" element={<Customers />} />
        <Route path="categories" element={<Categories />} />
        <Route path="suppliers" element={<Suppliers />} />
        <Route path="reports" element={<Reports />} />
        <Route path="cash-register" element={<CashRegister />} />
        <Route path="returns" element={<Returns />} />
        <Route path="expenses" element={<Expenses />} />
        <Route path="patients" element={<Patients />} />
        <Route path="prescriptions" element={<Prescriptions />} />
        <Route path="alerts" element={<Alerts />} />
        <Route path="inventory" element={<Inventory />} />
        <Route path="company" element={<Company />} />
        <Route path="users" element={<Users />} />
        <Route path="facturas" element={<Facturas />} />
      </Route>
    </Routes>
  );
}
