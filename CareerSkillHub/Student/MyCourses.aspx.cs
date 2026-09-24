using System;
using System.Collections.Generic;
using System.Web.UI;
using CareerSkillHub.BLL;

namespace CareerSkillHub.Student
{
    public partial class MyCourses : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthBLL.RequireRole("Student");

            if (!IsPostBack)
            {
                // Shows only the courses the student has enrolled in (SELECT + JOIN).
                List<Models.Enrollment> list = new EnrollmentBLL().GetByUser(AuthBLL.CurrentUserID);
                rptCourses.DataSource = list;
                rptCourses.DataBind();

                if (list.Count == 0)
                {
                    lblMessage.CssClass = "alert alert-info";
                    lblMessage.Text = "You have not enrolled in any courses yet. " +
                        "<a href='~/Student/Courses.aspx' runat='server'>Browse courses</a> to get started.";
                    lblMessage.Visible = true;
                }
            }
        }
    }
}