<%@ Page Title="Courses" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="CareerSkillHub.Pages.Courses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Available Courses</h1>
    <p class="muted" style="margin-bottom:20px;">
        Browse all career-skill courses. Register and log in to enrol and start learning.
    </p>

    <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="alert alert-info" />

    <div class="card-grid">
        <asp:Repeater ID="rptCourses" runat="server">
            <ItemTemplate>
                <article class="card">
                    <img src="<%# ResolveUrl(Eval("ImageURL") == DBNull.Value ? "~/Images/course.jpg" : "/" + Eval("ImageURL").ToString().Replace("~","")) %>"
                         alt="<%# Eval("Title") %>" onerror="this.src='<%# ResolveUrl("~/Images/course.jpg") %>'" />
                    <h3><%# Eval("Title") %></h3>
                    <span class="badge"><%# Eval("Category") %></span>
                    <p class="muted"><%# Eval("Description").ToString().Length > 160 ? Eval("Description").ToString().Substring(0, 160) + "..." : Eval("Description") %></p>
                    <p class="muted">Instructor: <%# Eval("InstructorName") %></p>
                    <a class="btn btn-view" style="display:block;width:100%;text-align:center;background:#16a34a;color:#fff;padding:14px 16px;font-weight:700;font-size:15px;border-radius:8px;text-decoration:none;" href='<%# "/Pages/CourseDetails.aspx?courseId=" + Eval("CourseID") %>'>View Course</a>
                </article>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>