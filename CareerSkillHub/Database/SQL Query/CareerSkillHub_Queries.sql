

/* ----------------------------------------------------------------------------
   PART 1 - CREATE DATABASE
---------------------------------------------------------------------------- */
USE master;
GO

IF DB_ID('CareerSkillHub') IS NOT NULL
BEGIN
    ALTER DATABASE CareerSkillHub SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CareerSkillHub;
END
GO

CREATE DATABASE CareerSkillHub;
GO

USE CareerSkillHub;
GO

/* ----------------------------------------------------------------------------
   PART 2 - CREATE TABLES
---------------------------------------------------------------------------- */

/* ==========================================================================
   1. USERS  (registered members: Student / Instructor / Admin)
   ========================================================================= */
CREATE TABLE Users (
    UserID       INT IDENTITY(1,1) NOT NULL,
    FullName     NVARCHAR(100) NOT NULL,
    Email        NVARCHAR(150) NOT NULL,
    PasswordHash CHAR(64)      NOT NULL,   -- SHA-256 hex
    PasswordSalt CHAR(32)      NOT NULL,   -- 16 random bytes as hex
    Role         NVARCHAR(20)  NOT NULL DEFAULT 'Student',
    CreatedDate  DATETIME2(0)  NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_Users PRIMARY KEY (UserID),
    CONSTRAINT UQ_Users_Email UNIQUE (Email)
);
GO

/* ==========================================================================
   2. COURSES  (one instructor owns many courses)
   ========================================================================= */
CREATE TABLE Courses (
    CourseID     INT IDENTITY(1,1) NOT NULL,
    Title        NVARCHAR(200)  NOT NULL,
    Description  NVARCHAR(MAX)  NOT NULL,
    Category     NVARCHAR(60)   NOT NULL,
    InstructorID INT            NOT NULL,
    ImageURL     NVARCHAR(260)  NULL,
    CreatedDate  DATETIME2(0)   NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_Courses PRIMARY KEY (CourseID),
    CONSTRAINT FK_Courses_Users FOREIGN KEY (InstructorID) REFERENCES Users(UserID)
);
GO

/* ==========================================================================
   3. ENROLLMENTS  (a student can enrol in a course only once)
   ========================================================================= */
CREATE TABLE Enrollments (
    EnrollmentID   INT IDENTITY(1,1) NOT NULL,
    UserID         INT           NOT NULL,
    CourseID       INT           NOT NULL,
    EnrollmentDate DATETIME2(0)  NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_Enrollments PRIMARY KEY (EnrollmentID),
    CONSTRAINT FK_Enrollments_Users   FOREIGN KEY (UserID)   REFERENCES Users(UserID),
    CONSTRAINT FK_Enrollments_Courses FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
    CONSTRAINT UQ_Enrollments_UserCourse UNIQUE (UserID, CourseID)
);
GO

/* ==========================================================================
   4. LEARNING MATERIALS  (notes / videos / resources inside a course)
   ========================================================================= */
CREATE TABLE LearningMaterials (
    MaterialID   INT IDENTITY(1,1) NOT NULL,
    CourseID     INT           NOT NULL,
    Title        NVARCHAR(200) NOT NULL,
    Description  NVARCHAR(MAX) NULL,
    Content      NVARCHAR(MAX) NULL,
    VideoURL     NVARCHAR(500) NULL,
    ResourceURL  NVARCHAR(500) NULL,
    CreatedDate  DATETIME2(0)  NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_LearningMaterials PRIMARY KEY (MaterialID),
    CONSTRAINT FK_Materials_Courses FOREIGN KEY (CourseID) REFERENCES Courses(CourseID) ON DELETE CASCADE
);
GO

/* ==========================================================================
   5. QUIZZES  (one quiz belongs to one course)
   ========================================================================= */
CREATE TABLE Quizzes (
    QuizID      INT IDENTITY(1,1) NOT NULL,
    CourseID    INT           NOT NULL,
    Title       NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CreatedDate DATETIME2(0)  NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_Quizzes PRIMARY KEY (QuizID),
    CONSTRAINT FK_Quizzes_Courses FOREIGN KEY (CourseID) REFERENCES Courses(CourseID) ON DELETE CASCADE
);
GO

/* ==========================================================================
   6. QUESTIONS  (MCQ: four options + correct answer)
   ========================================================================= */
CREATE TABLE Questions (
    QuestionID    INT IDENTITY(1,1) NOT NULL,
    QuizID        INT           NOT NULL,
    QuestionText  NVARCHAR(500) NOT NULL,
    OptionA       NVARCHAR(250) NOT NULL,
    OptionB       NVARCHAR(250) NOT NULL,
    OptionC       NVARCHAR(250) NOT NULL,
    OptionD       NVARCHAR(250) NOT NULL,
    CorrectAnswer CHAR(1)       NOT NULL,   -- 'A', 'B', 'C' or 'D'
    CONSTRAINT PK_Questions PRIMARY KEY (QuestionID),
    CONSTRAINT FK_Questions_Quizzes FOREIGN KEY (QuizID) REFERENCES Quizzes(QuizID) ON DELETE CASCADE
);
GO

/* ==========================================================================
   7. QUIZ RESULTS  (saved attempt of a student on a quiz)
   ========================================================================= */
CREATE TABLE QuizResults (
    ResultID       INT IDENTITY(1,1) NOT NULL,
    UserID         INT            NOT NULL,
    QuizID         INT            NOT NULL,
    Score          INT            NOT NULL,
    TotalQuestions INT            NOT NULL,
    Percentage     DECIMAL(5,2)   NOT NULL,
    AttemptDate    DATETIME2(0)   NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_QuizResults PRIMARY KEY (ResultID),
    CONSTRAINT FK_QuizResults_Users  FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_QuizResults_Quizzes FOREIGN KEY (QuizID) REFERENCES Quizzes(QuizID)
);
GO

/* ==========================================================================
   8. PROGRESS  (one progress row per student per course, 0..100%)
   ========================================================================= */
CREATE TABLE Progress (
    ProgressID         INT IDENTITY(1,1) NOT NULL,
    UserID             INT           NOT NULL,
    CourseID           INT           NOT NULL,
    ProgressPercentage INT           NOT NULL DEFAULT 0,
    LastUpdated        DATETIME2(0)  NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_Progress PRIMARY KEY (ProgressID),
    CONSTRAINT FK_Progress_Users   FOREIGN KEY (UserID)   REFERENCES Users(UserID),
    CONSTRAINT FK_Progress_Courses FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
    CONSTRAINT UQ_Progress_UserCourse UNIQUE (UserID, CourseID)
);
GO

/* ==========================================================================
   9. FEEDBACK  (student rating 1-5 + message)
   ========================================================================= */
CREATE TABLE Feedback (
    FeedbackID  INT IDENTITY(1,1) NOT NULL,
    UserID      INT           NOT NULL,
    Rating      INT           NOT NULL,   -- 1 to 5
    Message     NVARCHAR(MAX) NOT NULL,
    CreatedDate DATETIME2(0)  NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_Feedback PRIMARY KEY (FeedbackID),
    CONSTRAINT FK_Feedback_Users FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

/* ==========================================================================
   10. WEBSITE CONTENT  (homepage announcement / about / featured content)
   ========================================================================= */
CREATE TABLE WebsiteContent (
    ContentID   INT IDENTITY(1,1) NOT NULL,
    ContentType NVARCHAR(50)  NOT NULL,   -- 'Homepage' / 'About' / 'Featured'
    Title       NVARCHAR(150) NOT NULL,
    Content     NVARCHAR(MAX) NOT NULL,
    UpdatedDate DATETIME2(0)  NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_WebsiteContent PRIMARY KEY (ContentID)
);
GO

/* ----------------------------------------------------------------------------
   PART 3 - SAMPLE DATA
---------------------------------------------------------------------------- */
INSERT INTO Users (FullName, Email, PasswordHash, PasswordSalt, Role) VALUES
    ('System Admin',     'admin@careerskillhub.com',     'fd72ccc64a40e0837f525d381c86c51d3cb67e748eb2b574c2de95cfd362bd69', 'a1b2c3d4e5f60718293a4b5c6d7e8f90', 'Admin'),
    ('John Instructor',  'instructor@careerskillhub.com',  '69fdf0da993c6cceb20ba56a8207a41605e078267f8e398df0579a029847cbd7', 'a1b2c3d4e5f60718293a4b5c6d7e8f90', 'Instructor'),
    ('Ana Student',      'student@careerskillhub.com',    '69fdf0da993c6cceb20ba56a8207a41605e078267f8e398df0579a029847cbd7', 'a1b2c3d4e5f60718293a4b5c6d7e8f90', 'Student');
GO

INSERT INTO Courses (Title, Description, Category, InstructorID, ImageURL) VALUES
    ('Resume Writing', 'Learn how to write an impressive, ATS-friendly resume that presents your skills clearly and gets you shortlisted for interviews.', 'Career Skills', 2, 'images/resume.png'),
    ('Interview Preparation', 'Master common interview questions, structure great answers and build confidence for job interviews.', 'Career Skills', 2, 'images/interview.png'),
    ('Communication Skills', 'Improve your speaking, listening and writing so you can communicate clearly in work, study and daily life.', 'Career Skills', 2, 'images/communication.png'),
    ('Leadership', 'Understand what makes an effective leader and learn practical ways to lead teams with confidence and integrity.', 'Career Skills', 2, 'images/leadership.png'),
    ('Teamwork', 'Discover how to work effectively in teams, share responsibilities and reach goals together.', 'Career Skills', 2, 'images/teamwork.png'),
    ('Time Management', 'Learn proven techniques to plan your time, avoid procrastination and get more done with less stress.', 'Career Skills', 2, 'images/time.png'),
    ('Presentation Skills', 'Plan, design and deliver presentations that keep an audience engaged and communicate your message clearly.', 'Career Skills', 2, 'images/presentation.png');
GO

INSERT INTO LearningMaterials (CourseID, Title, Description, Content, VideoURL, ResourceURL) VALUES
    (1, 'Resume Essentials', 'The key sections every resume should include.',
     'A strong resume has a clear structure: contact information, professional summary, work experience, education, skills and achievements. Keep it to one page (or two for senior roles), tailor it to every job you apply for, and use action verbs to describe your responsibilities.',
     'https://www.youtube.com/embed/jmgqW9yIfV8', 'https://www.myresume.com'),
    (1, 'Action Verbs and Achievements', 'Write impressive bullet points.',
     'Start each bullet point with an action verb such as Managed, Designed, Improved or Led. Wherever possible, add a number to show the impact, for example "Increased monthly sales by 20 percent".',
     NULL, NULL),
    (2, 'Common Interview Questions', 'Practise the questions you will most likely be asked.',
     'Prepare answers for: Tell me about yourself; What are your strengths and weaknesses?; Where do you see yourself in five years?; and Why should we hire you? Use the STAR method (Situation, Task, Action, Result) to keep answers clear and structured.',
     'https://www.youtube.com/embed/HGw2jGmLWzU', NULL),
    (2, 'STAR Method', 'Structure your answers under pressure.',
     'STAR stands for Situation, Task, Action and Result. Describe the situation briefly, explain the task you had to complete, list the actions you took, and finish with the result you achieved.',
     NULL, 'https://www.themuse.com/advice/star-interview-method'),
    (3, 'Active Listening', 'The foundation of good communication.',
     'Active listening means paying full attention, not interrupting, and confirming you understood the other person. It builds trust and avoids misunderstanding in the workplace.',
     'https://www.youtube.com/embed/pQvsQH8e9K4', NULL),
    (3, 'Clear Writing at Work', 'Write emails and messages that people actually read.',
     'Keep business writing short and direct. Use short paragraphs, a clear subject line, and one main idea per message. Proofread before you send.',
     NULL, NULL),
    (4, 'Leadership Styles', 'Find the style that works for you and your team.',
     'Common leadership styles include autocratic, democratic, transformational and laissez-faire. Great leaders adapt their style to the situation and the people they lead.',
     'https://www.youtube.com/embed/SQbChqzXH0A', NULL),
    (5, 'Roles in a Team', 'Understand how teams succeed.',
     'Effective teams have clear goals, open communication and members who understand their roles. Trust and mutual respect are the glue that holds a team together.',
     NULL, NULL),
    (6, 'The Pomodoro Technique', 'A simple, powerful way to focus.',
     'Work in focused 25-minute blocks, then take a 5-minute break. After four blocks, take a longer break. This technique reduces procrastination and keeps your energy up.',
     'https://www.youtube.com/embed/VFW3LQ7n9bc', NULL),
    (7, 'Designing a Presentation', 'Structure slides that support your message.',
     'Start with a strong opening, keep each slide to one key idea, and use visuals instead of walls of text. End with a clear conclusion and a call to action.',
     'https://www.youtube.com/embed/pFryZOFWHu4', NULL);
GO

INSERT INTO Quizzes (CourseID, Title, Description) VALUES
    (1, 'Resume Writing Quiz', 'Test what you know about writing a winning resume.'),
    (2, 'Interview Preparation Quiz', 'Check your interview knowledge.'),
    (3, 'Communication Skills Quiz', 'A quick quiz about clear communication.'),
    (4, 'Leadership Quiz', 'How well do you understand leadership?'),
    (7, 'Presentation Skills Quiz', 'Quiz on planning and delivering presentations.');
GO

INSERT INTO Questions (QuizID, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer) VALUES
    (1, 'How long should a typical entry-level resume be?', 'Five pages', 'One page', 'Ten pages', 'Half a page', 'B'),
    (1, 'Which of these is an action verb?', 'Managed', 'Quiet', 'Orange', 'Slowly', 'A'),
    (1, 'What should appear at the top of your resume?', 'Your hobbies', 'Your contact information', 'Your photo of a pet', 'Your favourite colour', 'B'),
    (1, 'Why should you tailor your resume for each job?', 'It looks different', 'To match the skills in the job advert', 'To make it longer', 'To skip the summary', 'B'),
    (2, 'What does the A in the STAR method stand for?', 'Action', 'Agenda', 'Answer', 'Analysis', 'A'),
    (2, 'Which question is very common in interviews?', 'Tell me about yourself', 'What is your shoe size?', 'Do you like pizza?', 'What is your birthday?', 'A'),
    (2, 'How should you prepare for a job interview?', 'Do nothing', 'Research the company and practise answers', 'Arrive early without research', 'Memorise one answer', 'B'),
    (3, 'Which of the following describes active listening?', 'Talking the most', 'Paying full attention and confirming understanding', 'Checking your phone', 'Finishing other people sentences', 'B'),
    (3, 'What makes business writing clear?', 'Long sentences', 'Short, direct sentences with one idea', 'Using slang', 'Very long paragraphs', 'B'),
    (4, 'What is an autocratic leadership style?', 'Decisions are made by the leader alone', 'Everyone votes', 'No decisions are made', 'Leadership by luck', 'A'),
    (4, 'What is a key quality of a good leader?', 'Integrity', 'Always ordering people', 'Never listening', 'Blame others', 'A'),
    (5, 'What should a closing slide of a presentation include?', 'A clear conclusion and call to action', 'A blank screen', 'Your whole resume', 'Random clipart', 'A'),
    (5, 'Which of these keeps a presentation engaging?', 'Walls of text', 'Visuals and a clear key idea per slide', 'Reading slides aloud', 'Speaking very fast', 'B'),
    (1, 'What is an ATS-friendly resume?', 'One not stored on a computer', 'One that is easy for software to scan', 'A very colourful resume', 'A handwritten resume', 'B'),
    (2, 'How can you reduce interview nerves?', 'Practise and prepare', 'Stay up all night', 'Do not attend', 'Read your resume aloud repeatedly for 9 hours', 'A');
GO

INSERT INTO Enrollments (UserID, CourseID) VALUES
    (3, 1),
    (3, 2),
    (3, 3);
GO

INSERT INTO Progress (UserID, CourseID, ProgressPercentage) VALUES
    (3, 1, 70),
    (3, 2, 50),
    (3, 3, 85);
GO

INSERT INTO QuizResults (UserID, QuizID, Score, TotalQuestions, Percentage) VALUES
    (3, 1, 8, 10, 80.00),
    (3, 2, 9, 10, 90.00);
GO

INSERT INTO Feedback (UserID, Rating, Message) VALUES
    (3, 5, 'The resume and interview courses are really helpful. The quizzes made studying fun!');
GO

INSERT INTO WebsiteContent (ContentType, Title, Content) VALUES
    ('Homepage', 'Welcome Announcement', 'Welcome to Career Skill Hub - build the career skills employers are looking for.'),
    ('About', 'Our Mission', 'Career Skill Hub helps students and fresh graduates learn in-demand career skills through notes, videos and quizzes.'),
    ('Featured', 'Featured Course', 'Interview Preparation - check it out today!');
GO

