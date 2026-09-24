<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CareerSkillHub.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Admin Dashboard</h1>

    <div class="stat-grid">
        <div class="card">
            <div class="card-value"><asp:Literal ID="litStudents" runat="server" /></div>
            <div class="card-label">Students</div>
        </div>
        <div class="card">
            <div class="card-value"><asp:Literal ID="litInstructors" runat="server" /></div>
            <div class="card-label">Instructors</div>
        </div>
        <div class="card">
            <div class="card-value"><asp:Literal ID="litCourses" runat="server" /></div>
            <div class="card-label">Courses</div>
        </div>
        <div class="card">
            <div class="card-value"><asp:Literal ID="litEnrolments" runat="server" /></div>
            <div class="card-label">Total Enrolments</div>
        </div>
        <div class="card">
            <div class="card-value"><asp:Literal ID="litQuizzes" runat="server" /></div>
            <div class="card-label">Quizzes</div>
        </div>
        <div class="card">
            <div class="card-value"><asp:Literal ID="litFeedback" runat="server" /></div>
            <div class="card-label">Feedback Submitted</div>
        </div>
    </div>

    <section>
        <h2 class="section-title">Quick Actions</h2>
        <p>
            <a class="btn btn-small" href="/Admin/Users.aspx" runat="server">Manage Users</a>
            <a class="btn btn-small" href="/Admin/Courses.aspx" runat="server">Manage Courses</a>
            <a class="btn btn-small" href="/Admin/LearningMaterials.aspx" runat="server">Manage Materials</a>
            <a class="btn btn-small" href="/Admin/Quizzes.aspx" runat="server">Manage Quizzes</a>
            <a class="btn btn-small" href="/Admin/Feedback.aspx" runat="server">View Feedback</a>
            <a class="btn btn-small" href="/Admin/WebsiteContent.aspx" runat="server">Website Content</a>
        </p>
    </section>

    <section>
        <h2 class="section-title">Latest Feedback</h2>
        <div class="table-wrap">
            <table class="grid">
                <thead>
                    <tr>
                        <th>Student</th>
                        <th>Rating</th>
                        <th>Message</th>
                        <th>Date</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptFeedback" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("StudentName") %></td>
                                <td><%# Eval("Rating") %> / 5</td>
                                <td><%# Eval("Message") %></td>
                                <td><%# Eval("CreatedDate", "{0:dd MMM yyyy}") %></td>
                            </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                    <tr id="trNoFeedback" runat="server" Visible="false">
                        <td colspan="4">No feedback yet.</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </section>

</asp:Content>