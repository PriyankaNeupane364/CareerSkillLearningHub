<%@ Page Title="Student Dashboard" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CareerSkillHub.Student.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Welcome, <%: StudentName %></h1>

    <div class="stat-grid">
        <div class="card">
            <div class="card-value"><asp:Literal ID="litEnrolled" runat="server" /></div>
            <div class="card-label">Courses Enrolled</div>
        </div>
        <div class="card">
            <div class="card-value"><asp:Literal ID="litQuizzes" runat="server" /></div>
            <div class="card-label">Quizzes Completed</div>
        </div>
        <div class="card">
            <div class="card-value"><asp:Literal ID="litBestScore" runat="server" /></div>
            <div class="card-label">Best Quiz Score</div>
        </div>
        <div class="card">
            <div class="card-value"><asp:Literal ID="litProgress" runat="server" /></div>
            <div class="card-label">Overall Progress</div>
        </div>
    </div>

    <section>
        <h2 class="section-title">Quick Actions</h2>
        <p>
            <a class="btn btn-small" href="/Student/Courses.aspx" runat="server">Browse Courses</a>
            <a class="btn btn-small btn-success" href="/Student/MyCourses.aspx" runat="server">My Courses</a>
            <a class="btn btn-small" href="/Student/Progress.aspx" runat="server">View Progress</a>
            <a class="btn btn-small btn-secondary" href="/Student/Feedback.aspx" runat="server">Give Feedback</a>
        </p>
    </section>

    <section>
        <h2 class="section-title">Recent Quiz Results</h2>
        <div class="table-wrap">
            <table class="grid">
                <thead>
                    <tr>
                        <th>Quiz</th>
                        <th>Course</th>
                        <th>Score</th>
                        <th>Percentage</th>
                        <th>Date</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptResults" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("QuizTitle") %></td>
                                <td><%# Eval("CourseTitle") %></td>
                                <td><%# Eval("Score") %> / <%# Eval("TotalQuestions") %></td>
                                <td><%# Eval("Percentage", "{0:0.00}") %>%</td>
                                <td><%# Eval("AttemptDate", "{0:dd MMM yyyy}") %></td>
                            </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                        <tr id="trNoResults" runat="server" Visible="false">
                            <td colspan="5">No quiz results yet.</td>
                        </tr>
                </tbody>
            </table>
        </div>
    </section>

</asp:Content>