<%@ Page Title="Take Quiz" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Quiz.aspx.cs" Inherits="CareerSkillHub.Student.Quiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-title">Quiz</h1>

    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <asp:PlaceHolder ID="phQuiz" runat="server" Visible="false">
        <div class="alert alert-info"><%: QuizTitle %></div>

        <asp:Repeater ID="rptQuestions" runat="server">
            <ItemTemplate>
                <div class="card" style="margin-bottom:16px;">
                    <h3>Q<%# Container.ItemIndex + 1 %>. <%# Eval("QuestionText") %></h3>
                    <div style="margin-top:10px;">
                        <asp:RadioButtonList ID="rblAnswer" runat="server" CssClass="option-list" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <p style="display:grid;gap:10px;margin-top:16px;">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit Quiz" CssClass="btn btn-success"
                OnClick="btnSubmit_Click" OnClientClick="return confirmSubmit();"
                Style="display:block;width:100%;background:#16a34a;color:#fff;padding:13px 16px;font-size:15px;font-weight:700;border:none;border-radius:8px;cursor:pointer;font-family:inherit;" />
            <a class="btn btn-view" style="display:block;width:100%;text-align:center;background:#0284c7;color:#fff;padding:13px 16px;font-weight:700;font-size:15px;border-radius:8px;text-decoration:none;" href="/Student/MyCourses.aspx" runat="server">Cancel</a>
        </p>
    </asp:PlaceHolder>

    <script type="text/javascript">
        function confirmSubmit() {
            return window.confirm('Are you sure you want to submit this quiz?');
        }
    </script>

</asp:Content>