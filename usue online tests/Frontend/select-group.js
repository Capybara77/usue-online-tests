import ReactDOM from "react-dom";
import Select from "react-select";
import customStyles from "./custom-select-styles";

function SelectGroup({ input, groups }) {
  function updateForm({ label }) {
    input.value = label;
  }

  return (
    <Select
      styles={customStyles}
      noOptionsMessage={() => "Пусто"}
      placeholder="Выберите..."
      options={groups}
      onChange={updateForm}
    />
  );
}

const selects = document.querySelectorAll(".select-group");

if (selects) {
  const inputs = document.querySelectorAll("input[name=group]");
  fetch("/api/getgrouplist")
    .then((res) => res.json())
    .then((res) => {
      const preparedGroups = res.map((group, i) => ({
        label: group,
        value: i,
      }));
      [...selects].map((select, i) => {
        ReactDOM.render(
          <SelectGroup input={inputs[i]} groups={preparedGroups} />,
          select
        );
      });
    });
}
