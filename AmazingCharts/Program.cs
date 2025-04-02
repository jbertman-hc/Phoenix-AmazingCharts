using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AmazingCharts;
using AmazingCharts.ApiClient;
using MudBlazor.Services;
using AmazingCharts.Services;
using System.Net.Http.Headers;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add MudBlazor services
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = true;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
});

// Configure HttpClient with proper headers for CORS
builder.Services.AddScoped(sp => {
    var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? builder.HostEnvironment.BaseAddress;
    var httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
    
    // Add headers to help with CORS
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    
    return httpClient;
});

// Register the mock data service
builder.Services.AddSingleton<MockDataService>();

// Register the direct API client (not used directly by the application)
builder.Services.AddScoped<EhrApiClient>();

// Register the API proxy service as the implementation of IEhrApiClient
builder.Services.AddScoped<ApiProxyService>(sp => 
    new ApiProxyService(
        sp.GetRequiredService<EhrApiClient>(),
        sp.GetRequiredService<MockDataService>(),
        sp.GetRequiredService<ILogger<ApiProxyService>>(),
        sp.GetRequiredService<HttpClient>(),
        sp.GetRequiredService<IConfiguration>()
    )
);

// Also register the API proxy service as the implementation of IEhrApiClient
builder.Services.AddScoped<IEhrApiClient>(sp => sp.GetRequiredService<ApiProxyService>());

// Register application services
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<ScheduleService>();
builder.Services.AddScoped<LabService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<BillingService>();
builder.Services.AddScoped<AiAssistantService>();
builder.Services.AddSingleton<ThemeService>(sp => new ThemeService(sp.GetRequiredService<IJSRuntime>()));

// Add logging
builder.Logging.SetMinimumLevel(LogLevel.Information);

await builder.Build().RunAsync();
