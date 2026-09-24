using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CareerSkillHub.BLL;

namespace CareerSkillHub.Admin
{
    public partial class Feedback : Page
    {
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
            // All submitted feedback (SELECT with JOIN).
            List<Models.Feedback> all = new FeedbackBLL().GetAll();
            rptFeedback.DataSource = all;
            rptFeedback.DataBind();
            trNoFeedback.Visible = rptFeedback.Items.Count == 0;
        }

        protected void Delete_Command(object sender, CommandEventArgs e)
        {
            int feedbackId = Convert.ToInt32(e.CommandArgument);

            try
            {
                // DELETE.
                new FeedbackBLL().DeleteFeedback(feedbackId);

                lblMessage.CssClass = "alert alert-success";
                lblMessage.Text = "Feedback deleted.";
                lblMessage.Visible = true;

                BindList();
            }
            catch (Exception)
            {
                lblMessage.CssClass = "alert alert-error";
                lblMessage.Text = "Unable to delete the feedback.";
                lblMessage.Visible = true;
            }
        }
    }
}