using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AmazingCharts.Models
{
    /// <summary>
    /// Model representing a user in the system
    /// </summary>
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        
        [JsonIgnore] // Don't serialize password
        public string Password { get; set; } = string.Empty;
        
        public List<string> Permissions { get; set; } = new List<string>();
        public int? ProviderId { get; set; } // Link to provider if user is a provider
    }
}
