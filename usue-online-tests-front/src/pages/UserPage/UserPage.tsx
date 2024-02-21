import { MainLayout } from '@/components/MainLayout';
import { useState, useEffect } from 'react';

export const UserPage = () => {
  const [userName, setUserName] = useState('');

  useEffect(() => {
    fetch('/api/current-user')
      .then((response) => response.json())
      .then((data) => setUserName(data.name))
      .catch((error) => console.error('Ошибка при получении данных:', error));
  }, []);

  return (
    <MainLayout>
      <h2>Профиль пользователя</h2>
      <p>Здравствуйте, {userName}</p>
    </MainLayout>
  );
};
