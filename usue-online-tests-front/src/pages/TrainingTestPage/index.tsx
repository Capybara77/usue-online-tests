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
        hash: hash,
        formData: formData,
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
        // здесь вы можете выполнить какие-либо дополнительные действия после успешной отправки
      } else {
        console.error('Ошибка при отправке данных:', response.status);
        // здесь вы можете обработать ошибку, если необходимо
      }

      console.log('Отправка формы:', formData);
    } catch (error) {
      console.error('Ошибка при отправке данных:', error);
      // здесь вы можете обработать ошибку, если необходимо
    }

    setSubmitting(false);
  };

  const gatherFormData = () => {
    // const formData = {};
    // const inputs = document.querySelectorAll('input[name]'); // Выбор всех input элементов с атрибутом name
    // inputs.forEach((input) => {
    //   // Если у input есть значение и нет записи в formData, сохранить его значение
    //   if (input.value && !formData.hasOwnProperty(input.name)) {
    //     formData[input.name] = input.value;
    //   } else if (!formData.hasOwnProperty(input.name)) {
    //     // Если нет значения и нет записи в formData, добавить пустую строку
    //     formData[input.name] = '';
    //   }
    //   // В случае наличия значения, но запись уже существует, не менять её
    // });
    // return formData;
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

  if (!testText) {
    return null;
  }

  return (
    <MainLayout>
      <h1 className="italic">Режим тренировки</h1>
      <form onSubmit={handleSubmit} action="/sadf">
        <ViewTest text={testText} checkBoxes={checkboxes}></ViewTest>
        <button type="submit" disabled={submitting}>
          {submitting ? 'Отправка...' : 'Отправить'}
        </button>
      </form>
    </MainLayout>
  );
};

export default TrainingTestPage;
