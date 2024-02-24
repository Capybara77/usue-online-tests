import React from 'react';

type TRoute = {
  path: string;
  element: React.LazyExoticComponent<() => JSX.Element>;
};

const LoginPage = React.lazy(() => import('../pages/LoginPage'));
const UserPage = React.lazy(() => import('../pages/UserPage'));
const TestsPage = React.lazy(() => import('../pages/TestsPage'));
const TrainingTestPage = React.lazy(() => import('../pages/TrainingTestPage'));

export const routes: TRoute[] = [
  {
    path: '/',
    element: LoginPage,
  },
  {
    path: '/user',
    element: UserPage,
  },
  {
    path: '/alltests',
    element: TestsPage,
  },
  {
    path: '/test/:testid',
    element: TrainingTestPage,
  },
];
