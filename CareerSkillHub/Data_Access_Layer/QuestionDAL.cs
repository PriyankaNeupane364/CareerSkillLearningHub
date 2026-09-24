using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CareerSkillHub.Models;

namespace CareerSkillHub.Data_Access_Layer
{
    /// <summary>
    /// All SQL statements that touch the Questions table.
    /// Demonstrates INSERT, SELECT, UPDATE and DELETE for questions.
    /// </summary>
    public class QuestionDAL
    {
        public int Insert(Question q)
        {
            const string sql = @"
                INSERT INTO Questions (QuizID, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer)
                VALUES (@QuizID, @QuestionText, @OptionA, @OptionB, @OptionC, @OptionD, @CorrectAnswer);
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@QuizID", q.QuizID);
                DbHelper.AddParam(cmd, "@QuestionText", q.QuestionText);
                DbHelper.AddParam(cmd, "@OptionA", q.OptionA);
                DbHelper.AddParam(cmd, "@OptionB", q.OptionB);
                DbHelper.AddParam(cmd, "@OptionC", q.OptionC);
                DbHelper.AddParam(cmd, "@OptionD", q.OptionD);
                DbHelper.AddParam(cmd, "@CorrectAnswer", q.CorrectAnswer);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public Question SelectById(int questionId)
        {
            const string sql = "SELECT * FROM Questions WHERE QuestionID = @QuestionID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@QuestionID", questionId);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read()) return Map(r);
                    return null;
                }
            }
        }

        public List<Question> SelectByQuiz(int quizId)
        {
            const string sql = @"
                SELECT * FROM Questions
                WHERE QuizID = @QuizID
                ORDER BY QuestionID;";

            List<Question> list = new List<Question>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@QuizID", quizId);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read()) list.Add(Map(r));
                }
            }
            return list;
        }

        public bool Update(Question q)
        {
            const string sql = @"
                UPDATE Questions
                SET QuestionText = @QuestionText, OptionA = @OptionA, OptionB = @OptionB,
                    OptionC = @OptionC, OptionD = @OptionD, CorrectAnswer = @CorrectAnswer
                WHERE QuestionID = @QuestionID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@QuestionText", q.QuestionText);
                DbHelper.AddParam(cmd, "@OptionA", q.OptionA);
                DbHelper.AddParam(cmd, "@OptionB", q.OptionB);
                DbHelper.AddParam(cmd, "@OptionC", q.OptionC);
                DbHelper.AddParam(cmd, "@OptionD", q.OptionD);
                DbHelper.AddParam(cmd, "@CorrectAnswer", q.CorrectAnswer);
                DbHelper.AddParam(cmd, "@QuestionID", q.QuestionID);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int questionId)
        {
            const string sql = "DELETE FROM Questions WHERE QuestionID = @QuestionID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@QuestionID", questionId);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int CountByQuiz(int quizId)
        {
            const string sql = "SELECT COUNT(*) FROM Questions WHERE QuizID = @QuizID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@QuizID", quizId);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int CountAll()
        {
            const string sql = "SELECT COUNT(*) FROM Questions;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private Question Map(IDataRecord r)
        {
            return new Question
            {
                QuestionID = DbHelper.GetInt(r, "QuestionID"),
                QuizID = DbHelper.GetInt(r, "QuizID"),
                QuestionText = DbHelper.GetString(r, "QuestionText"),
                OptionA = DbHelper.GetString(r, "OptionA"),
                OptionB = DbHelper.GetString(r, "OptionB"),
                OptionC = DbHelper.GetString(r, "OptionC"),
                OptionD = DbHelper.GetString(r, "OptionD"),
                CorrectAnswer = DbHelper.GetString(r, "CorrectAnswer")
            };
        }
    }
}