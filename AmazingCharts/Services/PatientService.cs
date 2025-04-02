using AmazingCharts.ApiClient;
using AmazingCharts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingCharts.Services
{
    public class PatientService
    {
        private readonly IEhrApiClient _apiClient;

        public PatientService(IEhrApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<PatientModel>> GetPatientsAsync()
        {
            // Since there's no direct "Get all patients" endpoint in the API,
            // we'll use the Demographics endpoint as a proxy for patient data
            try
            {
                // This is a placeholder. In a real implementation, you would:
                // 1. Call the appropriate API endpoint(s) to get patient data
                // 2. Transform the API response into PatientModel objects
                
                // For demonstration purposes, we'll return mock data
                return GetMockPatients();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving patients: {ex.Message}");
                return new List<PatientModel>();
            }
        }

        public async Task<PatientModel?> GetPatientByIdAsync(int id)
        {
            try
            {
                // In a real implementation, you would call the API
                // For demonstration, we'll return a mock patient
                return GetMockPatients().FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving patient {id}: {ex.Message}");
                return null;
            }
        }

        // Mock data for demonstration purposes
        private List<PatientModel> GetMockPatients()
        {
            return new List<PatientModel>
            {
                new PatientModel
                {
                    Id = 1,
                    FirstName = "Alice",
                    LastName = "Brown",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    Gender = "Female",
                    PhoneNumber = "555-123-4567",
                    Email = "alice.brown@example.com",
                    Address = "123 Main St, Anytown, USA",
                    InsuranceProvider = "Blue Cross",
                    MedicalRecordNumber = "MRN12345"
                },
                new PatientModel
                {
                    Id = 2,
                    FirstName = "Bob",
                    LastName = "Baker",
                    DateOfBirth = new DateTime(1978, 8, 22),
                    Gender = "Male",
                    PhoneNumber = "555-987-6543",
                    Email = "bob.baker@example.com",
                    Address = "456 Oak Ave, Somewhere, USA",
                    InsuranceProvider = "Aetna",
                    MedicalRecordNumber = "MRN67890"
                },
                new PatientModel
                {
                    Id = 3,
                    FirstName = "Carol",
                    LastName = "Benson",
                    DateOfBirth = new DateTime(1990, 3, 10),
                    Gender = "Female",
                    PhoneNumber = "555-456-7890",
                    Email = "carol.benson@example.com",
                    Address = "789 Pine St, Elsewhere, USA",
                    InsuranceProvider = "United Healthcare",
                    MedicalRecordNumber = "MRN24680"
                },
                new PatientModel
                {
                    Id = 4,
                    FirstName = "David",
                    LastName = "Wilson",
                    DateOfBirth = new DateTime(1965, 11, 28),
                    Gender = "Male",
                    PhoneNumber = "555-789-0123",
                    Email = "david.wilson@example.com",
                    Address = "321 Elm St, Nowhere, USA",
                    InsuranceProvider = "Medicare",
                    MedicalRecordNumber = "MRN13579"
                },
                new PatientModel
                {
                    Id = 5,
                    FirstName = "Emma",
                    LastName = "White",
                    DateOfBirth = new DateTime(1992, 7, 4),
                    Gender = "Female",
                    PhoneNumber = "555-234-5678",
                    Email = "emma.white@example.com",
                    Address = "654 Maple Ave, Anywhere, USA",
                    InsuranceProvider = "Cigna",
                    MedicalRecordNumber = "MRN97531"
                }
            };
        }
    }
}
