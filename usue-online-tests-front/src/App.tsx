import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import React, { Suspense } from 'react';
import { routes } from './navigation/routes';
import { MainLayout } from './components/MainLayout';

const Loading = () => {
  return (
    <MainLayout>
      <h1>Загрузка...</h1>
    </MainLayout>
  );
};

const SuspenseComponent = ({
  children,
}: {
  children: React.LazyExoticComponent<() => JSX.Element>;
}) => {
  const Component = children;

  return (
    <Suspense fallback={<Loading />}>
      <Component />
    </Suspense>
  );
};

function App() {
  return (
    <Router>
      <Routes>
        {routes.map((route) => {
          return (
            <Route
              key={route.path}
              path={route.path}
              element={<SuspenseComponent>{route.element}</SuspenseComponent>}
            />
          );
        })}
      </Routes>
    </Router>
  );
}

export default App;
