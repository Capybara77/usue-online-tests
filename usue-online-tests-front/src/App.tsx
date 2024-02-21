import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { LoginPage } from './pages/LoginPage';
import { UserPage } from './pages/UserPage';
import { TestsPage } from './pages/TestsPage';
import { TestPage } from './pages/TestPage';

function App() {
  return (
    <Router>
      <Routes>
        <Route path='/' element={<LoginPage />} />
        <Route path='/user' element={<UserPage />} />
        <Route path='/alltests' element={<TestsPage />} />
        <Route path='/test/:testid' element={<TestPage />} />
      </Routes>
    </Router>
  );
}

export default App;
