import ReactDOM from "react-dom";
import Select from "react-select";
import customStyles from "./custom-select-styles";

const options = [
  { label: "Аналитическая геометрия", value: "1" },
  { label: "Дифференцирование", value: "2" },
  { label: "Умножение матриц на макроуровне", value: "3" },
];

function SelectTests() {
  return (
    <Select
      styles={customStyles}
      closeMenuOnSelect={false}
      isMulti
      options={options}
      placeholder="Выберите..."
      noOptionsMessage={() => "Пусто"}
    />
  );
}

const selectTestsElement = document.getElementById("select-tests");

if (selectTestsElement) {
  ReactDOM.render(<SelectTests />, selectTestsElement);
}
