import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export const LoginPage = () => {
  const [login, setLogin] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async () => {
    const response = await fetch('/api/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: `login=${login}&password=${password}`,
    });

    const json = await response.json();

    if (json === true) {
      const setCookieHeader = response.headers.get('Set-Cookie');
      if (setCookieHeader) {
        document.cookie = setCookieHeader;
      }
      navigate('/user');
    }
  };

  return (
    <div>
      <input
        placeholder='login'
        value={login}
        onChange={(e) => setLogin(e.target.value)}
      />
      <input
        placeholder='password'
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <button onClick={handleSubmit}>Войти</button>
    </div>
  );
};
