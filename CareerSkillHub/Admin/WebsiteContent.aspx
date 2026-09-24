<%@ Page Title="Website Content" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="WebsiteContent.aspx.cs" Inherits="CareerSkillHub.Admin.WebsiteContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Website Content</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <asp:PlaceHolder ID="pnlList" runat="server">
        <div class="table-wrap">
            <table class="grid">
                <thead>
                    <tr>
                        <th>Type</th>
                        <th>Title</th>
                        <th>Content</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptContent" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><span class="badge"><%# Eval("ContentType") %></span></td>
                                <td><%# Eval("Title") %></td>
                                <td><%# Eval("Content") %></td>
                                <td>
                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="lnk-btn"
                                        CommandArgument='<%# Eval("ContentID") %>' CommandName="Edit"
                                        OnCommand="Action_Command">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="lnk-btn danger"
                                        CommandArgument='<%# Eval("ContentID") %>' CommandName="Delete"
                                        OnCommand="Action_Command"
                                        OnClientClick="return confirm('Delete this content?');">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                    <tr id="trNoContent" runat="server" Visible="false">
                        <td colspan="4">No content yet.</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <p>
            <asp:Button ID="btnNew" runat="server" Text="+ Add New Content" CssClass="btn btn-success"
                OnClick="btnNew_Click" />
        </p>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="pnlForm" runat="server" Visible="false">
        <div class="form-card">
            <h3><asp:Literal ID="litFormTitle" runat="server" /></h3>

            <div class="form-group">
                <label for="ddlType">Content Type</label>
                <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem Text="Homepage" Value="Homepage" />
                    <asp:ListItem Text="About" Value="About" />
                    <asp:ListItem Text="Featured" Value="Featured" />
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="txtTitle">Title</label>
                <asp:TextBox ID="txtTitle" runat="server" MaxLength="150" />
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                    CssClass="field-error" Display="Dynamic" ErrorMessage="Title is required." />
            </div>

            <div class="form-group">
                <label for="txtContent">Content</label>
                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="4" MaxLength="2000" />
                <asp:RequiredFieldValidator ID="rfvContent" runat="server" ControlToValidate="txtContent"
                    CssClass="field-error" Display="Dynamic" ErrorMessage="Content is required." />
            </div>

            <div class="form-actions">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary"
                    OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
        </div>
    </asp:PlaceHolder>

</asp:Content>