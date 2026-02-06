/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      fontFamily: {
        orbitron: ['Orbitron', 'sans-serif'],
        poppins: ['Poppins', 'sans-serif'],
        inter: ['Inter', 'sans-serif'],
      },
      screens: {
        'xs': '320px',    // Extra small
        'sm': '768px',    // Small
        'md': '900px',   // Medium
        'lg': '1240px',   // Large
        'xl' : '1700px', // Desktop
      },
       colors: {
      default: '#77b4c1 '
    }
    },
  },
  plugins: [require('daisyui')],
  daisyui: {
    themes: [
      {
          cmyk: {
            "primary": "oklch(71.772% 0.133 239.443)",
            "default" : "#6EB6C5",
            "primary-content": "oklch(14.354% 0.026 239.443)",
            "secondary": "oklch(64.476% 0.202 359.339 )",
            "secondary-content": "oklch(12.895% 0.04 359.339)",
            "accent": "oklch(65% 0.25 160)",
            "accent-content": "oklch(18.845% 0.037 105.306)",
            "neutral": "oklch(21.778% 0 0)",
            "neutral-content": "oklch(84.355% 0 0)",
            "base-100": "oklch(100% 0 0)",
            "base-200": "oklch(95% 0 0)",
            "base-300": "oklch(90% 0 0)",
            "base-content": "oklch(20% 0 0)",
            "info": "oklch(68.475% 0.094 217.284)",
            "info-content": "oklch(13.695% 0.018 217.284)",
            "success": "oklch(46.949% 0.162 321.406)",
            "success-content": "oklch(89.389% 0.032 321.406)",
            "warning": "oklch(71.236% 0.159 52.023)",
            "warning-content": "oklch(14.247% 0.031 52.023)",
            "error": "oklch(62.013% 0.208 28.717)",
            "error-content": "oklch(12.402% 0.041 28.717)",
            "--rounded-btn": "1rem",
            "--rounded-box": "1rem",
            "--rounded-badge": "1.9rem",
            "--btn-text-case": "uppercase",
            "--btn-focus-scale": "0.95"
          },
          screens: {
            'mobile': '320px',
            'tablet': '768px',
            'laptop': '900px',
            'laptopL': '1240px',
        }
      }
    ],
    darkTheme: "cmyk",
  }
}
