using System;
using System.Web.UI;
using CareerSkillHub.BLL;

namespace CareerSkillHub.Masterpages
{
    public partial class Site : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Show guest or logged-in navigation based on the current session role.
            bool loggedIn = AuthBLL.IsLoggedIn;
            phGuest.Visible = !loggedIn;
            phLoggedIn.Visible = loggedIn;

            if (loggedIn)
            {
                litWelcome.Text = Server.HtmlEncode(AuthBLL.CurrentUserName);

                string role = AuthBLL.CurrentRole;
                phStudent.Visible = (role == "Student");
                phInstructor.Visible = (role == "Instructor");
                phAdmin.Visible = (role == "Admin");
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            AuthBLL.Logout();
            Response.Redirect("~/Default.aspx");
        }
    }
}