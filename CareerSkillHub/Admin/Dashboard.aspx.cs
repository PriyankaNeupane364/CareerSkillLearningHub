using System;
using System.Collections.Generic;
using System.Web.UI;
using CareerSkillHub.BLL;

namespace CareerSkillHub.Admin
{
    public partial class Dashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthBLL.RequireRole("Admin");

            if (!IsPostBack)
            {
                UserBLL userBll = new UserBLL();
                litStudents.Text = userBll.CountByRole("Student").ToString();
                litInstructors.Text = userBll.CountByRole("Instructor").ToString();
                litCourses.Text = new CourseBLL().CountAll().ToString();
                litEnrolments.Text = new EnrollmentBLL().CountAll().ToString();
                litQuizzes.Text = new QuizBLL().CountAll().ToString();
                litFeedback.Text = new FeedbackBLL().CountAll().ToString();

                List<Models.Feedback> latest = new FeedbackBLL().GetAll();
                rptFeedback.DataSource = latest;
                rptFeedback.DataBind();
                trNoFeedback.Visible = rptFeedback.Items.Count == 0;
            }
        }
    }
}