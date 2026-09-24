using System;
using System.Collections.Generic;
using System.Web.UI;
using CareerSkillHub.BLL;

namespace CareerSkillHub.Student
{
    public partial class Dashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Student-only page.
            AuthBLL.RequireRole("Student");

            if (!IsPostBack)
            {
                int userId = AuthBLL.CurrentUserID;

                // All numbers come from the database (SELECT + COUNT queries).
                litEnrolled.Text = new EnrollmentBLL().CountByUser(userId).ToString();

                int quizCount = new QuizResultBLL().CountByUser(userId);
                litQuizzes.Text = quizCount.ToString();

                decimal best = new QuizResultBLL().HighestPercentageForUser(userId);
                litBestScore.Text = best.ToString("0.0") + "%";

                int progress = new ProgressBLL().AverageForUser(userId);
                litProgress.Text = progress + "%";

                StudentName = AuthBLL.CurrentUserName;

                List<Models.QuizResult> results = new QuizResultBLL().GetByUser(userId);
                rptResults.DataSource = results;
                rptResults.DataBind();
                trNoResults.Visible = rptResults.Items.Count == 0;
            }
        }

        protected string StudentName = "";
    }
}