const themeSwitcher = document.querySelector(".theme-switcher");

themeSwitcher?.addEventListener("click", () => {
  fetch("/profile/changeusertheme").then(() => location.reload());
});
