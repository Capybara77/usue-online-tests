import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import React from 'react';

const LoginPage = React.lazy(() => import('./pages/LoginPage'));
const UserPage = React.lazy(() => import('./pages/UserPage'));
const TestsPage = React.lazy(() => import('./pages/TestsPage'));
const TrainingTestPage = React.lazy(() => import('./pages/TrainingTestPage'));

function App() {
  // TODO lazy loading
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/user" element={<UserPage />} />
        <Route path="/alltests" element={<TestsPage />} />
        <Route path="/test/:testid" element={<TrainingTestPage />} />
      </Routes>
    </Router>
  );
}

export default App;
