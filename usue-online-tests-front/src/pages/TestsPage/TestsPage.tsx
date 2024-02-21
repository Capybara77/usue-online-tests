import { useState, useEffect } from 'react';
import './Tests.css';
import { Link } from 'react-router-dom';
import { MainLayout } from '@/components/MainLayout';

interface Test {
  name: string;
  description: string;
  testID: number;
}

export const TestsPage = () => {
  const [tests, setTests] = useState<Test[]>([]);

  useEffect(() => {
    fetch('/api/tests-list')
      .then((response) => response.json())
      .then((data) => setTests(data))
      .catch((error) => console.error('Ошибка при получении данных:', error));
  }, []);

  return (
    <MainLayout>
      <div className="tests-container">
        <h2>Доступные тесты</h2>
        <div className="tests-list">
          {tests.map((test) => (
            <Link
              key={test.testID}
              to={`/test/${test.testID}`}
              className="test-item"
            >
              <strong>{test.name}</strong>: {test.description}
            </Link>
          ))}
        </div>
      </div>
    </MainLayout>
  );
};
