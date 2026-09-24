namespace CareerSkillHub.Admin
{
    public partial class Questions
    {
        protected global::System.Web.UI.WebControls.Label lblMessage;
        protected global::System.Web.UI.WebControls.DropDownList ddlQuiz;
        protected global::System.Web.UI.WebControls.PlaceHolder pnlList;
        protected global::System.Web.UI.WebControls.Repeater rptQuestions;
        protected global::System.Web.UI.WebControls.Button btnNew;
        protected global::System.Web.UI.WebControls.PlaceHolder pnlForm;
        protected global::System.Web.UI.WebControls.Literal litFormTitle;
        protected global::System.Web.UI.WebControls.TextBox txtQuestionText;
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvText;
        protected global::System.Web.UI.WebControls.TextBox txtOptionA;
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvOptionA;
        protected global::System.Web.UI.WebControls.TextBox txtOptionB;
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvOptionB;
        protected global::System.Web.UI.WebControls.TextBox txtOptionC;
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvOptionC;
        protected global::System.Web.UI.WebControls.TextBox txtOptionD;
        protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvOptionD;
        protected global::System.Web.UI.WebControls.DropDownList ddlCorrect;
        protected global::System.Web.UI.WebControls.Button btnSave;
        protected global::System.Web.UI.WebControls.Button btnCancel;
        protected global::System.Web.UI.HtmlControls.HtmlTableRow trNoQuestions;
    }
}