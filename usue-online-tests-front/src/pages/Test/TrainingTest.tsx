import { MainLayout } from '@/components/MainLayout';
import { ViewTest } from './ViewTest';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';

export const TrainingTest = () => {
  const { testid } = useParams();
  const [testText, setTestText] = useState('');
  const [checkboxes, setCheckboxes] = useState<string[]>([]);
  const [submitting, setSubmitting] = useState(false);

  const handleSubmit = async (event) => {
    event.preventDefault();
    setSubmitting(true);

    try {
      const formData = gatherFormData(); // Сбор данных формы
      const response = await fetch('/api/check-test-result', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
      });

      if (response.ok) {
        console.log('Данные успешно отправлены');
        // здесь вы можете выполнить какие-либо дополнительные действия после успешной отправки
      } else {
        console.error('Ошибка при отправке данных:', response.status);
        // здесь вы можете обработать ошибку, если необходимо
      }
    } catch (error) {
      console.error('Ошибка при отправке данных:', error);
      // здесь вы можете обработать ошибку, если необходимо
    }

    setSubmitting(false);
    console.log('Отправка формы:', formData);
  };

  const gatherFormData = () => {
    const formData = {};
    const inputs = document.querySelectorAll('input[name]'); // Выбор всех input элементов с атрибутом name
    inputs.forEach((input) => {
        // Если у input есть значение и нет записи в formData, сохранить его значение
        if (input.value && !formData.hasOwnProperty(input.name)) {
          formData[input.name] = input.value;
        } else if (!formData.hasOwnProperty(input.name)) {
          // Если нет значения и нет записи в formData, добавить пустую строку
          formData[input.name] = '';
        }
        // В случае наличия значения, но запись уже существует, не менять её
      });
    return formData;
  };

  useEffect(() => {
    const createTest = async () => {
      const userResponse = await fetch('/api/create-test?testid=' + testid);
      const testJson = await userResponse.json();

      setTestText(testJson.text);
      setCheckboxes(testJson.checkBoxes);
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
