<%@ Page Title="View Feedback" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="CareerSkillHub.Admin.Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Student Feedback</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <div class="table-wrap">
        <table class="grid">
            <thead>
                <tr>
                    <th>Student</th>
                    <th>Rating</th>
                    <th>Message</th>
                    <th>Date</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptFeedback" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("StudentName") %></td>
                            <td><span class="badge"><%# Eval("Rating") %> / 5</span></td>
                            <td><%# Eval("Message") %></td>
                            <td><%# Eval("CreatedDate", "{0:dd MMM yyyy}") %></td>
                            <td>
                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="lnk-btn danger"
                                    CommandArgument='<%# Eval("FeedbackID") %>' OnCommand="Delete_Command"
                                    OnClientClick="return confirm('Delete this feedback?');">Delete</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    <tr id="trNoFeedback" runat="server" Visible="false">
                        <td colspan="5">No feedback submitted yet.</td>
                    </tr>
                </tbody>
        </table>
    </div>

</asp:Content>