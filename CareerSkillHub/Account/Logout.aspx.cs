using System;
using System.Web.UI;
using CareerSkillHub.BLL;

namespace CareerSkillHub.Account
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clears the session and returns to the home page.
            AuthBLL.Logout();
            Response.Redirect("~/Default.aspx");
        }
    }
}