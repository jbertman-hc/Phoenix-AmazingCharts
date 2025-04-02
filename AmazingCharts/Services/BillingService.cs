using AmazingCharts.ApiClient;
using AmazingCharts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingCharts.Services
{
    public class BillingService
    {
        private readonly IEhrApiClient _apiClient;

        public BillingService(IEhrApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<ClaimModel>> GetDeniedClaimsAsync()
        {
            try
            {
                // In a real implementation, you would:
                // 1. Call the appropriate API endpoint (e.g., Billing, BillingCauseOfAccident, etc.)
                // 2. Filter for denied claims
                // 3. Transform the API response into ClaimModel objects
                
                // For demonstration purposes, we'll return mock data
                return GetMockClaims().Where(c => c.Status == "Denied").ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving denied claims: {ex.Message}");
                return new List<ClaimModel>();
            }
        }

        public async Task<List<ClaimModel>> GetClaimsByStatusAsync(string status)
        {
            try
            {
                // In a real implementation, you would call the API with status parameter
                // For demonstration, we'll filter our mock data
                return GetMockClaims().Where(c => c.Status == status).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving claims with status {status}: {ex.Message}");
                return new List<ClaimModel>();
            }
        }

        public async Task<List<ClaimModel>> GetClaimsForPatientAsync(int patientId)
        {
            try
            {
                // In a real implementation, you would call the API with patient ID parameter
                // For demonstration, we'll filter our mock data
                return GetMockClaims().Where(c => c.PatientId == patientId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving claims for patient {patientId}: {ex.Message}");
                return new List<ClaimModel>();
            }
        }

        public async Task<bool> ResubmitClaimAsync(int claimId)
        {
            try
            {
                // In a real implementation, you would call the API to resubmit the claim
                // For demonstration, we'll just return success
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error resubmitting claim {claimId}: {ex.Message}");
                return false;
            }
        }

        // Mock data for demonstration purposes
        private List<ClaimModel> GetMockClaims()
        {
            return new List<ClaimModel>
            {
                new ClaimModel
                {
                    Id = 1,
                    ClaimNumber = "CLM12345",
                    PatientId = 4,
                    PatientName = "David Wilson",
                    ServiceDate = DateTime.Now.AddDays(-20),
                    SubmissionDate = DateTime.Now.AddDays(-18),
                    Amount = 450.00m,
                    Status = "Denied",
                    DenialReason = "Non-covered service",
                    InsuranceProvider = "Medicare",
                    ActionRequired = true,
                    Notes = "Need to appeal with additional documentation"
                },
                new ClaimModel
                {
                    Id = 2,
                    ClaimNumber = "CLM23456",
                    PatientId = 3,
                    PatientName = "Carol Benson",
                    ServiceDate = DateTime.Now.AddDays(-15),
                    SubmissionDate = DateTime.Now.AddDays(-14),
                    Amount = 275.50m,
                    Status = "Pending",
                    DenialReason = null,
                    InsuranceProvider = "United Healthcare",
                    ActionRequired = false,
                    Notes = "Awaiting insurance response"
                },
                new ClaimModel
                {
                    Id = 3,
                    ClaimNumber = "CLM34567",
                    PatientId = 5,
                    PatientName = "Emma White",
                    ServiceDate = DateTime.Now.AddDays(-25),
                    SubmissionDate = DateTime.Now.AddDays(-23),
                    Amount = 375.00m,
                    Status = "Denied",
                    DenialReason = "Pre-authorization required",
                    InsuranceProvider = "Cigna",
                    ActionRequired = true,
                    Notes = "Need to obtain pre-authorization and resubmit"
                },
                new ClaimModel
                {
                    Id = 4,
                    ClaimNumber = "CLM45678",
                    PatientId = 1,
                    PatientName = "Alice Brown",
                    ServiceDate = DateTime.Now.AddDays(-10),
                    SubmissionDate = DateTime.Now.AddDays(-8),
                    Amount = 150.75m,
                    Status = "Approved",
                    DenialReason = null,
                    InsuranceProvider = "Blue Cross",
                    ActionRequired = false,
                    Notes = "Payment expected within 30 days"
                },
                new ClaimModel
                {
                    Id = 5,
                    ClaimNumber = "CLM56789",
                    PatientId = 2,
                    PatientName = "Bob Baker",
                    ServiceDate = DateTime.Now.AddDays(-30),
                    SubmissionDate = DateTime.Now.AddDays(-28),
                    Amount = 525.25m,
                    Status = "Partially Paid",
                    DenialReason = "Patient responsibility",
                    InsuranceProvider = "Aetna",
                    ActionRequired = true,
                    Notes = "Bill patient for remaining balance"
                }
            };
        }
    }
}
