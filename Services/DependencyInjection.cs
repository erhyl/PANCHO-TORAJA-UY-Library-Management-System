using System;
using Project5LMS.Data;
using Project5LMS.Repositories;
using Project5LMS.Strategies;
using Project5LMS.Interfaces;
namespace Project5LMS.Services
{
    public static class DependencyInjection
    {
        private static SimpleServiceContainer _container;
        private static readonly object _lock = new object();
        public static SimpleServiceContainer ConfigureServices()
        {
            if (_container == null)
            {
                lock (_lock)
                {
                    if (_container == null)
                    {
                        _container = new SimpleServiceContainer();
                        _container.RegisterTransient<DatabaseContext>(() => new DatabaseContext());
                        _container.RegisterTransient<IBookRepository, BookRepository>();
                        _container.RegisterTransient<IBookCopyRepository, BookCopyRepository>();
                        _container.RegisterTransient<IMemberRepository, MemberRepository>();
                        _container.RegisterTransient<ITransactionRepository, TransactionRepository>();
                        _container.RegisterTransient<IFineCalculationStrategy, StandardFineStrategy>();
                        _container.RegisterTransient<IBookService, BookService>();
                        _container.RegisterTransient<IFinesService, FinesService>();
                        _container.RegisterTransient<ICirculationService, CirculationService>();
                        _container.RegisterTransient<IMembersService, MembersService>();
                        _container.RegisterTransient<IDashboardService, DashboardService>();
                        _container.RegisterTransient<ISearchService, SearchService>();
                        _container.RegisterTransient<ISettingsService, SettingsService>();
                        _container.RegisterTransient<IUserService, UserService>();
                        _container.RegisterTransient<IAuthenticationService, AuthenticationService>();
                        _container.RegisterTransient<IPaymentService, PaymentService>();
                        _container.RegisterTransient<IReservationService, ReservationService>();
                    }
                }
            }
            return _container;
        }
        public static SimpleServiceContainer Container
        {
            get
            {
                if (_container == null)
                {
                    ConfigureServices();
                }
                return _container;
            }
        }
        public static T GetService<T>() where T : class
        {
            return Container.GetService<T>();
        }
        public static T GetRequiredService<T>() where T : class
        {
            return Container.GetRequiredService<T>();
        }
    }
}