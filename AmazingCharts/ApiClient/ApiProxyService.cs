using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using AmazingCharts.Models;
using AmazingCharts.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace AmazingCharts.ApiClient
{
    /// <summary>
    /// Proxy service that connects to the live API endpoint.
    /// This service is responsible for:
    /// 1. Connecting to the API via the proxy server
    /// 2. Performing periodic health checks to monitor API status
    /// 3. Providing visual indication of the API connection status
    /// 4. Handling API errors with appropriate logging
    /// </summary>
    public class ApiProxyService : IEhrApiClient, IDisposable
    {
        private readonly EhrApiClient _apiClient;
        private readonly MockDataService _mockDataService;
        private readonly ILogger<ApiProxyService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private Timer _healthCheckTimer;
        private const int HealthCheckIntervalMs = 30000; // Check every 30 seconds
        private string _proxyUrl = "http://localhost:3000"; // Default proxy URL
        private const string _healthCheckEndpoint = "api/Addendum/1"; // Use a specific endpoint that exists in swagger.json
        
        // Event that UI components can subscribe to for data source changes
        public event EventHandler<DataSourceChangedEventArgs> DataSourceChanged;

        public ApiProxyService(
            EhrApiClient apiClient, 
            MockDataService mockDataService, 
            ILogger<ApiProxyService> logger,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _apiClient = apiClient;
            _mockDataService = mockDataService;
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
            
            // Get proxy URL from configuration if available
            var configProxyUrl = _configuration["ProxyUrl"];
            if (!string.IsNullOrEmpty(configProxyUrl))
            {
                _proxyUrl = configProxyUrl;
            }
            
            _logger.LogInformation("Starting with live API data.");
            
            // Start periodic health checks with a delay to allow the application to fully initialize
            _healthCheckTimer = new Timer(async _ => await CheckApiAvailabilityAsync(), null, 5000, HealthCheckIntervalMs);
        }

        public void Dispose()
        {
            _healthCheckTimer?.Dispose();
        }

        public bool IsUsingMockData => false; // Always return false since we're always using live data

        private async Task CheckApiAvailabilityAsync()
        {
            try
            {
                _logger.LogInformation("Performing API health check.");
                
                var isHealthy = await CheckHealthAsync();
                
                if (!isHealthy)
                {
                    _logger.LogWarning("API health check failed. Please check the API connection.");
                    // We no longer fall back to mock data, just log the warning
                }
                else
                {
                    _logger.LogInformation("API health check successful.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during API health check.");
            }
        }

        private void OnDataSourceChanged(bool isUsingLiveData)
        {
            DataSourceChanged?.Invoke(this, new DataSourceChangedEventArgs { IsUsingLiveData = isUsingLiveData });
        }

        // Health endpoint
        public async Task<bool> CheckHealthAsync()
        {
            try
            {
                _logger.LogInformation($"Checking API health using endpoint: {_healthCheckEndpoint}");
                
                // Try direct API health check first
                try
                {
                    var response = await _httpClient.GetAsync(_healthCheckEndpoint);
                    if (response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("API health check successful via direct connection");
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning($"API health check failed with status: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Direct API health check failed. Trying through proxy...");
                }

                // If direct check fails, try through proxy
                try
                {
                    using var proxyClient = new HttpClient();
                    var proxyResponse = await proxyClient.GetAsync($"{_proxyUrl}/{_healthCheckEndpoint}");
                    
                    if (proxyResponse.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("API health check successful via proxy");
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning($"API health check via proxy failed with status: {proxyResponse.StatusCode}. Response: {await proxyResponse.Content.ReadAsStringAsync()}");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "API health check via proxy failed with exception");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during health check");
                return false;
            }
        }

        // Addendum endpoints
        public async Task<AddendumModel> GetAddendumAsync(int id)
        {
            try
            {
                return await _apiClient.GetAddendumAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetAddendumAsync.");
                throw;
            }
        }

        public async Task<List<AddendumModel>> GetAddendumsByPatientIdAsync(int patientId)
        {
            try
            {
                return await _apiClient.GetAddendumsByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetAddendumsByPatientIdAsync.");
                throw;
            }
        }

        // Allergy endpoints
        public async Task<AllergyModel> GetAllergyAsync(int id)
        {
            try
            {
                return await _apiClient.GetAllergyAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetAllergyAsync.");
                throw;
            }
        }

        public async Task<List<AllergyModel>> GetAllergiesByPatientIdAsync(int patientId)
        {
            try
            {
                return await _apiClient.GetAllergiesByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetAllergiesByPatientIdAsync.");
                throw;
            }
        }

        // Appointment endpoints
        public async Task<AppointmentModel> GetAppointmentAsync(int id)
        {
            try
            {
                return await _apiClient.GetAppointmentAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetAppointmentAsync.");
                throw;
            }
        }

        public async Task<List<AppointmentModel>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _apiClient.GetAppointmentsByDateRangeAsync(startDate, endDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetAppointmentsByDateRangeAsync.");
                throw;
            }
        }

        // Billing endpoints
        public async Task<ClaimModel> GetBillingAsync(int id)
        {
            try
            {
                return await _apiClient.GetBillingAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetBillingAsync.");
                throw;
            }
        }

        public async Task<List<ClaimModel>> GetBillingsByPatientIdAsync(int patientId)
        {
            try
            {
                return await _apiClient.GetBillingsByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetBillingsByPatientIdAsync.");
                throw;
            }
        }

        // Demographics endpoints
        public async Task<PatientModel> GetDemographicsAsync(int id)
        {
            try
            {
                return await _apiClient.GetDemographicsAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetDemographicsAsync.");
                throw;
            }
        }

        public async Task<List<PatientModel>> GetDemographicsByPatientIdAsync(int patientId)
        {
            try
            {
                return await _apiClient.GetDemographicsByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetDemographicsByPatientIdAsync.");
                throw;
            }
        }

        // Lab endpoints
        public async Task<LabResultModel> GetLabResultAsync(int id)
        {
            try
            {
                return await _apiClient.GetLabResultAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetLabResultAsync.");
                throw;
            }
        }

        public async Task<List<LabResultModel>> GetLabResultsByPatientIdAsync(int patientId)
        {
            try
            {
                return await _apiClient.GetLabResultsByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetLabResultsByPatientIdAsync.");
                throw;
            }
        }

        // Medication endpoints
        public async Task<MedicationModel> GetMedicationAsync(int id)
        {
            try
            {
                return await _apiClient.GetMedicationAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetMedicationAsync.");
                throw;
            }
        }

        public async Task<List<MedicationModel>> GetMedicationsByPatientIdAsync(int patientId)
        {
            try
            {
                return await _apiClient.GetMedicationsByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetMedicationsByPatientIdAsync.");
                throw;
            }
        }

        // Message endpoints
        public async Task<MessageModel> GetMessageAsync(int id)
        {
            try
            {
                return await _apiClient.GetMessageAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetMessageAsync.");
                throw;
            }
        }

        public async Task<List<MessageModel>> GetMessagesByRecipientIdAsync(string recipientId)
        {
            try
            {
                return await _apiClient.GetMessagesByRecipientIdAsync(recipientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling GetMessagesByRecipientIdAsync.");
                throw;
            }
        }
    }

    // Event args for data source change notifications
    public class DataSourceChangedEventArgs : EventArgs
    {
        public bool IsUsingLiveData { get; set; } = true;
    }
}
