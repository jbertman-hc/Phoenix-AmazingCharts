using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Microsoft.JSInterop;

namespace AmazingCharts.Services
{
    public class ThemeService
    {
        private bool _isDarkMode;
        private MudTheme _currentTheme;
        private readonly MudTheme _defaultLightTheme;
        private readonly MudTheme _defaultDarkTheme;
        private readonly MudTheme _customLightTheme;
        private readonly MudTheme _customDarkTheme;
        private readonly IJSRuntime? _jsRuntime;

        public bool IsDarkMode 
        { 
            get => _isDarkMode; 
            set
            {
                if (_isDarkMode != value)
                {
                    _isDarkMode = value;
                    UpdateCurrentTheme();
                    ThemeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public MudTheme CurrentTheme => _currentTheme;
        public MudTheme DefaultLightTheme => _defaultLightTheme;
        public MudTheme DefaultDarkTheme => _defaultDarkTheme;
        public MudTheme CustomLightTheme => _customLightTheme;
        public MudTheme CustomDarkTheme => _customDarkTheme;

        public event EventHandler? ThemeChanged;

        public ThemeService(IJSRuntime? jsRuntime = null)
        {
            _jsRuntime = jsRuntime;
            
            // Default Light Theme
            _defaultLightTheme = new MudTheme();

            // Default Dark Theme
            _defaultDarkTheme = new MudTheme
            {
                Palette = new Palette
                {
                    Black = "#27272f",
                    Background = "#32333d",
                    BackgroundGrey = "#27272f",
                    Surface = "#373740",
                    DrawerBackground = "#27272f",
                    DrawerText = "rgba(255,255,255, 0.50)",
                    DrawerIcon = "rgba(255,255,255, 0.50)",
                    AppbarBackground = "#27272f",
                    AppbarText = "rgba(255,255,255, 0.70)",
                    TextPrimary = "rgba(255,255,255, 0.70)",
                    TextSecondary = "rgba(255,255,255, 0.50)",
                    ActionDefault = "#adadb1",
                    ActionDisabled = "rgba(255,255,255, 0.26)",
                    ActionDisabledBackground = "rgba(255,255,255, 0.12)",
                    Primary = "#7e6fff",
                    Secondary = "#ff4081",
                    Tertiary = "#1ec8a5",
                    Info = "#4a86ff",
                    Success = "#3dcb6c",
                    Warning = "#ffb545",
                    Error = "#ff3f5f",
                    Dark = "#27272f"
                }
            };

            // Custom Light Theme - Amazing Charts Branding
            _customLightTheme = new MudTheme
            {
                Palette = new Palette
                {
                    Primary = "#2196f3ff",   // Blue - specified primary color
                    Secondary = "#9c27b0",   // Purple
                    Tertiary = "#009688",    // Teal
                    Info = "#2196f3",        // Light Blue
                    Success = "#4caf50",     // Green
                    Warning = "#ff9800",     // Orange
                    Error = "#f44336",       // Red
                    AppbarBackground = "#2196f3ff", // Match primary color
                    AppbarText = "#ffffff",
                    Background = "#f5f5f5",
                    DrawerBackground = "#ffffff",
                    DrawerText = "rgba(0,0,0, 0.7)",
                    Surface = "#ffffff"
                },
                Typography = new Typography
                {
                    Default = new Default
                    {
                        FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" },
                        FontSize = "0.875rem",
                        FontWeight = 400,
                        LineHeight = 1.43,
                        LetterSpacing = ".01071em"
                    },
                    H1 = new H1
                    {
                        FontSize = "2rem",
                        FontWeight = 500,
                        LineHeight = 1.167,
                        LetterSpacing = "-.01562em"
                    }
                },
                LayoutProperties = new LayoutProperties
                {
                    DefaultBorderRadius = "4px"
                },
                Shadows = new Shadow(),
                ZIndex = new ZIndex()
            };

            // Custom Dark Theme - Amazing Charts Branding
            _customDarkTheme = new MudTheme
            {
                Palette = new Palette
                {
                    Primary = "#2196f3ff",   // Same primary color as light theme
                    Secondary = "#bb86fc",   // Light Purple
                    Tertiary = "#03dac6",    // Light Teal
                    Info = "#64b5f6",        // Lighter Blue
                    Success = "#81c784",     // Light Green
                    Warning = "#ffb74d",     // Light Orange
                    Error = "#e57373",       // Light Red
                    AppbarBackground = "#1e1e2d",
                    AppbarText = "#ffffff",
                    Background = "#121212",
                    DrawerBackground = "#1e1e2d",
                    DrawerText = "rgba(255,255,255, 0.8)",
                    Surface = "#1e1e2d",
                    TextPrimary = "rgba(255,255,255, 0.87)",
                    TextSecondary = "rgba(255,255,255, 0.60)",
                    Dark = "#27272f",
                    Black = "#27272f"
                },
                Typography = new Typography
                {
                    Default = new Default
                    {
                        FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" },
                        FontSize = "0.875rem",
                        FontWeight = 400,
                        LineHeight = 1.43,
                        LetterSpacing = ".01071em"
                    },
                    H1 = new H1
                    {
                        FontSize = "2rem",
                        FontWeight = 500,
                        LineHeight = 1.167,
                        LetterSpacing = "-.01562em"
                    }
                },
                LayoutProperties = new LayoutProperties
                {
                    DefaultBorderRadius = "4px"
                },
                Shadows = new Shadow(),
                ZIndex = new ZIndex()
            };

            // Set initial theme to light mode
            _isDarkMode = false;
            _currentTheme = _customLightTheme;
        }

        private void UpdateCurrentTheme()
        {
            _currentTheme = _isDarkMode ? _customDarkTheme : _customLightTheme;
        }

        public void ToggleDarkMode()
        {
            IsDarkMode = !IsDarkMode;
            _ = SaveThemePreference();
        }

        public void SetTheme(bool isDarkMode)
        {
            IsDarkMode = isDarkMode;
            _ = SaveThemePreference();
        }

        public async Task LoadSavedTheme()
        {
            if (_jsRuntime != null)
            {
                try
                {
                    var isDarkMode = await _jsRuntime.InvokeAsync<bool>("themeInterop.loadThemePreference");
                    IsDarkMode = isDarkMode;
                }
                catch (Exception)
                {
                    // Fallback to default theme (light mode) if there's an error
                    IsDarkMode = false;
                }
            }
        }

        public async Task SaveThemePreference()
        {
            if (_jsRuntime != null)
            {
                try
                {
                    await _jsRuntime.InvokeVoidAsync("themeInterop.saveThemePreference", IsDarkMode);
                }
                catch (Exception)
                {
                    // Silently fail if localStorage is not available
                }
            }
        }
    }
}
