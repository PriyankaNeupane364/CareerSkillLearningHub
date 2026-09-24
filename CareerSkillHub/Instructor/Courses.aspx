<%@ Page Title="Manage Courses" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="CareerSkillHub.Instructor.Courses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Manage My Courses</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <asp:PlaceHolder ID="pnlList" runat="server">
        <div class="table-wrap">
            <table class="grid">
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Category</th>
                        <th>Created</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptCourses" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Title") %></td>
                                <td><span class="badge"><%# Eval("Category") %></span></td>
                                <td><%# Eval("CreatedDate", "{0:dd MMM yyyy}") %></td>
                                <td>
                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="lnk-btn"
                                        CommandArgument='<%# Eval("CourseID") %>' CommandName="Edit"
                                        OnCommand="Action_Command">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="lnk-btn danger"
                                        CommandArgument='<%# Eval("CourseID") %>' CommandName="Delete"
                                        OnCommand="Action_Command"
                                        OnClientClick="return confirm('Delete this course and all of its materials/quizzes?');">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <p>
            <asp:Button ID="btnNew" runat="server" Text="+ Add New Course" CssClass="btn btn-success"
                OnClick="btnNew_Click" />
        </p>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="pnlForm" runat="server" Visible="false">
        <div class="form-card">
            <h3><asp:Literal ID="litFormTitle" runat="server" /></h3>

            <div class="form-group">
                <label for="txtTitle">Course Title</label>
                <asp:TextBox ID="txtTitle" runat="server" MaxLength="150" />
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                    CssClass="field-error" Display="Dynamic" ErrorMessage="Title is required." />
            </div>

            <div class="form-group">
                <label for="txtCategory">Category</label>
                <asp:TextBox ID="txtCategory" runat="server" MaxLength="50" />
                <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="txtCategory"
                    CssClass="field-error" Display="Dynamic" ErrorMessage="Category is required." />
            </div>

            <div class="form-group">
                <label for="txtDescription">Description</label>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4" MaxLength="2000" />
                <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                    CssClass="field-error" Display="Dynamic" ErrorMessage="Description is required." />
            </div>

            <div class="form-group">
                <label for="txtImageUrl">Image URL (optional)</label>
                <asp:TextBox ID="txtImageUrl" runat="server" MaxLength="300" />
            </div>

            <div class="form-actions">
                <asp:Button ID="btnSave" runat="server" Text="Save Course" CssClass="btn btn-success"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary"
                    OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
        </div>
    </asp:PlaceHolder>

</asp:Content>