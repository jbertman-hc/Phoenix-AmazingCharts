using System;

namespace AmazingCharts.Models
{
    /// <summary>
    /// Model representing a patient referral to another provider
    /// </summary>
    public class ReferralModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int ReferringProviderId { get; set; }
        public string ReferringProviderName { get; set; } = string.Empty;
        public int ReferredToProviderId { get; set; }
        public string ReferredToProviderName { get; set; } = string.Empty;
        public string ReferredToSpecialty { get; set; } = string.Empty;
        public string ReferredToFacility { get; set; } = string.Empty;
        public string ReferredToPhone { get; set; } = string.Empty;
        public string ReferredToFax { get; set; } = string.Empty;
        public string ReferredToAddress { get; set; } = string.Empty;
        public string ReferralReason { get; set; } = string.Empty;
        public string DiagnosisCode { get; set; } = string.Empty; // ICD-10
        public string DiagnosisDescription { get; set; } = string.Empty;
        public DateTime ReferralDate { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string Status { get; set; } = string.Empty; // Pending, Scheduled, Completed, Declined
        public string Priority { get; set; } = string.Empty; // Routine, Urgent, STAT
        public bool IsAuthorizationRequired { get; set; }
        public string AuthorizationNumber { get; set; } = string.Empty;
        public string AuthorizationStatus { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string ClinicalInformation { get; set; } = string.Empty;
        public bool DocumentsSent { get; set; }
        public DateTime? DocumentsSentDate { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string FollowUpNotes { get; set; } = string.Empty;
        public int? EncounterId { get; set; } // Encounter where referral was created
    }
}
