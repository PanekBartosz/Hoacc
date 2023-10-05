/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        "font-black": "var(--font-black)",
        "font-white": "var(--font-white)"
      }
    },
  },
  plugins: [],
}