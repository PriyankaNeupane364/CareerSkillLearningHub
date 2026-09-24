using System;
using System.Web.UI;
using CareerSkillHub.BLL;
using CareerSkillHub.Models;

namespace CareerSkillHub
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load the "About" content editable by the admin (SELECT).
                WebsiteContent wc = new WebsiteContentBLL().GetByTypeAndTitle("About", "Our Mission");
                litMission.Text = Server.HtmlEncode(wc != null ? wc.Content : "Building confident, career-ready graduates.");
            }
        }
    }
}