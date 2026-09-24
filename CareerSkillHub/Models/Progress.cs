using System;

namespace CareerSkillHub.Models
{
    /// Represents one row from the Progress table.
    public class Progress
    {
        public int ProgressID { get; set; }
        public int UserID { get; set; }
        public int CourseID { get; set; }
        public int ProgressPercentage { get; set; }
        public DateTime LastUpdated { get; set; }

        // Joined fields for display.
        public string CourseTitle { get; set; }
    }
}