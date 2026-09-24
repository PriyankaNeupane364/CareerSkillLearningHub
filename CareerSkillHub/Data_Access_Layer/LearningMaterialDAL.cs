using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CareerSkillHub.Models;

namespace CareerSkillHub.Data_Access_Layer
{
    /// <summary>
    /// All SQL statements that touch the LearningMaterials table.
    /// Demonstrates INSERT, SELECT, UPDATE and DELETE for learning materials.
    /// </summary>
    public class LearningMaterialDAL
    {
        public int Insert(LearningMaterial m)
        {
            const string sql = @"
                INSERT INTO LearningMaterials (CourseID, Title, Description, Content, VideoURL, ResourceURL, CreatedDate)
                VALUES (@CourseID, @Title, @Description, @Content, @VideoURL, @ResourceURL, GETDATE());
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@CourseID", m.CourseID);
                DbHelper.AddParam(cmd, "@Title", m.Title);
                DbHelper.AddParam(cmd, "@Description", m.Description);
                DbHelper.AddParam(cmd, "@Content", m.Content);
                DbHelper.AddParam(cmd, "@VideoURL", m.VideoURL);
                DbHelper.AddParam(cmd, "@ResourceURL", m.ResourceURL);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public LearningMaterial SelectById(int materialId)
        {
            const string sql = @"
                SELECT m.*, c.Title AS CourseTitle
                FROM LearningMaterials m
                INNER JOIN Courses c ON m.CourseID = c.CourseID
                WHERE m.MaterialID = @MaterialID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@MaterialID", materialId);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read()) return Map(r);
                    return null;
                }
            }
        }

        public List<LearningMaterial> SelectByCourse(int courseId)
        {
            const string sql = @"
                SELECT m.*, c.Title AS CourseTitle
                FROM LearningMaterials m
                INNER JOIN Courses c ON m.CourseID = c.CourseID
                WHERE m.CourseID = @CourseID
                ORDER BY m.CreatedDate DESC;";

            List<LearningMaterial> list = new List<LearningMaterial>();
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

        public List<LearningMaterial> SelectAll()
        {
            const string sql = @"
                SELECT m.*, c.Title AS CourseTitle
                FROM LearningMaterials m
                INNER JOIN Courses c ON m.CourseID = c.CourseID
                ORDER BY m.CreatedDate DESC;";

            List<LearningMaterial> list = new List<LearningMaterial>();
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

        public List<LearningMaterial> SelectByInstructor(int instructorId)
        {
            const string sql = @"
                SELECT m.*, c.Title AS CourseTitle
                FROM LearningMaterials m
                INNER JOIN Courses c ON m.CourseID = c.CourseID
                WHERE c.InstructorID = @InstructorID
                ORDER BY m.CreatedDate DESC;";

            List<LearningMaterial> list = new List<LearningMaterial>();
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

        public bool Update(LearningMaterial m)
        {
            const string sql = @"
                UPDATE LearningMaterials
                SET CourseID = @CourseID, Title = @Title, Description = @Description,
                    Content = @Content, VideoURL = @VideoURL, ResourceURL = @ResourceURL
                WHERE MaterialID = @MaterialID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@CourseID", m.CourseID);
                DbHelper.AddParam(cmd, "@Title", m.Title);
                DbHelper.AddParam(cmd, "@Description", m.Description);
                DbHelper.AddParam(cmd, "@Content", m.Content);
                DbHelper.AddParam(cmd, "@VideoURL", m.VideoURL);
                DbHelper.AddParam(cmd, "@ResourceURL", m.ResourceURL);
                DbHelper.AddParam(cmd, "@MaterialID", m.MaterialID);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int materialId)
        {
            const string sql = "DELETE FROM LearningMaterials WHERE MaterialID = @MaterialID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@MaterialID", materialId);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int CountAll()
        {
            const string sql = "SELECT COUNT(*) FROM LearningMaterials;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private LearningMaterial Map(IDataRecord r)
        {
            return new LearningMaterial
            {
                MaterialID = DbHelper.GetInt(r, "MaterialID"),
                CourseID = DbHelper.GetInt(r, "CourseID"),
                Title = DbHelper.GetString(r, "Title"),
                Description = DbHelper.GetString(r, "Description"),
                Content = DbHelper.GetString(r, "Content"),
                VideoURL = DbHelper.GetString(r, "VideoURL"),
                ResourceURL = DbHelper.GetString(r, "ResourceURL"),
                CreatedDate = DbHelper.GetDate(r, "CreatedDate"),
                CourseTitle = DbHelper.GetString(r, "CourseTitle")
            };
        }
    }
}