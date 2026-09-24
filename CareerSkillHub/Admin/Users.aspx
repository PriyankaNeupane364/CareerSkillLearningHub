<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="CareerSkillHub.Admin.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Manage Users</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <asp:PlaceHolder ID="pnlList" runat="server">
        <div class="table-wrap">
            <table class="grid">
                <thead>
                    <tr>
                        <th>Full Name</th>
                        <th>Email</th>
                        <th>Role</th>
                        <th>Joined</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptUsers" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("FullName") %></td>
                                <td><%# Eval("Email") %></td>
                                <td><span class="badge"><%# Eval("Role") %></span></td>
                                <td><%# Eval("CreatedDate", "{0:dd MMM yyyy}") %></td>
                                <td>
                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="lnk-btn"
                                        CommandArgument='<%# Eval("UserID") %>' CommandName="Edit"
                                        OnCommand="Action_Command">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="lnk-btn danger"
                                        CommandArgument='<%# Eval("UserID") %>' CommandName="Delete"
                                        OnCommand="Action_Command"
                                        OnClientClick="return confirm('Delete this user? This cannot be undone.');">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <p>
            <asp:Button ID="btnNew" runat="server" Text="+ Add New User" CssClass="btn btn-success"
                OnClick="btnNew_Click" />
        </p>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="pnlForm" runat="server" Visible="false">
        <div class="form-card">
            <h3><asp:Literal ID="litFormTitle" runat="server" /></h3>

            <div class="form-group">
                <label for="txtFullName">Full Name</label>
                <asp:TextBox ID="txtFullName" runat="server" MaxLength="100" />
                <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName"
                    CssClass="field-error" Display="Dynamic" ErrorMessage="Full name is required." />
            </div>

            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" MaxLength="150" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    CssClass="field-error" Display="Dynamic" ErrorMessage="Email is required." />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                    CssClass="field-error" Display="Dynamic"
                    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                    ErrorMessage="Enter a valid email." />
            </div>

            <asp:PlaceHolder ID="pnlPassword" runat="server">
                <div class="form-group">
                    <label for="txtPassword">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="50" />
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                        CssClass="field-error" Display="Dynamic" ErrorMessage="Password is required." />
                </div>
            </asp:PlaceHolder>

            <div class="form-group">
                <label for="ddlRole">Role</label>
                <asp:DropDownList ID="ddlRole" runat="server">
                    <asp:ListItem Text="Student" Value="Student" />
                    <asp:ListItem Text="Instructor" Value="Instructor" />
                    <asp:ListItem Text="Admin" Value="Admin" />
                </asp:DropDownList>
            </div>

            <div class="form-actions">
                <asp:Button ID="btnSave" runat="server" Text="Save User" CssClass="btn btn-success"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary"
                    OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
        </div>
    </asp:PlaceHolder>

</asp:Content>