using System;
using System.Collections.Generic;

namespace AmazingCharts.Models
{
    /// <summary>
    /// Model representing a healthcare provider in the system
    /// </summary>
    public class ProviderModel
    {
        public int Id { get; set; }
        public string NPI { get; set; } = string.Empty; // National Provider Identifier
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Credentials { get; set; } = string.Empty; // MD, DO, NP, PA, etc.
        public string Specialty { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string FaxNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string DEANumber { get; set; } = string.Empty; // For prescribing controlled substances
        public string StateLicenseNumber { get; set; } = string.Empty;
        public DateTime LicenseExpirationDate { get; set; }
        public List<string> PracticeLocations { get; set; } = new List<string>();
        public int? UserId { get; set; } // Link to user account
    }
}
