using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CareerSkillHub.BLL;
using CareerSkillHub.Helpers;
using CareerSkillHub.Models;

namespace CareerSkillHub.Instructor
{
    public partial class Quizzes : Page
    {
        private readonly QuizBLL _quizBll = new QuizBLL();

        private int _editQuizId
        {
            get { return ViewState["EditQuizID"] == null ? 0 : (int)ViewState["EditQuizID"]; }
            set { ViewState["EditQuizID"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthBLL.RequireRole("Instructor");

            if (!IsPostBack)
            {
                BindList();
            }
        }

        private void BindList()
        {
            // Quizzes belonging to the instructor's own courses (SELECT with JOIN).
            List<Models.Quiz> mine = _quizBll.GetByInstructor(AuthBLL.CurrentUserID);

            if (mine.Count == 0)
            {
                ShowMessage("No quizzes yet.", "alert-info");
                lblMessage.Visible = true;
            }

            rptQuizzes.DataSource = mine;
            rptQuizzes.DataBind();
        }

        private void BindCourseDropdown(int selectedCourseId)
        {
            List<Course> courses = new CourseBLL().GetByInstructor(AuthBLL.CurrentUserID);
            ddlCourse.DataSource = courses;
            ddlCourse.DataTextField = "Title";
            ddlCourse.DataValueField = "CourseID";
            ddlCourse.DataBind();
            ddlCourse.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select a course --", "0"));
            if (selectedCourseId > 0)
                ddlCourse.SelectedValue = selectedCourseId.ToString();
        }

        protected void Action_Command(object sender, CommandEventArgs e)
        {
            int quizId = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Questions":
                    Response.Redirect("~/Instructor/Questions.aspx?quizId=" + quizId);
                    break;

                case "Edit":
                    Models.Quiz q = _quizBll.GetById(quizId);
                    if (!OwnsQuiz(q)) { Response.Redirect("~/Pages/AccessDenied.aspx"); return; }

                    _editQuizId = quizId;
                    BindCourseDropdown(q.CourseID);
                    txtTitle.Text = q.Title;
                    txtDescription.Text = q.Description;
                    litFormTitle.Text = "Edit Quiz";
                    ShowForm();
                    break;

                case "Delete":
                    try
                    {
                        // DELETE (also removes its questions and results).
                        _quizBll.DeleteQuiz(quizId);
                        ShowMessage("Quiz deleted.", "alert-success");
                        BindList();
                    }
                    catch (Exception)
                    {
                        ShowMessage("Unable to delete the quiz.", "alert-error");
                    }
                    break;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            _editQuizId = 0;
            BindCourseDropdown(0);
            txtTitle.Text = "";
            txtDescription.Text = "";
            litFormTitle.Text = "Add New Quiz";
            ShowForm();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int courseId = Convert.ToInt32(ddlCourse.SelectedValue);

            Course course = new CourseBLL().GetById(courseId);
            if (course == null || course.InstructorID != AuthBLL.CurrentUserID)
            {
                Response.Redirect("~/Pages/AccessDenied.aspx");
                return;
            }

            try
            {
                if (_editQuizId == 0)
                {
                    // INSERT.
                    _quizBll.AddQuiz(courseId, txtTitle.Text, txtDescription.Text);
                    ShowMessage("Quiz added.", "alert-success");
                }
                else
                {
                    // UPDATE.
                    _quizBll.UpdateQuiz(_editQuizId, courseId, txtTitle.Text, txtDescription.Text);
                    ShowMessage("Quiz updated.", "alert-success");
                }

                ShowList();
                BindList();
            }
            catch (ValidationException vex)
            {
                ShowMessage(vex.Message, "alert-error");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ShowList();
            BindList();
        }

        private bool OwnsQuiz(Models.Quiz q)
        {
            if (q == null) return false;
            Course c = new CourseBLL().GetById(q.CourseID);
            return c != null && c.InstructorID == AuthBLL.CurrentUserID;
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