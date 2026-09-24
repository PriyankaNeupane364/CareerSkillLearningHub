<%@ Page Title="Manage Quizzes" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Quizzes.aspx.cs" Inherits="CareerSkillHub.Instructor.Quizzes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Manage Quizzes</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <asp:PlaceHolder ID="pnlList" runat="server">
        <div class="table-wrap">
            <table class="grid">
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Course</th>
                        <th>Questions</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptQuizzes" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Title") %></td>
                                <td><%# Eval("CourseTitle") %></td>
                                <td><%# Eval("QuestionCount") %></td>
                                <td>
                                    <asp:LinkButton ID="btnQuestions" runat="server" CssClass="lnk-btn"
                                        CommandArgument='<%# Eval("QuizID") %>' CommandName="Questions"
                                        OnCommand="Action_Command">Questions</asp:LinkButton>
                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="lnk-btn"
                                        CommandArgument='<%# Eval("QuizID") %>' CommandName="Edit"
                                        OnCommand="Action_Command">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="lnk-btn danger"
                                        CommandArgument='<%# Eval("QuizID") %>' CommandName="Delete"
                                        OnCommand="Action_Command"
                                        OnClientClick="return confirm('Delete this quiz and all its questions?');">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <p>
            <asp:Button ID="btnNew" runat="server" Text="+ Add New Quiz" CssClass="btn btn-success"
                OnClick="btnNew_Click" />
        </p>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="pnlForm" runat="server" Visible="false">
        <div class="form-card">
            <h3><asp:Literal ID="litFormTitle" runat="server" /></h3>

            <div class="form-group">
                <label for="ddlCourse">Course</label>
                <asp:DropDownList ID="ddlCourse" runat="server" />
                <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                    InitialValue="0" CssClass="field-error" Display="Dynamic" ErrorMessage="Select a course." />
            </div>

            <div class="form-group">
                <label for="txtTitle">Quiz Title</label>
                <asp:TextBox ID="txtTitle" runat="server" MaxLength="150" />
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                    CssClass="field-error" Display="Dynamic" ErrorMessage="Title is required." />
            </div>

            <div class="form-group">
                <label for="txtDescription">Description (optional)</label>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="2" MaxLength="500" />
            </div>

            <div class="form-actions">
                <asp:Button ID="btnSave" runat="server" Text="Save Quiz" CssClass="btn btn-success"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary"
                    OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
        </div>
    </asp:PlaceHolder>

</asp:Content>