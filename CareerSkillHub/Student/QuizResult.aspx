<%@ Page Title="Quiz Result" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="QuizResult.aspx.cs" Inherits="CareerSkillHub.Student.QuizResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Quiz Result</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <asp:PlaceHolder ID="phResult" runat="server" Visible="false">
        <div class="card">
            <h3><%: QuizTitle %></h3>
            <p class="muted"><%: CourseTitle %></p>

            <div class="stat-grid" style="margin:20px 0;">
                <div class="card">
                    <div class="card-value"><%: Score %> / <%: TotalQuestions %></div>
                    <div class="card-label">Score</div>
                </div>
                <div class="card">
                    <div class="card-value"><%: Wrong %> </div>
                    <div class="card-label">Wrong Answers</div>
                </div>
                <div class="card">
                    <div class="card-value"><%: Percentage %>%</div>
                    <div class="card-label">Percentage</div>
                </div>
            </div>

            <asp:Literal ID="litGrade" runat="server" />

            <div class="progress-track" style="margin:16px 0;">
                <div class="progress-fill" style="width:<%: Percentage %>%"></div>
            </div>

            <div style="display:flex;gap:20px;flex-wrap:wrap;margin-top:20px;">
                <a class="btn btn-small" href="/Student/MyCourses.aspx" runat="server">Back to My Courses</a>
                <a class="btn btn-small btn-secondary" href="/Student/Progress.aspx" runat="server">View Progress</a>
            </div>
        </div>
    </asp:PlaceHolder>

</asp:Content>