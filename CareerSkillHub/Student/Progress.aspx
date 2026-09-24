<%@ Page Title="My Progress" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Progress.aspx.cs" Inherits="CareerSkillHub.Student.Progress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">My Learning Progress</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <div class="table-wrap">
        <table class="grid">
            <thead>
                <tr>
                    <th>Course</th>
                    <th style="width:45%;">Progress</th>
                    <th>Last Updated</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptProgress" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("CourseTitle") %></td>
                            <td>
                                <div class="progress-track">
                                    <div class="progress-fill" style="width:<%# Eval("ProgressPercentage") %>%"></div>
                                </div>
                            </td>
                            <td>
                                <strong><%# Eval("ProgressPercentage") %>%</strong><br />
                                <span class="muted" style="font-size:.8rem;"><%# Eval("LastUpdated", "{0:dd MMM yyyy}") %></span>
                            </td>
                            <td>
                                <a class="btn btn-small" style="display:inline-block;text-align:center;background:#0284c7;color:#fff;padding:8px 14px;font-weight:700;font-size:13px;border-radius:8px;text-decoration:none;" href='<%# "/Student/Materials.aspx?courseId=" + Eval("CourseID") %>'>Materials</a>
                                <a class="btn btn-small" style="display:inline-block;text-align:center;background:#16a34a;color:#fff;padding:8px 14px;font-weight:700;font-size:13px;border-radius:8px;text-decoration:none;" href='<%# "/Student/Quiz.aspx?courseId=" + Eval("CourseID") %>'>Take Quiz</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

</asp:Content>