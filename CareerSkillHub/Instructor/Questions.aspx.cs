using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CareerSkillHub.BLL;
using CareerSkillHub.Helpers;
using CareerSkillHub.Models;

namespace CareerSkillHub.Instructor
{
    public partial class Questions : Page
    {
        private readonly QuizBLL _quizBll = new QuizBLL();

        private int _editQuestionId
        {
            get { return ViewState["EditQuestionID"] == null ? 0 : (int)ViewState["EditQuestionID"]; }
            set { ViewState["EditQuestionID"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthBLL.RequireRole("Instructor");

            if (!IsPostBack)
            {
                BindQuizDropdown(0);
                int quizId;
                if (int.TryParse(Request.QueryString["quizId"], out quizId) && quizId > 0)
                {
                    ddlQuiz.SelectedValue = quizId.ToString();
                }
                BindQuestions();
            }
        }

        private void BindQuizDropdown(int selectedQuizId)
        {
            // Only quizzes of the instructor's own courses (SELECT with JOIN).
            List<Models.Quiz> mine = _quizBll.GetByInstructor(AuthBLL.CurrentUserID);
            ddlQuiz.DataSource = mine;
            ddlQuiz.DataTextField = "Title";
            ddlQuiz.DataValueField = "QuizID";
            ddlQuiz.DataBind();
            ddlQuiz.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select a quiz --", "0"));
            if (selectedQuizId > 0)
                ddlQuiz.SelectedValue = selectedQuizId.ToString();
        }

        protected void ddlQuiz_SelectedIndexChanged(object sender, EventArgs e)
        {
            _editQuestionId = 0;
            BindQuestions();
        }

        private void BindQuestions()
        {
            int quizId = GetQuizId();

            if (quizId == 0)
            {
                ShowMessage("Select a quiz to manage its questions.", "alert-info");
                lblMessage.Visible = true;
                rptQuestions.DataSource = new List<Models.Question>();
                rptQuestions.DataBind();
                trNoQuestions.Visible = rptQuestions.Items.Count == 0;
                return;
            }

            // All questions for the selected quiz (SELECT).
            rptQuestions.DataSource = _quizBll.GetQuestions(quizId);
            rptQuestions.DataBind();
            trNoQuestions.Visible = rptQuestions.Items.Count == 0;
        }

        private int GetQuizId()
        {
            int quizId;
            int.TryParse(ddlQuiz.SelectedValue, out quizId);
            if (quizId <= 0) return 0;

            // Make sure the quiz belongs to this instructor.
            Models.Quiz q = _quizBll.GetById(quizId);
            if (q == null) return 0;
            Course c = new CourseBLL().GetById(q.CourseID);
            return (c != null && c.InstructorID == AuthBLL.CurrentUserID) ? quizId : 0;
        }

        protected void Action_Command(object sender, CommandEventArgs e)
        {
            int questionId = Convert.ToInt32(e.CommandArgument);
            int quizId = GetQuizId();

            if (e.CommandName == "Edit")
            {
                Models.Question q = _quizBll.GetQuestionById(questionId);

                // The question must belong to the selected (own) quiz.
                if (quizId == 0 || q == null || q.QuizID != quizId)
                {
                    Response.Redirect("~/Pages/AccessDenied.aspx");
                    return;
                }

                _editQuestionId = questionId;
                txtQuestionText.Text = q.QuestionText;
                txtOptionA.Text = q.OptionA;
                txtOptionB.Text = q.OptionB;
                txtOptionC.Text = q.OptionC;
                txtOptionD.Text = q.OptionD;
                ddlCorrect.SelectedValue = q.CorrectAnswer;
                litFormTitle.Text = "Edit Question";
                ShowForm();
            }
            else if (e.CommandName == "Delete")
            {
                if (quizId == 0)
                {
                    Response.Redirect("~/Pages/AccessDenied.aspx");
                    return;
                }

                try
                {
                    // DELETE.
                    _quizBll.DeleteQuestion(questionId);
                    ShowMessage("Question deleted.", "alert-success");
                    BindQuestions();
                }
                catch (Exception)
                {
                    ShowMessage("Unable to delete the question.", "alert-error");
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (GetQuizId() == 0)
            {
                ShowMessage("Select a quiz first.", "alert-error");
                return;
            }

            _editQuestionId = 0;
            txtQuestionText.Text = "";
            txtOptionA.Text = "";
            txtOptionB.Text = "";
            txtOptionC.Text = "";
            txtOptionD.Text = "";
            ddlCorrect.SelectedIndex = 0;
            litFormTitle.Text = "Add New Question";
            ShowForm();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int quizId = GetQuizId();
            if (quizId == 0)
            {
                Response.Redirect("~/Pages/AccessDenied.aspx");
                return;
            }

            try
            {
                if (_editQuestionId == 0)
                {
                    // INSERT.
                    _quizBll.AddQuestion(quizId, txtQuestionText.Text,
                        txtOptionA.Text, txtOptionB.Text, txtOptionC.Text, txtOptionD.Text,
                        ddlCorrect.SelectedValue);
                    ShowMessage("Question added.", "alert-success");
                }
                else
                {
                    // UPDATE.
                    _quizBll.UpdateQuestion(_editQuestionId, quizId, txtQuestionText.Text,
                        txtOptionA.Text, txtOptionB.Text, txtOptionC.Text, txtOptionD.Text,
                        ddlCorrect.SelectedValue);
                    ShowMessage("Question updated.", "alert-success");
                }

                ShowList();
                BindQuestions();
            }
            catch (ValidationException vex)
            {
                ShowMessage(vex.Message, "alert-error");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ShowList();
            BindQuestions();
        }

        private void ShowForm()
        {
            pnlList.Visible = false;
            pnlForm.Visible = true;
        }

        private void ShowList()
        {
            pnlForm.Visible = false;
            pnlList.Visible = true;
        }

        private void ShowMessage(string message, string cssClass)
        {
            lblMessage.CssClass = "alert " + cssClass;
            lblMessage.Text = Server.HtmlEncode(message);
            lblMessage.Visible = true;
        }
    }
}