using System.Collections.Generic;
using Project5LMS.Models;
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
        decimal GetFinesCollectedToday();
        int GetTotalReservations();
        int GetPendingReservations();
        List<DashboardActivity> GetRecentActivities(int limit = 20);
        Dictionary<string, int> GetWeeklyBorrowData();
        Dictionary<string, int> GetWeeklyReturnData();
        Dictionary<string, int> GetCategoryDistribution();
        Dictionary<string, int> GetMonthlyBorrowData(int months = 6);
        Dictionary<string, int> GetMonthlyReturnData(int months = 6);
    }
}