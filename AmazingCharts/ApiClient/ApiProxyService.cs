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
    /// Proxy service that attempts to use the live API endpoint first,
    /// and falls back to mock data if the API is unavailable or returns an error.
    /// </summary>
    public class ApiProxyService : IEhrApiClient, IDisposable
    {
        private readonly EhrApiClient _apiClient;
        private readonly MockDataService _mockDataService;
        private readonly ILogger<ApiProxyService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private bool _useApiEndpoint = false;
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
            
            // Start with mock data
            _useApiEndpoint = false;
            _logger.LogInformation("Starting with mock data. Will attempt to connect to API.");
            
            // Start periodic health checks with a delay to allow the application to fully initialize
            _healthCheckTimer = new Timer(async _ => await CheckApiAvailabilityAsync(), null, 5000, HealthCheckIntervalMs);
        }

        public void Dispose()
        {
            _healthCheckTimer?.Dispose();
        }

        public bool IsUsingMockData => !_useApiEndpoint;

        private async Task CheckApiAvailabilityAsync()
        {
            try
            {
                var wasUsingMockData = !_useApiEndpoint;
                _logger.LogInformation($"Performing API health check. Currently using {(wasUsingMockData ? "mock data" : "live API data")}");
                
                var isHealthy = await CheckHealthAsync();
                
                if (isHealthy && wasUsingMockData)
                {
                    _useApiEndpoint = true;
                    _logger.LogInformation("API is now available. Switching to live data.");
                    OnDataSourceChanged(true);
                }
                else if (!isHealthy && !wasUsingMockData)
                {
                    _useApiEndpoint = false;
                    _logger.LogWarning("API is not available. Falling back to mock data.");
                    OnDataSourceChanged(false);
                }
                else
                {
                    _logger.LogInformation($"No change in data source. Still using {(_useApiEndpoint ? "live API data" : "mock data")}");
                }
            }
            catch (Exception ex)
            {
                _useApiEndpoint = false;
                _logger.LogError(ex, "Error during API health check. Using mock data.");
                OnDataSourceChanged(false);
            }
        }

        // Notify subscribers when data source changes
        private void OnDataSourceChanged(bool isUsingLiveData)
        {
            _logger.LogInformation($"Data source changed to {(isUsingLiveData ? "live API data" : "mock data")}. Notifying subscribers.");
            DataSourceChanged?.Invoke(this, new DataSourceChangedEventArgs(isUsingLiveData));
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
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetAddendumAsync(id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetAddendumAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data
            return new AddendumModel { 
                Id = id, 
                PatientId = 1, 
                Content = "Sample addendum content",
                CreatedDate = DateTime.Now.AddDays(-5),
                CreatedBy = "Dr. Smith"
            };
        }

        public async Task<List<AddendumModel>> GetAddendumsByPatientIdAsync(int patientId)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetAddendumsByPatientIdAsync(patientId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetAddendumsByPatientIdAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data
            return new List<AddendumModel>
            {
                new AddendumModel { Id = 1, PatientId = patientId, Content = "Sample addendum 1", CreatedDate = DateTime.Now.AddDays(-10), CreatedBy = "Dr. Johnson" },
                new AddendumModel { Id = 2, PatientId = patientId, Content = "Sample addendum 2", CreatedDate = DateTime.Now.AddDays(-5), CreatedBy = "Dr. Smith" }
            };
        }

        // Allergy endpoints
        public async Task<AllergyModel> GetAllergyAsync(int id)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetAllergyAsync(id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetAllergyAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data
            return new AllergyModel { 
                Id = id, 
                PatientId = 1, 
                AllergyName = "Sample Allergy", 
                Severity = "Moderate", 
                Reaction = "Rash",
                OnsetDate = DateTime.Now.AddYears(-2),
                IsActive = true
            };
        }

        public async Task<List<AllergyModel>> GetAllergiesByPatientIdAsync(int patientId)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetAllergiesByPatientIdAsync(patientId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetAllergiesByPatientIdAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data
            return new List<AllergyModel>
            {
                new AllergyModel { Id = 1, PatientId = patientId, AllergyName = "Penicillin", Severity = "Severe", Reaction = "Anaphylaxis", OnsetDate = DateTime.Now.AddYears(-5), IsActive = true },
                new AllergyModel { Id = 2, PatientId = patientId, AllergyName = "Peanuts", Severity = "Moderate", Reaction = "Hives", OnsetDate = DateTime.Now.AddYears(-3), IsActive = true }
            };
        }

        // Appointment endpoints
        public async Task<AppointmentModel> GetAppointmentAsync(int id)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetAppointmentAsync(id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetAppointmentAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data from appointments
            var appointment = _mockDataService.GetAppointments().Find(a => a.Id == id);
            return appointment ?? new AppointmentModel { 
                Id = id, 
                PatientName = "Not found", 
                AppointmentType = "Unknown",
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(1).AddHours(1),
                Status = "Unknown"
            };
        }

        public async Task<List<AppointmentModel>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetAppointmentsByDateRangeAsync(startDate, endDate);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetAppointmentsByDateRangeAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data
            var appointments = _mockDataService.GetAppointments().FindAll(a => a.StartTime >= startDate && a.EndTime <= endDate);
            return appointments.Count > 0 ? appointments : _mockDataService.GetAppointments();
        }

        // Billing endpoints
        public async Task<ClaimModel> GetBillingAsync(int id)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetBillingAsync(id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetBillingAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data
            var claim = _mockDataService.GetClaims().Find(c => c.Id == id);
            return claim ?? new ClaimModel { 
                Id = id, 
                PatientName = "Not found", 
                Amount = 0.00m,
                Status = "Unknown",
                ClaimDate = DateTime.Now
            };
        }

        public async Task<List<ClaimModel>> GetBillingsByPatientIdAsync(int patientId)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetBillingsByPatientIdAsync(patientId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetBillingsByPatientIdAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data
            return _mockDataService.GetClaims().FindAll(c => c.PatientId == patientId);
        }

        // Demographics endpoints
        public async Task<PatientModel> GetDemographicsAsync(int id)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetDemographicsAsync(id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetDemographicsAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data
            var patient = _mockDataService.GetPatients().Find(p => p.Id == id);
            return patient ?? new PatientModel { 
                Id = id, 
                FirstName = "Unknown", 
                LastName = "Patient",
                DateOfBirth = DateTime.Now.AddYears(-30)
            };
        }

        public async Task<List<PatientModel>> GetDemographicsByPatientIdAsync(int patientId)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetDemographicsByPatientIdAsync(patientId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetDemographicsByPatientIdAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data - typically there would be only one demographic record per patient
            var patient = _mockDataService.GetPatients().Find(p => p.Id == patientId);
            return patient != null ? new List<PatientModel> { patient } : new List<PatientModel>();
        }

        // Lab endpoints
        public async Task<LabResultModel> GetLabResultAsync(int id)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetLabResultAsync(id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetLabResultAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data
            var labResult = _mockDataService.GetLabResults().Find(l => l.Id == id);
            return labResult ?? new LabResultModel { 
                Id = id, 
                TestName = "Unknown", 
                Status = "Not Found",
                TestDate = DateTime.Now.AddDays(-7)
            };
        }

        public async Task<List<LabResultModel>> GetLabResultsByPatientIdAsync(int patientId)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetLabResultsByPatientIdAsync(patientId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetLabResultsByPatientIdAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data
            return _mockDataService.GetLabResults().FindAll(l => l.PatientId == patientId);
        }

        // Medication endpoints
        public async Task<MedicationModel> GetMedicationAsync(int id)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetMedicationAsync(id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetMedicationAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data
            return new MedicationModel { 
                Id = id, 
                Name = "Sample Medication", 
                Dosage = "10mg", 
                Frequency = "Once daily",
                StartDate = DateTime.Now.AddMonths(-3),
                Prescriber = "Dr. Smith",
                IsActive = true
            };
        }

        public async Task<List<MedicationModel>> GetMedicationsByPatientIdAsync(int patientId)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetMedicationsByPatientIdAsync(patientId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetMedicationsByPatientIdAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data
            return new List<MedicationModel>
            {
                new MedicationModel { Id = 1, PatientId = patientId, Name = "Lisinopril", Dosage = "10mg", Frequency = "Once daily", StartDate = DateTime.Now.AddMonths(-6), Prescriber = "Dr. Smith", IsActive = true },
                new MedicationModel { Id = 2, PatientId = patientId, Name = "Metformin", Dosage = "500mg", Frequency = "Twice daily", StartDate = DateTime.Now.AddMonths(-3), Prescriber = "Dr. Johnson", IsActive = true }
            };
        }

        // Message endpoints
        public async Task<MessageModel> GetMessageAsync(int id)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetMessageAsync(id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetMessageAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data from the message with the matching id
            var message = _mockDataService.GetMessages().Find(m => m.Id == id);
            return message ?? new MessageModel { 
                Id = id, 
                Subject = "Not found", 
                Body = "Message not found",
                DateSent = DateTime.Now
            };
        }

        public async Task<List<MessageModel>> GetMessagesByRecipientIdAsync(string recipientId)
        {
            if (_useApiEndpoint)
            {
                try
                {
                    return await _apiClient.GetMessagesByRecipientIdAsync(recipientId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling GetMessagesByRecipientIdAsync. Falling back to mock data.");
                    _useApiEndpoint = false;
                    OnDataSourceChanged(false);
                }
            }
            
            // Return mock data
            return _mockDataService.GetMessages().FindAll(m => m.RecipientName == recipientId);
        }
    }

    // Event args for data source change notifications
    public class DataSourceChangedEventArgs : EventArgs
    {
        public bool IsUsingLiveData { get; }

        public DataSourceChangedEventArgs(bool isUsingLiveData)
        {
            IsUsingLiveData = isUsingLiveData;
        }
    }
}
