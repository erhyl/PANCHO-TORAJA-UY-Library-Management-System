using Project5LMS.Data;
using Project5LMS.Repositories;
using Project5LMS.Strategies;

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

        public static BookService CreateBookService()
        {
            return new BookService(new BookRepository(GetDbContext()));
        }

        public static BookService CreateBookService(DatabaseContext dbContext)
        {
            return new BookService(new BookRepository(dbContext));
        }

        public static FinesService CreateFinesService()
        {
            return new FinesService(new TransactionRepository(GetDbContext()), new StandardFineStrategy());
        }

        public static FinesService CreateFinesService(IFineCalculationStrategy strategy)
        {
            return new FinesService(new TransactionRepository(GetDbContext()), strategy);
        }

        public static CirculationService CreateCirculationService()
        {
            var dbContext = GetDbContext();
            return new CirculationService(
                new TransactionRepository(dbContext),
                new BookRepository(dbContext),
                new MemberRepository(dbContext));
        }

        public static MembersService CreateMembersService()
        {
            return new MembersService(new MemberRepository(GetDbContext()));
        }

        public static DashboardService CreateDashboardService()
        {
            var dbContext = GetDbContext();
            return new DashboardService(dbContext);
        }

        public static SearchService CreateSearchService()
        {
            var dbContext = GetDbContext();
            return new SearchService(dbContext);
        }

        public static SettingsService CreateSettingsService()
        {
            return new SettingsService(GetDbContext());
        }
    }
}

