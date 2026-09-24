using System;
using System.Collections.Generic;
using System.Web.UI;
using CareerSkillHub.BLL;
using CareerSkillHub.Helpers;

namespace CareerSkillHub.Student
{
    public partial class Courses : Page
    {
        private readonly EnrollmentBLL _enrollmentBll = new EnrollmentBLL();
        private int _userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthBLL.RequireRole("Student");
            _userId = AuthBLL.CurrentUserID;

            if (!IsPostBack)
            {
                BindCourses();

                // Handle the "enroll" action passed in the query string.
                string raw = Request.QueryString["enroll"];
                int courseId;
                if (!string.IsNullOrEmpty(raw) && int.TryParse(raw, out courseId))
                {
                    TryEnroll(courseId);
                }
            }
        }

        private void BindCourses()
        {
            List<Models.Course> all = new CourseBLL().GetAll();
            rptCourses.DataSource = all;
            rptCourses.DataBind();
        }

        private void TryEnroll(int courseId)
        {
            try
            {
                // INSERT into Enrollments (with duplicate-check).
                _enrollmentBll.Enroll(_userId, courseId);
                // Send the student straight to their learning progress.
                Response.Redirect("~/Student/Progress.aspx");
            }
            catch (ValidationException vex)
            {
                ShowMessage(vex.Message, "alert-error");
                BindCourses();
            }
            catch (Exception)
            {
                ShowMessage("Enrollment failed. Please try again.", "alert-error");
                BindCourses();
            }
        }

        /// <summary>Used by the Repeater to show a badge for already-enrolled courses.</summary>
        protected bool IsEnrolled(int courseId)
        {
            return _enrollmentBll.IsEnrolled(_userId, courseId);
        }

        private void ShowMessage(string message, string cssClass)
        {
            lblMessage.CssClass = "alert " + cssClass;
            lblMessage.Text = Server.HtmlEncode(message);
            lblMessage.Visible = true;
        }
    }
}