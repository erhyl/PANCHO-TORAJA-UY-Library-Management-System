using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Project5LMS.Models;
using Project5LMS.Repositories;
using Project5LMS.Data;
using Project5LMS.Helpers;

namespace Project5LMS.Services
{
    public class SearchService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly DatabaseContext _dbContext;

        public SearchService(
            IBookRepository bookRepository,
            IMemberRepository memberRepository,
            DatabaseContext dbContext)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public SearchService(DatabaseContext dbContext)
            : this(
                new BookRepository(dbContext),
                new MemberRepository(dbContext),
                dbContext)
        {
        }

        public SearchResults SearchBooks(string searchTerm)
        {
            var stopwatch = Stopwatch.StartNew();
            var results = _bookRepository.Search(searchTerm).Take(100).ToList();
            stopwatch.Stop();

            AuditLogger.Log("Search", $"Books search: '{searchTerm}' - {results.Count} results in {stopwatch.ElapsedMilliseconds}ms", "Success");

            return new SearchResults
            {
                Books = results,
                SearchTime = stopwatch.ElapsedMilliseconds,
                TotalResults = results.Count
            };
        }

        public SearchResults SearchMembers(string searchTerm)
        {
            var stopwatch = Stopwatch.StartNew();
            var results = _memberRepository.Search(searchTerm).Take(100).ToList();
            stopwatch.Stop();

            AuditLogger.Log("Search", $"Members search: '{searchTerm}' - {results.Count} results in {stopwatch.ElapsedMilliseconds}ms", "Success");

            return new SearchResults
            {
                Members = results,
                SearchTime = stopwatch.ElapsedMilliseconds,
                TotalResults = results.Count
            };
        }

        public SearchResults SearchAll(string searchTerm)
        {
            var stopwatch = Stopwatch.StartNew();
            var books = _bookRepository.Search(searchTerm).Take(50).ToList();
            var members = _memberRepository.Search(searchTerm).Take(50).ToList();
            stopwatch.Stop();

            AuditLogger.Log("Search", $"All search: '{searchTerm}' - {books.Count} books, {members.Count} members in {stopwatch.ElapsedMilliseconds}ms", "Success");

            return new SearchResults
            {
                Books = books,
                Members = members,
                SearchTime = stopwatch.ElapsedMilliseconds,
                TotalResults = books.Count + members.Count
            };
        }

        public List<string> GetBookCategories()
        {
            try
            {
                return _bookRepository.GetAll()
                    .Where(b => !string.IsNullOrWhiteSpace(b.Category))
                    .Select(b => b.Category)
                    .Distinct()
                    .OrderBy(c => c)
                    .ToList();
            }
            catch
            {
                return new List<string>();
            }
        }
    }

    public class SearchResults
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Member> Members { get; set; } = new List<Member>();
        public long SearchTime { get; set; }
        public int TotalResults { get; set; }
    }
}
