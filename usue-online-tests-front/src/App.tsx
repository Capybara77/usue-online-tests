import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import React, { ReactNode, Suspense } from 'react';

const LoginPage = React.lazy(() => import('./pages/LoginPage'));
const UserPage = React.lazy(() => import('./pages/UserPage'));
const TestsPage = React.lazy(() => import('./pages/TestsPage'));
const TrainingTestPage = React.lazy(() => import('./pages/TrainingTestPage'));

const SuspenseComponent = ({ children }: { children: ReactNode }) => {
  return <Suspense fallback={<div>Loading...</div>}>{children}</Suspense>;
};

function App() {
  return (
    <Router>
      <Routes>
        <Route
          path="/"
          element={
            <SuspenseComponent>
              <LoginPage />
            </SuspenseComponent>
          }
        />
        <Route
          path="/user"
          element={
            <SuspenseComponent>
              <UserPage />
            </SuspenseComponent>
          }
        />
        <Route
          path="/alltests"
          element={
            <SuspenseComponent>
              <TestsPage />
            </SuspenseComponent>
          }
        />
        <Route
          path="/test/:testid"
          element={
            <SuspenseComponent>
              <TrainingTestPage />
            </SuspenseComponent>
          }
        />
      </Routes>
    </Router>
  );
}

export default App;
