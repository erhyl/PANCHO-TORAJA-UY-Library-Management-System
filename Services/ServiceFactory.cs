using Project5LMS.Data;
using Project5LMS.Interfaces;
namespace Project5LMS.Services
{
    public static class ServiceFactory
    {
        public static DatabaseContext GetDbContext()
        {
            return DependencyInjection.GetRequiredService<DatabaseContext>();
        }
        public static IBookService CreateBookService()
        {
            return DependencyInjection.GetRequiredService<IBookService>();
        }
        public static IFinesService CreateFinesService()
        {
            return DependencyInjection.GetRequiredService<IFinesService>();
        }
        public static ICirculationService CreateCirculationService()
        {
            return DependencyInjection.GetRequiredService<ICirculationService>();
        }
        public static IMembersService CreateMembersService()
        {
            return DependencyInjection.GetRequiredService<IMembersService>();
        }
        public static IDashboardService CreateDashboardService()
        {
            return DependencyInjection.GetRequiredService<IDashboardService>();
        }
        public static ISearchService CreateSearchService()
        {
            return DependencyInjection.GetRequiredService<ISearchService>();
        }
        public static ISettingsService CreateSettingsService()
        {
            return DependencyInjection.GetRequiredService<ISettingsService>();
        }
        public static IUserService CreateUserService()
        {
            return DependencyInjection.GetRequiredService<IUserService>();
        }
        public static IAuthenticationService CreateAuthenticationService()
        {
            return DependencyInjection.GetRequiredService<IAuthenticationService>();
        }
        public static IPaymentService CreatePaymentService()
        {
            return DependencyInjection.GetRequiredService<IPaymentService>();
        }
        public static IReservationService CreateReservationService()
        {
            return DependencyInjection.GetRequiredService<IReservationService>();
        }
    }
}