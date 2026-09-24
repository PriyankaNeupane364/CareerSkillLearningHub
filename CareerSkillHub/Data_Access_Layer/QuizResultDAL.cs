using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CareerSkillHub.Models;

namespace CareerSkillHub.Data_Access_Layer
{
    /// <summary>
    /// All SQL statements that touch the QuizResults table.
    /// Demonstrates INSERT and SELECT for quiz results.
    /// </summary>
    public class QuizResultDAL
    {
        public int Insert(QuizResult result)
        {
            const string sql = @"
                INSERT INTO QuizResults (UserID, QuizID, Score, TotalQuestions, Percentage, AttemptDate)
                VALUES (@UserID, @QuizID, @Score, @TotalQuestions, @Percentage, GETDATE());
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", result.UserID);
                DbHelper.AddParam(cmd, "@QuizID", result.QuizID);
                DbHelper.AddParam(cmd, "@Score", result.Score);
                DbHelper.AddParam(cmd, "@TotalQuestions", result.TotalQuestions);
                DbHelper.AddParam(cmd, "@Percentage", result.Percentage);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public QuizResult SelectById(int resultId)
        {
            const string sql = @"
                SELECT r.*, q.Title AS QuizTitle, c.Title AS CourseTitle
                FROM QuizResults r
                INNER JOIN Quizzes q ON r.QuizID = q.QuizID
                INNER JOIN Courses c ON q.CourseID = c.CourseID
                WHERE r.ResultID = @ResultID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@ResultID", resultId);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read()) return Map(r);
                    return null;
                }
            }
        }

        public List<QuizResult> SelectByUser(int userId)
        {
            const string sql = @"
                SELECT r.*, q.Title AS QuizTitle, c.Title AS CourseTitle
                FROM QuizResults r
                INNER JOIN Quizzes q ON r.QuizID = q.QuizID
                INNER JOIN Courses c ON q.CourseID = c.CourseID
                WHERE r.UserID = @UserID
                ORDER BY r.AttemptDate DESC;";

            List<QuizResult> list = new List<QuizResult>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read()) list.Add(Map(r));
                }
            }
            return list;
        }

        public int CountByUser(int userId)
        {
            const string sql = "SELECT COUNT(*) FROM QuizResults WHERE UserID = @UserID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public decimal HighestPercentageForUser(int userId)
        {
            const string sql = "SELECT ISNULL(MAX(Percentage), 0) FROM QuizResults WHERE UserID = @UserID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        private QuizResult Map(IDataRecord r)
        {
            return new QuizResult
            {
                ResultID = DbHelper.GetInt(r, "ResultID"),
                UserID = DbHelper.GetInt(r, "UserID"),
                QuizID = DbHelper.GetInt(r, "QuizID"),
                Score = DbHelper.GetInt(r, "Score"),
                TotalQuestions = DbHelper.GetInt(r, "TotalQuestions"),
                Percentage = DbHelper.GetDecimal(r, "Percentage"),
                AttemptDate = DbHelper.GetDate(r, "AttemptDate"),
                QuizTitle = DbHelper.GetString(r, "QuizTitle"),
                CourseTitle = DbHelper.GetString(r, "CourseTitle")
            };
        }
    }
}