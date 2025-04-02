using System;
using System.Collections.Generic;

namespace AmazingCharts.Models
{
    /// <summary>
    /// Model representing a medication prescription
    /// </summary>
    public class PrescriptionModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public string DrugName { get; set; } = string.Empty;
        public string DrugCode { get; set; } = string.Empty; // RxNorm, NDC, etc.
        public string Strength { get; set; } = string.Empty;
        public string Form { get; set; } = string.Empty; // Tablet, Capsule, etc.
        public string Sig { get; set; } = string.Empty; // Instructions for use
        public string Route { get; set; } = string.Empty; // Oral, Topical, etc.
        public int Quantity { get; set; }
        public int DaysSupply { get; set; }
        public int Refills { get; set; }
        public int RefillsRemaining { get; set; }
        public bool DispenseAsWritten { get; set; }
        public bool IsControlled { get; set; }
        public string ControlledSubstanceSchedule { get; set; } = string.Empty; // II, III, IV, V
        public DateTime PrescribedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? LastFilledDate { get; set; }
        public string Status { get; set; } = string.Empty; // Active, Completed, Discontinued
        public string PharmacyName { get; set; } = string.Empty;
        public string PharmacyPhone { get; set; } = string.Empty;
        public string PharmacyAddress { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public List<string> DrugAlerts { get; set; } = new List<string>();
        public bool IsSent { get; set; }
        public bool IsERx { get; set; } // Electronic prescription
    }
}
