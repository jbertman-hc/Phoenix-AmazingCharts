using System;

namespace AmazingCharts.Models
{
    /// <summary>
    /// Model representing a patient document
    /// </summary>
    public class DocumentModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string DocumentType { get; set; } = string.Empty; // Lab Report, Imaging, Consultation, etc.
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty; // PDF, JPEG, etc.
        public string FilePath { get; set; } = string.Empty;
        public long FileSize { get; set; } // In bytes
        public DateTime DocumentDate { get; set; } // Date of the document content
        public DateTime UploadDate { get; set; }
        public int UploadedByUserId { get; set; }
        public string UploadedByName { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty; // Patient Portal, Fax, Scan, etc.
        public string Status { get; set; } = string.Empty; // New, Reviewed, Filed
        public int? ReviewedByProviderId { get; set; }
        public string ReviewedByProviderName { get; set; } = string.Empty;
        public DateTime? ReviewDate { get; set; }
        public string Category { get; set; } = string.Empty; // Clinical, Administrative, etc.
        public string Tags { get; set; } = string.Empty; // Comma-separated tags
        public bool IsConfidential { get; set; }
        public int? EncounterId { get; set; } // Related encounter if applicable
        public string Notes { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedByUserId { get; set; }
    }
}
