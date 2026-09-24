using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CareerSkillHub.BLL;

namespace CareerSkillHub.Student
{
    public partial class Quiz : Page
    {
        private readonly QuizBLL _quizBll = new QuizBLL();
        private List<Models.Question> _questions = new List<Models.Question>();
        private int _courseId;
        private int _quizId;

        public string QuizTitle { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthBLL.RequireRole("Student");

            if (!int.TryParse(Request.QueryString["courseId"], out _courseId) || _courseId <= 0)
            {
                Response.Redirect("~/Student/MyCourses.aspx");
                return;
            }

            // A student may only take quizzes for a course they are enrolled in.
            if (!new EnrollmentBLL().IsEnrolled(AuthBLL.CurrentUserID, _courseId))
            {
                Response.Redirect("~/Pages/AccessDenied.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadQuiz();
            }
        }

        private void LoadQuiz()
        {
            // Pick the first quiz published for this course (SELECT).
            List<Models.Quiz> quizzes = _quizBll.GetByCourse(_courseId);
            if (quizzes.Count == 0)
            {
                ShowError("No quiz is available for this course yet.");
                return;
            }

            _quizId = quizzes[0].QuizID;
            QuizTitle = Server.HtmlEncode(quizzes[0].Title);

            // Questions are loaded fresh from the database (SELECT).
            _questions = _quizBll.GetQuestions(_quizId);
            if (_questions.Count == 0)
            {
                ShowError("This quiz has no questions yet.");
                return;
            }

            rptQuestions.DataSource = _questions;
            rptQuestions.ItemDataBound += rptQuestions_ItemDataBound;
            rptQuestions.DataBind();
            phQuiz.Visible = true;
        }

        protected void rptQuestions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            Models.Question q = (Models.Question)e.Item.DataItem;

            // Fill the four radio options (client-side display only).
            RadioButtonList rbl = (RadioButtonList)e.Item.FindControl("rblAnswer");
            rbl.Items.Add(new ListItem("A. " + q.OptionA, "A"));
            rbl.Items.Add(new ListItem("B. " + q.OptionB, "B"));
            rbl.Items.Add(new ListItem("C. " + q.OptionC, "C"));
            rbl.Items.Add(new ListItem("D. " + q.OptionD, "D"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Questions are re-loaded from the database so the correct answers
            // can never be tampered with in the browser (server-side grading).
            int quizIdFromCourse = _quizBll.GetByCourse(_courseId)[0].QuizID;
            _questions = _quizBll.GetQuestions(quizIdFromCourse);

            int score = 0;
            int index = 0;

            foreach (RepeaterItem item in rptQuestions.Items)
            {
                RadioButtonList rbl = (RadioButtonList)item.FindControl("rblAnswer");
                string selected = rbl.SelectedValue;

                // Compare student's answer with the correct answer (server-side).
                if (index < _questions.Count &&
                    string.Equals(selected, _questions[index].CorrectAnswer))
                {
                    score++;
                }
                index++;
            }

            int total = _questions.Count;

            // Update progress for this course based on the quiz result.
            int progress = total > 0 ? (int)(score * 100 / total) : 0;
            new ProgressBLL().SetProgress(AuthBLL.CurrentUserID, _courseId, progress);

            // Store the result in the database (INSERT).
            int resultId = new QuizResultBLL().SaveResult(AuthBLL.CurrentUserID, quizIdFromCourse, score, total);

            // Show the result page.
            Response.Redirect("~/Student/QuizResult.aspx?resultId=" + resultId);
        }

        private void ShowError(string message)
        {
            lblMessage.CssClass = "alert alert-error";
            lblMessage.Text = Server.HtmlEncode(message);
            lblMessage.Visible = true;
        }
    }
}