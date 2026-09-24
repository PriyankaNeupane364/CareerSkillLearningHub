using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CareerSkillHub.Models;

namespace CareerSkillHub.Data_Access_Layer
{
    /// <summary>
    /// All SQL statements that touch the WebsiteContent table
    /// (homepage announcement, about content, featured content).
    /// Demonstrates INSERT, SELECT, UPDATE and DELETE for website content.
    /// </summary>
    public class WebsiteContentDAL
    {
        public int Insert(WebsiteContent wc)
        {
            const string sql = @"
                INSERT INTO WebsiteContent (ContentType, Title, Content, UpdatedDate)
                VALUES (@ContentType, @Title, @Content, GETDATE());
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@ContentType", wc.ContentType);
                DbHelper.AddParam(cmd, "@Title", wc.Title);
                DbHelper.AddParam(cmd, "@Content", wc.Content);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public WebsiteContent SelectByTypeAndTitle(string contentType, string title)
        {
            const string sql = @"
                SELECT TOP 1 * FROM WebsiteContent
                WHERE ContentType = @ContentType AND Title = @Title;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@ContentType", contentType);
                DbHelper.AddParam(cmd, "@Title", title);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read()) return Map(r);
                    return null;
                }
            }
        }

        public List<WebsiteContent> SelectByType(string contentType)
        {
            const string sql = @"
                SELECT * FROM WebsiteContent
                WHERE ContentType = @ContentType
                ORDER BY Title;";

            List<WebsiteContent> list = new List<WebsiteContent>();
            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@ContentType", contentType);
                con.Open();

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read()) list.Add(Map(r));
                }
            }
            return list;
        }

        public List<WebsiteContent> SelectAll()
        {
            const string sql = "SELECT * FROM WebsiteContent ORDER BY ContentType, Title;";

            List<WebsiteContent> list = new List<WebsiteContent>();
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

        public bool Update(WebsiteContent wc)
        {
            const string sql = @"
                UPDATE WebsiteContent
                SET ContentType = @ContentType, Title = @Title, Content = @Content, UpdatedDate = GETDATE()
                WHERE ContentID = @ContentID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@ContentType", wc.ContentType);
                DbHelper.AddParam(cmd, "@Title", wc.Title);
                DbHelper.AddParam(cmd, "@Content", wc.Content);
                DbHelper.AddParam(cmd, "@ContentID", wc.ContentID);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int contentId)
        {
            const string sql = "DELETE FROM WebsiteContent WHERE ContentID = @ContentID;";

            using (SqlConnection con = DbHelper.GetConnection())
            {
                SqlCommand cmd = DbHelper.CreateCommand(con, sql);
                DbHelper.AddParam(cmd, "@ContentID", contentId);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private WebsiteContent Map(IDataRecord r)
        {
            return new WebsiteContent
            {
                ContentID = DbHelper.GetInt(r, "ContentID"),
                ContentType = DbHelper.GetString(r, "ContentType"),
                Title = DbHelper.GetString(r, "Title"),
                Content = DbHelper.GetString(r, "Content"),
                UpdatedDate = DbHelper.GetDate(r, "UpdatedDate")
            };
        }
    }
}