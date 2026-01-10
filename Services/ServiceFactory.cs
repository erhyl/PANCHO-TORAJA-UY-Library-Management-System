using Project5LMS.Data;
using Project5LMS.Interfaces;

namespace Project5LMS.Services
{
    /// <summary>
    /// Service Factory that uses Dependency Injection container
    /// This maintains backward compatibility while following DIP
    /// </summary>
    public static class ServiceFactory
    {
        /// <summary>
        /// Get DatabaseContext from DI container
        /// </summary>
        public static DatabaseContext GetDbContext()
        {
            return DependencyInjection.GetRequiredService<DatabaseContext>();
        }

        /// <summary>
        /// Get IBookService from DI container
        /// </summary>
        public static IBookService CreateBookService()
        {
            return DependencyInjection.GetRequiredService<IBookService>();
        }

        /// <summary>
        /// Get IFinesService from DI container
        /// </summary>
        public static IFinesService CreateFinesService()
        {
            return DependencyInjection.GetRequiredService<IFinesService>();
        }

        /// <summary>
        /// Get ICirculationService from DI container
        /// </summary>
        public static ICirculationService CreateCirculationService()
        {
            return DependencyInjection.GetRequiredService<ICirculationService>();
        }

        /// <summary>
        /// Get IMembersService from DI container
        /// </summary>
        public static IMembersService CreateMembersService()
        {
            return DependencyInjection.GetRequiredService<IMembersService>();
        }

        /// <summary>
        /// Get IDashboardService from DI container
        /// </summary>
        public static IDashboardService CreateDashboardService()
        {
            return DependencyInjection.GetRequiredService<IDashboardService>();
        }

        /// <summary>
        /// Get ISearchService from DI container
        /// </summary>
        public static ISearchService CreateSearchService()
        {
            return DependencyInjection.GetRequiredService<ISearchService>();
        }

        /// <summary>
        /// Get ISettingsService from DI container
        /// </summary>
        public static ISettingsService CreateSettingsService()
        {
            return DependencyInjection.GetRequiredService<ISettingsService>();
        }

        /// <summary>
        /// Get IUserService from DI container
        /// </summary>
        public static IUserService CreateUserService()
        {
            return DependencyInjection.GetRequiredService<IUserService>();
        }

        /// <summary>
        /// Get IAuthenticationService from DI container
        /// </summary>
        public static IAuthenticationService CreateAuthenticationService()
        {
            return DependencyInjection.GetRequiredService<IAuthenticationService>();
        }

        /// <summary>
        /// Get IPaymentService from DI container
        /// </summary>
        public static IPaymentService CreatePaymentService()
        {
            return DependencyInjection.GetRequiredService<IPaymentService>();
        }

        /// <summary>
        /// Get IReservationService from DI container
        /// </summary>
        public static IReservationService CreateReservationService()
        {
            return DependencyInjection.GetRequiredService<IReservationService>();
        }
    }
}

