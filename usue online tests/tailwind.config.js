const defaultTheme = require("tailwindcss/defaultTheme");

module.exports = {
  content: ["./Views/**/*.cshtml"],
  theme: {
    extend: {
      fontFamily: {
        sans: ["Inter", ...defaultTheme.fontFamily.sans],
      },
    },
  },
  darkMode: "class",
};
