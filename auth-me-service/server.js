const express = require('express');
const jwt = require('jsonwebtoken');
const { Pool } = require('pg');

const app = express();
const PORT = 3050;

const pool = new Pool({
  host: process.env.DB_HOST || 'localhost',
  port: parseInt(process.env.DB_PORT || '5432'),
  database: process.env.DB_NAME || 'pos_ferreteria',
  user: process.env.DB_USER || 'postgres',
  password: process.env.DB_PASSWORD || 'postgres',
});

const JWT_SECRET = process.env.JWT_SECRET || 'FerreteriaPOS_SuperSecretKey_2024_MustBeLongEnough!';

app.get('/api/auth/me', async (req, res) => {
  const authHeader = req.headers.authorization;
  if (!authHeader || !authHeader.startsWith('Bearer ')) {
    return res.status(401).json({ error: 'Token required' });
  }

  const token = authHeader.substring(7);
  try {
    const decoded = jwt.verify(token, JWT_SECRET);
    const userId = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] 
      || decoded.sub || decoded.nameidentifier;
    
    if (!userId) {
      return res.status(401).json({ error: 'Invalid token claims' });
    }

    const result = await pool.query(
      `SELECT u.id, u.username, u.email, u.fullname, r.name as role
       FROM users u JOIN roles r ON u.roleid = r.id
       WHERE u.id = $1 AND u.isactive = true`,
      [typeof userId === 'string' ? parseInt(userId) : userId]
    );

    if (result.rows.length === 0) {
      return res.status(404).json({ error: 'User not found' });
    }

    const user = result.rows[0];
    res.json({
      id: user.id,
      username: user.username,
      email: user.email,
      fullname: user.fullname,
      role: user.role,
    });
  } catch (err) {
    console.error('Auth error:', err.message);
    return res.status(401).json({ error: 'Invalid token' });
  }
});

app.listen(PORT, '0.0.0.0', () => {
  console.log(`Auth-me service running on port ${PORT}`);
});
