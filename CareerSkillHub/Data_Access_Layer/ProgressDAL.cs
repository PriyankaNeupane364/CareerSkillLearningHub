using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CareerSkillHub.Models;

namespace CareerSkillHub.Data_Access_Layer
{
    /// <summary>
    /// All SQL statements that touch the Progress table.
    /// Demonstrates INSERT, SELECT and UPDATE for learning progress.
    /// </summary>
    public class ProgressDAL
    {
        public int Insert(int userId, int courseId, int percentage)
        {
            const string sql = @"
                INSERT INTO Progress (UserID, CourseID, ProgressPercentage, LastUpdated)
                VALUES (@UserID, @CourseID, @Percentage, GETDATE());
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                DbHelper.AddParam(cmd, "@Percentage", percentage);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool Update(int userId, int courseId, int percentage)
        {
            const string sql = @"
                UPDATE Progress
                SET ProgressPercentage = @Percentage, LastUpdated = GETDATE()
                WHERE UserID = @UserID AND CourseID = @CourseID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                DbHelper.AddParam(cmd, "@Percentage", percentage);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Exists(int userId, int courseId)
        {
            const string sql = "SELECT COUNT(*) FROM Progress WHERE UserID = @UserID AND CourseID = @CourseID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public List<Progress> SelectByUser(int userId)
        {
            const string sql = @"
                SELECT p.*, c.Title AS CourseTitle
                FROM Progress p
                INNER JOIN Courses c ON p.CourseID = c.CourseID
                WHERE p.UserID = @UserID
                ORDER BY c.Title;";

            List<Progress> list = new List<Progress>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Progress
                        {
                            ProgressID = DbHelper.GetInt(r, "ProgressID"),
                            UserID = DbHelper.GetInt(r, "UserID"),
                            CourseID = DbHelper.GetInt(r, "CourseID"),
                            ProgressPercentage = DbHelper.GetInt(r, "ProgressPercentage"),
                            LastUpdated = DbHelper.GetDate(r, "LastUpdated"),
                            CourseTitle = DbHelper.GetString(r, "CourseTitle")
                        });
                    }
                }
            }
            return list;
        }

        public int AverageForUser(int userId)
        {
            const string sql = "SELECT ISNULL(AVG(ProgressPercentage), 0) FROM Progress WHERE UserID = @UserID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}