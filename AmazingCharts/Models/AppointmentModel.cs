using System;

namespace AmazingCharts.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration => EndTime - StartTime;
        public string AppointmentType { get; set; } = string.Empty;
        public string? Status { get; set; }
        public string? Provider { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string ProviderId { get; set; } = string.Empty;
        public string ProviderName { get; set; } = "Dr. J. Bertman"; // Default provider
        public string LocationId { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public bool IsTelemedicine { get; set; }
        public bool IsConfirmed => Status == "Confirmed";
    }
}
