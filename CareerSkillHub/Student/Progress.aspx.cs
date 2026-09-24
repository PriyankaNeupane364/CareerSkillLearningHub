using System;
using System.Collections.Generic;
using System.Web.UI;
using CareerSkillHub.BLL;

namespace CareerSkillHub.Student
{
    public partial class Progress : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthBLL.RequireRole("Student");

            if (!IsPostBack)
            {
                int userId = AuthBLL.CurrentUserID;

                // Progress rows for courses the student has started (SELECT with JOIN).
                List<Models.Progress> rows = new ProgressBLL().GetByUser(userId);

                // Enrolled courses, so even before opening materials / taking a quiz
                // the student sees their courses here at 0%.
                List<Models.Enrollment> enrolled = new EnrollmentBLL().GetByUser(userId);

                Dictionary<int, Models.Progress> byCourse = new Dictionary<int, Models.Progress>();
                foreach (Models.Progress p in rows)
                {
                    byCourse[p.CourseID] = p;
                }

                List<Models.Progress> merged = new List<Models.Progress>();
                foreach (Models.Enrollment enr in enrolled)
                {
                    if (byCourse.ContainsKey(enr.CourseID))
                    {
                        merged.Add(byCourse[enr.CourseID]);
                    }
                    else
                    {
                        merged.Add(new Models.Progress
                        {
                            CourseID = enr.CourseID,
                            CourseTitle = enr.CourseTitle,
                            ProgressPercentage = 0,
                            LastUpdated = enr.EnrollmentDate
                        });
                    }
                }

                rptProgress.DataSource = merged;
                rptProgress.DataBind();

                if (merged.Count == 0)
                {
                    lblMessage.CssClass = "alert alert-info";
                    lblMessage.Text = "You have not enrolled in any course yet. Browse courses and enroll to start learning!";
                    lblMessage.Visible = true;
                }
            }
        }
    }
}