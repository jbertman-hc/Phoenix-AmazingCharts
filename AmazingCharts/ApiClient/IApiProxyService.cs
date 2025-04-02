using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AmazingCharts.Models;

namespace AmazingCharts.ApiClient
{
    /// <summary>
    /// Interface for the API proxy service that connects to the live API endpoint.
    /// This service manages the API connection and provides health check functionality.
    /// </summary>
    public interface IApiProxyService
    {
        bool IsApiAvailable { get; }
        event Action<bool> ApiAvailabilityChanged;

        // Addendum endpoints
        Task<AddendumModel> GetAddendumAsync(int id);
        Task<List<AddendumModel>> GetAddendumsByPatientIdAsync(int patientId);

        // Allergy endpoints
        Task<AllergyModel> GetAllergyAsync(int id);
        Task<List<AllergyModel>> GetAllergiesByPatientIdAsync(int patientId);

        // Appointment endpoints
        Task<AppointmentModel> GetAppointmentAsync(int id);
        Task<List<AppointmentModel>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate);

        // Billing endpoints
        Task<ClaimModel> GetBillingAsync(int id);
        Task<List<ClaimModel>> GetBillingsByPatientIdAsync(int patientId);

        // Demographics endpoints
        Task<PatientModel> GetDemographicsAsync(int id);
        Task<List<PatientModel>> GetDemographicsByPatientIdAsync(int patientId);
        
        // Patient endpoints
        Task<List<PatientModel>> GetAllPatientsAsync();
        Task<List<PatientModel>> SearchPatientsAsync(string searchCriteria);

        // Lab endpoints
        Task<LabResultModel> GetLabResultAsync(int id);
        Task<List<LabResultModel>> GetLabResultsByPatientIdAsync(int patientId);

        // Medication endpoints
        Task<MedicationModel> GetMedicationAsync(int id);
        Task<List<MedicationModel>> GetMedicationsByPatientIdAsync(int patientId);

        // Message endpoints
        Task<MessageModel> GetMessageAsync(int id);
        Task<List<MessageModel>> GetMessagesByRecipientIdAsync(string recipientId);
        
        // User endpoints
        Task<UserModel> GetUserAsync(int id);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByUsernameAsync(string username);
        Task<bool> AuthenticateUserAsync(string username, string password);
        
        // Provider endpoints
        Task<ProviderModel> GetProviderAsync(int id);
        Task<List<ProviderModel>> GetAllProvidersAsync();
        Task<List<ProviderModel>> GetProvidersBySpecialtyAsync(string specialty);
        
        // Prescription endpoints
        Task<PrescriptionModel> GetPrescriptionAsync(int id);
        Task<List<PrescriptionModel>> GetPrescriptionsByPatientIdAsync(int patientId);
        Task<List<PrescriptionModel>> GetActivePrescriptionsByPatientIdAsync(int patientId);
        Task<bool> CreatePrescriptionAsync(PrescriptionModel prescription);
        Task<bool> RefillPrescriptionAsync(int prescriptionId, int refillCount);
        
        // Order endpoints (CPOE - Computerized Provider Order Entry)
        Task<OrderModel> GetOrderAsync(int id);
        Task<List<OrderModel>> GetOrdersByPatientIdAsync(int patientId);
        Task<List<OrderModel>> GetPendingOrdersAsync();
        Task<bool> CreateOrderAsync(OrderModel order);
        Task<bool> UpdateOrderStatusAsync(int orderId, string status);
        
        // Clinical notes endpoints
        Task<ClinicalNoteModel> GetClinicalNoteAsync(int id);
        Task<List<ClinicalNoteModel>> GetClinicalNotesByPatientIdAsync(int patientId);
        Task<List<ClinicalNoteModel>> GetClinicalNotesByEncounterIdAsync(int encounterId);
        Task<bool> CreateClinicalNoteAsync(ClinicalNoteModel note);
        
        // Immunization endpoints
        Task<ImmunizationModel> GetImmunizationAsync(int id);
        Task<List<ImmunizationModel>> GetImmunizationsByPatientIdAsync(int patientId);
        Task<bool> CreateImmunizationAsync(ImmunizationModel immunization);
        
        // Encounter endpoints
        Task<EncounterModel> GetEncounterAsync(int id);
        Task<List<EncounterModel>> GetEncountersByPatientIdAsync(int patientId);
        Task<EncounterModel> GetCurrentEncounterAsync(int patientId);
        Task<bool> CreateEncounterAsync(EncounterModel encounter);
        Task<bool> CloseEncounterAsync(int encounterId);
        
        // Problem list endpoints
        Task<ProblemModel> GetProblemAsync(int id);
        Task<List<ProblemModel>> GetProblemsByPatientIdAsync(int patientId);
        Task<List<ProblemModel>> GetActiveProblemsByPatientIdAsync(int patientId);
        Task<bool> CreateProblemAsync(ProblemModel problem);
        Task<bool> ResolveProblemAsync(int problemId);
        
        // Referral endpoints
        Task<ReferralModel> GetReferralAsync(int id);
        Task<List<ReferralModel>> GetReferralsByPatientIdAsync(int patientId);
        Task<List<ReferralModel>> GetPendingReferralsAsync();
        Task<bool> CreateReferralAsync(ReferralModel referral);
        Task<bool> UpdateReferralStatusAsync(int referralId, string status);
        
        // Insurance endpoints
        Task<InsuranceModel> GetInsuranceAsync(int id);
        Task<List<InsuranceModel>> GetInsurancesByPatientIdAsync(int patientId);
        Task<List<InsuranceModel>> GetAllInsuranceProvidersAsync();
        Task<bool> VerifyInsuranceEligibilityAsync(int patientId, int insuranceId);
        
        // Document endpoints
        Task<DocumentModel> GetDocumentAsync(int id);
        Task<List<DocumentModel>> GetDocumentsByPatientIdAsync(int patientId);
        Task<bool> UploadDocumentAsync(DocumentModel document);
    }
}
