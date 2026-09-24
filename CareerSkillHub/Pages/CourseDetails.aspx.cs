using System;
using System.Collections.Generic;
using System.Web.UI;
using CareerSkillHub.BLL;
using CareerSkillHub.Models;

namespace CareerSkillHub.Pages
{
    public partial class CourseDetails : Page
    {
        protected string courseTitle = "";
        protected string courseDescription = "";
        protected string courseCategory = "";
        protected string courseInstructor = "";
        protected string courseImage = "~/Images/course.jpg";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int courseId;
                if (int.TryParse(Request.QueryString["courseId"], out courseId) && courseId > 0)
                {
                    Course c = new CourseBLL().GetById(courseId);
                    if (c != null)
                    {
                        phCourse.Visible = true;
                        courseTitle = c.Title;
                        courseDescription = c.Description;
                        courseCategory = c.Category;
                        courseInstructor = c.InstructorName;
                        courseImage = string.IsNullOrEmpty(c.ImageURL) ? "~/Images/course.jpg"
                            : c.ImageURL.Trim().Replace("~", "").StartsWith("/")
                                ? c.ImageURL.Trim().Replace("~", "")
                                : "/" + c.ImageURL.Trim().Replace("~", "");

                        // Show the public learning materials for this course.
                        List<LearningMaterial> materials = new LearningMaterialBLL().GetByCourse(courseId);
                        rptMaterials.DataSource = materials;
                        rptMaterials.DataBind();
                        phMaterials.Visible = materials.Count > 0;
                    }
                    else
                    {
                        phNotFound.Visible = true;
                    }
                }
                else
                {
                    phNotFound.Visible = true;
                }
            }
        }
    }
}