using System;

namespace CareerSkillHub.Models
{
    /// Represents one row from the Feedback table.
    public class Feedback
    {
        public int FeedbackID { get; set; }
        public int UserID { get; set; }
        public int Rating { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }

        // Joined field for display.
        public string StudentName { get; set; }
    }
}