using System;

namespace CareerSkillHub.Models
{
    /// Represents one row from the WebsiteContent table
    /// (used for homepage announcement, about content, featured content).
    public class WebsiteContent
    {
        public int ContentID { get; set; }
        public string ContentType { get; set; } // Homepage, About, Featured
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}