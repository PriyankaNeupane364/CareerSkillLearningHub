using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CareerSkillHub.Models;

namespace CareerSkillHub.Data_Access_Layer
{
    /// <summary>
    /// All SQL statements that touch the Enrollments table.
    /// Demonstrates INSERT and SELECT for enrollments.
    /// </summary>
    public class EnrollmentDAL
    {
        public int Insert(int userId, int courseId)
        {
            const string sql = @"
                INSERT INTO Enrollments (UserID, CourseID, EnrollmentDate)
                VALUES (@UserID, @CourseID, GETDATE());
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool IsEnrolled(int userId, int courseId)
        {
            const string sql = "SELECT COUNT(*) FROM Enrollments WHERE UserID = @UserID AND CourseID = @CourseID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public List<Enrollment> SelectByUser(int userId)
        {
            const string sql = @"
                SELECT e.EnrollmentID, e.UserID, e.CourseID, e.EnrollmentDate,
                       c.Title AS CourseTitle, c.Category AS CourseCategory, c.ImageURL AS CourseImage
                FROM Enrollments e
                INNER JOIN Courses c ON e.CourseID = c.CourseID
                WHERE e.UserID = @UserID
                ORDER BY e.EnrollmentDate DESC;";

            List<Enrollment> list = new List<Enrollment>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Enrollment
                        {
                            EnrollmentID = DbHelper.GetInt(r, "EnrollmentID"),
                            UserID = DbHelper.GetInt(r, "UserID"),
                            CourseID = DbHelper.GetInt(r, "CourseID"),
                            EnrollmentDate = DbHelper.GetDate(r, "EnrollmentDate"),
                            CourseTitle = DbHelper.GetString(r, "CourseTitle"),
                            CourseCategory = DbHelper.GetString(r, "CourseCategory"),
                            CourseImage = DbHelper.GetString(r, "CourseImage")
                        });
                    }
                }
            }
            return list;
        }

        public int CountByUser(int userId)
        {
            const string sql = "SELECT COUNT(*) FROM Enrollments WHERE UserID = @UserID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int CountByCourse(int courseId)
        {
            const string sql = "SELECT COUNT(*) FROM Enrollments WHERE CourseID = @CourseID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int CountAll()
        {
            const string sql = "SELECT COUNT(*) FROM Enrollments;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool Delete(int userId, int courseId)
        {
            const string sql = "DELETE FROM Enrollments WHERE UserID = @UserID AND CourseID = @CourseID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}