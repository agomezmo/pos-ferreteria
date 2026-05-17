import { useState, useEffect, FormEvent } from 'react';
import { companyApi } from '../services/api';

export default function Company() {
  const [company, setCompany] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState({ name: '', rfc: '', address: '', phone: '', email: '', logo_url: '' });
  const [success, setSuccess] = useState('');

  useEffect(() => {
    companyApi.get()
      .then(res => {
        const c = res.data;
        setCompany(c);
        setForm({ name: c.name || '', rfc: c.rfc || '', address: c.address || '', phone: c.phone || '', email: c.email || '', logo_url: c.logo_url || '' });
      })
      .catch(() => {})
      .finally(() => setLoading(false));
  }, []);

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setSaving(true);
    setSuccess('');
    try {
      const res = await companyApi.update(form);
      setCompany(res.data);
      setSuccess('Datos guardados correctamente');
    } catch (err: any) {
      alert(err.response?.data?.error?.message || 'Error al guardar');
    } finally {
      setSaving(false);
    }
  };

  if (loading) return <div className="page-loading">Cargando...</div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Configuración de la Empresa</h1>
      </div>

      <div className="card" style={{maxWidth:'600px'}}>
        <form onSubmit={handleSubmit}>
          {success && <div className="success-message">{success}</div>}
          <div className="form-group">
            <label>Nombre de la Empresa *</label>
            <input value={form.name} onChange={e => setForm({...form, name: e.target.value})} required />
          </div>
          <div className="form-group">
            <label>RFC</label>
            <input value={form.rfc} onChange={e => setForm({...form, rfc: e.target.value})} />
          </div>
          <div className="form-group">
            <label>Dirección</label>
            <textarea value={form.address} onChange={e => setForm({...form, address: e.target.value})} />
          </div>
          <div className="form-row">
            <div className="form-group">
              <label>Teléfono</label>
              <input value={form.phone} onChange={e => setForm({...form, phone: e.target.value})} />
            </div>
            <div className="form-group">
              <label>Email</label>
              <input type="email" value={form.email} onChange={e => setForm({...form, email: e.target.value})} />
            </div>
          </div>
          <div className="form-group">
            <label>URL del Logo</label>
            <input value={form.logo_url} onChange={e => setForm({...form, logo_url: e.target.value})} placeholder="https://..." />
          </div>
          <button type="submit" className="btn-primary" disabled={saving}>
            {saving ? 'Guardando...' : 'Guardar Cambios'}
          </button>
        </form>
      </div>

      {company?.name && (
        <div className="card" style={{marginTop:'1.5rem', maxWidth:'600px'}}>
          <h2>Vista Previa</h2>
          <div className="company-preview">
            <h3>{company.name}</h3>
            {company.rfc && <p>RFC: {company.rfc}</p>}
            {company.address && <p>Dirección: {company.address}</p>}
            {company.phone && <p>Tel: {company.phone}</p>}
            {company.email && <p>Email: {company.email}</p>}
          </div>
        </div>
      )}
    </div>
  );
}
