<%@ Page Title="Register" Language="C#" MasterPageFile="~/Masterpages/Site.Master" ClientIDMode="Static"
    AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CareerSkillHub.Account.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Create a Free Account</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />
    <div id="clientError" class="alert alert-error" style="display:none;"></div>

    <div class="form-card">
        <div class="form-group">
            <label for="txtFullName">Full Name</label>
            <asp:TextBox ID="txtFullName" runat="server" MaxLength="100" placeholder="e.g. Ana Student" />
            <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName"
                CssClass="field-error" Display="Dynamic" ErrorMessage="Full name is required." />
        </div>

        <div class="form-group">
            <label for="txtEmail">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" MaxLength="150" placeholder="you@example.com" />
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                CssClass="field-error" Display="Dynamic" ErrorMessage="Email is required." />
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                CssClass="field-error" Display="Dynamic"
                ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                ErrorMessage="Enter a valid email address." />
        </div>

        <div class="form-group">
            <label for="txtPassword">Password</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="50" placeholder="At least 6 characters" />
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                CssClass="field-error" Display="Dynamic" ErrorMessage="Password is required." />
            <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword"
                CssClass="field-error" Display="Dynamic"
                ValidationExpression="^.{6,}$"
                ErrorMessage="Password must be at least 6 characters." />
        </div>

        <div class="form-group">
            <label for="txtConfirmPassword">Confirm Password</label>
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="50" placeholder="Re-enter your password" />
            <asp:RequiredFieldValidator ID="rfvConfirm" runat="server" ControlToValidate="txtConfirmPassword"
                CssClass="field-error" Display="Dynamic" ErrorMessage="Confirm your password." />
            <asp:CompareValidator ID="cvPassword" runat="server" ControlToValidate="txtConfirmPassword"
                ControlToCompare="txtPassword" Operator="Equal"
                CssClass="field-error" Display="Dynamic" ErrorMessage="Passwords do not match." />
        </div>

        <div class="form-group">
            <label>
                <asp:CheckBox ID="chkShowPassword" runat="server" onclick="togglePassword('chkShowPassword','txtPassword'); togglePassword('chkShowPassword','txtConfirmPassword'); return true;" />
                Show password
            </label>
        </div>

        <div class="form-actions">
            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-success"
                OnClick="btnRegister_Click" OnClientClick="return validateRegistration(this.form);" />
        </div>

        <p class="muted" style="margin-top:14px;">
            Already have an account? <a href="/Account/Login.aspx" runat="server">Login</a>
        </p>
    </div>

    <script type="text/javascript">
        function togglePassword(checkboxId, passwordId) {
            var box = document.getElementById(checkboxId);
            var field = document.getElementById(passwordId);
            if (box && field) field.type = box.checked ? 'text' : 'password';
        }
        // Client-side validation (real validation also happens server-side in C#).
        function validateRegistration(btn) {
            var form = document.getElementById('frmMain');
            var email = document.getElementById('txtEmail');
            var password = document.getElementById('txtPassword');
            var confirm = document.getElementById('txtConfirmPassword');
            var err = document.getElementById('clientError');
            var errors = [];
            if (email && !/^[^@\s]+@[^@\s]+\.[^@\s]+$/.test(email.value.trim())) errors.push('Enter a valid email address.');
            if (password && password.value.length < 6) errors.push('Password must be at least 6 characters.');
            if (password && confirm && password.value !== confirm.value) errors.push('Passwords do not match.');
            if (errors.length > 0) {
                if (err) { err.textContent = errors.join(' '); err.style.display = 'block'; }
                return false;
            }
            if (err) err.style.display = 'none';
            return true;
        }
    </script>

</asp:Content>