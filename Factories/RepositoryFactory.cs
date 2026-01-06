using Project5LMS.Data;
using Project5LMS.Repositories;

namespace Project5LMS.Factories
{

    public static class RepositoryFactory
    {
        private static DatabaseContext _dbContext;
        private static readonly object _lock = new object();

        private static DatabaseContext GetDbContext()
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

        public static IBookRepository CreateBookRepository()
        {
            return new BookRepository(GetDbContext());
        }

        public static IBookRepository CreateBookRepository(DatabaseContext dbContext)
        {
            return new BookRepository(dbContext);
        }

    }
}
