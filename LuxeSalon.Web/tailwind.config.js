/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Pages/**/*.cshtml",
        "./Views/**/*.cshtml",
        "./wwwroot/**/*.html"
    ],
    theme: {
        extend: {
            colors: {
                'luxe-ivory': '#F9F8F6',
                'luxe-charcoal': '#1A1A1A',
                'luxe-gold': '#C5A059', // Opsiyonel bir vurgu rengi
            },
            fontFamily: {
                'serif': ['"Playfair Display"', 'serif'],
                'sans': ['Inter', 'sans-serif'],
            },
        },
    },
    plugins: [],
}
