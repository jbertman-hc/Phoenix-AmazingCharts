using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AmazingCharts.Models;

namespace AmazingCharts.ApiClient
{
    /// <summary>
    /// Interface for the EHR API client generated from swagger.json
    /// </summary>
    public interface IEhrApiClient
    {
        // This interface will be implemented by the NSwag-generated client
        // The actual implementation will contain methods corresponding to the API endpoints
        
        // Example method signatures based on the swagger.json endpoints:
        
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
        
        // Lab endpoints
        Task<LabResultModel> GetLabResultAsync(int id);
        Task<List<LabResultModel>> GetLabResultsByPatientIdAsync(int patientId);
        
        // Medication endpoints
        Task<MedicationModel> GetMedicationAsync(int id);
        Task<List<MedicationModel>> GetMedicationsByPatientIdAsync(int patientId);
        
        // Message endpoints
        Task<MessageModel> GetMessageAsync(int id);
        Task<List<MessageModel>> GetMessagesByRecipientIdAsync(string recipientId);
        
        // Health endpoint
        Task<bool> CheckHealthAsync();
    }
}
