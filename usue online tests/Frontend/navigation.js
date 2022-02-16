const navigationProfile = document.querySelector(".navigation-profile");
const profileMenu = document.querySelector(".navigation-profile-menu");

navigationProfile?.addEventListener("click", () => {
  profileMenu.classList.toggle("hidden");
});
