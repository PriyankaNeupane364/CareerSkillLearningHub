<%@ Page Title="Instructor Dashboard" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CareerSkillHub.Instructor.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Instructor Dashboard</h1>

    <div class="stat-grid">
        <div class="card">
            <div class="card-value"><asp:Literal ID="litCourses" runat="server" /></div>
            <div class="card-label">My Courses</div>
        </div>
        <div class="card">
            <div class="card-value"><asp:Literal ID="litStudents" runat="server" /></div>
            <div class="card-label">Students Enrolled</div>
        </div>
        <div class="card">
            <div class="card-value"><asp:Literal ID="litMaterials" runat="server" /></div>
            <div class="card-label">Materials Published</div>
        </div>
        <div class="card">
            <div class="card-value"><asp:Literal ID="litQuizzes" runat="server" /></div>
            <div class="card-label">Quizzes</div>
        </div>
    </div>

    <section>
        <h2 class="section-title">My Courses</h2>
        <a class="btn btn-small btn-success" href="/Instructor/Courses.aspx" runat="server">Manage Courses</a>
    </section>

    <div class="card-grid">
        <asp:Repeater ID="rptCourses" runat="server">
            <ItemTemplate>
                <article class="card">
                    <span class="badge"><%# Eval("Category") %></span>
                    <h3><%# Eval("Title") %></h3>
                    <p class="muted"><%# Eval("Description") %></p>
                    <p class="muted"><strong><%# CountStudents((int)Eval("CourseID")) %></strong> student(s) enrolled</p>
                </article>
            </ItemTemplate>
            </asp:Repeater>
        <p id="pnlNoCourses" runat="server" Visible="false">You have not created any courses yet.</p>
    </div>

</asp:Content>