<%@ Page Title="All Courses" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="CareerSkillHub.Student.Courses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">All Courses</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <div class="card-grid">
        <asp:Repeater ID="rptCourses" runat="server">
            <ItemTemplate>
                <article class="card">
                    <img src="<%# ResolveUrl(Eval("ImageURL") == DBNull.Value ? "~/Images/course.jpg" : "/" + Eval("ImageURL").ToString().Replace("~","")) %>"
                         alt="<%# Eval("Title") %>" onerror="this.src='<%# ResolveUrl("~/Images/course.jpg") %>'" />
                    <h3><%# Eval("Title") %></h3>
                    <span class="badge"><%# Eval("Category") %></span>
                    <p class="muted"><%# Eval("Description").ToString().Length > 130 ? Eval("Description").ToString().Substring(0, 130) + "..." : Eval("Description") %></p>
                    <p class="muted">Instructor: <%# Eval("InstructorName") %></p>

                    <asp:PlaceHolder ID="phEnrolled" runat="server"
                        Visible='<%# IsEnrolled((int)Eval("CourseID")) %>'>
                        <span class="badge" style="background:#dcfce7;color:#166534;">Enrolled</span>
                    </asp:PlaceHolder>

                    <a class="btn btn-small btn-success" style="display:inline-block;text-align:center;background:#16a34a;color:#fff;padding:9px 16px;font-weight:700;font-size:14px;border-radius:8px;text-decoration:none;"
                       href='<%# "/Student/Courses.aspx?enroll=" + Eval("CourseID") %>'>Enroll</a>
                    <a class="btn btn-small btn-secondary" style="display:inline-block;text-align:center;background:#0284c7;color:#fff;padding:9px 16px;font-weight:700;font-size:14px;border-radius:8px;text-decoration:none;"
                       href='<%# "/Pages/CourseDetails.aspx?courseId=" + Eval("CourseID") %>'>Details</a>
                </article>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>