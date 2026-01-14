using System.Collections.Generic;
using Project5LMS.Models;
using Project5LMS.Helpers;
namespace Project5LMS.Interfaces
{
    public interface IDashboardService
    {
        int GetTotalBooks();
        int GetBooksAddedThisMonth();
        int GetActiveMembers();
        int GetActiveBorrowings();
        int GetOverdueBooks();
        decimal GetPendingFines();
        int GetMembersAddedThisWeek();
        int GetBorrowedToday();
        int GetReturnedToday();
        decimal GetFinesCollectedToday();
        int GetTotalReservations();
        int GetPendingReservations();
        List<DashboardActivity> GetRecentActivities(int limit = Constants.DefaultQueryLimit);
        Dictionary<string, int> GetWeeklyBorrowData();
        Dictionary<string, int> GetWeeklyReturnData();
        Dictionary<string, int> GetCategoryDistribution();
        Dictionary<string, int> GetMonthlyBorrowData(int months = 6);
        Dictionary<string, int> GetMonthlyReturnData(int months = 6);
    }
}