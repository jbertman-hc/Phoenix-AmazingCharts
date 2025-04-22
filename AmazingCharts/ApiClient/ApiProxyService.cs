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
        
        // Helper method to log and proxy unimplemented method calls
        private async Task<T> ProxyUnimplementedAsync<T>(string methodName, Func<Task<T>> apiCall)
        {
            try
            {
                _logger.LogInformation($"Proxying unimplemented method: {methodName}");
                return await apiCall();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {methodName}");
                throw;
            }
        }
        
        // Helper method for boolean return types
        private async Task<bool> ProxyUnimplementedBoolAsync(string methodName, Func<Task<bool>> apiCall)
        {
            try
            {
                _logger.LogInformation($"Proxying unimplemented method: {methodName}");
                return await apiCall();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {methodName}");
                throw;
            }
        }

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
        
        // Patient endpoints
        public async Task<List<PatientModel>> GetAllPatientsAsync()
        {
            try
            {
                return await _apiClient.GetAllPatientsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all patients");
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
        
        // Unimplemented Patient endpoints
        public Task<List<PatientModel>> SearchPatientsAsync(string searchCriteria)
        {
            return ProxyUnimplementedAsync("SearchPatientsAsync", () => _apiClient.SearchPatientsAsync(searchCriteria));
        }
        
        // Unimplemented User endpoints
        public Task<UserModel> GetUserAsync(int id)
        {
            return ProxyUnimplementedAsync("GetUserAsync", () => _apiClient.GetUserAsync(id));
        }
        
        public Task<List<UserModel>> GetAllUsersAsync()
        {
            return ProxyUnimplementedAsync("GetAllUsersAsync", () => _apiClient.GetAllUsersAsync());
        }
        
        public Task<UserModel> GetUserByUsernameAsync(string username)
        {
            return ProxyUnimplementedAsync("GetUserByUsernameAsync", () => _apiClient.GetUserByUsernameAsync(username));
        }
        
        public Task<bool> AuthenticateUserAsync(string username, string password)
        {
            return ProxyUnimplementedBoolAsync("AuthenticateUserAsync", () => _apiClient.AuthenticateUserAsync(username, password));
        }
        
        // Unimplemented Provider endpoints
        public Task<ProviderModel> GetProviderAsync(int id)
        {
            return ProxyUnimplementedAsync("GetProviderAsync", () => _apiClient.GetProviderAsync(id));
        }
        
        public Task<List<ProviderModel>> GetAllProvidersAsync()
        {
            return ProxyUnimplementedAsync("GetAllProvidersAsync", () => _apiClient.GetAllProvidersAsync());
        }
        
        public Task<List<ProviderModel>> GetProvidersBySpecialtyAsync(string specialty)
        {
            return ProxyUnimplementedAsync("GetProvidersBySpecialtyAsync", () => _apiClient.GetProvidersBySpecialtyAsync(specialty));
        }
        
        // Unimplemented Prescription endpoints
        public Task<PrescriptionModel> GetPrescriptionAsync(int id)
        {
            return ProxyUnimplementedAsync("GetPrescriptionAsync", () => _apiClient.GetPrescriptionAsync(id));
        }
        
        public Task<List<PrescriptionModel>> GetPrescriptionsByPatientIdAsync(int patientId)
        {
            return ProxyUnimplementedAsync("GetPrescriptionsByPatientIdAsync", () => _apiClient.GetPrescriptionsByPatientIdAsync(patientId));
        }
        
        public Task<List<PrescriptionModel>> GetActivePrescriptionsByPatientIdAsync(int patientId)
        {
            return ProxyUnimplementedAsync("GetActivePrescriptionsByPatientIdAsync", () => _apiClient.GetActivePrescriptionsByPatientIdAsync(patientId));
        }
        
        public Task<bool> CreatePrescriptionAsync(PrescriptionModel prescription)
        {
            return ProxyUnimplementedBoolAsync("CreatePrescriptionAsync", () => _apiClient.CreatePrescriptionAsync(prescription));
        }
        
        public Task<bool> RefillPrescriptionAsync(int prescriptionId, int refillCount)
        {
            return ProxyUnimplementedBoolAsync("RefillPrescriptionAsync", () => _apiClient.RefillPrescriptionAsync(prescriptionId, refillCount));
        }
        
        // Unimplemented Order endpoints
        public Task<OrderModel> GetOrderAsync(int id)
        {
            return ProxyUnimplementedAsync("GetOrderAsync", () => _apiClient.GetOrderAsync(id));
        }
        
        public Task<List<OrderModel>> GetOrdersByPatientIdAsync(int patientId)
        {
            return ProxyUnimplementedAsync("GetOrdersByPatientIdAsync", () => _apiClient.GetOrdersByPatientIdAsync(patientId));
        }
        
        public Task<List<OrderModel>> GetPendingOrdersAsync()
        {
            return ProxyUnimplementedAsync("GetPendingOrdersAsync", () => _apiClient.GetPendingOrdersAsync());
        }
        
        public Task<bool> CreateOrderAsync(OrderModel order)
        {
            return ProxyUnimplementedBoolAsync("CreateOrderAsync", () => _apiClient.CreateOrderAsync(order));
        }
        
        public Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            return ProxyUnimplementedBoolAsync("UpdateOrderStatusAsync", () => _apiClient.UpdateOrderStatusAsync(orderId, status));
        }
        
        // Unimplemented Clinical Note endpoints
        public Task<ClinicalNoteModel> GetClinicalNoteAsync(int id)
        {
            return ProxyUnimplementedAsync("GetClinicalNoteAsync", () => _apiClient.GetClinicalNoteAsync(id));
        }
        
        public Task<List<ClinicalNoteModel>> GetClinicalNotesByPatientIdAsync(int patientId)
        {
            return ProxyUnimplementedAsync("GetClinicalNotesByPatientIdAsync", () => _apiClient.GetClinicalNotesByPatientIdAsync(patientId));
        }
        
        public Task<List<ClinicalNoteModel>> GetClinicalNotesByEncounterIdAsync(int encounterId)
        {
            return ProxyUnimplementedAsync("GetClinicalNotesByEncounterIdAsync", () => _apiClient.GetClinicalNotesByEncounterIdAsync(encounterId));
        }
        
        public Task<bool> CreateClinicalNoteAsync(ClinicalNoteModel note)
        {
            return ProxyUnimplementedBoolAsync("CreateClinicalNoteAsync", () => _apiClient.CreateClinicalNoteAsync(note));
        }
        
        // Unimplemented Immunization endpoints
        public Task<ImmunizationModel> GetImmunizationAsync(int id)
        {
            return ProxyUnimplementedAsync("GetImmunizationAsync", () => _apiClient.GetImmunizationAsync(id));
        }
        
        public Task<List<ImmunizationModel>> GetImmunizationsByPatientIdAsync(int patientId)
        {
            return ProxyUnimplementedAsync("GetImmunizationsByPatientIdAsync", () => _apiClient.GetImmunizationsByPatientIdAsync(patientId));
        }
        
        public Task<bool> CreateImmunizationAsync(ImmunizationModel immunization)
        {
            return ProxyUnimplementedBoolAsync("CreateImmunizationAsync", () => _apiClient.CreateImmunizationAsync(immunization));
        }
        
        // Unimplemented Encounter endpoints
        public Task<EncounterModel> GetEncounterAsync(int id)
        {
            return ProxyUnimplementedAsync("GetEncounterAsync", () => _apiClient.GetEncounterAsync(id));
        }
        
        public Task<List<EncounterModel>> GetEncountersByPatientIdAsync(int patientId)
        {
            return ProxyUnimplementedAsync("GetEncountersByPatientIdAsync", () => _apiClient.GetEncountersByPatientIdAsync(patientId));
        }
        
        public Task<EncounterModel> GetCurrentEncounterAsync(int patientId)
        {
            return ProxyUnimplementedAsync("GetCurrentEncounterAsync", () => _apiClient.GetCurrentEncounterAsync(patientId));
        }
        
        public Task<bool> CreateEncounterAsync(EncounterModel encounter)
        {
            return ProxyUnimplementedBoolAsync("CreateEncounterAsync", () => _apiClient.CreateEncounterAsync(encounter));
        }
        
        public Task<bool> CloseEncounterAsync(int encounterId)
        {
            return ProxyUnimplementedBoolAsync("CloseEncounterAsync", () => _apiClient.CloseEncounterAsync(encounterId));
        }
        
        // Unimplemented Problem list endpoints
        public Task<ProblemModel> GetProblemAsync(int id)
        {
            return ProxyUnimplementedAsync("GetProblemAsync", () => _apiClient.GetProblemAsync(id));
        }
        
        public Task<List<ProblemModel>> GetProblemsByPatientIdAsync(int patientId)
        {
            return ProxyUnimplementedAsync("GetProblemsByPatientIdAsync", () => _apiClient.GetProblemsByPatientIdAsync(patientId));
        }
        
        public Task<List<ProblemModel>> GetActiveProblemsByPatientIdAsync(int patientId)
        {
            return ProxyUnimplementedAsync("GetActiveProblemsByPatientIdAsync", () => _apiClient.GetActiveProblemsByPatientIdAsync(patientId));
        }
        
        public Task<bool> CreateProblemAsync(ProblemModel problem)
        {
            return ProxyUnimplementedBoolAsync("CreateProblemAsync", () => _apiClient.CreateProblemAsync(problem));
        }
        
        public Task<bool> ResolveProblemAsync(int problemId)
        {
            return ProxyUnimplementedBoolAsync("ResolveProblemAsync", () => _apiClient.ResolveProblemAsync(problemId));
        }
        
        // Unimplemented Referral endpoints
        public Task<ReferralModel> GetReferralAsync(int id)
        {
            return ProxyUnimplementedAsync("GetReferralAsync", () => _apiClient.GetReferralAsync(id));
        }
        
        public Task<List<ReferralModel>> GetReferralsByPatientIdAsync(int patientId)
        {
            return ProxyUnimplementedAsync("GetReferralsByPatientIdAsync", () => _apiClient.GetReferralsByPatientIdAsync(patientId));
        }
        
        public Task<List<ReferralModel>> GetPendingReferralsAsync()
        {
            return ProxyUnimplementedAsync("GetPendingReferralsAsync", () => _apiClient.GetPendingReferralsAsync());
        }
        
        public Task<bool> CreateReferralAsync(ReferralModel referral)
        {
            return ProxyUnimplementedBoolAsync("CreateReferralAsync", () => _apiClient.CreateReferralAsync(referral));
        }
        
        public Task<bool> UpdateReferralStatusAsync(int referralId, string status)
        {
            return ProxyUnimplementedBoolAsync("UpdateReferralStatusAsync", () => _apiClient.UpdateReferralStatusAsync(referralId, status));
        }
        
        // Unimplemented Insurance endpoints
        public Task<InsuranceModel> GetInsuranceAsync(int id)
        {
            return ProxyUnimplementedAsync("GetInsuranceAsync", () => _apiClient.GetInsuranceAsync(id));
        }
        
        public Task<List<InsuranceModel>> GetInsurancesByPatientIdAsync(int patientId)
        {
            return ProxyUnimplementedAsync("GetInsurancesByPatientIdAsync", () => _apiClient.GetInsurancesByPatientIdAsync(patientId));
        }
        
        public Task<List<InsuranceModel>> GetAllInsuranceProvidersAsync()
        {
            return ProxyUnimplementedAsync("GetAllInsuranceProvidersAsync", () => _apiClient.GetAllInsuranceProvidersAsync());
        }
        
        public Task<bool> VerifyInsuranceEligibilityAsync(int patientId, int insuranceId)
        {
            return ProxyUnimplementedBoolAsync("VerifyInsuranceEligibilityAsync", () => _apiClient.VerifyInsuranceEligibilityAsync(patientId, insuranceId));
        }
        
        // Unimplemented Document endpoints
        public Task<DocumentModel> GetDocumentAsync(int id)
        {
            return ProxyUnimplementedAsync("GetDocumentAsync", () => _apiClient.GetDocumentAsync(id));
        }
        
        public Task<List<DocumentModel>> GetDocumentsByPatientIdAsync(int patientId)
        {
            return ProxyUnimplementedAsync("GetDocumentsByPatientIdAsync", () => _apiClient.GetDocumentsByPatientIdAsync(patientId));
        }
        
        public Task<bool> UploadDocumentAsync(DocumentModel document)
        {
            return ProxyUnimplementedBoolAsync("UploadDocumentAsync", () => _apiClient.UploadDocumentAsync(document));
        }
    }

    // Event args for data source change notifications
    public class DataSourceChangedEventArgs : EventArgs
    {
        public bool IsUsingLiveData { get; set; } = true;
    }
}
