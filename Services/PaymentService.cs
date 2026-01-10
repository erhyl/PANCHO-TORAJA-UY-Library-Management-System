using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Project5LMS.Data;
using Project5LMS.Models;
using Project5LMS.Interfaces;
namespace Project5LMS.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly DatabaseContext _dbContext;
        public PaymentService(DatabaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public bool ProcessPayment(FinePayment payment)
        {
            try
            {
                if (payment == null || payment.AmountPaid <= 0)
                    return false;
                if (string.IsNullOrWhiteSpace(payment.ReceiptNumber))
                {
                    payment.ReceiptNumber = GenerateReceiptNumber();
                }
                payment.PaymentDate = DateTime.Now;
                return _dbContext.ExecuteInTransaction((conn, trans) =>
                {
                    string query = @"INSERT INTO FinePayments (TransactionID, MemberID, AmountPaid, PaymentMode,
                                    PaymentDate, ReceiptNumber, ProcessedBy, Notes)
                                    VALUES (@TransactionID, @MemberID, @AmountPaid, @PaymentMode,
                                    @PaymentDate, @ReceiptNumber, @ProcessedBy, @Notes)";
                    using (var cmd = new MySqlCommand(query, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@TransactionID", payment.TransactionID);
                        cmd.Parameters.AddWithValue("@MemberID", payment.MemberID);
                        cmd.Parameters.AddWithValue("@AmountPaid", payment.AmountPaid);
                        cmd.Parameters.AddWithValue("@PaymentMode", payment.PaymentMode ?? "Cash");
                        cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                        cmd.Parameters.AddWithValue("@ReceiptNumber", payment.ReceiptNumber);
                        cmd.Parameters.AddWithValue("@ProcessedBy", payment.ProcessedBy ?? "System");
                        cmd.Parameters.AddWithValue("@Notes", payment.Notes ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                    if (payment.TransactionID > 0)
                    {
                        UpdateTransactionAfterPayment(conn, trans, payment.TransactionID, payment.AmountPaid);
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error processing payment: {ex.Message}");
                return false;
            }
        }
        public bool WaiveFine(FineAdjustment adjustment)
        {
            try
            {
                if (adjustment == null)
                    return false;
                adjustment.AdjustmentDate = DateTime.Now;
                return _dbContext.ExecuteInTransaction((conn, trans) =>
                {
                    string query = @"INSERT INTO FineAdjustments (TransactionID, MemberID, OriginalAmount,
                                    AdjustedAmount, AdjustmentAmount, AdjustmentType, Reason, AdjustedBy,
                                    AdjustmentDate, ApprovalRequired, ApprovedBy, ApprovalDate)
                                    VALUES (@TransactionID, @MemberID, @OriginalAmount, @AdjustedAmount,
                                    @AdjustmentAmount, @AdjustmentType, @Reason, @AdjustedBy,
                                    @AdjustmentDate, @ApprovalRequired, @ApprovedBy, @ApprovalDate)";
                    using (var cmd = new MySqlCommand(query, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@TransactionID", adjustment.TransactionID);
                        cmd.Parameters.AddWithValue("@MemberID", adjustment.MemberID);
                        cmd.Parameters.AddWithValue("@OriginalAmount", adjustment.OriginalAmount);
                        cmd.Parameters.AddWithValue("@AdjustedAmount", adjustment.AdjustedAmount);
                        cmd.Parameters.AddWithValue("@AdjustmentAmount", adjustment.AdjustmentAmount);
                        cmd.Parameters.AddWithValue("@AdjustmentType", adjustment.AdjustmentType ?? "Waiver");
                        cmd.Parameters.AddWithValue("@Reason", adjustment.Reason ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@AdjustedBy", adjustment.AdjustedBy ?? "System");
                        cmd.Parameters.AddWithValue("@AdjustmentDate", adjustment.AdjustmentDate);
                        cmd.Parameters.AddWithValue("@ApprovalRequired", adjustment.ApprovalRequired ?? "No");
                        cmd.Parameters.AddWithValue("@ApprovedBy", adjustment.ApprovedBy ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ApprovalDate", adjustment.ApprovalDate.HasValue ? adjustment.ApprovalDate.Value : (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                    if (adjustment.TransactionID > 0)
                    {
                        UpdateTransactionAfterAdjustment(conn, trans, adjustment.TransactionID, adjustment.AdjustedAmount);
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error waiving fine: {ex.Message}");
                return false;
            }
        }
        public IEnumerable<FinePayment> GetPaymentsByMember(int memberId)
        {
            List<FinePayment> payments = new List<FinePayment>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM FinePayments WHERE MemberID = @MemberID ORDER BY PaymentDate DESC";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                payments.Add(MapDataRowToPayment(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting payments by member: {ex.Message}");
            }
            return payments;
        }
        public IEnumerable<FineAdjustment> GetAdjustmentsByMember(int memberId)
        {
            List<FineAdjustment> adjustments = new List<FineAdjustment>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM FineAdjustments WHERE MemberID = @MemberID ORDER BY AdjustmentDate DESC";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberId);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                adjustments.Add(MapDataRowToAdjustment(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting adjustments by member: {ex.Message}");
            }
            return adjustments;
        }
        private string GenerateReceiptNumber()
        {
            return $"RCP-{DateTime.Now:yyyyMMdd}-{DateTime.Now.Ticks % 1000000:D6}";
        }
        private void UpdateTransactionAfterPayment(MySqlConnection conn, MySqlTransaction trans, int transactionId, decimal amountPaid)
        {
            try
            {
                string getFineQuery = "SELECT Fine FROM Transactions WHERE TransactionID = @TransactionID";
                decimal currentFine = 0m;
                using (var cmd = new MySqlCommand(getFineQuery, conn, trans))
                {
                    cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        currentFine = Convert.ToDecimal(result);
                    }
                }
                decimal newFine = Math.Max(0, currentFine - amountPaid);
                string updateQuery = "UPDATE Transactions SET Fine = @Fine WHERE TransactionID = @TransactionID";
                using (var cmd = new MySqlCommand(updateQuery, conn, trans))
                {
                    cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                    cmd.Parameters.AddWithValue("@Fine", newFine);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating transaction after payment: {ex.Message}");
                throw;
            }
        }
        private void UpdateTransactionAfterAdjustment(MySqlConnection conn, MySqlTransaction trans, int transactionId, decimal adjustedAmount)
        {
            try
            {
                string query = "UPDATE Transactions SET Fine = @Fine WHERE TransactionID = @TransactionID";
                using (var cmd = new MySqlCommand(query, conn, trans))
                {
                    cmd.Parameters.AddWithValue("@TransactionID", transactionId);
                    cmd.Parameters.AddWithValue("@Fine", adjustedAmount);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating transaction after adjustment: {ex.Message}");
                throw;
            }
        }
        private FinePayment MapDataRowToPayment(DataRow row)
        {
            return new FinePayment
            {
                PaymentID = Convert.ToInt32(row["PaymentID"]),
                TransactionID = Convert.ToInt32(row["TransactionID"]),
                MemberID = Convert.ToInt32(row["MemberID"]),
                AmountPaid = Convert.ToDecimal(row["AmountPaid"]),
                PaymentMode = row["PaymentMode"]?.ToString() ?? "Cash",
                PaymentDate = Convert.ToDateTime(row["PaymentDate"]),
                ReceiptNumber = row["ReceiptNumber"]?.ToString() ?? string.Empty,
                ProcessedBy = row["ProcessedBy"]?.ToString() ?? string.Empty,
                Notes = row["Notes"]?.ToString() ?? string.Empty,
                IsWaived = row.Table.Columns.Contains("IsWaived") && row["IsWaived"] != DBNull.Value && Convert.ToBoolean(row["IsWaived"]),
                WaiverReason = row.Table.Columns.Contains("WaiverReason") ? row["WaiverReason"]?.ToString() : null,
                WaivedBy = row.Table.Columns.Contains("WaivedBy") ? row["WaivedBy"]?.ToString() : null,
                WaiverDate = row.Table.Columns.Contains("WaiverDate") && row["WaiverDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["WaiverDate"]) : null
            };
        }
        private FineAdjustment MapDataRowToAdjustment(DataRow row)
        {
            return new FineAdjustment
            {
                AdjustmentID = Convert.ToInt32(row["AdjustmentID"]),
                TransactionID = Convert.ToInt32(row["TransactionID"]),
                MemberID = Convert.ToInt32(row["MemberID"]),
                OriginalAmount = Convert.ToDecimal(row["OriginalAmount"]),
                AdjustedAmount = Convert.ToDecimal(row["AdjustedAmount"]),
                AdjustmentAmount = Convert.ToDecimal(row["AdjustmentAmount"]),
                AdjustmentType = row["AdjustmentType"]?.ToString() ?? "Waiver",
                Reason = row["Reason"]?.ToString() ?? string.Empty,
                AdjustedBy = row["AdjustedBy"]?.ToString() ?? string.Empty,
                AdjustmentDate = Convert.ToDateTime(row["AdjustmentDate"]),
                ApprovalRequired = row["ApprovalRequired"]?.ToString() ?? "No",
                ApprovedBy = row["ApprovedBy"]?.ToString() ?? null,
                ApprovalDate = row["ApprovalDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["ApprovalDate"]) : null
            };
        }
    }
}