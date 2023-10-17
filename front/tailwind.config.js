/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
    'node_modules/flowbite-vue/**/*.{js,jsx,ts,tsx}',
    'node_modules/flowbite/**/*.{js,jsx,ts,tsx}'
  ],
  theme: {
    screens: {
      'xxs': '350px',
      'xs': '415px',
      'sm': '640px',
      'md': '768px',
      'lg': '1024px',
      'xl': '1280px',
      '2xl': '1536px'
    },
    extend: {
      colors: {
        "font-black": "var(--font-black)",
        "font-white": "var(--font-white)",
        "font-grey": "var(--font-grey)",
        "font-logo": "var(--font-logo)",
        "button-blue": "#1EAAF1",
        "button-blue-hover": "#6CC9F8",
      },
      boxShadow: {
        'custom': '4px 4px 4px rgba(0, 0, 0, 0.25)',
      },
      filter: {
        'logo': '4px 4px 4px rgba(0, 0, 0, 0.25)',
      }
    },
  },
  plugins: [
    require('flowbite/plugin')
  ],
}