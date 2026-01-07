using System;
using System.Collections.Generic;
using System.Linq;
using Project5LMS.Models;
using Project5LMS.Repositories;
using Project5LMS.Data;

namespace Project5LMS.Services
{
    public class DashboardService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly DatabaseContext _dbContext;

        public DashboardService(
            IBookRepository bookRepository,
            IMemberRepository memberRepository,
            ITransactionRepository transactionRepository,
            DatabaseContext dbContext)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public DashboardService(DatabaseContext dbContext)
            : this(
                new BookRepository(dbContext),
                new MemberRepository(dbContext),
                new TransactionRepository(dbContext),
                dbContext)
        {
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
            }
            return total;
        }

        public int GetMembersAddedThisWeek()
        {
            var startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
            return _memberRepository.GetAll()
                .Count(m => m.RegistrationDate >= startOfWeek && m.RegistrationDate <= DateTime.Now);
        }

        public int GetBorrowedToday()
        {
            var today = DateTime.Today;
            return _transactionRepository.GetByStatus("Borrowed")
                .Count(t => t.BorrowDate.Date == today);
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

        public List<DashboardActivity> GetRecentActivities(int limit = 20)
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
                            Details = $"{member.FullName} - ${transaction.Fine.Value:F2}",
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
                var startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
                var transactions = _transactionRepository.GetByStatus("Borrowed")
                    .Where(t => t.BorrowDate >= startOfWeek && t.BorrowDate <= DateTime.Now);

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
                var startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
                var transactions = _transactionRepository.GetByStatus("Returned")
                    .Where(t => t.ReturnDate.HasValue && 
                               t.ReturnDate.Value >= startOfWeek && 
                               t.ReturnDate.Value <= DateTime.Now);

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
    }

    public class DashboardActivity
    {
        public string Type { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

