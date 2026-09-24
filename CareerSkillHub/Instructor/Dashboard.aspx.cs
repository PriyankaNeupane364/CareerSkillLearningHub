using System;
using System.Collections.Generic;
using System.Web.UI;
using CareerSkillHub.BLL;

namespace CareerSkillHub.Instructor
{
    public partial class Dashboard : Page
    {
        private readonly EnrollmentBLL _enrollmentBll = new EnrollmentBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthBLL.RequireRole("Instructor");

            if (!IsPostBack)
            {
                int instructorId = AuthBLL.CurrentUserID;

                // All numbers come from the database.
                List<Models.Course> myCourses = new CourseBLL().GetByInstructor(instructorId);
                litCourses.Text = myCourses.Count.ToString();

                int totalStudents = 0;
                int totalMaterials = 0;
                int totalQuizzes = 0;

                foreach (Models.Course c in myCourses)
                {
                    totalStudents += _enrollmentBll.CountByCourse(c.CourseID);
                    totalMaterials += new LearningMaterialBLL().GetByCourse(c.CourseID).Count;
                    totalQuizzes += new QuizBLL().GetByCourse(c.CourseID).Count;
                }

                litStudents.Text = totalStudents.ToString();
                litMaterials.Text = totalMaterials.ToString();
                litQuizzes.Text = totalQuizzes.ToString();

                rptCourses.DataSource = myCourses;
                rptCourses.DataBind();
                pnlNoCourses.Visible = rptCourses.Items.Count == 0;
            }
        }

        /// <summary>Used by the Repeater to show how many students enrolled in each course.</summary>
        protected int CountStudents(int courseId)
        {
            return _enrollmentBll.CountByCourse(courseId);
        }
    }
}