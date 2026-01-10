using System;
using System.Data;
using MySql.Data.MySqlClient;
using Project5LMS.Data;
using Project5LMS.Models;

namespace Project5LMS.Services
{
    /// <summary>
    /// Service for executing stored procedures for complex operations
    /// </summary>
    public class StoredProcedureService
    {
        private readonly DatabaseContext _dbContext;

        public StoredProcedureService(DatabaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Borrow a book using stored procedure
        /// </summary>
        public BorrowResult BorrowBook(int memberId, int bookId, DateTime borrowDate, DateTime dueDate)
        {
            var result = new BorrowResult();
            try
            {
                var parameters = new[]
                {
                    new MySqlParameter("@p_MemberID", memberId),
                    new MySqlParameter("@p_BookID", bookId),
                    new MySqlParameter("@p_BorrowDate", borrowDate),
                    new MySqlParameter("@p_DueDate", dueDate),
                    new MySqlParameter("@p_TransactionID", MySqlDbType.Int32) { Direction = ParameterDirection.Output },
                    new MySqlParameter("@p_Success", MySqlDbType.Bit) { Direction = ParameterDirection.Output },
                    new MySqlParameter("@p_Message", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Output }
                };

                _dbContext.ExecuteStoredProcedureNonQuery("sp_BorrowBook", parameters);

                result.TransactionID = Convert.ToInt32(parameters[4].Value);
                result.Success = Convert.ToBoolean(parameters[5].Value);
                result.Message = parameters[6].Value?.ToString() ?? "Unknown error";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error executing stored procedure: {ex.Message}";
            }
            return result;
        }

        /// <summary>
        /// Return a book using stored procedure
        /// </summary>
        public ReturnResult ReturnBook(int transactionId, DateTime returnDate, decimal fineRatePerDay, decimal maxFineCap)
        {
            var result = new ReturnResult();
            try
            {
                var parameters = new[]
                {
                    new MySqlParameter("@p_TransactionID", transactionId),
                    new MySqlParameter("@p_ReturnDate", returnDate),
                    new MySqlParameter("@p_FineRatePerDay", fineRatePerDay),
                    new MySqlParameter("@p_MaxFineCap", maxFineCap),
                    new MySqlParameter("@p_Success", MySqlDbType.Bit) { Direction = ParameterDirection.Output },
                    new MySqlParameter("@p_FineAmount", MySqlDbType.Decimal) { Direction = ParameterDirection.Output },
                    new MySqlParameter("@p_Message", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Output }
                };

                _dbContext.ExecuteStoredProcedureNonQuery("sp_ReturnBook", parameters);

                result.Success = Convert.ToBoolean(parameters[4].Value);
                result.FineAmount = Convert.ToDecimal(parameters[5].Value ?? 0);
                result.Message = parameters[6].Value?.ToString() ?? "Unknown error";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.FineAmount = 0;
                result.Message = $"Error executing stored procedure: {ex.Message}";
            }
            return result;
        }

        /// <summary>
        /// Process payment using stored procedure
        /// </summary>
        public PaymentResult ProcessPayment(int transactionId, int memberId, decimal amountPaid, string paymentMode, string processedBy)
        {
            var result = new PaymentResult();
            try
            {
                var parameters = new[]
                {
                    new MySqlParameter("@p_TransactionID", transactionId),
                    new MySqlParameter("@p_MemberID", memberId),
                    new MySqlParameter("@p_AmountPaid", amountPaid),
                    new MySqlParameter("@p_PaymentMode", paymentMode),
                    new MySqlParameter("@p_ProcessedBy", processedBy),
                    new MySqlParameter("@p_ReceiptNumber", MySqlDbType.VarChar, 50) { Direction = ParameterDirection.Output },
                    new MySqlParameter("@p_Success", MySqlDbType.Bit) { Direction = ParameterDirection.Output },
                    new MySqlParameter("@p_Message", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Output }
                };

                _dbContext.ExecuteStoredProcedureNonQuery("sp_ProcessPayment", parameters);

                result.ReceiptNumber = parameters[5].Value?.ToString() ?? string.Empty;
                result.Success = Convert.ToBoolean(parameters[6].Value);
                result.Message = parameters[7].Value?.ToString() ?? "Unknown error";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error executing stored procedure: {ex.Message}";
            }
            return result;
        }

        /// <summary>
        /// Renew a book using stored procedure
        /// </summary>
        public RenewResult RenewBook(int transactionId, DateTime newDueDate, int maxRenewals)
        {
            var result = new RenewResult();
            try
            {
                var parameters = new[]
                {
                    new MySqlParameter("@p_TransactionID", transactionId),
                    new MySqlParameter("@p_NewDueDate", newDueDate),
                    new MySqlParameter("@p_MaxRenewals", maxRenewals),
                    new MySqlParameter("@p_Success", MySqlDbType.Bit) { Direction = ParameterDirection.Output },
                    new MySqlParameter("@p_Message", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Output }
                };

                _dbContext.ExecuteStoredProcedureNonQuery("sp_RenewBook", parameters);

                result.Success = Convert.ToBoolean(parameters[3].Value);
                result.Message = parameters[4].Value?.ToString() ?? "Unknown error";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error executing stored procedure: {ex.Message}";
            }
            return result;
        }
    }

    public class BorrowResult
    {
        public int TransactionID { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ReturnResult
    {
        public bool Success { get; set; }
        public decimal FineAmount { get; set; }
        public string Message { get; set; }
    }

    public class PaymentResult
    {
        public bool Success { get; set; }
        public string ReceiptNumber { get; set; }
        public string Message { get; set; }
    }

    public class RenewResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}

