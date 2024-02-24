import { MainLayout } from '@/components/MainLayout';
import { useState, useEffect } from 'react';

type TUserRole = 'User';

type TUser = {
  name: string;
  login: string;
  role: TUserRole;
  group: string;
  isDark: boolean;
};

const UserPage = () => {
  const [user, setUser] = useState<TUser>();

  useEffect(() => {
    const getUser = async () => {
      const response = await fetch('/api/current-user');
      const json = (await response.json()) as TUser;

      if (!json) return;

      setUser(json);

      console.log('🚀 ~ getUser ~ json:', json);
    };

    getUser();
  }, []);

  if (!user) return;

  return (
    <MainLayout>
      <h2>Профиль пользователя</h2>
      <p>Здравствуйте, {user.name}</p>
    </MainLayout>
  );
};

export default UserPage;
