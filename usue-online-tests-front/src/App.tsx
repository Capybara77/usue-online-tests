import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { LoginPage } from './pages/LoginPage';
import { UserPage } from './pages/UserPage';
import { TestsPage } from './pages/TestsPage';
import { TrainingTest } from './pages/Test/TrainingTest';

function App() {
  // TODO lazy loading
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/user" element={<UserPage />} />
        <Route path="/alltests" element={<TestsPage />} />
        <Route path="/test/:testid" element={<TrainingTest />} />
      </Routes>
    </Router>
  );
}

export default App;
