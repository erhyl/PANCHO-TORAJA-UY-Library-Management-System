using Project5LMS.Data;
using Project5LMS.Repositories;
using Project5LMS.Strategies;
using Project5LMS.Interfaces;

namespace Project5LMS.Services
{
    public static class ServiceFactory
    {
        private static DatabaseContext _dbContext;
        private static readonly object _lock = new object();

        public static DatabaseContext GetDbContext()
        {
            if (_dbContext == null)
            {
                lock (_lock)
                {
                    if (_dbContext == null)
                    {
                        _dbContext = new DatabaseContext();
                    }
                }
            }
            return _dbContext;
        }

        public static IBookService CreateBookService()
        {
            return new BookService(new BookRepository(GetDbContext()));
        }

        public static IBookService CreateBookService(DatabaseContext dbContext)
        {
            return new BookService(new BookRepository(dbContext));
        }

        public static IFinesService CreateFinesService()
        {
            return new FinesService(new TransactionRepository(GetDbContext()), new StandardFineStrategy());
        }

        public static IFinesService CreateFinesService(IFineCalculationStrategy strategy)
        {
            return new FinesService(new TransactionRepository(GetDbContext()), strategy);
        }

        public static ICirculationService CreateCirculationService()
        {
            var dbContext = GetDbContext();
            return new CirculationService(
                new TransactionRepository(dbContext),
                new BookRepository(dbContext),
                new MemberRepository(dbContext));
        }

        public static IMembersService CreateMembersService()
        {
            return new MembersService(new MemberRepository(GetDbContext()));
        }

        public static IDashboardService CreateDashboardService()
        {
            var dbContext = GetDbContext();
            return new DashboardService(dbContext);
        }

        public static ISearchService CreateSearchService()
        {
            var dbContext = GetDbContext();
            return new SearchService(dbContext);
        }

        public static ISettingsService CreateSettingsService()
        {
            return new SettingsService(GetDbContext());
        }

        public static IUserService CreateUserService()
        {
            return new UserService(GetDbContext());
        }

        public static IUserService CreateUserService(DatabaseContext dbContext)
        {
            return new UserService(dbContext);
        }
    }
}

