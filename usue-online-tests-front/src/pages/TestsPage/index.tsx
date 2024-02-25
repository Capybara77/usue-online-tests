import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { MainLayout } from '@/components/MainLayout';
import { PageHeader } from '@/components/PageHeader';

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
      <PageHeader>Доступные тесты</PageHeader>
      <div className="grid grid-cols-2 gap-[10px] p-0">
        {tests.map((test) => (
          <Link
            key={test.testID}
            to={`/test/${test.testID}`}
            className="text-lg bg-base-100 px-[32px] py-[25px] rounded-md transition-all shadow-sm hover:-translate-y-1 hover:shadow"
          >
            <strong>{test.name}</strong>: {test.description}
          </Link>
        ))}
      </div>
    </MainLayout>
  );
};

export default TestsPage;
