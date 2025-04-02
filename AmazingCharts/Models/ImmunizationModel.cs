using System;

namespace AmazingCharts.Models
{
    /// <summary>
    /// Model representing a patient immunization
    /// </summary>
    public class ImmunizationModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string VaccineName { get; set; } = string.Empty;
        public string CVXCode { get; set; } = string.Empty; // CDC Vaccine Code
        public string MVXCode { get; set; } = string.Empty; // Manufacturer Code
        public string NDCCode { get; set; } = string.Empty; // National Drug Code
        public string LotNumber { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public DateTime AdministrationDate { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public string AdministrationSite { get; set; } = string.Empty; // Left Arm, Right Thigh, etc.
        public string Route { get; set; } = string.Empty; // Intramuscular, Subcutaneous, etc.
        public string Dose { get; set; } = string.Empty;
        public string DoseUnit { get; set; } = string.Empty; // mL, mcg, etc.
        public int DoseNumber { get; set; } // Dose number in series
        public int TotalSeriesDoses { get; set; } // Total number of doses in series
        public bool IsHistorical { get; set; } // Administered elsewhere
        public string HistoricalSource { get; set; } = string.Empty;
        public bool InformationStatementProvided { get; set; } // VIS provided
        public DateTime? InformationStatementDate { get; set; } // VIS publication date
        public string ReasonCode { get; set; } = string.Empty; // Reason for immunization
        public string ReasonNotGivenCode { get; set; } = string.Empty; // If applicable
        public bool ReportedToRegistry { get; set; } // Reported to immunization registry
        public DateTime? NextDoseDate { get; set; } // Recommended date for next dose
        public string Notes { get; set; } = string.Empty;
        public string FundingSource { get; set; } = string.Empty; // VFC, Private, etc.
        public string FundingEligibility { get; set; } = string.Empty; // VFC eligibility
    }
}
