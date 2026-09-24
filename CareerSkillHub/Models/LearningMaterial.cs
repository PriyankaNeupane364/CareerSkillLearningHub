using System;

namespace CareerSkillHub.Models
{
    /// Represents one row from the LearningMaterials table.
    public class LearningMaterial
    {
        public int MaterialID { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string VideoURL { get; set; }
        public string ResourceURL { get; set; }
        public DateTime CreatedDate { get; set; }

        // Joined field for display.
        public string CourseTitle { get; set; }
    }
}