using System;
using System.Collections.Generic;
using System.Web.UI;
using CareerSkillHub.BLL;
using CareerSkillHub.Models;

namespace CareerSkillHub.Pages
{
    public partial class Courses : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Public course browsing: display all courses (SELECT).
                List<Course> all = new CourseBLL().GetAll();
                rptCourses.DataSource = all;
                rptCourses.DataBind();

                if (all.Count == 0)
                {
                    lblMessage.Text = "No courses are available yet.";
                    lblMessage.Visible = true;
                }
            }
        }
    }
}