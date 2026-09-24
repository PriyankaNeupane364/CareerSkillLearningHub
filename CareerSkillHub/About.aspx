<%@ Page Title="About" Language="C#" MasterPageFile="~/Masterpages/Site.Master"
    AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="CareerSkillHub.About" %>

<asp:Content ID="HeadInternal" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        /* Internal CSS (page-specific) - demonstrates internal stylesheet usage */
        .about-hero { border-bottom: 3px solid var(--accent); }
        .about-card h3 { color: var(--primary-dark); }
        .skill-pills span { transition: transform .15s ease, box-shadow .15s ease; cursor: default; }
        .skill-pills span:hover { transform: translateY(-2px); box-shadow: 0 4px 8px rgba(2,132,199,.15); }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="about-hero">
        <span class="hero-badge">About Us</span>
        <h1>About Career Skill Hub</h1>
        <p>
            A free learning platform that helps students and fresh graduates build the career
            skills employers look for - all in one place.
        </p>
    </section>

    <section>
        <h2 class="section-title">Our Purpose</h2>
        <div class="card card-accent">
            <p>
                Career Skill Hub is a centralized web-based learning platform that helps students and
                fresh graduates prepare for the world of work. Instead of searching across many websites,
                learners can build the essential career skills employers look for in one place - with the
                help of structured notes, tutorial videos, digital resources, courses and self-assessment quizzes.
            </p>
        </div>
    </section>

    <section>
        <h2 class="section-title">Who Is It For?</h2>
        <div class="about-grid">
            <article class="card about-card">
                <div class="about-icon">S</div>
                <h3>Students</h3>
                <p class="muted">Learn important career skills, track progress, take quizzes and build confidence before entering the job market.</p>
            </article>
            <article class="card about-card">
                <div class="about-icon">G</div>
                <h3>Fresh Graduates</h3>
                <p class="muted">Refresh and improve your skills, prepare for interviews and understand what employers expect.</p>
            </article>
        </div>
    </section>

    <section>
        <h2 class="section-title">Career Skills Covered</h2>
        <div class="skill-pills">
            <span>Resume Writing &amp; Preparation</span>
            <span>Interview Preparation</span>
            <span>Communication Skills</span>
            <span>Leadership</span>
            <span>Teamwork</span>
            <span>Time Management</span>
            <span>Presentation Skills</span>
        </div>
    </section>

    <section>
        <h2 class="section-title">How It Works</h2>
        <div class="card-grid">
            <article class="card how-card">
                <div class="step-num">1</div>
                <h3>Register</h3>
                <p class="muted">Create a free student account to start learning.</p>
            </article>
            <article class="card how-card">
                <div class="step-num">2</div>
                <h3>Enroll in courses</h3>
                <p class="muted">Choose the career skills you want to improve.</p>
            </article>
            <article class="card how-card">
                <div class="step-num">3</div>
                <h3>Learn with materials</h3>
                <p class="muted">Read notes, watch tutorial videos and download resources.</p>
            </article>
            <article class="card how-card">
                <div class="step-num">4</div>
                <h3>Take quizzes</h3>
                <p class="muted">Test your knowledge and get instant results.</p>
            </article>
        </div>
    </section>

    <section>
        <h2 class="section-title">Our Mission</h2>
        <div class="card card-mission">
            <p>
                <asp:Literal ID="litMission" runat="server" />
            </p>
        </div>
    </section>

</asp:Content>