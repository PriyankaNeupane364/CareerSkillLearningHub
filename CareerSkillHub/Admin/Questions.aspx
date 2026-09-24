<%@ Page Title="Manage Questions" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Questions.aspx.cs" Inherits="CareerSkillHub.Admin.Questions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Manage Questions</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <div class="form-card">
        <div class="form-group">
            <label for="ddlQuiz">Quiz</label>
            <asp:DropDownList ID="ddlQuiz" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlQuiz_SelectedIndexChanged" />
        </div>
    </div>

    <asp:PlaceHolder ID="pnlList" runat="server">
        <div class="table-wrap">
            <table class="grid">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Question</th>
                        <th>Correct</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptQuestions" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("QuestionText") %></td>
                                <td><span class="badge"><%# Eval("CorrectAnswer") %></span></td>
                                <td>
                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="lnk-btn"
                                        CommandArgument='<%# Eval("QuestionID") %>' CommandName="Edit"
                                        OnCommand="Action_Command">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="lnk-btn danger"
                                        CommandArgument='<%# Eval("QuestionID") %>' CommandName="Delete"
                                        OnCommand="Action_Command"
                                        OnClientClick="return confirm('Delete this question?');">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                    <tr id="trNoQuestions" runat="server" Visible="false">
                        <td colspan="4">No questions yet for this quiz. Add one below.</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <p>
            <asp:Button ID="btnNew" runat="server" Text="+ Add New Question" CssClass="btn btn-success"
                OnClick="btnNew_Click" />
        </p>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="pnlForm" runat="server" Visible="false">
        <div class="form-card">
            <h3><asp:Literal ID="litFormTitle" runat="server" /></h3>

            <div class="form-group">
                <label for="txtQuestionText">Question Text</label>
                <asp:TextBox ID="txtQuestionText" runat="server" TextMode="MultiLine" Rows="2" MaxLength="500" />
                <asp:RequiredFieldValidator ID="rfvText" runat="server" ControlToValidate="txtQuestionText"
                    CssClass="field-error" Display="Dynamic" ErrorMessage="Question text is required." />
            </div>

            <div class="form-group">
                <label for="txtOptionA">Option A</label>
                <asp:TextBox ID="txtOptionA" runat="server" MaxLength="200" />
                <asp:RequiredFieldValidator ID="rfvOptionA" runat="server" ControlToValidate="txtOptionA"
                    CssClass="field-error" Display="Dynamic" ErrorMessage="Required." />
            </div>

            <div class="form-group">
                <label for="txtOptionB">Option B</label>
                <asp:TextBox ID="txtOptionB" runat="server" MaxLength="200" />
                <asp:RequiredFieldValidator ID="rfvOptionB" runat="server" ControlToValidate="txtOptionB"
                    CssClass="field-error" Display="Dynamic" ErrorMessage="Required." />
            </div>

            <div class="form-group">
                <label for="txtOptionC">Option C</label>
                <asp:TextBox ID="txtOptionC" runat="server" MaxLength="200" />
                <asp:RequiredFieldValidator ID="rfvOptionC" runat="server" ControlToValidate="txtOptionC"
                    CssClass="field-error" Display="Dynamic" ErrorMessage="Required." />
            </div>

            <div class="form-group">
                <label for="txtOptionD">Option D</label>
                <asp:TextBox ID="txtOptionD" runat="server" MaxLength="200" />
                <asp:RequiredFieldValidator ID="rfvOptionD" runat="server" ControlToValidate="txtOptionD"
                    CssClass="field-error" Display="Dynamic" ErrorMessage="Required." />
            </div>

            <div class="form-group">
                <label for="ddlCorrect">Correct Answer</label>
                <asp:DropDownList ID="ddlCorrect" runat="server">
                    <asp:ListItem Text="A" Value="A" />
                    <asp:ListItem Text="B" Value="B" />
                    <asp:ListItem Text="C" Value="C" />
                    <asp:ListItem Text="D" Value="D" />
                </asp:DropDownList>
            </div>

            <div class="form-actions">
                <asp:Button ID="btnSave" runat="server" Text="Save Question" CssClass="btn btn-success"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary"
                    OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
        </div>
    </asp:PlaceHolder>

</asp:Content>