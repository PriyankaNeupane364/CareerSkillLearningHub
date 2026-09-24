<%@ Page Title="Give Feedback" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="CareerSkillHub.Student.Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Give Feedback</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <div class="form-card">
        <div class="form-group">
            <label>Rating</label>
            <asp:RadioButtonList ID="rblRating" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="1 Star" Value="1" />
                <asp:ListItem Text="2 Stars" Value="2" />
                <asp:ListItem Text="3 Stars" Value="3" />
                <asp:ListItem Text="4 Stars" Value="4" />
                <asp:ListItem Text="5 Stars" Value="5" Selected="True" />
            </asp:RadioButtonList>
        </div>

        <div class="form-group">
            <label for="txtMessage">Your Feedback</label>
            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="5" MaxLength="1000" />
            <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="txtMessage"
                CssClass="field-error" Display="Dynamic" ErrorMessage="Feedback message is required." />
        </div>

        <div class="form-actions">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit Feedback" CssClass="btn btn-success"
                OnClick="btnSubmit_Click" />
        </div>
    </div>

</asp:Content>