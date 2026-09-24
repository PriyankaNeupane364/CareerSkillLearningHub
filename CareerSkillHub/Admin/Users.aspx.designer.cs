namespace CareerSkillHub.Admin
{
    public partial class Users
    {
        protected global::System.Web.UI.WebControls.Label lblMessage;
        protected global::System.Web.UI.WebControls.PlaceHolder pnlList;
        protected global::System.Web.UI.WebControls.Repeater rptUsers;
        protected global::System.Web.UI.WebControls.Button btnNew;
        protected global::System.Web.UI.WebControls.PlaceHolder pnlForm;
        protected global::System.Web.UI.WebControls.Literal litFormTitle;
        protected global::System.Web.UI.WebControls.TextBox txtFullName;
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvFullName;
        protected global::System.Web.UI.WebControls.TextBox txtEmail;
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvEmail;
        protected global::System.Web.UI.WebControls.RegularExpressionValidator revEmail;
        protected global::System.Web.UI.WebControls.PlaceHolder pnlPassword;
        protected global::System.Web.UI.WebControls.TextBox txtPassword;
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvPassword;
        protected global::System.Web.UI.WebControls.DropDownList ddlRole;
        protected global::System.Web.UI.WebControls.Button btnSave;
        protected global::System.Web.UI.WebControls.Button btnCancel;
    }
}