import { useEffect, useState } from "react";
import ReactDOM from "react-dom";
import Select from "react-select";

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

function SelectGroup({ input, groups }) {
  function updateForm({ label }) {
    input.value = label;
  }

  return (
    <Select
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

const themeSwitcher = document.getElementById("theme-switcher");

themeSwitcher?.addEventListener("click", () => {
  fetch("/profile/changeusertheme").then(() => {
    if (document.documentElement.dataset.theme === "light") {
      document.documentElement.dataset.theme = "dark";
    } else {
      document.documentElement.dataset.theme = "light";
    }
  });
});

const deleteForm = document.querySelectorAll(".test-presets-delete-form");
const timeLimitedCheckbox = document.querySelector("input[name='timeLimited']");
const minutesToPassInput = document.querySelector(
  "input[name='minutesToPass']"
);

deleteForm?.forEach((form) =>
  form.addEventListener("submit", (e) => {
    if (!confirm("Вы действительно хотите удалить этот шаблон?")) {
      e.preventDefault();
    }
  })
);

timeLimitedCheckbox?.addEventListener("change", function () {
  minutesToPassInput.classList.toggle("hidden");
});
