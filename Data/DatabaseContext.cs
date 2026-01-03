using MySql.Data.MySqlClient;
using Project5LMS.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project5LMS.Data
{
    public class DatabaseContext
    {
        private readonly string connectionString;

        public DatabaseContext()
        {
            connectionString = DatabaseHelper.GetConnectionString();
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public DataTable ExecuteQuery(string query)
        {
            using (var con = GetConnection())
            {
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public int ExecuteNonQuery(string query)
        {
            using (var con = GetConnection())
            {
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
