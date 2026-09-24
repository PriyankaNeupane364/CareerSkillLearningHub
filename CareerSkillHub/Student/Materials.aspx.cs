using System;
using System.Collections.Generic;
using System.Web.UI;
using CareerSkillHub.BLL;

namespace CareerSkillHub.Student
{
    public partial class Materials : Page
    {
        protected int QuizCourseId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthBLL.RequireRole("Student");

            int courseId;
            if (!int.TryParse(Request.QueryString["courseId"], out courseId) || courseId <= 0)
            {
                Response.Redirect("~/Student/MyCourses.aspx");
                return;
            }

            // A student may only view materials for a course they are enrolled in.
            if (!new EnrollmentBLL().IsEnrolled(AuthBLL.CurrentUserID, courseId))
            {
                Response.Redirect("~/Pages/AccessDenied.aspx");
                return;
            }

            QuizCourseId = courseId;

            if (!IsPostBack)
            {
                // Materials belonging to this course (SELECT).
                List<Models.LearningMaterial> materials = new LearningMaterialBLL().GetByCourse(courseId);
                rptMaterials.DataSource = materials;
                rptMaterials.DataBind();

                if (materials.Count > 0)
                    phMaterials.Visible = true;
                else
                    ShowError("No learning materials available for this course yet.");

                // Slight progress bump when the student views the materials.
                new ProgressBLL().SetProgress(AuthBLL.CurrentUserID, courseId, 25);
            }
        }

        private void ShowError(string message)
        {
            lblMessage.CssClass = "alert alert-error";
            lblMessage.Text = Server.HtmlEncode(message);
            lblMessage.Visible = true;
        }
    }
}