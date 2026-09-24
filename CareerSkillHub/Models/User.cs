using System;

namespace CareerSkillHub.Models
{
    /// Represents one row from the Users table.
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}