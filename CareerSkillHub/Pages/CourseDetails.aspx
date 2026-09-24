<%@ Page Title="Course Details" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="CourseDetails.aspx.cs" Inherits="CareerSkillHub.Pages.CourseDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:PlaceHolder ID="phNotFound" runat="server" Visible="false">
        <div class="alert alert-error">Course not found.</div>
        <a class="btn" href="/Pages/Courses.aspx" runat="server">Back to Courses</a>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phCourse" runat="server" Visible="false">
        <h1 class="page-title"><%: courseTitle %></h1>

        <div class="card">
            <img src="<%: ResolveUrl(courseImage) %>" alt="<%: courseTitle %>" onerror="this.src='<%: ResolveUrl("~/Images/course.jpg") %>'" />
            <span class="badge"><%: courseCategory %></span>
            <p><%: courseDescription %></p>
            <p class="muted">Instructor: <%: courseInstructor %></p>
        </div>

        <p style="margin-top:18px;">
            <a class="btn btn-small" href="/Pages/Courses.aspx" runat="server">Back to Courses</a>
            <a class="btn btn-view" style="display:block;width:100%;text-align:center;background:#16a34a;color:#fff;padding:14px 16px;font-weight:700;font-size:15px;border-radius:8px;text-decoration:none;" href="/Account/Register.aspx" runat="server">Enroll - Register Free</a>
        </p>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phMaterials" runat="server" Visible="false">
        <h2 class="section-title" style="margin-top:24px;">Sample Learning Materials</h2>
        <div class="card-grid">
            <asp:Repeater ID="rptMaterials" runat="server">
                <ItemTemplate>
                    <article class="card">
                        <h3><%# Eval("Title") %></h3>
                        <p class="muted"><%# Eval("Description") %></p>
                    </article>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </asp:PlaceHolder>

</asp:Content>