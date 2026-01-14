using System;
using System.Collections.Generic;
using System.Linq;
using Project5LMS.Models;
using Project5LMS.Repositories;
using Project5LMS.Data;
using Project5LMS.Interfaces;
using Project5LMS.Strategies;
using Project5LMS.Helpers;
namespace Project5LMS.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly DatabaseContext _dbContext;
        private readonly IFineCalculationStrategy _fineStrategy;
        public DashboardService(
            IBookRepository bookRepository,
            IMemberRepository memberRepository,
            ITransactionRepository transactionRepository,
            DatabaseContext dbContext,
            IFineCalculationStrategy fineStrategy = null)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _fineStrategy = fineStrategy ?? new StandardFineStrategy();
        }
        public int GetTotalBooks()
        {
            return _bookRepository.GetAll().Count();
        }
        public int GetBooksAddedThisMonth()
        {
            try
            {
                var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var query = "SELECT COUNT(*) FROM Books WHERE CreatedDate >= @startDate";
                var result = _dbContext.ExecuteQuery(query.Replace("@startDate", $"'{startOfMonth:yyyy-MM-dd}'"));
                if (result.Rows.Count > 0)
                {
                    return Convert.ToInt32(result.Rows[0][0]);
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }
        public int GetActiveMembers()
        {
            return _memberRepository.GetAll().Count(m => m.IsActive);
        }
        public int GetActiveBorrowings()
        {
            return _transactionRepository.GetByStatus("Borrowed").Count();
        }
        public int GetOverdueBooks()
        {
            return _transactionRepository.GetOverdue().Count();
        }
        public decimal GetPendingFines()
        {
            var overdue = _transactionRepository.GetOverdue();
            decimal total = 0;
            foreach (var transaction in overdue)
            {
                if (transaction.Fine.HasValue)
                {
                    total += transaction.Fine.Value;
                }
                else
                {
                    // Calculate fine for overdue transactions that don't have fines set yet
                    if (DateTime.Now > transaction.DueDate)
                    {
                        TimeSpan overdueTime = DateTime.Now - transaction.DueDate;
                        int daysOverdue = overdueTime.Days;
                        if (daysOverdue == 0 && overdueTime.TotalHours > 0)
                        {
                            daysOverdue = 1;
                        }
                        if (daysOverdue > 0)
                        {
                            // Use fine strategy to calculate fine
                            decimal fine = _fineStrategy.CalculateFine(daysOverdue);
                            total += fine;
                        }
                    }
                }
            }
            return total;
        }
        public int GetMembersAddedThisWeek()
        {
            // Calculate start of week (Monday)
            DateTime now = DateTime.Now;
            int daysUntilMonday = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            var startOfWeek = now.Date.AddDays(-daysUntilMonday);
            var endOfWeek = startOfWeek.AddDays(7);
            
            return _memberRepository.GetAll()
                .Count(m => m.RegistrationDate >= startOfWeek && m.RegistrationDate < endOfWeek);
        }
        public int GetBorrowedToday()
        {
            var today = DateTime.Today;
            return _transactionRepository.GetByStatus("Borrowed")
                .Count(t => t.BorrowDate.Date == today);
        }
        public int GetReturnedToday()
        {
            var today = DateTime.Today;
            return _transactionRepository.GetByStatus("Returned")
                .Count(t => t.ReturnDate.HasValue && t.ReturnDate.Value.Date == today);
        }
        public decimal GetFinesCollectedToday()
        {
            var today = DateTime.Today;
            return _transactionRepository.GetByStatus("Returned")
                .Where(t => t.ReturnDate.HasValue && t.ReturnDate.Value.Date == today && t.Fine.HasValue)
                .Sum(t => t.Fine.Value);
        }
        public int GetTotalReservations()
        {
            try
            {
                var query = "SELECT COUNT(*) FROM Reservations WHERE Status IN ('Active', 'Ready')";
                var result = _dbContext.ExecuteQuery(query);
                if (result.Rows.Count > 0)
                {
                    return Convert.ToInt32(result.Rows[0][0]);
                }
            }
            catch { }
            return 0;
        }
        public int GetPendingReservations()
        {
            try
            {
                var query = "SELECT COUNT(*) FROM Reservations WHERE Status = 'Active'";
                var result = _dbContext.ExecuteQuery(query);
                if (result.Rows.Count > 0)
                {
                    return Convert.ToInt32(result.Rows[0][0]);
                }
            }
            catch { }
            return 0;
        }
        public List<DashboardActivity> GetRecentActivities(int limit = Constants.DefaultQueryLimit)
        {
            var activities = new List<DashboardActivity>();
            try
            {
                var recentBorrows = _transactionRepository.GetByStatus("Borrowed")
                    .OrderByDescending(t => t.BorrowDate)
                    .Take(limit / 4);
                foreach (var transaction in recentBorrows)
                {
                    var member = _memberRepository.GetById(transaction.MemberID);
                    var book = _bookRepository.GetById(transaction.BookID);
                    if (member != null && book != null)
                    {
                        activities.Add(new DashboardActivity
                        {
                            Type = "Book Borrowed",
                            Details = $"{member.FullName} - {book.Title}",
                            Timestamp = transaction.BorrowDate
                        });
                    }
                }
                var recentReturns = _transactionRepository.GetByStatus("Returned")
                    .Where(t => t.ReturnDate.HasValue)
                    .OrderByDescending(t => t.ReturnDate)
                    .Take(limit / 4);
                foreach (var transaction in recentReturns)
                {
                    var member = _memberRepository.GetById(transaction.MemberID);
                    var book = _bookRepository.GetById(transaction.BookID);
                    if (member != null && book != null)
                    {
                        activities.Add(new DashboardActivity
                        {
                            Type = "Book Returned",
                            Details = $"{member.FullName} - {book.Title}",
                            Timestamp = transaction.ReturnDate.Value
                        });
                    }
                }
                var recentMembers = _memberRepository.GetAll()
                    .OrderByDescending(m => m.RegistrationDate)
                    .Take(limit / 4);
                foreach (var member in recentMembers)
                {
                    activities.Add(new DashboardActivity
                    {
                        Type = "New Member",
                        Details = $"{member.FullName} - Member Registration",
                        Timestamp = member.RegistrationDate
                    });
                }
                var recentFines = _transactionRepository.GetByStatus("Returned")
                    .Where(t => t.ReturnDate.HasValue && t.Fine.HasValue && t.Fine.Value > 0)
                    .OrderByDescending(t => t.ReturnDate)
                    .Take(limit / 4);
                foreach (var transaction in recentFines)
                {
                    var member = _memberRepository.GetById(transaction.MemberID);
                    if (member != null)
                    {
                        activities.Add(new DashboardActivity
                        {
                            Type = "Fine Paid",
                            Details = $"{member.FullName} - {Project5LMS.Helpers.IDFormatter.FormatCurrency(transaction.Fine.Value)}",
                            Timestamp = transaction.ReturnDate.Value
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting recent activities: {ex.Message}");
            }
            return activities.OrderByDescending(a => a.Timestamp).Take(limit).ToList();
        }
        public Dictionary<string, int> GetWeeklyBorrowData()
        {
            var data = new Dictionary<string, int>();
            var days = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            foreach (var day in days)
            {
                data[day] = 0;
            }
            try
            {
                // Calculate start of week (Monday)
                DateTime now = DateTime.Now;
                int daysUntilMonday = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
                var startOfWeek = now.Date.AddDays(-daysUntilMonday);
                var endOfWeek = startOfWeek.AddDays(7);
                
                var transactions = _transactionRepository.GetByStatus("Borrowed")
                    .Where(t => t.BorrowDate >= startOfWeek && t.BorrowDate < endOfWeek);
                foreach (var transaction in transactions)
                {
                    string dayName = transaction.BorrowDate.DayOfWeek.ToString().Substring(0, 3);
                    if (data.ContainsKey(dayName))
                    {
                        data[dayName]++;
                    }
                }
            }
            catch { }
            return data;
        }
        public Dictionary<string, int> GetWeeklyReturnData()
        {
            var data = new Dictionary<string, int>();
            var days = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            foreach (var day in days)
            {
                data[day] = 0;
            }
            try
            {
                // Calculate start of week (Monday)
                DateTime now = DateTime.Now;
                int daysUntilMonday = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
                var startOfWeek = now.Date.AddDays(-daysUntilMonday);
                var endOfWeek = startOfWeek.AddDays(7);
                
                var transactions = _transactionRepository.GetByStatus("Returned")
                    .Where(t => t.ReturnDate.HasValue &&
                               t.ReturnDate.Value >= startOfWeek &&
                               t.ReturnDate.Value < endOfWeek);
                foreach (var transaction in transactions)
                {
                    string dayName = transaction.ReturnDate.Value.DayOfWeek.ToString().Substring(0, 3);
                    if (data.ContainsKey(dayName))
                    {
                        data[dayName]++;
                    }
                }
            }
            catch { }
            return data;
        }
        public Dictionary<string, int> GetCategoryDistribution()
        {
            var distribution = new Dictionary<string, int>();
            try
            {
                var books = _bookRepository.GetAll();
                foreach (var book in books)
                {
                    string category = string.IsNullOrWhiteSpace(book.Category) ? "Uncategorized" : book.Category;
                    if (distribution.ContainsKey(category))
                    {
                        distribution[category]++;
                    }
                    else
                    {
                        distribution[category] = 1;
                    }
                }
            }
            catch { }
            return distribution.OrderByDescending(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
        public Dictionary<string, int> GetMonthlyBorrowData(int months = 6)
        {
            var data = new Dictionary<string, int>();
            var monthNames = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            try
            {
                var startDate = DateTime.Now.AddMonths(-months);
                var transactions = _transactionRepository.GetByStatus("Borrowed")
                    .Where(t => t.BorrowDate >= startDate && t.BorrowDate <= DateTime.Now);
                foreach (var transaction in transactions)
                {
                    string monthKey = $"{monthNames[transaction.BorrowDate.Month - 1]} {transaction.BorrowDate.Year}";
                    if (data.ContainsKey(monthKey))
                    {
                        data[monthKey]++;
                    }
                    else
                    {
                        data[monthKey] = 1;
                    }
                }
            }
            catch { }
            return data;
        }
        public Dictionary<string, int> GetMonthlyReturnData(int months = 6)
        {
            var data = new Dictionary<string, int>();
            var monthNames = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            try
            {
                var startDate = DateTime.Now.AddMonths(-months);
                var transactions = _transactionRepository.GetByStatus("Returned")
                    .Where(t => t.ReturnDate.HasValue &&
                               t.ReturnDate.Value >= startDate &&
                               t.ReturnDate.Value <= DateTime.Now);
                foreach (var transaction in transactions)
                {
                    string monthKey = $"{monthNames[transaction.ReturnDate.Value.Month - 1]} {transaction.ReturnDate.Value.Year}";
                    if (data.ContainsKey(monthKey))
                    {
                        data[monthKey]++;
                    }
                    else
                    {
                        data[monthKey] = 1;
                    }
                }
            }
            catch { }
            return data;
        }
    }
}