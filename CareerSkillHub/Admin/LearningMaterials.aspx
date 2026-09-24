<%@ Page Title="Manage Learning Materials" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="LearningMaterials.aspx.cs" Inherits="CareerSkillHub.Admin.LearningMaterials" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Manage Learning Materials</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <asp:PlaceHolder ID="pnlList" runat="server">
        <div class="table-wrap">
            <table class="grid">
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Course</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptMaterials" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Title") %></td>
                                <td><%# Eval("CourseTitle") %></td>
                                <td>
                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="lnk-btn"
                                        CommandArgument='<%# Eval("MaterialID") %>' CommandName="Edit"
                                        OnCommand="Action_Command">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="lnk-btn danger"
                                        CommandArgument='<%# Eval("MaterialID") %>' CommandName="Delete"
                                        OnCommand="Action_Command"
                                        OnClientClick="return confirm('Delete this material?');">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                    <tr id="trNoMaterials" runat="server" Visible="false">
                        <td colspan="3">No materials yet.</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <p>
            <asp:Button ID="btnNew" runat="server" Text="+ Add New Material" CssClass="btn btn-success"
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
                <label for="txtTitle">Title</label>
                <asp:TextBox ID="txtTitle" runat="server" MaxLength="150" />
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                    CssClass="field-error" Display="Dynamic" ErrorMessage="Title is required." />
            </div>

            <div class="form-group">
                <label for="txtDescription">Description</label>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="2" MaxLength="500" />
            </div>

            <div class="form-group">
                <label for="txtContent">Content</label>
                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="5" MaxLength="4000" />
            </div>

            <div class="form-group">
                <label for="txtVideoUrl">Video URL (optional)</label>
                <asp:TextBox ID="txtVideoUrl" runat="server" MaxLength="300" />
            </div>

            <div class="form-group">
                <label for="txtResourceUrl">Resource URL (optional)</label>
                <asp:TextBox ID="txtResourceUrl" runat="server" MaxLength="300" />
            </div>

            <div class="form-actions">
                <asp:Button ID="btnSave" runat="server" Text="Save Material" CssClass="btn btn-success"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary"
                    OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
        </div>
    </asp:PlaceHolder>

</asp:Content>