import { useState, useEffect } from "react";
import './Tests.css'; 
import { Link } from 'react-router-dom';

interface Test {
  name: string;
  description: string;
  testID: number;
}

function Tests() {
  const [tests, setTests] = useState<Test[]>([]);
//   const navigate = useNavigate();

  useEffect(() => {
    fetch("/api/tests-list")
      .then((response) => response.json())
      .then((data) => setTests(data))
      .catch((error) => console.error("Ошибка при получении данных:", error));
  }, []);

  return (
    <div className="tests-container">
      <h2>Доступные тесты</h2>
      <div className="tests-list">
        {tests.map((test) => (
          <Link key={test.testID} to={`/test?testid=${test.testID}`} className="test-item">
            <strong>{test.name}</strong>: {test.description}
          </Link>
        ))}
      </div>
    </div>
  );
}

export default Tests;
