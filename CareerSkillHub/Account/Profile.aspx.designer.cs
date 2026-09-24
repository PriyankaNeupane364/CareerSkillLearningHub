namespace CareerSkillHub.Account
{
    public partial class Profile
    {
        protected global::System.Web.UI.WebControls.Label lblMessage;
        protected global::System.Web.UI.WebControls.Label lblRole;
        protected global::System.Web.UI.WebControls.TextBox txtFullName;
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvFullName;
        protected global::System.Web.UI.WebControls.TextBox txtEmail;
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvEmail;
        protected global::System.Web.UI.WebControls.RegularExpressionValidator revEmail;
        protected global::System.Web.UI.WebControls.Button btnSave;
        protected global::System.Web.UI.WebControls.TextBox txtCurrentPassword;
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvCurrent;
        protected global::System.Web.UI.WebControls.TextBox txtNewPassword;
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvNew;
        protected global::System.Web.UI.WebControls.RegularExpressionValidator revNew;
        protected global::System.Web.UI.WebControls.TextBox txtConfirmNew;
        protected global::System.Web.UI.WebControls.CompareValidator cvNew;
        protected global::System.Web.UI.WebControls.Button btnChangePassword;
    }
}