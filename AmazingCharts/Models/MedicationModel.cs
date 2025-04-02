using System;

namespace AmazingCharts.Models
{
    public class MedicationModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string? Name { get; set; }
        public string? Dosage { get; set; }
        public string? Frequency { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public string? Prescriber { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
