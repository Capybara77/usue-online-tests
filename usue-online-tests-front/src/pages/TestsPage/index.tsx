import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { MainLayout } from '@/components/MainLayout';
import { PageHeader } from '@/components/PageHeader';

interface Test {
  name: string;
  description: string;
  testID: number;
  group: string;
}

const TestsPage = () => {
  const [testsByGroup, setTestsByGroup] = useState<{ [key: string]: Test[] }>({});
  
  useEffect(() => {
    fetch('/api/tests-list')
      .then((response) => response.json())
      .then((data) => {
        // Группируем тесты по группам
        const groupedTests: { [key: string]: Test[] } = {};
        Object.keys(data).forEach((groupName) => {
          groupedTests[groupName] = data[groupName];
        });

        setTestsByGroup(groupedTests);
      })
      .catch((error) => console.error('Ошибка при получении данных:', error));
  }, []);

  return (
    <MainLayout>
      <PageHeader>Доступные тесты</PageHeader>
      {Object.entries(testsByGroup).map(([group, tests]) => (
        <div key={group} className="mb-4">
          <h2 className="text-xl font-bold mb-2">{group}</h2>
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
        </div>
      ))}
    </MainLayout>
  );
};

export default TestsPage;
