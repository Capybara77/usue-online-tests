module.exports = {
  content: ["./Views/**/*.cshtml"],
  plugins: [
    require("daisyui"),
    require("@tailwindcss/typography"),
  ],
  daisyui: {
    themes: ["light", "dark"],
  },
  theme: {
    extend: {
      padding: {
        full: "100%",
      },
    },
  },
};
