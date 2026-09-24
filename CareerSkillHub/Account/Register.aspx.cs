using System;
using System.Web.UI;
using CareerSkillHub.BLL;
using CareerSkillHub.Helpers;

namespace CareerSkillHub.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Server-side creation of a new Student account (INSERT).
            try
            {
                new AuthBLL().Register(txtFullName.Text, txtEmail.Text, txtPassword.Text);

                ShowSuccess("Account created successfully. You can now log in.");

                // Clear the form after a successful registration.
                txtFullName.Text = "";
                txtEmail.Text = "";
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
            }
            catch (ValidationException vex)
            {
                ShowError(vex.Message);
            }
            catch (Exception)
            {
                ShowError("Registration failed. Please try again.");
            }
        }

        private void ShowError(string message)
        {
            lblMessage.CssClass = "alert alert-error";
            lblMessage.Text = Server.HtmlEncode(message);
            lblMessage.Visible = true;
        }

        private void ShowSuccess(string message)
        {
            lblMessage.CssClass = "alert alert-success";
            lblMessage.Text = Server.HtmlEncode(message) + " <a href='~/Account/Login.aspx' runat='server' style='font-weight:600;'>Login now</a>";
            lblMessage.Visible = true;
        }
    }
}