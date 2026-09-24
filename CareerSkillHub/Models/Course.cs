using System;

namespace CareerSkillHub.Models
{
    /// Represents one row from the Courses table.
    public class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int InstructorID { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreatedDate { get; set; }

        // Joined from the Users table for display purposes.
        public string InstructorName { get; set; }
    }
}