using System;
using System.Collections.Generic;
using System.Linq;
using Project5LMS.Models;
using Project5LMS.Repositories;
using Project5LMS.Interfaces;
using Project5LMS.Helpers;
namespace Project5LMS.Services
{
    /// <summary>
    /// Validates member eligibility for borrowing books
    /// Centralizes all borrowing validation logic to reduce duplication
    /// </summary>
    public class BorrowingValidator
    {
        private readonly IMembersService _membersService;
        private readonly IBookService _bookService;
        private readonly IFinesService _finesService;
        private readonly ICirculationService _circulationService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMemberRepository _memberRepository;
        
        public BorrowingValidator(
            IMembersService membersService,
            IBookService bookService,
            IFinesService finesService,
            ICirculationService circulationService,
            ITransactionRepository transactionRepository,
            IMemberRepository memberRepository)
        {
            _membersService = membersService ?? throw new ArgumentNullException(nameof(membersService));
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _finesService = finesService ?? throw new ArgumentNullException(nameof(finesService));
            _circulationService = circulationService ?? throw new ArgumentNullException(nameof(circulationService));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
        }
        
        /// <summary>
        /// Validates if a member can borrow a specific book
        /// </summary>
        public BorrowingValidationResult ValidateBorrowing(int memberId, int bookId)
        {
            var result = new BorrowingValidationResult();
            
            // 1. Validate member exists
            var member = _membersService.GetMember(memberId);
            if (member == null)
            {
                result.AddError("Member not found.");
                return result;
            }
            
            // 2. Validate book exists
            var book = _bookService.GetBook(bookId);
            if (book == null)
            {
                result.AddError("Book not found.");
                return result;
            }
            
            // 3. Check book availability
            if (!_bookService.IsBookAvailable(bookId))
            {
                result.AddError("Book is not available for borrowing.");
                return result;
            }
            
            // 4. Check member status
            if (!member.IsActive || member.IsExpired)
            {
                result.AddError("Member account is not active or has expired.");
                return result;
            }
            
            // 5. Check for overdue books
            var overdueTransactions = _transactionRepository.GetOverdue()
                .Where(t => t.MemberID == memberId)
                .ToList();
            if (overdueTransactions.Count > 0)
            {
                result.AddError($"Member has {overdueTransactions.Count} overdue book(s). Please return overdue books before borrowing new ones.");
                return result;
            }
            
            // 6. Check for unpaid fines
            decimal totalFines = _finesService.GetTotalFinesForMember(memberId);
            if (totalFines > Constants.MaxAllowedFineThreshold)
            {
                result.AddError($"Member has unpaid fines of {IDFormatter.FormatCurrency(totalFines)}. Please pay fines before borrowing new books.");
                return result;
            }
            
            // 7. Check borrowing limit
            if (!_circulationService.CanBorrow(memberId))
            {
                var privileges = MemberTypePrivileges.GetDefaultPrivileges(member.Type);
                int activeBorrowings = _memberRepository.GetActiveBorrowingCount(memberId);
                result.AddError($"Member has reached borrowing limit ({activeBorrowings}/{privileges.MaxBooksAllowed} books).");
                return result;
            }
            
            // 8. Check if book is reference-only
            if (book.BookType?.Equals("Reference", StringComparison.OrdinalIgnoreCase) == true)
            {
                var privileges = MemberTypePrivileges.GetDefaultPrivileges(member.Type);
                if (!privileges.CanBorrowReference)
                {
                    result.AddError("This is a reference book and cannot be borrowed by this member type.");
                    return result;
                }
            }
            
            // All validations passed
            result.IsValid = true;
            result.Member = member;
            result.Book = book;
            result.BorrowingPeriodDays = MemberTypePrivileges.GetDefaultPrivileges(member.Type).BorrowingPeriodDays;
            
            return result;
        }
        
        /// <summary>
        /// Gets member eligibility information for display
        /// </summary>
        public MemberEligibilityInfo GetMemberEligibility(int memberId)
        {
            var member = _membersService.GetMember(memberId);
            if (member == null)
                return null;
            
            var privileges = MemberTypePrivileges.GetDefaultPrivileges(member.Type);
            int activeBorrowings = _memberRepository.GetActiveBorrowingCount(memberId);
            var overdueTransactions = _transactionRepository.GetOverdue()
                .Where(t => t.MemberID == memberId)
                .ToList();
            decimal totalFines = _finesService.GetTotalFinesForMember(memberId);
            
            return new MemberEligibilityInfo
            {
                Member = member,
                IsActive = member.IsActive && !member.IsExpired,
                ActiveBorrowings = activeBorrowings,
                MaxBooksAllowed = privileges.MaxBooksAllowed,
                WithinLimit = activeBorrowings < privileges.MaxBooksAllowed,
                OverdueCount = overdueTransactions.Count,
                NoOverdue = overdueTransactions.Count == 0,
                TotalFines = totalFines,
                FinesPaid = totalFines == 0 || totalFines <= Constants.MaxAllowedFineThreshold,
                IsEligible = member.IsActive && !member.IsExpired && 
                            activeBorrowings < privileges.MaxBooksAllowed && 
                            overdueTransactions.Count == 0 && 
                            (totalFines == 0 || totalFines <= Constants.MaxAllowedFineThreshold)
            };
        }
    }
    
    /// <summary>
    /// Result of borrowing validation
    /// </summary>
    public class BorrowingValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; private set; }
        public Member Member { get; set; }
        public Book Book { get; set; }
        public int BorrowingPeriodDays { get; set; }
        
        public BorrowingValidationResult()
        {
            Errors = new List<string>();
            IsValid = false;
        }
        
        public void AddError(string error)
        {
            Errors.Add(error);
            IsValid = false;
        }
        
        public string GetErrorMessage()
        {
            return string.Join("\n", Errors);
        }
    }
    
    /// <summary>
    /// Member eligibility information for display
    /// </summary>
    public class MemberEligibilityInfo
    {
        public Member Member { get; set; }
        public bool IsActive { get; set; }
        public int ActiveBorrowings { get; set; }
        public int MaxBooksAllowed { get; set; }
        public bool WithinLimit { get; set; }
        public int OverdueCount { get; set; }
        public bool NoOverdue { get; set; }
        public decimal TotalFines { get; set; }
        public bool FinesPaid { get; set; }
        public bool IsEligible { get; set; }
    }
}
