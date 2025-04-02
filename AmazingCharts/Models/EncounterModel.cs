using System;
using System.Collections.Generic;

namespace AmazingCharts.Models
{
    /// <summary>
    /// Model representing a patient encounter/visit
    /// </summary>
    public class EncounterModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int ProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public string EncounterType { get; set; } = string.Empty; // Office Visit, Telehealth, Hospital, etc.
        public string VisitType { get; set; } = string.Empty; // New Patient, Established Patient, Follow-up
        public string AppointmentType { get; set; } = string.Empty; // Annual Physical, Sick Visit, etc.
        public DateTime EncounterDate { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string Status { get; set; } = string.Empty; // Scheduled, Checked In, In Progress, Completed, Cancelled
        public string Location { get; set; } = string.Empty; // Facility/Practice location
        public string Room { get; set; } = string.Empty;
        
        // Vital signs
        public decimal? Height { get; set; }
        public string HeightUnit { get; set; } = "in"; // in, cm
        public decimal? Weight { get; set; }
        public string WeightUnit { get; set; } = "lbs"; // lbs, kg
        public decimal? BMI { get; set; }
        public decimal? Temperature { get; set; }
        public string TemperatureUnit { get; set; } = "F"; // F, C
        public int? HeartRate { get; set; }
        public int? RespiratoryRate { get; set; }
        public int? SystolicBP { get; set; }
        public int? DiastolicBP { get; set; }
        public decimal? O2Saturation { get; set; }
        public string Pain { get; set; } = string.Empty; // Pain scale
        
        public string ChiefComplaint { get; set; } = string.Empty;
        public List<string> DiagnosisCodes { get; set; } = new List<string>(); // ICD-10 codes
        public List<string> ProcedureCodes { get; set; } = new List<string>(); // CPT codes
        
        // Billing information
        public string BillingStatus { get; set; } = string.Empty; // Not Billed, Billed, Paid, Denied
        public string ServiceLevel { get; set; } = string.Empty; // E&M code level (99213, etc.)
        public decimal? Copay { get; set; }
        public bool CopaySatisfied { get; set; }
        public string InsuranceName { get; set; } = string.Empty;
        public string InsuranceId { get; set; } = string.Empty;
        
        public string Notes { get; set; } = string.Empty;
        public bool IsSignedOff { get; set; }
        public DateTime? SignedOffDate { get; set; }
        
        // References to related data
        public int? ClinicalNoteId { get; set; }
        public List<int> OrderIds { get; set; } = new List<int>();
        public List<int> PrescriptionIds { get; set; } = new List<int>();
    }
}
