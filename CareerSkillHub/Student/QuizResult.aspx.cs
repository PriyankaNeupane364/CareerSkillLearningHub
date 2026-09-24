using System;
using System.Web.UI;
using CareerSkillHub.BLL;

namespace CareerSkillHub.Student
{
    public partial class QuizResult : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthBLL.RequireRole("Student");

            int resultId;
            if (!int.TryParse(Request.QueryString["resultId"], out resultId) || resultId <= 0)
            {
                ShowError("No result found.");
                return;
            }

            if (!IsPostBack)
            {
                // Load the stored result from the database (SELECT).
                Models.QuizResult r = new QuizResultBLL().GetById(resultId);

                if (r == null || r.UserID != AuthBLL.CurrentUserID)
                {
                    ShowError("Result not found.");
                    return;
                }

                Score = r.Score;
                TotalQuestions = r.TotalQuestions;
                Wrong = r.TotalQuestions - r.Score;
                Percentage = Convert.ToInt32(r.Percentage);
                QuizTitle = r.QuizTitle;
                CourseTitle = r.CourseTitle;

                DisplayGrade();
                phResult.Visible = true;
            }
        }

        private void DisplayGrade()
        {
            int p = Percentage;
            string grade = p >= 80 ? "Excellent - keep it up!" :
                           p >= 60 ? "Good job!" :
                           p >= 40 ? "Fair. Review the materials and try again." :
                           "Keep practising. Review the materials and try again.";

            litGrade.Text =
                "<div class=\"alert " + (p >= 60 ? "alert-success" : "alert-info") + "\">" +
                Server.HtmlEncode(grade) + "</div>";
        }

        private void ShowError(string message)
        {
            lblMessage.CssClass = "alert alert-error";
            lblMessage.Text = Server.HtmlEncode(message);
            lblMessage.Visible = true;
        }

        protected int Score = 0;
        protected int TotalQuestions = 0;
        protected int Wrong = 0;
        protected int Percentage = 0;
        protected string QuizTitle = "";
        protected string CourseTitle = "";
    }
}