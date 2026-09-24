using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CareerSkillHub.BLL;
using CareerSkillHub.Helpers;
using CareerSkillHub.Models;

namespace CareerSkillHub.Instructor
{
    public partial class Materials : Page
    {
        private readonly LearningMaterialBLL _materialBll = new LearningMaterialBLL();

        private int _editMaterialId
        {
            get { return ViewState["EditMaterialID"] == null ? 0 : (int)ViewState["EditMaterialID"]; }
            set { ViewState["EditMaterialID"] = value; }
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
            // Materials for the instructor's own courses (SELECT with JOIN).
            List<LearningMaterial> mine = new LearningMaterialBLL().GetByInstructor(AuthBLL.CurrentUserID);

            if (mine.Count == 0)
            {
                ShowMessage("No materials yet.", "alert-info");
                lblMessage.Visible = true;
            }

            rptMaterials.DataSource = mine;
            rptMaterials.DataBind();
        }

        private void BindCourseDropdown(int selectedCourseId)
        {
            // Only the instructor's own courses are available.
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
            int materialId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                LearningMaterial m = _materialBll.GetById(materialId);
                if (!OwnsCourse(m.CourseID)) { Response.Redirect("~/Pages/AccessDenied.aspx"); return; }

                _editMaterialId = materialId;
                BindCourseDropdown(m.CourseID);
                txtTitle.Text = m.Title;
                txtDescription.Text = m.Description;
                txtContent.Text = m.Content;
                txtVideoUrl.Text = m.VideoURL;
                txtResourceUrl.Text = m.ResourceURL;
                litFormTitle.Text = "Edit Material";
                ShowForm();
            }
            else if (e.CommandName == "Delete")
            {
                try
                {
                    // DELETE.
                    _materialBll.DeleteMaterial(materialId);
                    ShowMessage("Material deleted.", "alert-success");
                    BindList();
                }
                catch (Exception)
                {
                    ShowMessage("Unable to delete the material.", "alert-error");
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            _editMaterialId = 0;
            BindCourseDropdown(0);
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtContent.Text = "";
            txtVideoUrl.Text = "";
            txtResourceUrl.Text = "";
            litFormTitle.Text = "Add New Material";
            ShowForm();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            int courseId = Convert.ToInt32(ddlCourse.SelectedValue);

            if (!OwnsCourse(courseId)) { Response.Redirect("~/Pages/AccessDenied.aspx"); return; }

            try
            {
                if (_editMaterialId == 0)
                {
                    // INSERT.
                    _materialBll.AddMaterial(courseId, txtTitle.Text, txtDescription.Text,
                        txtContent.Text, txtVideoUrl.Text, txtResourceUrl.Text);
                    ShowMessage("Material added.", "alert-success");
                }
                else
                {
                    // UPDATE.
                    _materialBll.UpdateMaterial(_editMaterialId, courseId, txtTitle.Text,
                        txtDescription.Text, txtContent.Text, txtVideoUrl.Text, txtResourceUrl.Text);
                    ShowMessage("Material updated.", "alert-success");
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

        /// <summary>Returns true only if the course belongs to the logged-in instructor.</summary>
        private bool OwnsCourse(int courseId)
        {
            Course c = new CourseBLL().GetById(courseId);
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