using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AmazingCharts.Models;

namespace AmazingCharts.ApiClient
{
    /// <summary>
    /// Direct implementation of the EHR API client.
    /// This client makes HTTP requests to the API endpoints and deserializes the responses.
    /// It handles errors by throwing exceptions which are caught by the ApiProxyService.
    /// </summary>
    public class EhrApiClient : IEhrApiClient
    {
        private readonly HttpClient _httpClient;

        public EhrApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Helper method to build API URLs
        private string BuildProxyUrl(string endpoint)
        {
            // No need to add proxy base URL since it's now in the appsettings.json
            return endpoint.TrimStart('/');
        }
        
        // Helper method to handle unimplemented methods
        private Task<T> NotImplementedAsync<T>(string methodName)
        {
            Console.WriteLine($"Method not implemented: {methodName}");
            throw new NotImplementedException($"The API endpoint for {methodName} has not been implemented on the server yet.");
        }
        
        // Helper method for boolean returns from unimplemented methods
        private Task<bool> NotImplementedBoolAsync(string methodName)
        {
            Console.WriteLine($"Method not implemented: {methodName}");
            throw new NotImplementedException($"The API endpoint for {methodName} has not been implemented on the server yet.");
        }

        // Addendum endpoints
        public async Task<AddendumModel> GetAddendumAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Addendum/{id}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AddendumModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting addendum: {ex.Message}");
                throw;
            }
        }

        public async Task<List<AddendumModel>> GetAddendumsByPatientIdAsync(int patientId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Addendum/patient/{patientId}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<AddendumModel>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting addendums: {ex.Message}");
                throw;
            }
        }

        // Allergy endpoints
        public async Task<AllergyModel> GetAllergyAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/AllergyLibrary/{id}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AllergyModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting allergy: {ex.Message}");
                throw;
            }
        }

        public async Task<List<AllergyModel>> GetAllergiesByPatientIdAsync(int patientId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/AllergyLibrary/patient/{patientId}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<AllergyModel>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting allergies: {ex.Message}");
                throw;
            }
        }

        // Appointment endpoints
        public async Task<AppointmentModel> GetAppointmentAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Appointment/{id}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AppointmentModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting appointment: {ex.Message}");
                throw;
            }
        }

        public async Task<List<AppointmentModel>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Appointment/date-range?start={startDate.ToString("yyyy-MM-dd")}&end={endDate.ToString("yyyy-MM-dd")}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<AppointmentModel>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting appointments: {ex.Message}");
                throw;
            }
        }

        // Billing endpoints
        public async Task<ClaimModel> GetBillingAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Claim/{id}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ClaimModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting billing: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ClaimModel>> GetBillingsByPatientIdAsync(int patientId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Claim/patient/{patientId}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<ClaimModel>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting billings: {ex.Message}");
                throw;
            }
        }

        // Demographics endpoints
        public async Task<PatientModel> GetDemographicsAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Patient/{id}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PatientModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting demographics: {ex.Message}");
                throw;
            }
        }

        public async Task<List<PatientModel>> GetDemographicsByPatientIdAsync(int patientId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Patient/patient/{patientId}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<PatientModel>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting demographics: {ex.Message}");
                throw;
            }
        }
        
        // Patient endpoints
        public async Task<List<PatientModel>> GetAllPatientsAsync()
        {
            try
            {
                // Since there's no direct endpoint for retrieving all patients in the swagger.json,
                // we'll need to implement a custom solution or use a workaround
                
                // Option 1: If the API eventually provides an endpoint for all patients
                var response = await _httpClient.GetAsync(BuildProxyUrl("api/Patient"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<PatientModel>>();
                
                // Option 2 (alternative): If we need to use a search or filter endpoint
                // var response = await _httpClient.GetAsync(BuildProxyUrl("api/Patient/search?criteria=all"));
                // response.EnsureSuccessStatusCode();
                // return await response.Content.ReadFromJsonAsync<List<PatientModel>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all patients: {ex.Message}");
                throw;
            }
        }

        // Lab endpoints
        public async Task<LabResultModel> GetLabResultAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/LabResult/{id}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<LabResultModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting lab result: {ex.Message}");
                throw;
            }
        }

        public async Task<List<LabResultModel>> GetLabResultsByPatientIdAsync(int patientId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/LabResult/patient/{patientId}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<LabResultModel>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting lab results: {ex.Message}");
                throw;
            }
        }

        // Medication endpoints
        public async Task<MedicationModel> GetMedicationAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Medication/{id}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<MedicationModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting medication: {ex.Message}");
                throw;
            }
        }

        public async Task<List<MedicationModel>> GetMedicationsByPatientIdAsync(int patientId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Medication/patient/{patientId}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<MedicationModel>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting medications: {ex.Message}");
                throw;
            }
        }

        // Message endpoints
        public async Task<MessageModel> GetMessageAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Message/{id}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<MessageModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting message: {ex.Message}");
                throw;
            }
        }

        public async Task<List<MessageModel>> GetMessagesByRecipientIdAsync(string recipientId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Message/recipient/{recipientId}"));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<MessageModel>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting messages: {ex.Message}");
                throw;
            }
        }

        // Health endpoint
        public async Task<bool> CheckHealthAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl("api/health"));
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Health check failed: {ex.Message}");
                throw;
            }
        }
        
        // Unimplemented Patient endpoints
        public Task<List<PatientModel>> SearchPatientsAsync(string searchCriteria)
        {
            return NotImplementedAsync<List<PatientModel>>("SearchPatientsAsync");
        }
        
        // Unimplemented User endpoints
        public Task<UserModel> GetUserAsync(int id)
        {
            return NotImplementedAsync<UserModel>("GetUserAsync");
        }
        
        public Task<List<UserModel>> GetAllUsersAsync()
        {
            return NotImplementedAsync<List<UserModel>>("GetAllUsersAsync");
        }
        
        public Task<UserModel> GetUserByUsernameAsync(string username)
        {
            return NotImplementedAsync<UserModel>("GetUserByUsernameAsync");
        }
        
        public Task<bool> AuthenticateUserAsync(string username, string password)
        {
            return NotImplementedBoolAsync("AuthenticateUserAsync");
        }
        
        // Unimplemented Provider endpoints
        public Task<ProviderModel> GetProviderAsync(int id)
        {
            return NotImplementedAsync<ProviderModel>("GetProviderAsync");
        }
        
        public Task<List<ProviderModel>> GetAllProvidersAsync()
        {
            return NotImplementedAsync<List<ProviderModel>>("GetAllProvidersAsync");
        }
        
        public Task<List<ProviderModel>> GetProvidersBySpecialtyAsync(string specialty)
        {
            return NotImplementedAsync<List<ProviderModel>>("GetProvidersBySpecialtyAsync");
        }
        
        // Unimplemented Prescription endpoints
        public Task<PrescriptionModel> GetPrescriptionAsync(int id)
        {
            return NotImplementedAsync<PrescriptionModel>("GetPrescriptionAsync");
        }
        
        public Task<List<PrescriptionModel>> GetPrescriptionsByPatientIdAsync(int patientId)
        {
            return NotImplementedAsync<List<PrescriptionModel>>("GetPrescriptionsByPatientIdAsync");
        }
        
        public Task<List<PrescriptionModel>> GetActivePrescriptionsByPatientIdAsync(int patientId)
        {
            return NotImplementedAsync<List<PrescriptionModel>>("GetActivePrescriptionsByPatientIdAsync");
        }
        
        public Task<bool> CreatePrescriptionAsync(PrescriptionModel prescription)
        {
            return NotImplementedBoolAsync("CreatePrescriptionAsync");
        }
        
        public Task<bool> RefillPrescriptionAsync(int prescriptionId, int refillCount)
        {
            return NotImplementedBoolAsync("RefillPrescriptionAsync");
        }
        
        // Unimplemented Order endpoints
        public Task<OrderModel> GetOrderAsync(int id)
        {
            return NotImplementedAsync<OrderModel>("GetOrderAsync");
        }
        
        public Task<List<OrderModel>> GetOrdersByPatientIdAsync(int patientId)
        {
            return NotImplementedAsync<List<OrderModel>>("GetOrdersByPatientIdAsync");
        }
        
        public Task<List<OrderModel>> GetPendingOrdersAsync()
        {
            return NotImplementedAsync<List<OrderModel>>("GetPendingOrdersAsync");
        }
        
        public Task<bool> CreateOrderAsync(OrderModel order)
        {
            return NotImplementedBoolAsync("CreateOrderAsync");
        }
        
        public Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            return NotImplementedBoolAsync("UpdateOrderStatusAsync");
        }
        
        // Unimplemented Clinical Note endpoints
        public Task<ClinicalNoteModel> GetClinicalNoteAsync(int id)
        {
            return NotImplementedAsync<ClinicalNoteModel>("GetClinicalNoteAsync");
        }
        
        public Task<List<ClinicalNoteModel>> GetClinicalNotesByPatientIdAsync(int patientId)
        {
            return NotImplementedAsync<List<ClinicalNoteModel>>("GetClinicalNotesByPatientIdAsync");
        }
        
        public Task<List<ClinicalNoteModel>> GetClinicalNotesByEncounterIdAsync(int encounterId)
        {
            return NotImplementedAsync<List<ClinicalNoteModel>>("GetClinicalNotesByEncounterIdAsync");
        }
        
        public Task<bool> CreateClinicalNoteAsync(ClinicalNoteModel note)
        {
            return NotImplementedBoolAsync("CreateClinicalNoteAsync");
        }
        
        // Unimplemented Immunization endpoints
        public Task<ImmunizationModel> GetImmunizationAsync(int id)
        {
            return NotImplementedAsync<ImmunizationModel>("GetImmunizationAsync");
        }
        
        public Task<List<ImmunizationModel>> GetImmunizationsByPatientIdAsync(int patientId)
        {
            return NotImplementedAsync<List<ImmunizationModel>>("GetImmunizationsByPatientIdAsync");
        }
        
        public Task<bool> CreateImmunizationAsync(ImmunizationModel immunization)
        {
            return NotImplementedBoolAsync("CreateImmunizationAsync");
        }
        
        // Unimplemented Encounter endpoints
        public Task<EncounterModel> GetEncounterAsync(int id)
        {
            return NotImplementedAsync<EncounterModel>("GetEncounterAsync");
        }
        
        public Task<List<EncounterModel>> GetEncountersByPatientIdAsync(int patientId)
        {
            return NotImplementedAsync<List<EncounterModel>>("GetEncountersByPatientIdAsync");
        }
        
        public Task<EncounterModel> GetCurrentEncounterAsync(int patientId)
        {
            return NotImplementedAsync<EncounterModel>("GetCurrentEncounterAsync");
        }
        
        public Task<bool> CreateEncounterAsync(EncounterModel encounter)
        {
            return NotImplementedBoolAsync("CreateEncounterAsync");
        }
        
        public Task<bool> CloseEncounterAsync(int encounterId)
        {
            return NotImplementedBoolAsync("CloseEncounterAsync");
        }
        
        // Unimplemented Problem list endpoints
        public Task<ProblemModel> GetProblemAsync(int id)
        {
            return NotImplementedAsync<ProblemModel>("GetProblemAsync");
        }
        
        public Task<List<ProblemModel>> GetProblemsByPatientIdAsync(int patientId)
        {
            return NotImplementedAsync<List<ProblemModel>>("GetProblemsByPatientIdAsync");
        }
        
        public Task<List<ProblemModel>> GetActiveProblemsByPatientIdAsync(int patientId)
        {
            return NotImplementedAsync<List<ProblemModel>>("GetActiveProblemsByPatientIdAsync");
        }
        
        public Task<bool> CreateProblemAsync(ProblemModel problem)
        {
            return NotImplementedBoolAsync("CreateProblemAsync");
        }
        
        public Task<bool> ResolveProblemAsync(int problemId)
        {
            return NotImplementedBoolAsync("ResolveProblemAsync");
        }
        
        // Unimplemented Referral endpoints
        public Task<ReferralModel> GetReferralAsync(int id)
        {
            return NotImplementedAsync<ReferralModel>("GetReferralAsync");
        }
        
        public Task<List<ReferralModel>> GetReferralsByPatientIdAsync(int patientId)
        {
            return NotImplementedAsync<List<ReferralModel>>("GetReferralsByPatientIdAsync");
        }
        
        public Task<List<ReferralModel>> GetPendingReferralsAsync()
        {
            return NotImplementedAsync<List<ReferralModel>>("GetPendingReferralsAsync");
        }
        
        public Task<bool> CreateReferralAsync(ReferralModel referral)
        {
            return NotImplementedBoolAsync("CreateReferralAsync");
        }
        
        public Task<bool> UpdateReferralStatusAsync(int referralId, string status)
        {
            return NotImplementedBoolAsync("UpdateReferralStatusAsync");
        }
        
        // Unimplemented Insurance endpoints
        public Task<InsuranceModel> GetInsuranceAsync(int id)
        {
            return NotImplementedAsync<InsuranceModel>("GetInsuranceAsync");
        }
        
        public Task<List<InsuranceModel>> GetInsurancesByPatientIdAsync(int patientId)
        {
            return NotImplementedAsync<List<InsuranceModel>>("GetInsurancesByPatientIdAsync");
        }
        
        public Task<List<InsuranceModel>> GetAllInsuranceProvidersAsync()
        {
            return NotImplementedAsync<List<InsuranceModel>>("GetAllInsuranceProvidersAsync");
        }
        
        public Task<bool> VerifyInsuranceEligibilityAsync(int patientId, int insuranceId)
        {
            return NotImplementedBoolAsync("VerifyInsuranceEligibilityAsync");
        }
        
        // Unimplemented Document endpoints
        public Task<DocumentModel> GetDocumentAsync(int id)
        {
            return NotImplementedAsync<DocumentModel>("GetDocumentAsync");
        }
        
        public Task<List<DocumentModel>> GetDocumentsByPatientIdAsync(int patientId)
        {
            return NotImplementedAsync<List<DocumentModel>>("GetDocumentsByPatientIdAsync");
        }
        
        public Task<bool> UploadDocumentAsync(DocumentModel document)
        {
            return NotImplementedBoolAsync("UploadDocumentAsync");
        }
    }
}
