using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CareerSkillHub.Data_Access_Layer
{
    /// <summary>
    /// Central helper for all ADO.NET database plumbing.
    /// It reads the connection string from Web.config and gives other
    /// classes safe ways to create connections, commands and parameters.
    /// </summary>
    public static class DbHelper
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["CareerSkillHubConnection"].ConnectionString;

        /// <summary>Creates a new (not yet opened) connection.</summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>Builds a command bound to a connection.</summary>
        public static SqlCommand CreateCommand(SqlConnection con, string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 30;
            return cmd;
        }

        /// <summary>Adds a parameter, converting C# null into database NULL.</summary>
        public static void AddParam(SqlCommand cmd, string name, object value)
        {
            cmd.Parameters.AddWithValue(name, value ?? DBNull.Value);
        }

        // ------- Safe readers: turn database NULL into usable C# values -------

        public static string GetString(IDataRecord r, string col)
        {
            int i = r.GetOrdinal(col);
            return r.IsDBNull(i) ? null : r.GetString(i);
        }

        public static int GetInt(IDataRecord r, string col, int fallback = 0)
        {
            int i = r.GetOrdinal(col);
            return r.IsDBNull(i) ? fallback : r.GetInt32(i);
        }

        public static decimal GetDecimal(IDataRecord r, string col, decimal fallback = 0m)
        {
            int i = r.GetOrdinal(col);
            return r.IsDBNull(i) ? fallback : r.GetDecimal(i);
        }

        public static DateTime GetDate(IDataRecord r, string col)
        {
            int i = r.GetOrdinal(col);
            return r.IsDBNull(i) ? DateTime.MinValue : r.GetDateTime(i);
        }
    }
}