using AmazingCharts.ApiClient;
using AmazingCharts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingCharts.Services
{
    public class LabService
    {
        private readonly IEhrApiClient _apiClient;

        public LabService(IEhrApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<LabResultModel>> GetPendingLabResultsAsync()
        {
            try
            {
                // In a real implementation, you would:
                // 1. Call the appropriate API endpoint (e.g., LabResultDetails or LabOrders)
                // 2. Filter for pending/unreviewed lab results
                // 3. Transform the API response into LabResultModel objects
                
                // For demonstration purposes, we'll return mock data
                return GetMockLabResults().Where(l => !l.IsReviewed).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving pending lab results: {ex.Message}");
                return new List<LabResultModel>();
            }
        }

        public async Task<List<LabResultModel>> GetLabResultsForPatientAsync(int patientId)
        {
            try
            {
                // In a real implementation, you would call the API with patient ID parameter
                // For demonstration, we'll filter our mock data
                return GetMockLabResults().Where(l => l.PatientId == patientId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving lab results for patient {patientId}: {ex.Message}");
                return new List<LabResultModel>();
            }
        }

        public async Task<bool> MarkLabResultAsReviewedAsync(int labResultId)
        {
            try
            {
                // In a real implementation, you would call the API to update the lab result status
                // For demonstration, we'll just return success
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking lab result {labResultId} as reviewed: {ex.Message}");
                return false;
            }
        }

        // Mock data for demonstration purposes
        private List<LabResultModel> GetMockLabResults()
        {
            return new List<LabResultModel>
            {
                new LabResultModel
                {
                    Id = 1,
                    PatientId = 1,
                    PatientName = "Alice Brown",
                    TestName = "Complete Blood Count (CBC)",
                    OrderDate = DateTime.Now.AddDays(-5),
                    ResultDate = DateTime.Now.AddDays(-1),
                    Status = "Completed",
                    IsUrgent = false,
                    IsReviewed = false,
                    ResultSummary = "WBC: 7.2, RBC: 4.8, Hemoglobin: 14.2, Hematocrit: 42%, Platelets: 250",
                    OrderingProvider = "Dr. J. Bertman"
                },
                new LabResultModel
                {
                    Id = 2,
                    PatientId = 5,
                    PatientName = "Emma White",
                    TestName = "Lipid Panel",
                    OrderDate = DateTime.Now.AddDays(-7),
                    ResultDate = DateTime.Now.AddDays(-2),
                    Status = "Completed",
                    IsUrgent = false,
                    IsReviewed = false,
                    ResultSummary = "Total Cholesterol: 210, HDL: 55, LDL: 130, Triglycerides: 125",
                    OrderingProvider = "Dr. J. Bertman"
                },
                new LabResultModel
                {
                    Id = 3,
                    PatientId = 2,
                    PatientName = "Bob Baker",
                    TestName = "Comprehensive Metabolic Panel",
                    OrderDate = DateTime.Now.AddDays(-3),
                    ResultDate = DateTime.Now.AddDays(-1),
                    Status = "Completed",
                    IsUrgent = true,
                    IsReviewed = false,
                    ResultSummary = "Glucose: 180 (High), BUN: 18, Creatinine: 0.9, Sodium: 140, Potassium: 4.5",
                    OrderingProvider = "Dr. J. Bertman"
                },
                new LabResultModel
                {
                    Id = 4,
                    PatientId = 3,
                    PatientName = "Carol Benson",
                    TestName = "Thyroid Stimulating Hormone",
                    OrderDate = DateTime.Now.AddDays(-10),
                    ResultDate = DateTime.Now.AddDays(-7),
                    Status = "Completed",
                    IsUrgent = false,
                    IsReviewed = true,
                    ResultSummary = "TSH: 2.5 mIU/L (Normal range: 0.4-4.0)",
                    OrderingProvider = "Dr. J. Bertman"
                },
                new LabResultModel
                {
                    Id = 5,
                    PatientId = 4,
                    PatientName = "David Wilson",
                    TestName = "Hemoglobin A1C",
                    OrderDate = DateTime.Now.AddDays(-14),
                    ResultDate = DateTime.Now.AddDays(-10),
                    Status = "Completed",
                    IsUrgent = false,
                    IsReviewed = true,
                    ResultSummary = "HbA1c: 7.2% (High)",
                    OrderingProvider = "Dr. J. Bertman"
                }
            };
        }
    }
}
