using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CareerSkillHub.BLL;
using CareerSkillHub.Helpers;

namespace CareerSkillHub.Admin
{
    public partial class Users : Page
    {
        private readonly UserBLL _userBll = new UserBLL();

        private int _editUserId
        {
            get { return ViewState["EditUserID"] == null ? 0 : (int)ViewState["EditUserID"]; }
            set { ViewState["EditUserID"] = value; }
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
            // All users (SELECT).
            List<Models.User> users = _userBll.GetAll();
            rptUsers.DataSource = users;
            rptUsers.DataBind();
        }

        protected void Action_Command(object sender, CommandEventArgs e)
        {
            int userId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                Models.User u = _userBll.GetById(userId);
                if (u == null) { Response.Redirect("~/Pages/AccessDenied.aspx"); return; }

                _editUserId = userId;
                txtFullName.Text = u.FullName;
                txtEmail.Text = u.Email;
                ddlRole.SelectedValue = u.Role;
                pnlPassword.Visible = false; // password stays unchanged when editing
                litFormTitle.Text = "Edit User";
                ShowForm();
            }
            else if (e.CommandName == "Delete")
            {
                try
                {
                    // DELETE.
                    _userBll.DeleteUser(userId);
                    ShowMessage("User deleted.", "alert-success");
                    BindList();
                }
                catch (ValidationException vex)
                {
                    ShowMessage(vex.Message, "alert-error");
                }
                catch (Exception)
                {
                    ShowMessage("Unable to delete this user.", "alert-error");
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            _editUserId = 0;
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            ddlRole.SelectedIndex = 0;
            pnlPassword.Visible = true; // password needed when creating
            litFormTitle.Text = "Add New User";
            ShowForm();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                if (_editUserId == 0)
                {
                    // INSERT (with password).
                    _userBll.AddUser(txtFullName.Text, txtEmail.Text, txtPassword.Text, ddlRole.SelectedValue);
                    ShowMessage("User created.", "alert-success");
                }
                else
                {
                    // UPDATE (password not changed here).
                    _userBll.UpdateUser(_editUserId, txtFullName.Text, txtEmail.Text, ddlRole.SelectedValue);
                    ShowMessage("User updated.", "alert-success");
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