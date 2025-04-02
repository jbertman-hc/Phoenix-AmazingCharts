using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AmazingCharts.Models;

namespace AmazingCharts.Services
{
    public class MockDataService
    {
        // Patients
        public List<PatientModel> GetPatients()
        {
            return new List<PatientModel>
            {
                new PatientModel
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1980, 5, 15),
                    Gender = "Male",
                    PhoneNumber = "555-123-4567",
                    Email = "john.doe@example.com",
                    Address = "123 Main St",
                    City = "Anytown",
                    State = "CA",
                    ZipCode = "12345",
                    InsuranceProvider = "Blue Cross",
                    InsurancePolicyNumber = "BC12345678",
                    MedicalRecordNumber = "MRN001",
                    LastVisitDate = DateTime.Now.AddMonths(-1)
                },
                new PatientModel
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1975, 8, 22),
                    Gender = "Female",
                    PhoneNumber = "555-987-6543",
                    Email = "jane.smith@example.com",
                    Address = "456 Oak Ave",
                    City = "Somewhere",
                    State = "NY",
                    ZipCode = "67890",
                    InsuranceProvider = "Aetna",
                    InsurancePolicyNumber = "AE87654321",
                    MedicalRecordNumber = "MRN002",
                    LastVisitDate = DateTime.Now.AddMonths(-2)
                },
                new PatientModel
                {
                    Id = 3,
                    FirstName = "Robert",
                    LastName = "Johnson",
                    DateOfBirth = new DateTime(1990, 3, 10),
                    Gender = "Male",
                    PhoneNumber = "555-456-7890",
                    Email = "robert.johnson@example.com",
                    Address = "789 Pine Rd",
                    City = "Elsewhere",
                    State = "TX",
                    ZipCode = "54321",
                    InsuranceProvider = "United Healthcare",
                    InsurancePolicyNumber = "UH24681012",
                    MedicalRecordNumber = "MRN003",
                    LastVisitDate = DateTime.Now.AddDays(-15)
                }
            };
        }

        // Appointments
        public List<AppointmentModel> GetAppointments()
        {
            return new List<AppointmentModel>
            {
                new AppointmentModel
                {
                    Id = 1,
                    PatientId = 1,
                    PatientName = "John Doe",
                    ProviderId = "1",
                    ProviderName = "Dr. Sarah Wilson",
                    AppointmentType = "New Patient",
                    StartTime = DateTime.Now.Date.AddHours(9),
                    EndTime = DateTime.Now.Date.AddHours(10),
                    Status = "Scheduled",
                    Notes = "Initial consultation"
                },
                new AppointmentModel
                {
                    Id = 2,
                    PatientId = 2,
                    PatientName = "Jane Smith",
                    ProviderId = "2",
                    ProviderName = "Dr. Michael Chen",
                    AppointmentType = "Follow-up",
                    StartTime = DateTime.Now.Date.AddHours(10).AddMinutes(30),
                    EndTime = DateTime.Now.Date.AddHours(11),
                    Status = "Scheduled",
                    Notes = "Follow-up for hypertension"
                },
                new AppointmentModel
                {
                    Id = 3,
                    PatientId = 3,
                    PatientName = "Robert Johnson",
                    ProviderId = "1",
                    ProviderName = "Dr. Sarah Wilson",
                    AppointmentType = "Annual Physical",
                    StartTime = DateTime.Now.Date.AddHours(13),
                    EndTime = DateTime.Now.Date.AddHours(14),
                    Status = "Scheduled",
                    Notes = "Annual wellness check"
                },
                new AppointmentModel
                {
                    Id = 4,
                    PatientId = 1,
                    PatientName = "John Doe",
                    ProviderId = "3",
                    ProviderName = "Dr. Emily Rodriguez",
                    AppointmentType = "Lab Results Review",
                    StartTime = DateTime.Now.Date.AddDays(1).AddHours(11),
                    EndTime = DateTime.Now.Date.AddDays(1).AddHours(11).AddMinutes(30),
                    Status = "Scheduled",
                    Notes = "Review recent lab results"
                }
            };
        }

        // Lab Results
        public List<LabResultModel> GetLabResults()
        {
            return new List<LabResultModel>
            {
                new LabResultModel
                {
                    Id = 1,
                    PatientId = 1,
                    PatientName = "John Doe",
                    TestName = "Complete Blood Count",
                    OrderDate = DateTime.Now.AddDays(-7),
                    TestDate = DateTime.Now.AddDays(-6),
                    ResultDate = DateTime.Now.AddDays(-5),
                    Status = "Completed",
                    ResultSummary = "Within normal range",
                    Result = "Normal",
                    ReferenceRange = "4.5-11.0 x10^9/L",
                    IsAbnormal = false,
                    ProviderId = "1",
                    ProviderName = "Dr. Sarah Wilson"
                },
                new LabResultModel
                {
                    Id = 2,
                    PatientId = 2,
                    PatientName = "Jane Smith",
                    TestName = "Lipid Panel",
                    OrderDate = DateTime.Now.AddDays(-10),
                    TestDate = DateTime.Now.AddDays(-9),
                    ResultDate = DateTime.Now.AddDays(-8),
                    Status = "Completed",
                    ResultSummary = "Elevated LDL cholesterol",
                    Result = "Abnormal",
                    ReferenceRange = "LDL < 100 mg/dL",
                    IsAbnormal = true,
                    ProviderId = "2",
                    ProviderName = "Dr. Michael Chen"
                },
                new LabResultModel
                {
                    Id = 3,
                    PatientId = 3,
                    PatientName = "Robert Johnson",
                    TestName = "Comprehensive Metabolic Panel",
                    OrderDate = DateTime.Now.AddDays(-3),
                    TestDate = DateTime.Now.AddDays(-2),
                    ResultDate = DateTime.Now.AddDays(-1),
                    Status = "Pending",
                    ResultSummary = "",
                    Result = "",
                    ReferenceRange = "",
                    IsAbnormal = false,
                    ProviderId = "1",
                    ProviderName = "Dr. Sarah Wilson"
                },
                new LabResultModel
                {
                    Id = 4,
                    PatientId = 1,
                    PatientName = "John Doe",
                    TestName = "Hemoglobin A1C",
                    OrderDate = DateTime.Now.AddDays(-7),
                    TestDate = DateTime.Now.AddDays(-6),
                    ResultDate = DateTime.Now.AddDays(-4),
                    Status = "Completed",
                    ResultSummary = "Slightly elevated",
                    Result = "Abnormal",
                    ReferenceRange = "< 6.5%",
                    IsAbnormal = true,
                    ProviderId = "3",
                    ProviderName = "Dr. Emily Rodriguez"
                }
            };
        }

        // Messages
        public List<MessageModel> GetMessages()
        {
            return new List<MessageModel>
            {
                new MessageModel
                {
                    Id = 1,
                    Subject = "Lab Results Available",
                    Body = "Your recent lab results are now available. Please schedule a follow-up appointment to discuss them.",
                    SenderName = "Dr. Sarah Wilson",
                    RecipientName = "John Doe",
                    DateSent = DateTime.Now.AddDays(-2),
                    IsRead = false,
                    IsUrgent = true,
                    RelatedPatientId = 1,
                    RelatedPatientName = "John Doe"
                },
                new MessageModel
                {
                    Id = 2,
                    Subject = "Appointment Reminder",
                    Body = "This is a reminder that you have an appointment scheduled for tomorrow at 10:30 AM with Dr. Michael Chen.",
                    SenderName = "Scheduling System",
                    RecipientName = "Jane Smith",
                    DateSent = DateTime.Now.AddDays(-1),
                    IsRead = true,
                    IsUrgent = false,
                    RelatedPatientId = 2,
                    RelatedPatientName = "Jane Smith"
                },
                new MessageModel
                {
                    Id = 3,
                    Subject = "Prescription Refill Request",
                    Body = "I would like to request a refill for my hypertension medication. I have about 5 days of pills left.",
                    SenderName = "Robert Johnson",
                    RecipientName = "Dr. Sarah Wilson",
                    DateSent = DateTime.Now.AddHours(-5),
                    IsRead = false,
                    IsUrgent = false,
                    RelatedPatientId = 3,
                    RelatedPatientName = "Robert Johnson"
                },
                new MessageModel
                {
                    Id = 4,
                    Subject = "Insurance Verification Needed",
                    Body = "We need to verify your updated insurance information before your next appointment. Please call our billing department at your earliest convenience.",
                    SenderName = "Billing Department",
                    RecipientName = "John Doe",
                    DateSent = DateTime.Now.AddDays(-3),
                    IsRead = true,
                    IsUrgent = false,
                    RelatedPatientId = 1,
                    RelatedPatientName = "John Doe"
                }
            };
        }

        // Claims
        public List<ClaimModel> GetClaims()
        {
            return new List<ClaimModel>
            {
                new ClaimModel
                {
                    Id = 1,
                    PatientId = 1,
                    PatientName = "John Doe",
                    ServiceDate = DateTime.Now.AddDays(-30),
                    SubmissionDate = DateTime.Now.AddDays(-28),
                    ClaimNumber = "CLM12345",
                    InsuranceProvider = "Blue Cross",
                    Amount = 150.00m,
                    Status = "Paid",
                    PaymentDate = DateTime.Now.AddDays(-15),
                    RejectionReason = ""
                },
                new ClaimModel
                {
                    Id = 2,
                    PatientId = 2,
                    PatientName = "Jane Smith",
                    ServiceDate = DateTime.Now.AddDays(-25),
                    SubmissionDate = DateTime.Now.AddDays(-23),
                    ClaimNumber = "CLM67890",
                    InsuranceProvider = "Aetna",
                    Amount = 275.50m,
                    Status = "Denied",
                    PaymentDate = null,
                    RejectionReason = "Service not covered under current plan"
                },
                new ClaimModel
                {
                    Id = 3,
                    PatientId = 3,
                    PatientName = "Robert Johnson",
                    ServiceDate = DateTime.Now.AddDays(-15),
                    SubmissionDate = DateTime.Now.AddDays(-13),
                    ClaimNumber = "CLM24680",
                    InsuranceProvider = "United Healthcare",
                    Amount = 425.00m,
                    Status = "Pending",
                    PaymentDate = null,
                    RejectionReason = ""
                },
                new ClaimModel
                {
                    Id = 4,
                    PatientId = 1,
                    PatientName = "John Doe",
                    ServiceDate = DateTime.Now.AddDays(-10),
                    SubmissionDate = DateTime.Now.AddDays(-8),
                    ClaimNumber = "CLM13579",
                    InsuranceProvider = "Blue Cross",
                    Amount = 95.00m,
                    Status = "Denied",
                    PaymentDate = null,
                    RejectionReason = "Missing information"
                }
            };
        }
    }
}
