import { MainLayout } from '@/components/MainLayout';
import { FormEvent, useState } from 'react';
import { useNavigate } from 'react-router-dom';

export const LoginPage = () => {
  const [login, setLogin] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
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
    <MainLayout>
      <form
        onSubmit={handleSubmit}
        className="space-y-4 my-28 max-w-sm mx-auto"
      >
        <div className="prose text-center">
          <h1>Вход</h1>
        </div>
        <input
          placeholder="login"
          value={login}
          onChange={(e) => setLogin(e.target.value)}
          className="input w-full"
        />
        <input
          placeholder="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="input w-full"
        />
        <button type="button" className="btn w-full">
          Войти
        </button>
      </form>
    </MainLayout>
  );
};
