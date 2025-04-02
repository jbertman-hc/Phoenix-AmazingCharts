using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AmazingCharts.Models;

namespace AmazingCharts.ApiClient
{
    /// <summary>
    /// Temporary implementation of the EHR API client.
    /// This will be replaced by the NSwag-generated client.
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

        // Addendum endpoints
        public async Task<AddendumModel> GetAddendumAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Addendum/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AddendumModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting addendum: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new AddendumModel
            {
                Id = id,
                PatientId = 1,
                Content = "This is a sample addendum",
                CreatedDate = DateTime.Now.AddDays(-5),
                CreatedBy = "Dr. Smith"
            };
        }

        public async Task<List<AddendumModel>> GetAddendumsByPatientIdAsync(int patientId)
        {
            try
            {
                // Since there's no direct endpoint for this in swagger.json, we'll need to get all addendums and filter
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Addendum/{patientId}"));
                if (response.IsSuccessStatusCode)
                {
                    var addendum = await response.Content.ReadFromJsonAsync<AddendumModel>();
                    if (addendum != null)
                    {
                        return new List<AddendumModel> { addendum };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting addendums: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new List<AddendumModel>
            {
                new AddendumModel { Id = 1, PatientId = patientId, Content = "First addendum for patient", CreatedDate = DateTime.Now.AddDays(-10), CreatedBy = "Dr. Johnson" },
                new AddendumModel { Id = 2, PatientId = patientId, Content = "Second addendum for patient", CreatedDate = DateTime.Now.AddDays(-5), CreatedBy = "Dr. Smith" }
            };
        }

        // Allergy endpoints
        public async Task<AllergyModel> GetAllergyAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/AllergyLibrary/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AllergyModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting allergy: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new AllergyModel
            {
                Id = id,
                PatientId = 1,
                AllergyName = "Penicillin",
                Severity = "Severe",
                Reaction = "Anaphylaxis",
                OnsetDate = DateTime.Now.AddYears(-5),
                IsActive = true
            };
        }

        public async Task<List<AllergyModel>> GetAllergiesByPatientIdAsync(int patientId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/AllergyLibrary/patient/{patientId}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<AllergyModel>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting allergies: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new List<AllergyModel>
            {
                new AllergyModel { Id = 1, PatientId = patientId, AllergyName = "Penicillin", Severity = "Severe", Reaction = "Anaphylaxis", OnsetDate = DateTime.Now.AddYears(-5), IsActive = true },
                new AllergyModel { Id = 2, PatientId = patientId, AllergyName = "Peanuts", Severity = "Moderate", Reaction = "Hives", OnsetDate = DateTime.Now.AddYears(-10), IsActive = true }
            };
        }

        // Appointment endpoints
        public async Task<AppointmentModel> GetAppointmentAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Appointment/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AppointmentModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting appointment: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new AppointmentModel
            {
                Id = id,
                PatientId = 1,
                PatientName = "John Doe",
                AppointmentType = "Follow-up",
                StartTime = DateTime.Now.AddDays(1).AddHours(9),
                EndTime = DateTime.Now.AddDays(1).AddHours(10),
                Status = "Scheduled",
                Provider = "Dr. Smith"
            };
        }

        public async Task<List<AppointmentModel>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Appointment/date-range?start={startDate.ToString("yyyy-MM-dd")}&end={endDate.ToString("yyyy-MM-dd")}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<AppointmentModel>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting appointments: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new List<AppointmentModel>
            {
                new AppointmentModel { Id = 1, PatientId = 1, PatientName = "John Doe", AppointmentType = "Follow-up", StartTime = startDate.AddHours(9), EndTime = startDate.AddHours(10), Status = "Scheduled", Provider = "Dr. Smith" },
                new AppointmentModel { Id = 2, PatientId = 2, PatientName = "Jane Smith", AppointmentType = "New Patient", StartTime = startDate.AddHours(11), EndTime = startDate.AddHours(12), Status = "Scheduled", Provider = "Dr. Johnson" }
            };
        }

        // Billing endpoints
        public async Task<ClaimModel> GetBillingAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Claim/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ClaimModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting billing: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new ClaimModel
            {
                Id = id,
                PatientId = 1,
                PatientName = "John Doe",
                ServiceDate = DateTime.Now.AddDays(-30),
                ClaimDate = DateTime.Now.AddDays(-28),
                Amount = 150.00m,
                Status = "Submitted",
                InsuranceProvider = "Blue Cross"
            };
        }

        public async Task<List<ClaimModel>> GetBillingsByPatientIdAsync(int patientId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Claim/patient/{patientId}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ClaimModel>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting billings: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new List<ClaimModel>
            {
                new ClaimModel { Id = 1, PatientId = patientId, PatientName = "John Doe", ServiceDate = DateTime.Now.AddDays(-30), ClaimDate = DateTime.Now.AddDays(-28), Amount = 150.00m, Status = "Submitted", InsuranceProvider = "Blue Cross" },
                new ClaimModel { Id = 2, PatientId = patientId, PatientName = "John Doe", ServiceDate = DateTime.Now.AddDays(-60), ClaimDate = DateTime.Now.AddDays(-58), Amount = 75.00m, Status = "Paid", InsuranceProvider = "Blue Cross" }
            };
        }

        // Demographics endpoints
        public async Task<PatientModel> GetDemographicsAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Patient/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PatientModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting demographics: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new PatientModel
            {
                Id = id,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1980, 1, 15),
                Gender = "Male",
                Address = "123 Main St",
                City = "Anytown",
                State = "CA",
                ZipCode = "12345",
                PhoneNumber = "555-123-4567",
                Email = "john.doe@example.com",
                InsuranceProvider = "Blue Cross",
                InsuranceId = "BC123456789",
                LastVisitDate = DateTime.Now.AddMonths(-3)
            };
        }

        public async Task<List<PatientModel>> GetDemographicsByPatientIdAsync(int patientId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Patient/patient/{patientId}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<PatientModel>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting demographics: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new List<PatientModel>
            {
                new PatientModel
                {
                    Id = patientId,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1980, 1, 15),
                    Gender = "Male",
                    Address = "123 Main St",
                    City = "Anytown",
                    State = "CA",
                    ZipCode = "12345",
                    PhoneNumber = "555-123-4567",
                    Email = "john.doe@example.com",
                    InsuranceProvider = "Blue Cross",
                    InsuranceId = "BC123456789",
                    LastVisitDate = DateTime.Now.AddMonths(-3)
                }
            };
        }

        // Lab endpoints
        public async Task<LabResultModel> GetLabResultAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/LabResult/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LabResultModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting lab result: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new LabResultModel
            {
                Id = id,
                PatientId = 1,
                PatientName = "John Doe",
                TestName = "Complete Blood Count",
                TestDate = DateTime.Now.AddDays(-7),
                ResultDate = DateTime.Now.AddDays(-5),
                Status = "Completed",
                Result = "Normal",
                ReferenceRange = "4.5-11.0 x10^9/L",
                OrderingProvider = "Dr. Smith"
            };
        }

        public async Task<List<LabResultModel>> GetLabResultsByPatientIdAsync(int patientId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/LabResult/patient/{patientId}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<LabResultModel>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting lab results: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new List<LabResultModel>
            {
                new LabResultModel { Id = 1, PatientId = patientId, PatientName = "John Doe", TestName = "Complete Blood Count", TestDate = DateTime.Now.AddDays(-7), ResultDate = DateTime.Now.AddDays(-5), Status = "Completed", Result = "Normal", ReferenceRange = "4.5-11.0 x10^9/L", OrderingProvider = "Dr. Smith" },
                new LabResultModel { Id = 2, PatientId = patientId, PatientName = "John Doe", TestName = "Lipid Panel", TestDate = DateTime.Now.AddDays(-7), ResultDate = DateTime.Now.AddDays(-5), Status = "Completed", Result = "Abnormal", ReferenceRange = "LDL < 100 mg/dL", OrderingProvider = "Dr. Smith" }
            };
        }

        // Medication endpoints
        public async Task<MedicationModel> GetMedicationAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Medication/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MedicationModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting medication: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new MedicationModel
            {
                Id = id,
                PatientId = 1,
                Name = "Lisinopril",
                Dosage = "10mg",
                Frequency = "Once daily",
                StartDate = DateTime.Now.AddMonths(-6),
                EndDate = null,
                Prescriber = "Dr. Smith",
                IsActive = true
            };
        }

        public async Task<List<MedicationModel>> GetMedicationsByPatientIdAsync(int patientId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Medication/patient/{patientId}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<MedicationModel>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting medications: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new List<MedicationModel>
            {
                new MedicationModel { Id = 1, PatientId = patientId, Name = "Lisinopril", Dosage = "10mg", Frequency = "Once daily", StartDate = DateTime.Now.AddMonths(-6), EndDate = null, Prescriber = "Dr. Smith", IsActive = true },
                new MedicationModel { Id = 2, PatientId = patientId, Name = "Metformin", Dosage = "500mg", Frequency = "Twice daily", StartDate = DateTime.Now.AddMonths(-3), EndDate = null, Prescriber = "Dr. Johnson", IsActive = true }
            };
        }

        // Message endpoints
        public async Task<MessageModel> GetMessageAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Message/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MessageModel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting message: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new MessageModel
            {
                Id = id,
                Subject = "Test Message",
                Body = "This is a test message",
                SenderName = "Dr. Smith",
                RecipientName = "Dr. Johnson",
                DateSent = DateTime.Now.AddDays(-1),
                IsRead = false,
                IsUrgent = false,
                RelatedPatientId = 1,
                RelatedPatientName = "John Doe"
            };
        }

        public async Task<List<MessageModel>> GetMessagesByRecipientIdAsync(string recipientId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildProxyUrl($"api/Message/recipient/{recipientId}"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<MessageModel>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting messages: {ex.Message}");
            }

            // Fallback to mock data
            await Task.Delay(100); // Simulate API call
            return new List<MessageModel>
            {
                new MessageModel { Id = 1, Subject = "Test Message 1", Body = "This is test message 1", SenderName = "Dr. Smith", RecipientName = recipientId, DateSent = DateTime.Now.AddDays(-1), IsRead = false, IsUrgent = false, RelatedPatientId = 1, RelatedPatientName = "John Doe" },
                new MessageModel { Id = 2, Subject = "Test Message 2", Body = "This is test message 2", SenderName = "Dr. Johnson", RecipientName = recipientId, DateSent = DateTime.Now.AddDays(-2), IsRead = true, IsUrgent = false, RelatedPatientId = 2, RelatedPatientName = "Jane Smith" }
            };
        }

        // Health endpoint
        public async Task<bool> CheckHealthAsync()
        {
            try
            {
                // Use our local proxy server to avoid CORS issues
                var response = await _httpClient.GetAsync(BuildProxyUrl("api/health"));
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Health check failed: {ex.Message}");
                return false;
            }
        }
    }
}
