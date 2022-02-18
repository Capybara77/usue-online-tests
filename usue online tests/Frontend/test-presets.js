const deleteForm = document.querySelectorAll(".test-presets-delete-form");

deleteForm?.forEach((form) =>
  form.addEventListener("submit", (e) => {
    if (!confirm("Вы действительно хотите удалить этот шаблон?")) {
      e.preventDefault();
    }
  })
);
