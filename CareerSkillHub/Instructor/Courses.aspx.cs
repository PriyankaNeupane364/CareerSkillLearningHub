using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CareerSkillHub.BLL;
using CareerSkillHub.Helpers;

namespace CareerSkillHub.Instructor
{
    public partial class Courses : Page
    {
        private readonly CourseBLL _courseBll = new CourseBLL();

        /// <summary>0 means "adding a new course"; otherwise the course being edited.</summary>
        private int _editCourseId
        {
            get { return ViewState["EditCourseID"] == null ? 0 : (int)ViewState["EditCourseID"]; }
            set { ViewState["EditCourseID"] = value; }
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
            // Instructors may only manage courses they created (SELECT).
            List<Models.Course> mine = _courseBll.GetByInstructor(AuthBLL.CurrentUserID);
            rptCourses.DataSource = mine;
            rptCourses.DataBind();
        }

        protected void Action_Command(object sender, CommandEventArgs e)
        {
            int courseId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                _editCourseId = courseId;
                LoadForm(courseId);
            }
            else if (e.CommandName == "Delete")
            {
                try
                {
                    // DELETE (also removes dependent materials/quizzes/enrolments).
                    _courseBll.DeleteCourse(courseId);
                    ShowMessage("Course deleted.", "alert-success");
                    BindList();
                }
                catch (Exception)
                {
                    ShowMessage("Unable to delete the course. Please try again.", "alert-error");
                }
            }
        }

        private void LoadForm(int courseId)
        {
            Models.Course c = _courseBll.GetById(courseId);

            // Only the owner may edit the course.
            if (c == null || c.InstructorID != AuthBLL.CurrentUserID)
            {
                Response.Redirect("~/Pages/AccessDenied.aspx");
                return;
            }

            txtTitle.Text = c.Title;
            txtCategory.Text = c.Category;
            txtDescription.Text = c.Description;
            txtImageUrl.Text = c.ImageURL;

            litFormTitle.Text = "Edit Course";
            ShowForm();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            _editCourseId = 0;
            txtTitle.Text = "";
            txtCategory.Text = "";
            txtDescription.Text = "";
            txtImageUrl.Text = "";
            litFormTitle.Text = "Add New Course";
            ShowForm();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                if (_editCourseId == 0)
                {
                    // INSERT.
                    _courseBll.AddCourse(txtTitle.Text, txtDescription.Text, txtCategory.Text,
                        AuthBLL.CurrentUserID, txtImageUrl.Text);
                    ShowMessage("Course added.", "alert-success");
                }
                else
                {
                    // UPDATE.
                    Models.Course c = _courseBll.GetById(_editCourseId);
                    if (c == null || c.InstructorID != AuthBLL.CurrentUserID)
                    {
                        Response.Redirect("~/Pages/AccessDenied.aspx");
                        return;
                    }
                    _courseBll.UpdateCourse(_editCourseId, txtTitle.Text, txtDescription.Text,
                        txtCategory.Text, AuthBLL.CurrentUserID, txtImageUrl.Text);
                    ShowMessage("Course updated.", "alert-success");
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