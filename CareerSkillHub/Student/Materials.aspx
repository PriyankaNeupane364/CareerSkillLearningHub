<%@ Page Title="Learning Materials" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Materials.aspx.cs" Inherits="CareerSkillHub.Student.Materials" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Learning Materials</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <asp:PlaceHolder ID="phMaterials" runat="server" Visible="false">
        <asp:Repeater ID="rptMaterials" runat="server">
            <ItemTemplate>
                <article class="card" style="margin-bottom:18px;">
                    <h3><%# Eval("Title") %></h3>
                    <p class="muted"><%# Eval("Description") %></p>
                    <p><%# Eval("Content") %></p>

                    <asp:PlaceHolder ID="phVideo" runat="server"
                        Visible='<%# !string.IsNullOrEmpty(Eval("VideoURL") == DBNull.Value ? null : (string)Eval("VideoURL")) %>'>
                        <video controls preload="metadata">
                            <source src="<%# Eval("VideoURL") %>" type="video/mp4" />
                            Your browser does not support HTML5 video. View it at:
                            <a href="<%# Eval("VideoURL") %>"><%# Eval("VideoURL") %></a>
                        </video>
                    </asp:PlaceHolder>

                    <asp:PlaceHolder ID="phResource" runat="server">
                        <p><a class="btn btn-small btn-secondary"
                            style='<%# string.IsNullOrEmpty(Eval("ResourceURL") as string) ? "opacity:.5;cursor:not-allowed;pointer-events:none;" : "" %>'
                            href='<%# string.IsNullOrEmpty(Eval("ResourceURL") as string) ? "javascript:void(0);" : Eval("ResourceURL") %>'
                            target="_blank">Open Resource</a></p>
                    </asp:PlaceHolder>
                </article>
            </ItemTemplate>
        </asp:Repeater>
    </asp:PlaceHolder>

    <div style="display:grid;gap:10px;margin-top:16px;">
        <a class="btn btn-view" style="display:block;width:100%;text-align:center;background:#16a34a;color:#fff;padding:13px 16px;font-weight:700;font-size:15px;border-radius:8px;text-decoration:none;" href="<%= "/Student/Quiz.aspx?courseId=" + QuizCourseId %>">Take the Course Quiz</a>
        <a class="btn btn-view" style="display:block;width:100%;text-align:center;background:#0284c7;color:#fff;padding:13px 16px;font-weight:700;font-size:15px;border-radius:8px;text-decoration:none;" href="<%= "/Student/MyCourses.aspx" %>">Back to My Courses</a>
    </div>

</asp:Content>