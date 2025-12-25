
///** @type {import('tailwindcss').Config} **/
module.exports = {
    darkMode: 'class',
    content: [
        "./Views/**/*.cshtml",
        "./Areas/**/*.cshtml",
        "./Pages/**/*.cshtml",
        "./wwwroot/js/**/*.js"
    ],
    theme: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/typography')
    ],
}
