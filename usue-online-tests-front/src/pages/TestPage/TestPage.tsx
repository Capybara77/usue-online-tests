import { MainLayout } from '@/components/MainLayout';
import { useParams } from 'react-router-dom';

export const TestPage = () => {
  const { testid } = useParams();

  return (
    <MainLayout>
      <div>
        <h1>TEST</h1>
        <p>Test ID: {testid}</p>
      </div>
    </MainLayout>
  );
};
