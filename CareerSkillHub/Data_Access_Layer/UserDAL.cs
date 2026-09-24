using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CareerSkillHub.Models;

namespace CareerSkillHub.Data_Access_Layer
{
    /// <summary>
    /// All SQL statements that touch the Users table.
    /// Demonstrates INSERT, SELECT, UPDATE and DELETE for users.
    /// </summary>
    public class UserDAL
    {
        public int Insert(User user)
        {
            const string sql = @"
                INSERT INTO Users (FullName, Email, PasswordHash, PasswordSalt, Role, CreatedDate)
                VALUES (@FullName, @Email, @PasswordHash, @PasswordSalt, @Role, GETDATE());
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@FullName", user.FullName);
                DbHelper.AddParam(cmd, "@Email", user.Email);
                DbHelper.AddParam(cmd, "@PasswordHash", user.PasswordHash);
                DbHelper.AddParam(cmd, "@PasswordSalt", user.PasswordSalt);
                DbHelper.AddParam(cmd, "@Role", user.Role);

                con.Open();
                object id = cmd.ExecuteScalar();
                return Convert.ToInt32(id);
            }
        }

        public User SelectByEmail(string email)
        {
            const string sql = "SELECT * FROM Users WHERE Email = @Email;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@Email", email);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read()) return Map(r);
                    return null;
                }
            }
        }

        public User SelectById(int userId)
        {
            const string sql = "SELECT * FROM Users WHERE UserID = @UserID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read()) return Map(r);
                    return null;
                }
            }
        }

        public List<User> SelectAll()
        {
            const string sql = "SELECT * FROM Users ORDER BY CreatedDate DESC;";
            List<User> list = new List<User>();

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

        public bool Update(User user)
        {
            const string sql = @"
                UPDATE Users
                SET FullName = @FullName, Email = @Email, Role = @Role
                WHERE UserID = @UserID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@FullName", user.FullName);
                DbHelper.AddParam(cmd, "@Email", user.Email);
                DbHelper.AddParam(cmd, "@Role", user.Role);
                DbHelper.AddParam(cmd, "@UserID", user.UserID);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateProfile(int userId, string fullName, string email)
        {
            const string sql = @"
                UPDATE Users
                SET FullName = @FullName, Email = @Email
                WHERE UserID = @UserID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@FullName", fullName);
                DbHelper.AddParam(cmd, "@Email", email);
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdatePassword(int userId, string passwordHash, string passwordSalt)
        {
            const string sql = @"
                UPDATE Users
                SET PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt
                WHERE UserID = @UserID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@PasswordHash", passwordHash);
                DbHelper.AddParam(cmd, "@PasswordSalt", passwordSalt);
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int userId)
        {
            // Removes the user together with all of their dependent rows
            // (enrolments, progress, quiz results and feedback).
            const string sql = @"
                DELETE FROM Enrollments WHERE UserID = @UserID;
                DELETE FROM Progress WHERE UserID = @UserID;
                DELETE FROM QuizResults WHERE UserID = @UserID;
                DELETE FROM Feedback WHERE UserID = @UserID;
                DELETE FROM Users WHERE UserID = @UserID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@UserID", userId);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int CountByRole(string role)
        {
            const string sql = "SELECT COUNT(*) FROM Users WHERE Role = @Role;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@Role", role);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int CountAll()
        {
            const string sql = "SELECT COUNT(*) FROM Users;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool EmailExists(string email, int excludeUserId = 0)
        {
            const string sql = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND UserID <> @UserID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@Email", email);
                DbHelper.AddParam(cmd, "@UserID", excludeUserId);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private User Map(IDataRecord r)
        {
            return new User
            {
                UserID = DbHelper.GetInt(r, "UserID"),
                FullName = DbHelper.GetString(r, "FullName"),
                Email = DbHelper.GetString(r, "Email"),
                PasswordHash = DbHelper.GetString(r, "PasswordHash"),
                PasswordSalt = DbHelper.GetString(r, "PasswordSalt"),
                Role = DbHelper.GetString(r, "Role"),
                CreatedDate = DbHelper.GetDate(r, "CreatedDate")
            };
        }
    }
}