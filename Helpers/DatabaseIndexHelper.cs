using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace Project5LMS.Helpers
{
    public static class DatabaseIndexHelper
    {
        public static void CreateRecommendedIndexes()
        {
            string connectionString = DatabaseHelper.GetConnectionString();
            List<string> indexQueries = new List<string>
            {
                "CREATE INDEX IF NOT EXISTS idx_books_title ON Books(Title)",
                "CREATE INDEX IF NOT EXISTS idx_books_author ON Books(Author)",
                "CREATE INDEX IF NOT EXISTS idx_books_isbn ON Books(ISBN)",
                "CREATE INDEX IF NOT EXISTS idx_books_accessionno ON Books(AccessionNo)",
                "CREATE INDEX IF NOT EXISTS idx_books_category ON Books(Category)",
                "CREATE INDEX IF NOT EXISTS idx_books_status ON Books(Status)",
                "CREATE INDEX IF NOT EXISTS idx_members_email ON Members(Email)",
                "CREATE INDEX IF NOT EXISTS idx_members_firstname ON Members(FirstName)",
                "CREATE INDEX IF NOT EXISTS idx_members_lastname ON Members(LastName)",
                "CREATE INDEX IF NOT EXISTS idx_members_type ON Members(Type)",
                "CREATE INDEX IF NOT EXISTS idx_transactions_memberid ON Transactions(MemberID)",
                "CREATE INDEX IF NOT EXISTS idx_transactions_bookid ON Transactions(BookID)",
                "CREATE INDEX IF NOT EXISTS idx_transactions_status ON Transactions(Status)",
                "CREATE INDEX IF NOT EXISTS idx_transactions_duedate ON Transactions(DueDate)",
                "CREATE INDEX IF NOT EXISTS idx_transactions_borrowdate ON Transactions(BorrowDate)",
                "CREATE INDEX IF NOT EXISTS idx_reservations_memberid ON Reservations(MemberID)",
                "CREATE INDEX IF NOT EXISTS idx_reservations_bookid ON Reservations(BookID)",
                "CREATE INDEX IF NOT EXISTS idx_reservations_status ON Reservations(Status)",
                "CREATE INDEX IF NOT EXISTS idx_users_email ON Users(Email)",
                "CREATE INDEX IF NOT EXISTS idx_users_role ON Users(Role)"
            };
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    int successCount = 0;
                    int failCount = 0;
                    foreach (string query in indexQueries)
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.ExecuteNonQuery();
                                successCount++;
                            }
                        }
                        catch (MySqlException ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Index creation warning: {ex.Message}");
                            failCount++;
                        }
                    }
                    System.Diagnostics.Debug.WriteLine($"Index creation completed: {successCount} succeeded, {failCount} failed/skipped");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating indexes: {ex.Message}");
                throw;
            }
        }
        public static List<string> GetIndexCreationStatements()
        {
            return new List<string>
            {
                "-- Books table indexes for search performance (<2 seconds target)",
                "CREATE INDEX IF NOT EXISTS idx_books_title ON Books(Title);",
                "CREATE INDEX IF NOT EXISTS idx_books_author ON Books(Author);",
                "CREATE INDEX IF NOT EXISTS idx_books_isbn ON Books(ISBN);",
                "CREATE INDEX IF NOT EXISTS idx_books_accessionno ON Books(AccessionNo);",
                "CREATE INDEX IF NOT EXISTS idx_books_category ON Books(Category);",
                "CREATE INDEX IF NOT EXISTS idx_books_status ON Books(Status);",
                "",
                "-- Members table indexes",
                "CREATE INDEX IF NOT EXISTS idx_members_email ON Members(Email);",
                "CREATE INDEX IF NOT EXISTS idx_members_firstname ON Members(FirstName);",
                "CREATE INDEX IF NOT EXISTS idx_members_lastname ON Members(LastName);",
                "CREATE INDEX IF NOT EXISTS idx_members_type ON Members(Type);",
                "",
                "-- Transactions table indexes",
                "CREATE INDEX IF NOT EXISTS idx_transactions_memberid ON Transactions(MemberID);",
                "CREATE INDEX IF NOT EXISTS idx_transactions_bookid ON Transactions(BookID);",
                "CREATE INDEX IF NOT EXISTS idx_transactions_status ON Transactions(Status);",
                "CREATE INDEX IF NOT EXISTS idx_transactions_duedate ON Transactions(DueDate);",
                "CREATE INDEX IF NOT EXISTS idx_transactions_borrowdate ON Transactions(BorrowDate);",
                "",
                "-- Reservations table indexes",
                "CREATE INDEX IF NOT EXISTS idx_reservations_memberid ON Reservations(MemberID);",
                "CREATE INDEX IF NOT EXISTS idx_reservations_bookid ON Reservations(BookID);",
                "CREATE INDEX IF NOT EXISTS idx_reservations_status ON Reservations(Status);",
                "",
                "-- Users table indexes",
                "CREATE INDEX IF NOT EXISTS idx_users_email ON Users(Email);",
                "CREATE INDEX IF NOT EXISTS idx_users_role ON Users(Role);"
            };
        }
        public static bool CheckIndexesExist()
        {
            string connectionString = DatabaseHelper.GetConnectionString();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT COUNT(*) as IndexCount
                        FROM INFORMATION_SCHEMA.STATISTICS
                        WHERE TABLE_SCHEMA = DATABASE()
                        AND INDEX_NAME IN (
                            'idx_books_title', 'idx_books_author', 'idx_books_isbn',
                            'idx_books_accessionno', 'idx_books_category', 'idx_books_status',
                            'idx_members_email', 'idx_members_firstname', 'idx_members_lastname',
                            'idx_transactions_memberid', 'idx_transactions_bookid', 'idx_transactions_status'
                        )";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            int indexCount = Convert.ToInt32(result);
                            return indexCount >= 12;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}