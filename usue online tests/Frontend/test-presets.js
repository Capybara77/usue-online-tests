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
