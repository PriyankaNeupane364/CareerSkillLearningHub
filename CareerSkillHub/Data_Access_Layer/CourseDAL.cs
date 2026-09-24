using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CareerSkillHub.Models;

namespace CareerSkillHub.Data_Access_Layer
{
    /// <summary>
    /// All SQL statements that touch the Courses table.
    /// Demonstrates INSERT, SELECT, UPDATE and DELETE for courses.
    /// </summary>
    public class CourseDAL
    {
        public int Insert(Course c)
        {
            const string sql = @"
                INSERT INTO Courses (Title, Description, Category, InstructorID, ImageURL, CreatedDate)
                VALUES (@Title, @Description, @Category, @InstructorID, @ImageURL, GETDATE());
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@Title", c.Title);
                DbHelper.AddParam(cmd, "@Description", c.Description);
                DbHelper.AddParam(cmd, "@Category", c.Category);
                DbHelper.AddParam(cmd, "@InstructorID", c.InstructorID);
                DbHelper.AddParam(cmd, "@ImageURL", c.ImageURL);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public Course SelectById(int courseId)
        {
            const string sql = @"
                SELECT c.*, u.FullName AS InstructorName
                FROM Courses c
                INNER JOIN Users u ON c.InstructorID = u.UserID
                WHERE c.CourseID = @CourseID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@CourseID", courseId);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read()) return Map(r);
                    return null;
                }
            }
        }

        public List<Course> SelectAll()
        {
            const string sql = @"
                SELECT c.*, u.FullName AS InstructorName
                FROM Courses c
                INNER JOIN Users u ON c.InstructorID = u.UserID
                ORDER BY c.CreatedDate DESC;";

            List<Course> list = new List<Course>();
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

        public List<Course> SelectByInstructor(int instructorId)
        {
            const string sql = @"
                SELECT c.*, u.FullName AS InstructorName
                FROM Courses c
                INNER JOIN Users u ON c.InstructorID = u.UserID
                WHERE c.InstructorID = @InstructorID
                ORDER BY c.CreatedDate DESC;";

            List<Course> list = new List<Course>();
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

        public bool Update(Course c)
        {
            const string sql = @"
                UPDATE Courses
                SET Title = @Title, Description = @Description, Category = @Category,
                    InstructorID = @InstructorID, ImageURL = @ImageURL
                WHERE CourseID = @CourseID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@Title", c.Title);
                DbHelper.AddParam(cmd, "@Description", c.Description);
                DbHelper.AddParam(cmd, "@Category", c.Category);
                DbHelper.AddParam(cmd, "@InstructorID", c.InstructorID);
                DbHelper.AddParam(cmd, "@ImageURL", c.ImageURL);
                DbHelper.AddParam(cmd, "@CourseID", c.CourseID);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int courseId)
        {
            // Deletes a course together with all of its dependent rows
            // (quiz results, questions, quizzes, materials, progress and
            // enrollments) inside one transaction.
            const string sql = @"
                DELETE qr FROM QuizResults qr
                    INNER JOIN Quizzes z ON qr.QuizID = z.QuizID
                WHERE z.CourseID = @CourseID;

                DELETE q FROM Questions q
                    INNER JOIN Quizzes z ON q.QuizID = z.QuizID
                WHERE z.CourseID = @CourseID;

                DELETE FROM Quizzes WHERE CourseID = @CourseID;

                DELETE FROM LearningMaterials WHERE CourseID = @CourseID;
                DELETE FROM Progress WHERE CourseID = @CourseID;
                DELETE FROM Enrollments WHERE CourseID = @CourseID;
                DELETE FROM Courses WHERE CourseID = @CourseID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                con.Open();
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                    cmd.Transaction = tx;
                    DbHelper.AddParam(cmd, "@CourseID", courseId);
                    int affected = cmd.ExecuteNonQuery();
                    tx.Commit();
                    return affected > 0;
                }
            }
        }

        public int CountAll()
        {
            const string sql = "SELECT COUNT(*) FROM Courses;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int CountByInstructor(int instructorId)
        {
            const string sql = "SELECT COUNT(*) FROM Courses WHERE InstructorID = @InstructorID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@InstructorID", instructorId);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private Course Map(IDataRecord r)
        {
            return new Course
            {
                CourseID = DbHelper.GetInt(r, "CourseID"),
                Title = DbHelper.GetString(r, "Title"),
                Description = DbHelper.GetString(r, "Description"),
                Category = DbHelper.GetString(r, "Category"),
                InstructorID = DbHelper.GetInt(r, "InstructorID"),
                ImageURL = DbHelper.GetString(r, "ImageURL"),
                CreatedDate = DbHelper.GetDate(r, "CreatedDate"),
                InstructorName = DbHelper.GetString(r, "InstructorName")
            };
        }
    }
}