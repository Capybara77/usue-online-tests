const navigationProfile = document.querySelector(".navigation-profile");
const profileMenu = document.querySelector(".navigation-profile-menu");

navigationProfile.onclick = () => {
  profileMenu.classList.toggle("hidden");
}
