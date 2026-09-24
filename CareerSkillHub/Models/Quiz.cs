using System;

namespace CareerSkillHub.Models
{
    /// Represents one row from the Quizzes table.
    public class Quiz
    {
        public int QuizID { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        // Joined field for display.
        public string CourseTitle { get; set; }

        // Aggregated value (number of questions in the quiz).
        public int QuestionCount { get; set; }
    }
}