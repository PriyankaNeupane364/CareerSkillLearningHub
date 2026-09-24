<%@ Page Title="Access Denied" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="CareerSkillHub.Pages.AccessDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align:center; padding:60px 0;">
        <h1 class="page-title">Access Denied</h1>
        <p class="muted">You do not have permission to view this page.</p>
        <p style="margin-top:18px;">
            <a class="btn" href="/Default.aspx" runat="server">Go to Home</a>
        </p>
    </div>
</asp:Content>