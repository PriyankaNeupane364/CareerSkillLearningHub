<%@ Page Title="Home" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CareerSkillHub.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<section class="hero home-hero">
        <div class="hero-inner">
            <div class="hero-text">
                <span class="hero-badge">Free Learning Platform for Students &amp; Graduates</span>
                <h1>Learn the Skills to Get Hired</h1>
                <p>
                    Start with <strong>Resume Writing &amp; Preparation</strong> and build your way up to
                    interviews, communication, leadership, teamwork, time management and presentations -
                    all in one place, completely free.
                </p>
                <p>
                    <a href="/Account/Register.aspx" runat="server" class="btn">Get Started - Register Free</a>
                    <a href="/Pages/Courses.aspx" runat="server" class="btn btn-outline" style="background:var(--white);color:var(--primary-dark);border-color:var(--white);">Browse Courses</a>
                </p>
            </div>
            <div class="hero-img">
                <svg viewBox="0 0 620 460" xmlns="http://www.w3.org/2000/svg" role="img" aria-label="Learn career skills">
                    <defs>
                        <linearGradient id="heroGrad" x1="0" y1="0" x2="1" y2="1">
                            <stop offset="0" stop-color="#1f6feb"/>
                            <stop offset="1" stop-color="#0f172a"/>
                        </linearGradient>
                    </defs>
                    <rect width="620" height="460" rx="18" fill="url(#heroGrad)"/>
                    <circle cx="-30" cy="430" r="130" fill="#ffffff" opacity="0.16"/>
                    <circle cx="580" cy="-40" r="110" fill="#ffffff" opacity="0.16"/>
                    <circle cx="60" cy="60" r="5" fill="#ffffff"/>
                    <circle cx="520" cy="120" r="4" fill="#ffffff"/>
                    <circle cx="560" cy="420" r="5" fill="#ffffff"/>
                    <circle cx="120" cy="330" r="3" fill="#ffffff"/>
                    <circle cx="480" cy="360" r="4" fill="#ffffff"/>
                    <circle cx="310" cy="115" r="72" fill="#ffffff"/>
                    <path d="M310 80 L365 125 L310 170 L255 125 Z" fill="#1457b3"/>
                    <path d="M262 139 L310 98 L310 112 L262 153 Z" fill="#1457b3"/>
                    <ellipse cx="310" cy="84" rx="10" ry="8" fill="#1457b3"/>
                    <path d="M310 84 C325 100 335 122 352 156" stroke="#1457b3" stroke-width="6" fill="none" stroke-linecap="round"/>
                    <path d="M352 158 L336 180 M352 158 L348 182 M352 158 L360 178" stroke="#1457b3" stroke-width="4" stroke-linecap="round"/>
                    <text x="310" y="295" text-anchor="middle" font-family="Segoe UI, Arial, sans-serif" font-size="40" font-weight="700" fill="#ffffff">Grow Your Career</text>
                    <text x="310" y="342" text-anchor="middle" font-family="Segoe UI, Arial, sans-serif" font-size="20" fill="#ffffff">Learn. Practise. Succeed.</text>
                    <rect x="130" y="368" width="360" height="48" rx="24" fill="#ffffff"/>
                    <text x="310" y="400" text-anchor="middle" font-family="Segoe UI, Arial, sans-serif" font-size="16" font-weight="700" fill="#1457b3">100% FREE - For Students &amp; Graduates</text>
                </svg>
            </div>
        </div>
    </section>

    <section>
        <h2 class="section-title">Welcome to Career Skill Hub</h2>
        <p class="muted">
            <asp:Literal ID="litAnnouncement" runat="server" />
        </p>
    </section>

    <section>
        <h2 class="section-title">Career Skills We Cover</h2>
        <div class="home-features">
            <article class="feature-item"><h4>Resume Writing &amp; Preparation</h4><p class="muted">Write resumes that impress recruiters.</p></article>
            <article class="feature-item"><h4>Interview Preparation</h4><p class="muted">Practise common questions with the STAR method.</p></article>
            <article class="feature-item"><h4>Communication Skills</h4><p class="muted">Speak, write and listen with confidence.</p></article>
            <article class="feature-item"><h4>Leadership</h4><p class="muted">Lead teams with integrity and vision.</p></article>
            <article class="feature-item"><h4>Teamwork</h4><p class="muted">Collaborate effectively to reach shared goals.</p></article>
            <article class="feature-item"><h4>Time Management</h4><p class="muted">Plan your time and beat procrastination.</p></article>
            <article class="feature-item"><h4>Presentation Skills</h4><p class="muted">Design and deliver engaging presentations.</p></article>
        </div>
    </section>

    <section>
        <h2 class="section-title">Featured Courses</h2>
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
                        <a class="btn btn-view" style="display:block;width:100%;text-align:center;background:#16a34a;color:#fff;padding:14px 16px;font-weight:700;font-size:15px;border-radius:8px;text-decoration:none;" href='<%# "/Pages/CourseDetails.aspx?courseId=" + Eval("CourseID") %>'>View Course</a>
                    </article>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>

    <section>
        <h2 class="section-title">Why Use Career Skill Hub?</h2>
        <div class="home-features">
            <article class="feature-item"><h4>Learn at your own pace</h4><p class="muted">Read notes and watch videos whenever you like.</p></article>
            <article class="feature-item"><h4>Track your progress</h4><p class="muted">Follow your learning progress course by course.</p></article>
            <article class="feature-item"><h4>Test yourself</h4><p class="muted">Interactive quizzes with instant results.</p></article>
            <article class="feature-item"><h4>Completely free</h4><p class="muted">For students and fresh graduates.</p></article>
        </div>
    </section>

    <section style="text-align:center;">
        <h2 class="section-title">Ready to Start Learning?</h2>
        <p class="muted" style="margin-bottom:16px;">Create a free account and begin improving your career skills today.</p>
        <a href="/Account/Register.aspx" runat="server" class="btn btn-success">Create My Free Account</a>
    </section>

</asp:Content>