using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CareerSkillHub.Models;

namespace CareerSkillHub.Data_Access_Layer
{
    /// <summary>
    /// All SQL statements that touch the Quizzes table.
    /// Demonstrates INSERT, SELECT, UPDATE and DELETE for quizzes.
    /// </summary>
    public class QuizDAL
    {
        public int Insert(Quiz quiz)
        {
            const string sql = @"
                INSERT INTO Quizzes (CourseID, Title, Description, CreatedDate)
                VALUES (@CourseID, @Title, @Description, GETDATE());
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@CourseID", quiz.CourseID);
                DbHelper.AddParam(cmd, "@Title", quiz.Title);
                DbHelper.AddParam(cmd, "@Description", quiz.Description);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public Quiz SelectById(int quizId)
        {
            const string sql = @"
                SELECT q.*, c.Title AS CourseTitle,
                       (SELECT COUNT(*) FROM Questions WHERE QuizID = q.QuizID) AS QuestionCount
                FROM Quizzes q
                INNER JOIN Courses c ON q.CourseID = c.CourseID
                WHERE q.QuizID = @QuizID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@QuizID", quizId);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read()) return Map(r);
                    return null;
                }
            }
        }

        public List<Quiz> SelectByCourse(int courseId)
        {
            const string sql = @"
                SELECT q.*, c.Title AS CourseTitle,
                       (SELECT COUNT(*) FROM Questions WHERE QuizID = q.QuizID) AS QuestionCount
                FROM Quizzes q
                INNER JOIN Courses c ON q.CourseID = c.CourseID
                WHERE q.CourseID = @CourseID
                ORDER BY q.CreatedDate DESC;";

            List<Quiz> list = new List<Quiz>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read()) list.Add(Map(r));
                }
            }
            return list;
        }

        public List<Quiz> SelectAll()
        {
            const string sql = @"
                SELECT q.*, c.Title AS CourseTitle,
                       (SELECT COUNT(*) FROM Questions WHERE QuizID = q.QuizID) AS QuestionCount
                FROM Quizzes q
                INNER JOIN Courses c ON q.CourseID = c.CourseID
                ORDER BY q.CreatedDate DESC;";

            List<Quiz> list = new List<Quiz>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read()) list.Add(Map(r));
                }
            }
            return list;
        }

        public List<Quiz> SelectByInstructor(int instructorId)
        {
            const string sql = @"
                SELECT q.*, c.Title AS CourseTitle,
                       (SELECT COUNT(*) FROM Questions WHERE QuizID = q.QuizID) AS QuestionCount
                FROM Quizzes q
                INNER JOIN Courses c ON q.CourseID = c.CourseID
                WHERE c.InstructorID = @InstructorID
                ORDER BY q.CreatedDate DESC;";

            List<Quiz> list = new List<Quiz>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@InstructorID", instructorId);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read()) list.Add(Map(r));
                }
            }
            return list;
        }

        public bool Update(Quiz quiz)
        {
            const string sql = @"
                UPDATE Quizzes
                SET CourseID = @CourseID, Title = @Title, Description = @Description
                WHERE QuizID = @QuizID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@CourseID", quiz.CourseID);
                DbHelper.AddParam(cmd, "@Title", quiz.Title);
                DbHelper.AddParam(cmd, "@Description", quiz.Description);
                DbHelper.AddParam(cmd, "@QuizID", quiz.QuizID);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int quizId)
        {
            // Removes the quiz together with its questions and stored
            // results (foreign keys must be removed first).
            const string sql = @"
                DELETE FROM QuizResults WHERE QuizID = @QuizID;
                DELETE FROM Questions WHERE QuizID = @QuizID;
                DELETE FROM Quizzes WHERE QuizID = @QuizID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@QuizID", quizId);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int CountAll()
        {
            const string sql = "SELECT COUNT(*) FROM Quizzes;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private Quiz Map(IDataRecord r)
        {
            return new Quiz
            {
                QuizID = DbHelper.GetInt(r, "QuizID"),
                CourseID = DbHelper.GetInt(r, "CourseID"),
                Title = DbHelper.GetString(r, "Title"),
                Description = DbHelper.GetString(r, "Description"),
                CreatedDate = DbHelper.GetDate(r, "CreatedDate"),
                CourseTitle = DbHelper.GetString(r, "CourseTitle"),
                QuestionCount = DbHelper.GetInt(r, "QuestionCount")
            };
        }
    }
}