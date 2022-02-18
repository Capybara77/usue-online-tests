const navigationProfile = document.querySelector(".navigation-profile");
const profileMenu = document.querySelector(".navigation-profile-menu");
const mobileNavigation = document.querySelector(".navigation-mobile");
const mobileMenu = document.querySelector(".navigation-mobile-menu");

document.documentElement.addEventListener("click", (e) => {
  if (!profileMenu.contains(e.target) && e.target !== navigationProfile) {
    console.log(e.target);
    console.log(navigationProfile)
    profileMenu.classList.add("hidden");
  }
});

navigationProfile?.addEventListener("click", () => {
  if (matchMedia("(min-width: 768px)").matches) {
    profileMenu.classList.toggle("hidden");
  }
});

mobileNavigation?.addEventListener("click", () => {
  mobileMenu.classList.toggle("hidden");
});
