using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Project5LMS.Controller
{
    public class CatalogController
    {
        private string connStr = "server=localhost;user=root;password=;database=library_db;";

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Books";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        BookID = Convert.ToInt32(reader["BookID"]),
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString(),
                        ISBN = reader["ISBN"].ToString(),
                        Publisher = reader["Publisher"].ToString(),
                        YearPublished = reader["YearPublished"].ToString(),
                        Category = reader["Category"].ToString(),
                        Copies = Convert.ToInt32(reader["Copies"]),
                        Available = Convert.ToInt32(reader["Available"]),
                        Barcode = reader["Barcode"].ToString(),
                        CoverImagePath = reader["CoverImagePath"].ToString()
                    });
                }
            }

            return books;
        }
    }

    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string YearPublished { get; set; }
        public string Category { get; set; }
        public int Copies { get; set; }
        public int Available { get; set; }
        public string Barcode { get; set; }
        public string CoverImagePath { get; set; }
    }
}
