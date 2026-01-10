using System;
using System.Collections.Generic;
using System.Linq;
using Project5LMS.Data;
using Project5LMS.Interfaces;
using Project5LMS.Models;
using MySql.Data.MySqlClient;
namespace Project5LMS.Services
{
    public class ReservationService : IReservationService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMembersService _membersService;
        private readonly IBookService _bookService;
        public ReservationService(DatabaseContext dbContext, IMembersService membersService, IBookService bookService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _membersService = membersService ?? throw new ArgumentNullException(nameof(membersService));
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }
        public bool CreateReservation(int memberId, int bookId)
        {
            if (!CanReserve(memberId, bookId))
                return false;
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string checkQuery = @"SELECT COUNT(*) FROM Reservations
                                        WHERE MemberID = @MemberID
                                        AND BookID = @BookID
                                        AND Status IN ('Pending', 'Active', 'Ready')";
                    using (var checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MemberID", memberId);
                        checkCmd.Parameters.AddWithValue("@BookID", bookId);
                        int existing = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (existing > 0)
                            return false;
                    }
                    string priorityQuery = @"SELECT COUNT(*) FROM Reservations
                                            WHERE BookID = @BookID
                                            AND Status IN ('Pending', 'Active')";
                    int priority = 0;
                    using (var priorityCmd = new MySqlCommand(priorityQuery, conn))
                    {
                        priorityCmd.Parameters.AddWithValue("@BookID", bookId);
                        priority = Convert.ToInt32(priorityCmd.ExecuteScalar());
                    }
                    var reservation = Reservation.Create(memberId, bookId, priority);
                    string insertQuery = @"INSERT INTO Reservations
                                          (MemberID, BookID, ReservationDate, PickupDate, ExpiryDate, Status, Priority)
                                          VALUES (@MemberID, @BookID, @ReservationDate, @PickupDate, @ExpiryDate, @Status, @Priority)";
                    using (var cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", reservation.MemberID);
                        cmd.Parameters.AddWithValue("@BookID", reservation.BookID);
                        cmd.Parameters.AddWithValue("@ReservationDate", reservation.ReservationDate);
                        cmd.Parameters.AddWithValue("@PickupDate", reservation.PickupDate);
                        cmd.Parameters.AddWithValue("@ExpiryDate", reservation.ExpiryDate);
                        cmd.Parameters.AddWithValue("@Status", reservation.Status);
                        cmd.Parameters.AddWithValue("@Priority", reservation.Priority);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating reservation: {ex.Message}");
                return false;
            }
        }
        public bool CancelReservation(int reservationId, string reason = null)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE Reservations
                                    SET Status = 'Cancelled',
                                        Notes = @Notes
                                    WHERE ReservationID = @ReservationID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        cmd.Parameters.AddWithValue("@Notes", reason ?? (object)DBNull.Value);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cancelling reservation: {ex.Message}");
                return false;
            }
        }
        public bool FulfillReservation(int reservationId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE Reservations
                                    SET Status = 'Ready',
                                        FulfilledDate = @FulfilledDate
                                    WHERE ReservationID = @ReservationID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        cmd.Parameters.AddWithValue("@FulfilledDate", DateTime.Now);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fulfilling reservation: {ex.Message}");
                return false;
            }
        }
        public bool MarkAsPickedUp(int reservationId)
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE Reservations
                                    SET Status = 'Fulfilled',
                                        FulfilledDate = @FulfilledDate
                                    WHERE ReservationID = @ReservationID";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        cmd.Parameters.AddWithValue("@FulfilledDate", DateTime.Now);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error marking reservation as picked up: {ex.Message}");
                return false;
            }
        }
        public IEnumerable<Reservation> GetMemberReservations(int memberId)
        {
            return GetReservations("WHERE r.MemberID = @MemberID", new { MemberID = memberId });
        }
        public IEnumerable<Reservation> GetBookReservations(int bookId)
        {
            return GetReservations("WHERE r.BookID = @BookID", new { BookID = bookId });
        }
        public IEnumerable<Reservation> GetPendingReservations()
        {
            return GetReservations("WHERE r.Status = 'Pending'", null);
        }
        public IEnumerable<Reservation> GetReadyReservations()
        {
            return GetReservations("WHERE r.Status = 'Ready'", null);
        }
        public bool CanReserve(int memberId, int bookId)
        {
            try
            {
                var member = _membersService.GetMember(memberId);
                if (member == null || !member.IsActive)
                    return false;
                var privileges = MemberTypePrivileges.GetDefaultPrivileges(member.Type);
                if (!privileges.CanReserve)
                    return false;
                int currentReservations = GetMemberReservations(memberId)
                    .Count(r => r.IsPending || r.IsActive || r.IsReady);
                if (currentReservations >= privileges.ReservationLimit)
                    return false;
                var book = _bookService.GetBook(bookId);
                if (book == null || !book.IsAvailable)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Reservation GetReservation(int reservationId)
        {
            var reservations = GetReservations("WHERE r.ReservationID = @ReservationID", new { ReservationID = reservationId });
            return reservations.FirstOrDefault();
        }
        public bool HasActiveReservations(int bookId)
        {
            return GetBookReservations(bookId).Any(r => r.IsPending || r.IsActive || r.IsReady);
        }
        private IEnumerable<Reservation> GetReservations(string whereClause, object parameters)
        {
            List<Reservation> reservations = new List<Reservation>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = $@"SELECT r.*,
                                      CONCAT(m.FirstName, ' ', m.LastName) as MemberName,
                                      b.Title as BookTitle,
                                      b.AccessionNo as AccessionNumber
                                      FROM Reservations r
                                      INNER JOIN Members m ON r.MemberID = m.MemberID
                                      INNER JOIN Books b ON r.BookID = b.BookID
                                      {whereClause}
                                      ORDER BY r.Priority ASC, r.ReservationDate ASC";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            foreach (var prop in parameters.GetType().GetProperties())
                            {
                                cmd.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(parameters));
                            }
                        }
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reservations.Add(MapReaderToReservation(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting reservations: {ex.Message}");
            }
            return reservations;
        }
        private Reservation MapReaderToReservation(MySqlDataReader reader)
        {
            return new Reservation
            {
                ReservationID = reader.GetInt32("ReservationID"),
                MemberID = reader.GetInt32("MemberID"),
                BookID = reader.GetInt32("BookID"),
                ReservationDate = reader.GetDateTime("ReservationDate"),
                PickupDate = reader["PickupDate"] != DBNull.Value ? (DateTime?)reader.GetDateTime("PickupDate") : null,
                ExpiryDate = reader["ExpiryDate"] != DBNull.Value ? (DateTime?)reader.GetDateTime("ExpiryDate") : null,
                Status = reader["Status"]?.ToString() ?? "Pending",
                Priority = reader["Priority"] != DBNull.Value ? reader.GetInt32("Priority") : 0,
                FulfilledDate = reader["FulfilledDate"] != DBNull.Value ? (DateTime?)reader.GetDateTime("FulfilledDate") : null,
                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                MemberName = reader["MemberName"]?.ToString(),
                BookTitle = reader["BookTitle"]?.ToString(),
                AccessionNumber = reader["AccessionNumber"]?.ToString()
            };
        }
    }
}