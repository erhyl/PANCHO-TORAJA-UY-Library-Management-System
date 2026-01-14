using System;
using Project5LMS.Models;
using Project5LMS.Helpers;
namespace Project5LMS.Services
{
    /// <summary>
    /// Service for generating receipts for library transactions
    /// Centralizes receipt generation logic to reduce duplication
    /// </summary>
    public class ReceiptService
    {
        /// <summary>
        /// Generates a borrowing receipt
        /// </summary>
        public string GenerateBorrowingReceipt(Member member, Book book, DateTime dueDate, string receiptNumber)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));
            if (book == null) throw new ArgumentNullException(nameof(book));
            
            string receipt = $"═══════════════════════════════════\n" +
                           $"   BORROWING RECEIPT\n" +
                           $"═══════════════════════════════════\n" +
                           $"Receipt No: {receiptNumber}\n" +
                           $"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                           $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                           $"Member Information:\n" +
                           $"  Name: {member.FullName}\n" +
                           $"  Member ID: {IDFormatter.FormatMemberID(member.MemberID)}\n" +
                           $"  Type: {member.Type ?? "N/A"}\n" +
                           $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                           $"Book Information:\n" +
                           $"  Title: {book.Title}\n" +
                           $"  Author: {book.Author}\n" +
                           $"  ISBN: {book.ISBN ?? "N/A"}\n" +
                           $"  Accession No: {book.AccessionNo ?? "N/A"}\n" +
                           $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                           $"Due Date: {dueDate:yyyy-MM-dd}\n" +
                           $"═══════════════════════════════════";
            
            return receipt;
        }
        
        /// <summary>
        /// Generates a return receipt
        /// </summary>
        public string GenerateReturnReceipt(Member member, Book book, CirculationRecord transaction, decimal fine, int daysOverdue, string receiptNumber)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            
            string receipt = $"═══════════════════════════════════\n" +
                            $"   RETURN RECEIPT\n" +
                            $"═══════════════════════════════════\n" +
                            $"Receipt No: {receiptNumber}\n" +
                            $"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                            $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                            $"Member Information:\n" +
                            $"  Name: {member.FullName}\n" +
                            $"  Member ID: {IDFormatter.FormatMemberID(member.MemberID)}\n" +
                            $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                            $"Book Information:\n" +
                            $"  Title: {book.Title}\n" +
                            $"  Author: {book.Author}\n" +
                            $"  Accession No: {book.AccessionNo ?? "N/A"}\n" +
                            $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                            $"Borrowed: {transaction.BorrowDate:yyyy-MM-dd}\n" +
                            $"Due Date: {transaction.DueDate:yyyy-MM-dd}\n" +
                            $"Returned: {DateTime.Now:yyyy-MM-dd}\n";
            
            if (daysOverdue > 0)
            {
                receipt += $"Days Overdue: {daysOverdue}\n";
            }
            
            if (fine > 0)
            {
                receipt += $"Fine: {IDFormatter.FormatCurrency(fine)}\n";
            }
            else
            {
                receipt += $"Status: Returned on time\n";
            }
            
            receipt += $"═══════════════════════════════════";
            return receipt;
        }
        
        /// <summary>
        /// Generates a renewal receipt
        /// </summary>
        public string GenerateRenewalReceipt(Member member, Book book, DateTime oldDueDate, DateTime newDueDate, string receiptNumber)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));
            if (book == null) throw new ArgumentNullException(nameof(book));
            
            string receipt = $"═══════════════════════════════════\n" +
                           $"   RENEWAL RECEIPT\n" +
                           $"═══════════════════════════════════\n" +
                           $"Receipt No: {receiptNumber}\n" +
                           $"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                           $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                           $"Member Information:\n" +
                           $"  Name: {member.FullName}\n" +
                           $"  Member ID: {IDFormatter.FormatMemberID(member.MemberID)}\n" +
                           $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                           $"Book Information:\n" +
                           $"  Title: {book.Title}\n" +
                           $"  Author: {book.Author}\n" +
                           $"  Accession No: {book.AccessionNo ?? "N/A"}\n" +
                           $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                           $"Previous Due Date: {oldDueDate:yyyy-MM-dd}\n" +
                           $"New Due Date: {newDueDate:yyyy-MM-dd}\n" +
                           $"═══════════════════════════════════";
            
            return receipt;
        }
    }
}
