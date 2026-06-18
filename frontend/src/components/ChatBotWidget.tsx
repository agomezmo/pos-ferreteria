import { useState, useRef, useEffect, type FormEvent } from 'react';

const CHATBOT_API = 'http://localhost:3090/api/chatbot';

interface Message {
  role: 'user' | 'assistant';
  content: string;
}

export default function ChatBotWidget() {
  const [open, setOpen] = useState(false);
  const [loggedIn, setLoggedIn] = useState(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [messages, setMessages] = useState<Message[]>([
    {
      role: 'assistant',
      content: '🤖 ¡Bienvenido al asistente de ventas!\n\nAutentícate para buscar productos, consultar precios y más.',
    },
  ]);
  const [input, setInput] = useState('');
  const [token, setToken] = useState<string | null>(null);
  const messagesEndRef = useRef<HTMLDivElement>(null);
  const inputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' });
  }, [messages]);

  useEffect(() => {
    if (open && loggedIn && inputRef.current) {
      inputRef.current.focus();
    }
  }, [open, loggedIn]);

  const apiRequest = async (path: string, options: RequestInit = {}) => {
    const headers: Record<string, string> = {
      'Content-Type': 'application/json',
      ...(options.headers as Record<string, string>),
    };
    if (token) {
      headers['Authorization'] = `Bearer ${token}`;
    }
    const res = await fetch(`${CHATBOT_API}${path}`, { ...options, headers });
    if (!res.ok) {
      const err = await res.json().catch(() => ({ error: { message: 'Error de conexión' } }));
      throw new Error(err.error?.message || `HTTP ${res.status}`);
    }
    return res.json();
  };

  const handleLogin = async (e: FormEvent) => {
    e.preventDefault();
    setError('');
    setLoading(true);
    try {
      const result = await apiRequest('/login', {
        method: 'POST',
        body: JSON.stringify({ username, password }),
      });
      setToken(result.token);
      setLoggedIn(true);
      setMessages((prev) => [
        ...prev,
        { role: 'assistant', content: `✅ ${result.message}\n\n¿En qué puedo ayudarte?` },
      ]);
    } catch (err: any) {
      setError(err.message || 'Error de autenticación');
    } finally {
      setLoading(false);
    }
  };

  const handleSend = async (e: FormEvent) => {
    e.preventDefault();
    const text = input.trim();
    if (!text) return;
    setInput('');
    setMessages((prev) => [...prev, { role: 'user', content: text }]);
    setLoading(true);
    try {
      const result = await apiRequest('/chat', {
        method: 'POST',
        body: JSON.stringify({ message: text }),
      });
      setMessages((prev) => [...prev, { role: 'assistant', content: result.reply }]);
    } catch (err: any) {
      if (err.message.includes('401') || err.message.includes('Sesión')) {
        setLoggedIn(false);
        setToken(null);
        setMessages((prev) => [
          ...prev,
          { role: 'assistant', content: '⏰ Sesión expirada. Inicia sesión de nuevo.' },
        ]);
      } else {
        setMessages((prev) => [
          ...prev,
          { role: 'assistant', content: `❌ Error: ${err.message}` },
        ]);
      }
    } finally {
      setLoading(false);
    }
  };

  const handleLogout = () => {
    setToken(null);
    setLoggedIn(false);
    setUsername('');
    setPassword('');
    setMessages([
      { role: 'assistant', content: '👋 Sesión cerrada.' },
    ]);
  };

  return (
    <div className="chatbot-widget">
      <button
        className="chatbot-toggle"
        onClick={() => setOpen(!open)}
        title="Asistente de ventas"
      >
        {open ? '✕' : '🤖'}
      </button>

      {open && (
        <div className="chatbot-panel">
          <div className="chatbot-panel-header">
            <span>🤖 Asistente de Ventas</span>
            <span className="chatbot-panel-status">En línea</span>
          </div>

          {!loggedIn ? (
            <form className="chatbot-login" onSubmit={handleLogin}>
              <p className="chatbot-login-desc">Ingresa tus credenciales del POS</p>
              <input
                type="text"
                placeholder="Usuario"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                required
                autoFocus
              />
              <input
                type="password"
                placeholder="Contraseña"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />
              {error && <span className="chatbot-error">{error}</span>}
              <button type="submit" disabled={loading}>
                {loading ? 'Autenticando...' : 'Iniciar Sesión'}
              </button>
            </form>
          ) : (
            <>
              <div className="chatbot-messages">
                {messages.map((msg, i) => (
                  <div key={i} className={`chatbot-msg ${msg.role}`}>
                    <span className="chatbot-msg-avatar">
                      {msg.role === 'user' ? '👤' : '🤖'}
                    </span>
                    <div className="chatbot-msg-content">{msg.content}</div>
                  </div>
                ))}
                {loading && (
                  <div className="chatbot-msg assistant">
                    <span className="chatbot-msg-avatar">🤖</span>
                    <div className="chatbot-msg-content">
                      <span className="chatbot-typing">Escribiendo...</span>
                    </div>
                  </div>
                )}
                <div ref={messagesEndRef} />
              </div>

              <form className="chatbot-input" onSubmit={handleSend}>
                <input
                  ref={inputRef}
                  type="text"
                  placeholder="Ej: buscar paracetamol, vender martillo..."
                  value={input}
                  onChange={(e) => setInput(e.target.value)}
                  disabled={loading}
                />
                <button type="submit" disabled={loading || !input.trim()}>
                  Enviar
                </button>
              </form>

              <button className="chatbot-logout-btn" onClick={handleLogout}>
                Cerrar sesión
              </button>
            </>
          )}
        </div>
      )}
    </div>
  );
}
