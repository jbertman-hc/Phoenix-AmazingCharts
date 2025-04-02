using System;

namespace AmazingCharts.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string RecipientName { get; set; } = string.Empty;
        public DateTime DateSent { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }
        public bool IsUrgent { get; set; }
        public string MessageType { get; set; } = string.Empty; // Internal, External, System, etc.
        public int? RelatedPatientId { get; set; }
        public string? RelatedPatientName { get; set; }
        public string? Category { get; set; } // Refill Request, Lab Result, General, etc.
        public string? AttachmentUrl { get; set; }
        public bool HasAttachment => !string.IsNullOrEmpty(AttachmentUrl);
    }
}
