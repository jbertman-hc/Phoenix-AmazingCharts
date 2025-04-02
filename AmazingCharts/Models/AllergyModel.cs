using System;

namespace AmazingCharts.Models
{
    public class AllergyModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string? AllergyName { get; set; }
        public string? Severity { get; set; }
        public string? Reaction { get; set; }
        public DateTime OnsetDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
