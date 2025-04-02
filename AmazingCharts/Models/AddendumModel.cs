using System;

namespace AmazingCharts.Models
{
    public class AddendumModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
    }
}
