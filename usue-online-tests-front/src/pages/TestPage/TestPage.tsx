import { useParams } from 'react-router-dom';

export const TestPage = () => {
  const { testid } = useParams();

  return (
    <div>
      <h1>TEST</h1>
      <p>Test ID: {testid}</p>
    </div>
  );
};
