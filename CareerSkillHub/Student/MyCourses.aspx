<%@ Page Title="My Courses" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="MyCourses.aspx.cs" Inherits="CareerSkillHub.Student.MyCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">My Courses</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <div class="card-grid">
        <asp:Repeater ID="rptCourses" runat="server">
            <ItemTemplate>
                <article class="card">
                    <h3><%# Eval("CourseTitle") %></h3>
                    <span class="badge"><%# Eval("CourseCategory") %></span>
                    <p class="muted">Enrolled on <%# Eval("EnrollmentDate", "{0:dd MMM yyyy}") %></p>
                    <div style="display:grid;gap:8px;width:100%;">
                        <a class="btn btn-primary btn-view" style="display:block;width:100%;text-align:center;background:#0284c7;color:#fff;padding:13px 16px;font-weight:700;font-size:15px;border-radius:8px;text-decoration:none;" href='<%# "/Student/Materials.aspx?courseId=" + Eval("CourseID") %>'>Learning Materials</a>
                        <a class="btn btn-view" style="display:block;width:100%;text-align:center;background:#16a34a;color:#fff;padding:13px 16px;font-weight:700;font-size:15px;border-radius:8px;text-decoration:none;" href='<%# "/Student/Quiz.aspx?courseId=" + Eval("CourseID") %>'>Take Quiz</a>
                    </div>
                </article>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>