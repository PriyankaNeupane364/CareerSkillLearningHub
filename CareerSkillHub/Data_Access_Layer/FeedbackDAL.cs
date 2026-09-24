using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CareerSkillHub.Models;

namespace CareerSkillHub.Data_Access_Layer
{
    /// <summary>
    /// All SQL statements that touch the Feedback table.
    /// Demonstrates INSERT, SELECT and DELETE for feedback.
    /// </summary>
    public class FeedbackDAL
    {
        public int Insert(Feedback f)
        {
            const string sql = @"
                INSERT INTO Feedback (UserID, Rating, Message, CreatedDate)
                VALUES (@UserID, @Rating, @Message, GETDATE());
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", f.UserID);
                DbHelper.AddParam(cmd, "@Rating", f.Rating);
                DbHelper.AddParam(cmd, "@Message", f.Message);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<Feedback> SelectAll()
        {
            const string sql = @"
                SELECT f.*, u.FullName AS StudentName
                FROM Feedback f
                INNER JOIN Users u ON f.UserID = u.UserID
                ORDER BY f.CreatedDate DESC;";

            List<Feedback> list = new List<Feedback>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Feedback
                        {
                            FeedbackID = DbHelper.GetInt(r, "FeedbackID"),
                            UserID = DbHelper.GetInt(r, "UserID"),
                            Rating = DbHelper.GetInt(r, "Rating"),
                            Message = DbHelper.GetString(r, "Message"),
                            CreatedDate = DbHelper.GetDate(r, "CreatedDate"),
                            StudentName = DbHelper.GetString(r, "StudentName")
                        });
                    }
                }
            }
            return list;
        }

        public bool Delete(int feedbackId)
        {
            const string sql = "DELETE FROM Feedback WHERE FeedbackID = @FeedbackID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@FeedbackID", feedbackId);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int CountAll()
        {
            const string sql = "SELECT COUNT(*) FROM Feedback;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}