using System;
using System.Web.UI;
using CareerSkillHub.BLL;
using CareerSkillHub.Helpers;

namespace CareerSkillHub.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Logged-in users do not need to see the login page.
            if (AuthBLL.IsLoggedIn)
            {
                Response.Redirect(HomeByRole());
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string role = new AuthBLL().Login(txtEmail.Text.Trim(), txtPassword.Text);

                // Role-based redirect: Student / Instructor / Admin each go to their own dashboard.
                Response.Redirect(HomeByRole());
            }
            catch (ValidationException vex)
            {
                ShowError(vex.Message);
            }
            catch (Exception)
            {
                ShowError("Login failed. Please try again.");
            }
        }

        private string HomeByRole()
        {
            switch (AuthBLL.CurrentRole)
            {
                case "Admin": return "~/Admin/Dashboard.aspx";
                case "Instructor": return "~/Instructor/Dashboard.aspx";
                default: return "~/Student/Dashboard.aspx";
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