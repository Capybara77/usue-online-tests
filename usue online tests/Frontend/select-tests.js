import { useEffect, useState } from "react";
import ReactDOM from "react-dom";
import Select from "react-select";
import customStyles from "./custom-select-styles";

function SelectTests() {
  const [tasks, setTasks] = useState([]);

  function updateForm(selected) {
    const values = selected.map(({ value }) => value).join();
    document.querySelector("input[name=Tests]").value = values;
  }

  useEffect(() => {
    fetch("/api/gettaskslist")
      .then((res) => res.json())
      .then((res) => setTasks(res));
  }, []);

  return (
    <Select
      styles={customStyles}
      closeMenuOnSelect={false}
      isMulti
      options={tasks}
      placeholder="Выберите..."
      noOptionsMessage={() => "Пусто"}
      onChange={updateForm}
    />
  );
}

const selectTestsElement = document.getElementById("select-tests");

if (selectTestsElement) {
  ReactDOM.render(<SelectTests />, selectTestsElement);
}
