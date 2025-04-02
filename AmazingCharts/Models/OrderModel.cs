using System;
using System.Collections.Generic;

namespace AmazingCharts.Models
{
    /// <summary>
    /// Model representing a clinical order (CPOE - Computerized Provider Order Entry)
    /// </summary>
    public class OrderModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public int? EncounterId { get; set; }
        public string OrderType { get; set; } = string.Empty; // Lab, Radiology, Procedure, Referral, etc.
        public string OrderName { get; set; } = string.Empty;
        public string OrderCode { get; set; } = string.Empty; // LOINC, CPT, etc.
        public string Status { get; set; } = string.Empty; // Pending, Completed, Cancelled
        public DateTime OrderDate { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Priority { get; set; } = string.Empty; // Routine, STAT, Urgent
        public string Diagnosis { get; set; } = string.Empty;
        public string DiagnosisCode { get; set; } = string.Empty; // ICD-10
        public string Instructions { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string PerformingFacility { get; set; } = string.Empty;
        public string PerformingProvider { get; set; } = string.Empty;
        public bool IsFasting { get; set; }
        public string InsuranceAuthorizationNumber { get; set; } = string.Empty;
        public bool IsAuthorizationRequired { get; set; }
        public string AuthorizationStatus { get; set; } = string.Empty;
        public List<string> OrderItems { get; set; } = new List<string>(); // For panel orders
        public string ResultsUrl { get; set; } = string.Empty; // Link to results if available
    }
}
