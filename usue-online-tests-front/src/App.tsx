import { useState } from 'react';
import './App.css';

function App() {
  const [login, setLogin] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = async () => {
    const response = await fetch('https://atusue.ru/login/loginin', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: `login=${login}&password=${password}`,
    });

    const json = await response.json()
    console.log("🚀 ~ handleSubmit ~ json:", json)
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
}

export default App;
