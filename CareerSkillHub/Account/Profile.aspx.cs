using System;
using System.Web.UI;
using CareerSkillHub.BLL;
using CareerSkillHub.Helpers;

namespace CareerSkillHub.Account
{
    public partial class Profile : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Any logged-in user (Student / Instructor / Admin) can view their own profile.
            AuthBLL.RequireLogin();

            if (!IsPostBack)
            {
                Models.User u = new UserBLL().GetById(AuthBLL.CurrentUserID);
                if (u != null)
                {
                    txtFullName.Text = u.FullName;
                    txtEmail.Text = u.Email;
                    lblRole.Text = u.Role;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                // Update profile (UPDATE).
                new UserBLL().UpdateProfile(AuthBLL.CurrentUserID, txtFullName.Text, txtEmail.Text);
                ShowSuccess("Your profile has been updated.");
            }
            catch (ValidationException vex)
            {
                ShowError(vex.Message);
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                // Change password (UPDATE).
                new UserBLL().ChangePassword(AuthBLL.CurrentUserID,
                    txtCurrentPassword.Text, txtNewPassword.Text);

                txtCurrentPassword.Text = "";
                txtNewPassword.Text = "";
                txtConfirmNew.Text = "";

                ShowSuccess("Your password has been changed.");
            }
            catch (ValidationException vex)
            {
                ShowError(vex.Message);
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
            lblMessage.Text = Server.HtmlEncode(message);
            lblMessage.Visible = true;
        }
    }
}