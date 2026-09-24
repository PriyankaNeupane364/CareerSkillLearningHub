using System;
using System.Collections.Generic;
using System.Web.UI;
using CareerSkillHub.BLL;
using CareerSkillHub.Models;

namespace CareerSkillHub
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAnnouncement();
                LoadFeaturedCourses();
            }
        }

        private void LoadAnnouncement()
        {
            // Display the homepage announcement edited by the admin (SELECT).
            WebsiteContent wc = new WebsiteContentBLL().GetByTypeAndTitle("Homepage", "Welcome Announcement");
            if (wc != null)
                litAnnouncement.Text = Server.HtmlEncode(wc.Content);
            else
                litAnnouncement.Text = "Learn in-demand career skills through notes, videos and interactive quizzes.";
        }

        private void LoadFeaturedCourses()
        {
            // Show the latest three courses on the home page (SELECT).
            List<Course> courses = new CourseBLL().GetAll();
            if (courses.Count > 3) courses = courses.GetRange(0, 3);
            rptCourses.DataSource = courses;
            rptCourses.DataBind();
        }
    }
}