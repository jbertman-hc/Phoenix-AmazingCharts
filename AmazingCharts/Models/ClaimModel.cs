using System;

namespace AmazingCharts.Models
{
    public class ClaimModel
    {
        public int Id { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public DateTime ServiceDate { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public decimal Amount { get; set; }
        public decimal? PaidAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? PatientResponsibility { get; set; }
        public string Status { get; set; } = string.Empty; // Pending, Approved, Denied, Partially Paid, etc.
        public string? DenialReason { get; set; }
        public string? RejectionReason { get; set; }
        public string InsuranceProvider { get; set; } = string.Empty;
        public string? InsurancePolicyNumber { get; set; }
        public bool ActionRequired { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string? DiagnosisCodes { get; set; }
        public string? ProcedureCodes { get; set; }
    }
}
