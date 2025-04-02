using System;

namespace AmazingCharts.Models
{
    public class LabResultModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public string TestCode { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public DateTime TestDate { get; set; }
        public DateTime? ResultDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsUrgent { get; set; }
        public bool IsReviewed { get; set; }
        public string? Result { get; set; }
        public string? ReferenceRange { get; set; }
        public string ResultSummary { get; set; } = string.Empty;
        public string DetailedResults { get; set; } = string.Empty;
        public string? OrderingProvider { get; set; }
        public string? ProviderId { get; set; }
        public string? ProviderName { get; set; }
        public string PerformingLab { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool HasAbnormalResults { get; set; }
        public bool IsAbnormal { get; set; }
    }
}
