using System;

namespace CareerSkillHub.Models
{
    /// Represents one row from the Enrollments table.
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int UserID { get; set; }
        public int CourseID { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // Joined fields for display.
        public string CourseTitle { get; set; }
        public string CourseCategory { get; set; }
        public string CourseImage { get; set; }
    }
}