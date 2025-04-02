using System;

namespace AmazingCharts.Models
{
    /// <summary>
    /// Model representing a patient's insurance information
    /// </summary>
    public class InsuranceModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string InsuranceName { get; set; } = string.Empty;
        public string PayerId { get; set; } = string.Empty; // Payer ID for electronic claims
        public string PlanName { get; set; } = string.Empty;
        public string PlanType { get; set; } = string.Empty; // HMO, PPO, Medicare, Medicaid, etc.
        public string PolicyNumber { get; set; } = string.Empty;
        public string GroupNumber { get; set; } = string.Empty;
        public string SubscriberId { get; set; } = string.Empty;
        public string SubscriberName { get; set; } = string.Empty;
        public DateTime? SubscriberDOB { get; set; }
        public string SubscriberRelationship { get; set; } = string.Empty; // Self, Spouse, Child, Other
        public string SubscriberAddress { get; set; } = string.Empty;
        public string SubscriberCity { get; set; } = string.Empty;
        public string SubscriberState { get; set; } = string.Empty;
        public string SubscriberZip { get; set; } = string.Empty;
        public string SubscriberPhone { get; set; } = string.Empty;
        public string SubscriberEmployer { get; set; } = string.Empty;
        public string InsurancePhone { get; set; } = string.Empty;
        public string InsuranceAddress { get; set; } = string.Empty;
        public string InsuranceCity { get; set; } = string.Empty;
        public string InsuranceState { get; set; } = string.Empty;
        public string InsuranceZip { get; set; } = string.Empty;
        public DateTime EffectiveDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public decimal? Copay { get; set; }
        public decimal? Deductible { get; set; }
        public decimal? DeductibleMet { get; set; }
        public decimal? CoInsurance { get; set; } // Percentage
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? EligibilityVerificationDate { get; set; }
        public string EligibilityStatus { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string CardFrontImageUrl { get; set; } = string.Empty;
        public string CardBackImageUrl { get; set; } = string.Empty;
    }
}
