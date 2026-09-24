using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CareerSkillHub.BLL;
using CareerSkillHub.Helpers;

namespace CareerSkillHub.Admin
{
    public partial class WebsiteContent : Page
    {
        private readonly WebsiteContentBLL _contentBll = new WebsiteContentBLL();

        private int _editContentId
        {
            get { return ViewState["EditContentID"] == null ? 0 : (int)ViewState["EditContentID"]; }
            set { ViewState["EditContentID"] = value; }
        }

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
            // All editable website content (SELECT).
            List<Models.WebsiteContent> all = _contentBll.GetAll();
            rptContent.DataSource = all;
            rptContent.DataBind();
            trNoContent.Visible = rptContent.Items.Count == 0;
        }

        protected void Action_Command(object sender, CommandEventArgs e)
        {
            int contentId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                Models.WebsiteContent c = _contentBll.GetAll().Find(x => x.ContentID == contentId);
                if (c == null) { Response.Redirect("~/Pages/AccessDenied.aspx"); return; }

                _editContentId = contentId;
                SelectDropdownValue(c.ContentType);
                txtTitle.Text = c.Title;
                txtContent.Text = c.Content;
                litFormTitle.Text = "Edit Content";
                ShowForm();
            }
            else if (e.CommandName == "Delete")
            {
                try
                {
                    // DELETE.
                    _contentBll.DeleteContent(contentId);
                    ShowMessage("Content deleted.", "alert-success");
                    BindList();
                }
                catch (Exception)
                {
                    ShowMessage("Unable to delete the content.", "alert-error");
                }
            }
        }

        private void SelectDropdownValue(string value)
        {
            if (ddlType.Items.FindByValue(value) != null)
                ddlType.SelectedValue = value;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            _editContentId = 0;
            ddlType.SelectedIndex = 0;
            txtTitle.Text = "";
            txtContent.Text = "";
            litFormTitle.Text = "Add New Content";
            ShowForm();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                if (_editContentId == 0)
                {
                    // INSERT.
                    _contentBll.AddContent(ddlType.SelectedValue, txtTitle.Text, txtContent.Text);
                    ShowMessage("Content added.", "alert-success");
                }
                else
                {
                    // UPDATE.
                    _contentBll.UpdateContent(_editContentId, ddlType.SelectedValue, txtTitle.Text, txtContent.Text);
                    ShowMessage("Content updated.", "alert-success");
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