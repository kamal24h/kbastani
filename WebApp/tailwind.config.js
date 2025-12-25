// tailwind.config.js
module.exports = {
    purge: [
        './Views/**/*.cshtml',
        './Areas/**/*.cshtml',
        './Pages/**/*.cshtml',
        './wwwroot/js/**/*.js'
    ],
    darkMode: 'class', // یا 'media' اگر بخواهی بر اساس تنظیمات سیستم کاربر باشه
    theme: {
        extend: {
            colors: {
                brand: {
                    light: '#3ab7bf',
                    DEFAULT: '#0fa9a7',
                    dark: '#0c7471',
                },
            },
        },
    },
    variants: {
        extend: {
            backgroundColor: ['active'],
            textColor: ['active'],
        },
    },
    plugins: [],
}
