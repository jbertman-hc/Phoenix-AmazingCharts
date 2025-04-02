// Theme management functions
window.themeInterop = {
    // Check if the user prefers dark mode
    prefersDarkMode: function() {
        return window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
    },
    
    // Save theme preference to local storage
    saveThemePreference: function(isDarkMode) {
        localStorage.setItem('theme_preference', isDarkMode ? 'dark' : 'light');
    },
    
    // Load theme preference from local storage
    loadThemePreference: function() {
        const storedTheme = localStorage.getItem('theme_preference');
        if (storedTheme) {
            return storedTheme === 'dark';
        }
        return this.prefersDarkMode();
    }
};
