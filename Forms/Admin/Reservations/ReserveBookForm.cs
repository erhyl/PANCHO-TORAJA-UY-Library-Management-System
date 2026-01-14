using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project5LMS.Services;
using Project5LMS.Data;
using Project5LMS.Interfaces;
using Project5LMS.Models;
using Project5LMS.Helpers;
using Project5LMS.Repositories;
using MySql.Data.MySqlClient;

namespace Project5LMS.Forms.Admin.Reservations
{
    public partial class ReserveBookForm : Form
    {
        private readonly IBookService _bookService;
        private readonly IMembersService _membersService;
        private readonly IReservationService _reservationService;
        private readonly BorrowingValidator _borrowingValidator;
        private readonly DatabaseContext _dbContext;

        public ReserveBookForm()
        {
            InitializeComponent();
            _dbContext = ServiceFactory.GetDbContext();
            _bookService = ServiceFactory.CreateBookService();
            _membersService = ServiceFactory.CreateMembersService();
            _reservationService = ServiceFactory.CreateReservationService();
            _borrowingValidator = DependencyInjection.GetRequiredService<BorrowingValidator>();
            
            // Set default dates
            dtpReservationDate.Value = DateTime.Now;
            dtpExpirationDate.Value = DateTime.Now.AddDays(7);
        }

        private void txtReservationBookID_TextChanged(object sender, EventArgs e)
        {
            string bookIdText = txtReservationBookID.Text.Trim();
            if (string.IsNullOrWhiteSpace(bookIdText) || bookIdText == "Format: 1" || bookIdText.Contains("Format:"))
            {
                ClearBookInfoDisplay();
                return;
            }

            try
            {
                // Check if input contains leading zeros (e.g., "001", "0001", etc.)
                // Only check if the input is purely numeric (no letters, dashes, etc.)
                if (System.Text.RegularExpressions.Regex.IsMatch(bookIdText, @"^0+[1-9]"))
                {
                    // Input has leading zeros - do not display book information
                    ClearBookInfoDisplay();
                    return;
                }

                // Also check if input is exactly "0" or starts with "0" followed by non-digit
                if (bookIdText == "0" || (bookIdText.Length > 1 && bookIdText.StartsWith("0") && char.IsDigit(bookIdText[1])))
                {
                    ClearBookInfoDisplay();
                    return;
                }

                int bookId = IDFormatter.ParseBookID(bookIdText);
                
                // Book ID must start from 1, not 0
                if (bookId <= 0)
                {
                    ClearBookInfoDisplay();
                    return;
                }

                if (bookId > 0)
                {
                    var book = _bookService.GetBook(bookId);
                    if (book != null)
                    {
                        DisplayBookInfo(book);
                        return;
                    }
                }

                // Try partial search only for non-numeric inputs or valid formats
                // Skip search if input contains leading zeros
                var searchResults = _bookService.SearchBooks(bookIdText);
                var matchingBook = searchResults.FirstOrDefault(b =>
                    b.BookID.ToString().Contains(bookIdText) ||
                    (b.Title != null && b.Title.IndexOf(bookIdText, StringComparison.OrdinalIgnoreCase) >= 0));

                if (matchingBook != null)
                {
                    DisplayBookInfo(matchingBook);
                    return;
                }

                ClearBookInfoDisplay();
            }
            catch
            {
                ClearBookInfoDisplay();
            }
        }

        private void txtReservationBookID_Enter(object sender, EventArgs e)
        {
            if (txtReservationBookID.Text == "Format: 1" || txtReservationBookID.Text.Contains("Format:"))
            {
                txtReservationBookID.Text = "";
                txtReservationBookID.ForeColor = Color.Black;
            }
        }

        private void txtReservationBookID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReservationBookID.Text))
            {
                txtReservationBookID.Text = "Format: 1";
                txtReservationBookID.ForeColor = Color.Gray;
            }
        }

        private void txtReservationMemberID_TextChanged(object sender, EventArgs e)
        {
            string memberIdText = txtReservationMemberID.Text.Trim();
            if (string.IsNullOrWhiteSpace(memberIdText) || memberIdText == "Format: MEM-000001 or 1" || memberIdText.Contains("Format:"))
            {
                ClearMemberEligibilityDisplay();
                return;
            }

            try
            {
                int memberId = IDFormatter.ParseMemberID(memberIdText);
                if (memberId > 0)
                {
                    DisplayMemberEligibility(memberId);
                    return;
                }

                // Try partial search
                string numericPart = System.Text.RegularExpressions.Regex.Replace(memberIdText, @"[^\d]", "");
                if (!string.IsNullOrWhiteSpace(numericPart) && numericPart.Length >= 1)
                {
                    var searchResults = _membersService.SearchMembers(memberIdText);
                    var matchingMember = searchResults.FirstOrDefault(m =>
                        IDFormatter.FormatMemberID(m.MemberID).IndexOf(memberIdText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        m.MemberID.ToString().Contains(numericPart));

                    if (matchingMember != null)
                    {
                        DisplayMemberEligibility(matchingMember.MemberID);
                        return;
                    }
                }

                ClearMemberEligibilityDisplay();
            }
            catch
            {
                ClearMemberEligibilityDisplay();
            }
        }

        private void txtReservationMemberID_Enter(object sender, EventArgs e)
        {
            if (txtReservationMemberID.Text == "Format: MEM-000001 or 1" || txtReservationMemberID.Text.Contains("Format:"))
            {
                txtReservationMemberID.Text = "";
                txtReservationMemberID.ForeColor = Color.Black;
            }
        }

        private void txtReservationMemberID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReservationMemberID.Text))
            {
                txtReservationMemberID.Text = "Format: MEM-000001 or 1";
                txtReservationMemberID.ForeColor = Color.Gray;
            }
        }

        private void DisplayBookInfo(Book book)
        {
            if (panelReserveBookInfo != null)
            {
                panelReserveBookInfo.Visible = true;

                if (lblReserveBookTitle != null)
                    lblReserveBookTitle.Text = $"Title: {book.Title}";
                if (lblReserveBookAuthor != null)
                    lblReserveBookAuthor.Text = $"Author: {book.Author}";
                if (lblReserveBookStatus != null)
                {
                    lblReserveBookStatus.Text = book.IsAvailable ? "Status: Available" : "Status: Not Available";
                    lblReserveBookStatus.ForeColor = book.IsAvailable ? Constants.GetSuccessColor() : Constants.GetErrorColor();
                }
                if (lblReserveBookCopies != null)
                    lblReserveBookCopies.Text = $"Copies: {book.Available}/{book.TotalCopies}";
            }
        }

        private void ClearBookInfoDisplay()
        {
            if (panelReserveBookInfo != null)
            {
                panelReserveBookInfo.Visible = false;
            }

            if (lblReserveBookTitle != null) lblReserveBookTitle.Text = "";
            if (lblReserveBookAuthor != null) lblReserveBookAuthor.Text = "";
            if (lblReserveBookStatus != null) lblReserveBookStatus.Text = "";
            if (lblReserveBookCopies != null) lblReserveBookCopies.Text = "";
        }

        private void DisplayMemberEligibility(int memberId)
        {
            try
            {
                var eligibilityInfo = _borrowingValidator.GetMemberEligibility(memberId);
                if (eligibilityInfo == null)
                {
                    ClearMemberEligibilityDisplay();
                    return;
                }

                var member = eligibilityInfo.Member;
                var privileges = MemberTypePrivileges.GetDefaultPrivileges(member.Type);
                int activeBorrowings = eligibilityInfo.ActiveBorrowings;
                int overdueCount = eligibilityInfo.OverdueCount;
                decimal totalFines = eligibilityInfo.TotalFines;
                bool isActive = eligibilityInfo.IsActive;
                bool withinLimit = eligibilityInfo.WithinLimit;
                bool noOverdue = eligibilityInfo.NoOverdue;
                bool finesPaid = eligibilityInfo.FinesPaid;
                bool isEligible = eligibilityInfo.IsEligible;

                if (panelMemberEligibility != null)
                {
                    panelMemberEligibility.Visible = true;

                    if (lblReserveMemberName != null)
                        lblReserveMemberName.Text = $"Name: {eligibilityInfo.Member.FullName}";

                    if (lblReserveMemberType != null)
                        lblReserveMemberType.Text = $"Type: {eligibilityInfo.Member.Type}";

                    if (lblReserveMemberStatus != null)
                    {
                        string statusText = isActive ? "Status: Active" : "Status: Inactive/Expired";
                        lblReserveMemberStatus.Text = statusText;
                        lblReserveMemberStatus.ForeColor = isActive ? Color.FromArgb(34, 139, 34) : Color.FromArgb(220, 20, 60);
                    }

                    if (lblReserveMemberBorrowings != null)
                    {
                        lblReserveMemberBorrowings.Text = $"Borrowings: {activeBorrowings}/{privileges.MaxBooksAllowed}";
                        lblReserveMemberBorrowings.ForeColor = withinLimit ? Color.FromArgb(64, 64, 64) : Color.FromArgb(220, 20, 60);
                    }

                    if (lblReserveMemberOverdue != null)
                    {
                        lblReserveMemberOverdue.Text = overdueCount > 0 ? $"Overdue: {overdueCount} book(s)" : "Overdue: None";
                        lblReserveMemberOverdue.ForeColor = noOverdue ? Constants.GetNeutralColor() : Constants.GetErrorColor();
                    }

                    if (lblReserveMemberFines != null)
                    {
                        lblReserveMemberFines.Text = totalFines > 0 ? $"Fines: {IDFormatter.FormatCurrency(totalFines)}" : "Fines: None";
                        lblReserveMemberFines.ForeColor = finesPaid ? Constants.GetNeutralColor() : Constants.GetErrorColor();
                    }

                    if (lblReserveEligibilityStatus != null)
                    {
                        if (isEligible)
                        {
                            lblReserveEligibilityStatus.Text = "✓ ELIGIBLE";
                            lblReserveEligibilityStatus.ForeColor = Constants.GetSuccessColor();
                        }
                        else
                        {
                            var reasons = new List<string>();
                            if (!isActive) reasons.Add("Account inactive/expired");
                            if (!withinLimit) reasons.Add("Borrowing limit reached");
                            if (!noOverdue) reasons.Add("Has overdue books");
                            if (!finesPaid) reasons.Add("Unpaid fines");

                            lblReserveEligibilityStatus.Text = $"✗ NOT ELIGIBLE - {string.Join(", ", reasons)}";
                            lblReserveEligibilityStatus.ForeColor = Constants.GetErrorColor();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error displaying member eligibility: {ex.Message}");
                ClearMemberEligibilityDisplay();
            }
        }

        private void ClearMemberEligibilityDisplay()
        {
            if (panelMemberEligibility != null)
            {
                panelMemberEligibility.Visible = false;
            }

            if (lblReserveMemberName != null) lblReserveMemberName.Text = "";
            if (lblReserveMemberType != null) lblReserveMemberType.Text = "";
            if (lblReserveMemberStatus != null) lblReserveMemberStatus.Text = "";
            if (lblReserveMemberBorrowings != null) lblReserveMemberBorrowings.Text = "";
            if (lblReserveMemberOverdue != null) lblReserveMemberOverdue.Text = "";
            if (lblReserveMemberFines != null) lblReserveMemberFines.Text = "";
            if (lblReserveEligibilityStatus != null) lblReserveEligibilityStatus.Text = "";
        }

        private void btnProccessReservation_Click(object sender, EventArgs e)
        {
            // Validate inputs
            string bookIdText = txtReservationBookID.Text.Trim();
            string memberIdText = txtReservationMemberID.Text.Trim();

            if (bookIdText == "Format: 1" || bookIdText.Contains("Format:") || string.IsNullOrWhiteSpace(bookIdText))
            {
                MessageBox.Show("Please enter a Book ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReservationBookID.Focus();
                return;
            }

            if (memberIdText == "Format: MEM-000001 or 1" || memberIdText.Contains("Format:") || string.IsNullOrWhiteSpace(memberIdText))
            {
                MessageBox.Show("Please enter a Member ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReservationMemberID.Focus();
                return;
            }

            try
            {
                int bookId = IDFormatter.ParseBookID(bookIdText);
                if (bookId == 0)
                {
                    MessageBox.Show("Invalid Book ID format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int memberId = IDFormatter.ParseMemberID(memberIdText);
                if (memberId == 0)
                {
                    MessageBox.Show("Invalid Member ID format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if book exists
                var book = _bookService.GetBook(bookId);
                if (book == null)
                {
                    MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if member exists
                var member = _membersService.GetMember(memberId);
                if (member == null)
                {
                    MessageBox.Show("Member not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check member eligibility - CRITICAL: Prevent reservations for inactive/expired/suspended members
                var eligibilityInfo = _borrowingValidator.GetMemberEligibility(memberId);
                if (eligibilityInfo == null)
                {
                    MessageBox.Show("Unable to verify member eligibility. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if member is inactive, expired, or suspended
                bool isSuspended = member.Status?.Equals("Suspended", StringComparison.OrdinalIgnoreCase) == true;
                if (!eligibilityInfo.IsActive || member.IsExpired || isSuspended)
                {
                    string statusReason;
                    if (isSuspended)
                        statusReason = "suspended";
                    else if (!eligibilityInfo.IsActive)
                        statusReason = "inactive";
                    else
                        statusReason = "expired";

                    string statusText = isSuspended ? "Suspended" : (!eligibilityInfo.IsActive ? "Inactive" : "Expired");
                    
                    MessageBox.Show($"Cannot process reservation. Member account is {statusReason}.\n\n" +
                        $"Member: {member.FullName}\n" +
                        $"Status: {statusText}\n\n" +
                        "Please activate, renew, or unsuspend the member's account before processing reservations.",
                        "Member Not Eligible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if member is eligible (no overdue, within limits, fines paid)
                if (!eligibilityInfo.IsEligible)
                {
                    var reasons = new List<string>();
                    if (!eligibilityInfo.WithinLimit) reasons.Add("Borrowing limit reached");
                    if (!eligibilityInfo.NoOverdue) reasons.Add("Has overdue books");
                    if (!eligibilityInfo.FinesPaid) reasons.Add("Unpaid fines");

                    MessageBox.Show($"Cannot process reservation. Member is not eligible.\n\n" +
                        $"Member: {member.FullName}\n" +
                        $"Reasons: {string.Join(", ", reasons)}\n\n" +
                        "Please resolve these issues before processing reservations.",
                        "Member Not Eligible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get dates
                DateTime reservationDate = dtpReservationDate.Value;
                DateTime expiryDate = dtpExpirationDate.Value;

                if (expiryDate <= reservationDate)
                {
                    MessageBox.Show("Expiration date must be after reservation date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create reservation (service method only takes memberId and bookId)
                bool success = _reservationService.CreateReservation(memberId, bookId);
                if (!success)
                {
                    MessageBox.Show("Failed to create reservation. The book may already be reserved or unavailable.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the newly created reservation to update with custom dates and priority
                var reservations = _reservationService.GetMemberReservations(memberId);
                var newReservation = reservations
                    .Where(r => r.BookID == bookId && r.Status == "Pending")
                    .OrderByDescending(r => r.ReservationID)
                    .FirstOrDefault();

                if (newReservation == null)
                {
                    MessageBox.Show("Reservation was created but could not be updated with custom dates.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }

                // Update reservation with custom dates and priority
                try
                {
                    using (var conn = _dbContext.GetConnection())
                    {
                        conn.Open();
                        
                        // Get priority from dropdown or use calculated priority
                        int priority = newReservation.Priority; // Use the auto-calculated priority as default
                        if (cmbPrioNo.SelectedItem != null && int.TryParse(cmbPrioNo.SelectedItem.ToString(), out int selectedPriority))
                        {
                            priority = selectedPriority;
                        }

                        string updateQuery = @"UPDATE Reservations
                                              SET ReservationDate = @ReservationDate,
                                                  ExpiryDate = @ExpiryDate,
                                                  Priority = @Priority
                                              WHERE ReservationID = @ReservationID";
                        using (var cmd = new MySqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@ReservationID", newReservation.ReservationID);
                            cmd.Parameters.AddWithValue("@ReservationDate", reservationDate);
                            cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
                            cmd.Parameters.AddWithValue("@Priority", priority);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Reservation created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception updateEx)
                {
                    MessageBox.Show($"Reservation created but failed to update with custom dates: {updateEx.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing reservation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
