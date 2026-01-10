using System.Collections.Generic;
using Project5LMS.Models;

namespace Project5LMS.Repositories
{
    public interface IBookCopyRepository
    {
        BookCopy GetById(int copyId);
        BookCopy GetByAccessionNumber(string accessionNumber);
        IEnumerable<BookCopy> GetByBookId(int bookId);
        IEnumerable<BookCopy> GetByStatus(string status);
        bool Add(BookCopy copy);
        bool Update(BookCopy copy);
        bool Delete(int copyId);
        bool UpdateStatus(int copyId, string status);
    }
}

