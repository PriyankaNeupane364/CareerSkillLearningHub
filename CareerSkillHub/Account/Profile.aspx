<%@ Page Title="My Profile" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="CareerSkillHub.Account.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">My Profile</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <div class="form-card">
        <div class="form-group">
            <label for="lblRole">Role</label>
            <asp:Label ID="lblRole" runat="server" CssClass="badge" />
        </div>

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
                ErrorMessage="Enter a valid email address." />
        </div>

        <div class="form-actions">
            <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn" OnClick="btnSave_Click" />
        </div>
    </div>

    <div class="form-card" style="margin-top:24px;">
        <h3>Change Password</h3>

        <div class="form-group">
            <label for="txtCurrentPassword">Current Password</label>
            <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password" MaxLength="50" />
            <asp:RequiredFieldValidator ID="rfvCurrent" runat="server" ControlToValidate="txtCurrentPassword"
                CssClass="field-error" Display="Dynamic" ErrorMessage="Current password is required." />
        </div>

        <div class="form-group">
            <label for="txtNewPassword">New Password</label>
            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" MaxLength="50" />
            <asp:RequiredFieldValidator ID="rfvNew" runat="server" ControlToValidate="txtNewPassword"
                CssClass="field-error" Display="Dynamic" ErrorMessage="New password is required." />
            <asp:RegularExpressionValidator ID="revNew" runat="server" ControlToValidate="txtNewPassword"
                CssClass="field-error" Display="Dynamic"
                ValidationExpression="^.{6,}$"
                ErrorMessage="New password must be at least 6 characters." />
        </div>

        <div class="form-group">
            <label for="txtConfirmNew">Confirm New Password</label>
            <asp:TextBox ID="txtConfirmNew" runat="server" TextMode="Password" MaxLength="50" />
            <asp:CompareValidator ID="cvNew" runat="server" ControlToValidate="txtConfirmNew"
                ControlToCompare="txtNewPassword" Operator="Equal"
                CssClass="field-error" Display="Dynamic" ErrorMessage="New passwords do not match." />
        </div>

        <div class="form-actions">
            <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-secondary"
                OnClick="btnChangePassword_Click" CausesValidation="false" />
        </div>
    </div>

</asp:Content>