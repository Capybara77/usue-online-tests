const themeSwitcher = document.querySelector(".theme-switcher");
const mobileThemeSwitcher = document.querySelector(".mobile-theme-switcher");

themeSwitcher?.addEventListener("click", () => {
  fetch("/profile/changeusertheme").then(() => location.reload());
});

mobileThemeSwitcher?.addEventListener("click", () => {
  fetch("/profile/changeusertheme").then(() => location.reload());
});
