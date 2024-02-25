import { MainLayout } from '@/components/MainLayout';
import { useParams } from 'react-router-dom';
import { FormEvent, useEffect, useState } from 'react';
import ViewTest from '../ViewTestPage';

const TrainingTestPage = () => {
  const { testid } = useParams();
  const [testText, setTestText] = useState();
  const [hash, setHash] = useState();
  const [checkboxes, setCheckboxes] = useState<string[]>([]);
  const [submitting, setSubmitting] = useState(false);

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setSubmitting(true);

    try {
      const formData = gatherFormData(); // Сбор данных формы
      const requestData = {
        testId: testid,
        hash,
        formData,
      };
      const response = await fetch('/api/check-test-result', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(requestData),
      });

      if (response.ok) {
        console.log('Данные успешно отправлены');
      } else {
        console.error('Ошибка при отправке данных:', response.status);
      }

      console.log('Отправка формы:', formData);
    } catch (error) {
      console.error('Ошибка при отправке данных:', error);
    } finally {
      setSubmitting(false);
    }
  };

  const gatherFormData = () => {
    const formData: { [key: string]: string } = {};
    const inputs = document.querySelectorAll<HTMLInputElement>('input[name]');

    inputs.forEach((input) => {
      if (
        input.value &&
        !Object.prototype.hasOwnProperty.call(formData, input.name)
      ) {
        formData[input.name] = input.value;
      } else if (!Object.prototype.hasOwnProperty.call(formData, input.name)) {
        formData[input.name] = '';
      }
    });

    return formData;
  };

  useEffect(() => {
    const createTest = async () => {
      const userResponse = await fetch('/api/create-test?testid=' + testid);
      const testJson = await userResponse.json();

      setTestText(testJson.text);
      setCheckboxes(testJson.checkBoxes);
      setHash(testJson.hash);
    };
    createTest();
  }, [testid]);

  return (
    <MainLayout>
      <h1 className="italic">Режим тренировки</h1>
      <form onSubmit={handleSubmit} action="/sadf">
        {testText && <ViewTest text={testText} checkBoxes={checkboxes} />}
        <button className="btn btn-neutral" type="submit" disabled={submitting}>
          {submitting ? (
            <>
              <span className="loading loading-spinner"></span>
              Отправка...
            </>
          ) : (
            'Отправить'
          )}
        </button>
      </form>
    </MainLayout>
  );
};

export default TrainingTestPage;
