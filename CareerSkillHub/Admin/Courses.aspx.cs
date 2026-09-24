using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CareerSkillHub.BLL;
using CareerSkillHub.Helpers;

namespace CareerSkillHub.Admin
{
    public partial class Courses : Page
    {
        private readonly CourseBLL _courseBll = new CourseBLL();

        private int _editCourseId
        {
            get { return ViewState["EditCourseID"] == null ? 0 : (int)ViewState["EditCourseID"]; }
            set { ViewState["EditCourseID"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthBLL.RequireRole("Admin");

            if (!IsPostBack)
            {
                BindList();
            }
        }

        private void BindList()
        {
            // Every course (SELECT).
            List<Models.Course> all = _courseBll.GetAll();
            rptCourses.DataSource = all;
            rptCourses.DataBind();
        }

        private void BindInstructorDropdown(int selectedInstructorId)
        {
            ddlInstructor.DataSource = new UserBLL().GetInstructors();
            ddlInstructor.DataTextField = "FullName";
            ddlInstructor.DataValueField = "UserID";
            ddlInstructor.DataBind();
            ddlInstructor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select an instructor --", "0"));
            if (selectedInstructorId > 0)
                ddlInstructor.SelectedValue = selectedInstructorId.ToString();
        }

        protected void Action_Command(object sender, CommandEventArgs e)
        {
            int courseId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                Models.Course c = _courseBll.GetById(courseId);
                if (c == null) { Response.Redirect("~/Pages/AccessDenied.aspx"); return; }

                _editCourseId = courseId;
                BindInstructorDropdown(c.InstructorID);
                txtTitle.Text = c.Title;
                txtCategory.Text = c.Category;
                txtDescription.Text = c.Description;
                txtImageUrl.Text = c.ImageURL;
                litFormTitle.Text = "Edit Course";
                ShowForm();
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
                    ShowMessage("Unable to delete the course.", "alert-error");
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            _editCourseId = 0;
            BindInstructorDropdown(0);
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
            int instructorId = Convert.ToInt32(ddlInstructor.SelectedValue);

            try
            {
                if (_editCourseId == 0)
                {
                    // INSERT.
                    _courseBll.AddCourse(txtTitle.Text, txtDescription.Text, txtCategory.Text,
                        instructorId, txtImageUrl.Text);
                    ShowMessage("Course added.", "alert-success");
                }
                else
                {
                    // UPDATE.
                    _courseBll.UpdateCourse(_editCourseId, txtTitle.Text, txtDescription.Text,
                        txtCategory.Text, instructorId, txtImageUrl.Text);
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