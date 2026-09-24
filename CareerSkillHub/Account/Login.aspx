<%@ Page Title="Login" Language="C#" MasterPageFile="~/Masterpages/Site.Master" ClientIDMode="Static"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CareerSkillHub.Account.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Login</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <div class="form-card">
        <div class="form-group">
            <label for="txtEmail">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="you@example.com" />
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                CssClass="field-error" Display="Dynamic" ErrorMessage="Email is required." />
        </div>

        <div class="form-group">
            <label for="txtPassword">Password</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Your password" />
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                CssClass="field-error" Display="Dynamic" ErrorMessage="Password is required." />
        </div>

        <div class="form-group">
            <label>
                <asp:CheckBox ID="chkShowPassword" runat="server" onclick="togglePassword('chkShowPassword','txtPassword'); return true;" />
                Show password
            </label>
        </div>

        <div class="form-actions">
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn" OnClick="btnLogin_Click" />
        </div>

        <p class="muted" style="margin-top:14px;">
            New here? <a href="/Account/Register.aspx" runat="server">Create an account</a>
        </p>
    </div>

    <script type="text/javascript">
        function togglePassword(checkboxId, passwordId) {
            var box = document.getElementById(checkboxId);
            var field = document.getElementById(passwordId);
            if (box && field) field.type = box.checked ? 'text' : 'password';
        }
    </script>

</asp:Content>