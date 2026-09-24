using System;

namespace CareerSkillHub.Models
{
    /// Represents one row from the QuizResults table.
    public class QuizResult
    {
        public int ResultID { get; set; }
        public int UserID { get; set; }
        public int QuizID { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public decimal Percentage { get; set; }
        public DateTime AttemptDate { get; set; }

        // Joined fields for display.
        public string QuizTitle { get; set; }
        public string CourseTitle { get; set; }
    }
}