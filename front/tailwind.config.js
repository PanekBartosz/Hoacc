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
        "font-white": "var(--font-white)",
        "font-grey": "var(--font-grey)"
      },
      boxShadow: {
        'custom': '4px 4px 4px rgba(0, 0, 0, 0.25)',
      },
      filter: {
        'logo': '4px 4px 4px rgba(0, 0, 0, 0.25)',
      },
    },
  },
  plugins: [],
}