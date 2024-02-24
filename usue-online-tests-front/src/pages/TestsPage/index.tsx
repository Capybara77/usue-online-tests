import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { MainLayout } from '@/components/MainLayout';

interface Test {
  name: string;
  description: string;
  testID: number;
}

const TestsPage = () => {
  const [tests, setTests] = useState<Test[]>([]);

  useEffect(() => {
    fetch('/api/tests-list')
      .then((response) => response.json())
      .then((data) => setTests(data))
      .catch((error) => console.error('Ошибка при получении данных:', error));
  }, []);

  return (
    <MainLayout>
      <div className="max-w-[600px] m-auto">
        <h2>Доступные тесты</h2>
        <div className="grid grid-cols-2 gap-[10px] p-0">
          {tests.map((test) => (
            <Link
              key={test.testID}
              to={`/test/${test.testID}`}
              className="p-[10px] rounded-md transition-all shadow-sm hover:-translate-y-1 hover:shadow"
            >
              <strong>{test.name}</strong>: {test.description}
            </Link>
          ))}
        </div>
      </div>
    </MainLayout>
  );
};

export default TestsPage;
