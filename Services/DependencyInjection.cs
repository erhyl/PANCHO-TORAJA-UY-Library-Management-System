using System;
using Project5LMS.Data;
using Project5LMS.Repositories;
using Project5LMS.Strategies;
using Project5LMS.Interfaces;

namespace Project5LMS.Services
{
    /// <summary>
    /// Dependency Injection container configuration
    /// Uses a simple custom container that doesn't require external packages
    /// </summary>
    public static class DependencyInjection
    {
        private static SimpleServiceContainer _container;
        private static readonly object _lock = new object();

        /// <summary>
        /// Configure and build the service container
        /// </summary>
        public static SimpleServiceContainer ConfigureServices()
        {
            if (_container == null)
            {
                lock (_lock)
                {
                    if (_container == null)
                    {
                        _container = new SimpleServiceContainer();

                        // Register DatabaseContext as Transient (new instance each time)
                        // This prevents connection pooling issues and ensures fresh connections
                        _container.RegisterTransient<DatabaseContext>(() => new DatabaseContext());

                        // Register Repositories as Transient (new instance each time for desktop app)
                        _container.RegisterTransient<IBookRepository, BookRepository>();
                        _container.RegisterTransient<IBookCopyRepository, BookCopyRepository>();
                        _container.RegisterTransient<IMemberRepository, MemberRepository>();
                        _container.RegisterTransient<ITransactionRepository, TransactionRepository>();

                        // Register Strategies as Transient (new instance each time)
                        _container.RegisterTransient<IFineCalculationStrategy, StandardFineStrategy>();

                        // Register Services as Transient (new instance each time for desktop app)
                        // This ensures proper dependency resolution and avoids shared state issues
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

        /// <summary>
        /// Get the service container instance
        /// </summary>
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

        /// <summary>
        /// Get a service from the container
        /// </summary>
        public static T GetService<T>() where T : class
        {
            return Container.GetService<T>();
        }

        /// <summary>
        /// Get a required service from the container (throws if not found)
        /// </summary>
        public static T GetRequiredService<T>() where T : class
        {
            return Container.GetRequiredService<T>();
        }
    }
}

