import ReactDOM from "react-dom";
import Select from "react-select";
import customStyles from "./custom-select-styles";

const options = [
  { label: "АИС-20", value: "1" },
  { label: "АИС-19", value: "2" },
  { label: "ИБ-20-1", value: "3" },
];

function SelectGroup() {
  return (
    <Select
      styles={customStyles}
      noOptionsMessage={() => "Пусто"}
      placeholder="Выберите..."
      options={options}
    />
  );
}

const selectGroupElement = document.getElementById("select-group");

if (selectGroupElement) {
  ReactDOM.render(<SelectGroup />, selectGroupElement);
}
