import { useState, useEffect } from "react";

interface Test {
  name: string;
  description: string;
  testID: number;
}

function Tests() {
  const [tests, setTests] = useState<Test[]>([]);


  useEffect(() => {
    fetch("/api/tests-list")
      .then((response) => response.json())
      .then((data) => setTests(data))
      .catch((error) => console.error("Ошибка при получении данных:", error));
  }, []);

  return (
    <div>
      <h2>Доступные тесты</h2>
      <ul>
        {tests.map((test) => (
          <li key={test.testID}>
            <strong>{test.name}</strong>: {test.description}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default Tests;
