module.exports = {
  content: ['./src/**/*.{js,jsx,ts,tsx}'],
  theme: {
    extend: {

      fontFamily: {
        apple: ['-apple-system', 'BlinkMacSystemFont', '"Segoe UI"', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', '"Fira Sans"', '"Droid Sans"', '"Helvetica Neue"', 'sans-serif'],
        code: ['source-code-pro', 'Menlo', 'Monaco', 'Consolas', '"Courier New"', 'monospace'],
        nunito: ['"Nunito Sans"', 'sans-serif'],
        mont: ['"Mont"', 'sans-serif'],
        inter: ['"Inter"', 'sans-serif'],
      },
      
    },
  },
  plugins: [
    function ({ addUtilities }) {
      addUtilities({
        '.scrollbar-hide': {
          /* Para navegadores baseados no Webkit */
          '::-webkit-scrollbar': { display: 'none' },
          /* Para Firefox */
          'scrollbar-width': 'none',
          /* Para Internet Explorer e Edge */
          '-ms-overflow-style': 'none',
        },
      });
    },
  ],
};
