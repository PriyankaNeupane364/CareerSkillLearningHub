using System;
using System.Web.UI;
using CareerSkillHub.BLL;

namespace CareerSkillHub.Student
{
    public partial class Feedback : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthBLL.RequireRole("Student");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            int rating = Convert.ToInt32(rblRating.SelectedValue);

            try
            {
                // INSERT into Feedback.
                new FeedbackBLL().AddFeedback(AuthBLL.CurrentUserID, rating, txtMessage.Text.Trim());

                lblMessage.CssClass = "alert alert-success";
                lblMessage.Text = "Thank you! Your feedback has been submitted.";
                lblMessage.Visible = true;

                txtMessage.Text = "";
            }
            catch (Helpers.ValidationException vex)
            {
                lblMessage.CssClass = "alert alert-error";
                lblMessage.Text = Server.HtmlEncode(vex.Message);
                lblMessage.Visible = true;
            }
        }
    }
}