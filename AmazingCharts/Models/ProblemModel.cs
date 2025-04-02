using System;

namespace AmazingCharts.Models
{
    /// <summary>
    /// Model representing a patient problem/diagnosis
    /// </summary>
    public class ProblemModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ICD10Code { get; set; } = string.Empty;
        public string SNOMEDCode { get; set; } = string.Empty;
        public DateTime OnsetDate { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Active, Resolved, Inactive
        public string Severity { get; set; } = string.Empty; // Mild, Moderate, Severe
        public string Type { get; set; } = string.Empty; // Chronic, Acute, etc.
        public bool IsPrimary { get; set; }
        public bool IsChronicCondition { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int? EncounterId { get; set; } // Encounter where problem was identified
        public string TreatmentPlan { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
    }
}
