using System.Collections.Generic;
using Project5LMS.Services;

namespace Project5LMS.Interfaces
{
    public interface ISearchService
    {
        SearchResults SearchBooks(string searchTerm);
        SearchResults SearchMembers(string searchTerm);
        SearchResults SearchAll(string searchTerm);
        List<string> GetBookCategories();
    }
}
