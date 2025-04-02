using System;
using System.Collections.Generic;

namespace AmazingCharts.Models
{
    /// <summary>
    /// Model representing a clinical note
    /// </summary>
    public class ClinicalNoteModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public int EncounterId { get; set; }
        public string NoteType { get; set; } = string.Empty; // Progress Note, H&P, Discharge Summary, etc.
        public DateTime NoteDate { get; set; }
        public DateTime? SignedDate { get; set; }
        public bool IsSigned { get; set; }
        public string Status { get; set; } = string.Empty; // Draft, Final, Amended
        
        // SOAP Note Components
        public string SubjectiveNote { get; set; } = string.Empty;
        public string ObjectiveNote { get; set; } = string.Empty;
        public string AssessmentNote { get; set; } = string.Empty;
        public string PlanNote { get; set; } = string.Empty;
        
        // Alternative structured components
        public string ChiefComplaint { get; set; } = string.Empty;
        public string HPI { get; set; } = string.Empty; // History of Present Illness
        public string ROS { get; set; } = string.Empty; // Review of Systems
        public string PhysicalExam { get; set; } = string.Empty;
        public string Assessment { get; set; } = string.Empty;
        public string Plan { get; set; } = string.Empty;
        
        public List<string> DiagnosisCodes { get; set; } = new List<string>(); // ICD-10 codes
        public List<string> ProcedureCodes { get; set; } = new List<string>(); // CPT codes
        
        public string FullNote { get; set; } = string.Empty; // Complete note text
        public List<string> Attachments { get; set; } = new List<string>(); // References to attached documents
        
        public List<string> CoSigners { get; set; } = new List<string>(); // For notes requiring co-signatures
        public bool RequiresCoSignature { get; set; }
    }
}
