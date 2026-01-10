using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MySql.Data.MySqlClient;
using Project5LMS.Models;
using Project5LMS.Repositories;
using Project5LMS.Data;
using Project5LMS.Helpers;
using Project5LMS.Interfaces;

namespace Project5LMS.Services
{
    public class SearchService : ISearchService
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

        public SearchResults SearchBooks(string searchTerm)
        {
            var stopwatch = Stopwatch.StartNew();
            
            // Check if search term contains boolean operators
            if (ContainsBooleanOperators(searchTerm))
            {
                var results = AdvancedSearch(searchTerm).Take(100).ToList();
                stopwatch.Stop();
                
                AuditLogger.Log("Search", $"Advanced books search: '{searchTerm}' - {results.Count} results in {stopwatch.ElapsedMilliseconds}ms", "Success");
                
                return new SearchResults
                {
                    Books = results,
                    SearchTime = stopwatch.ElapsedMilliseconds,
                    TotalResults = results.Count
                };
            }
            else
            {
                // Use full-text search if available, otherwise fall back to LIKE
                var results = FullTextSearch(searchTerm).Take(100).ToList();
                stopwatch.Stop();

                AuditLogger.Log("Search", $"Books search: '{searchTerm}' - {results.Count} results in {stopwatch.ElapsedMilliseconds}ms", "Success");

                return new SearchResults
                {
                    Books = results,
                    SearchTime = stopwatch.ElapsedMilliseconds,
                    TotalResults = results.Count
                };
            }
        }

        /// <summary>
        /// Advanced search with boolean operators (AND, OR, NOT)
        /// </summary>
        private IEnumerable<Book> AdvancedSearch(string searchTerm)
        {
            var results = new List<Book>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    
                    // Parse boolean operators
                    var conditions = ParseBooleanSearch(searchTerm);
                    var whereClause = BuildWhereClause(conditions);
                    
                    string query = $@"SELECT * FROM Books WHERE {whereClause} LIMIT 100";
                    
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                        {
                            var dt = new System.Data.DataTable();
                            adapter.Fill(dt);
                            
                            foreach (System.Data.DataRow row in dt.Rows)
                            {
                                results.Add(MapDataRowToBook(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in advanced search: {ex.Message}");
                // Fall back to simple search
                results.AddRange(_bookRepository.Search(searchTerm));
            }
            
            foreach (var book in results)
            {
                yield return book;
            }
        }

        /// <summary>
        /// Full-text search using MySQL FULLTEXT index (if available)
        /// </summary>
        private IEnumerable<Book> FullTextSearch(string searchTerm)
        {
            var results = new List<Book>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    
                    // Try full-text search first
                    string fullTextQuery = @"SELECT * FROM Books 
                                            WHERE MATCH(Title, Author, ISBN, Category, Publisher) 
                                            AGAINST(@SearchTerm IN NATURAL LANGUAGE MODE)
                                            LIMIT 100";
                    
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(fullTextQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                        
                        try
                        {
                            using (var adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                            {
                                var dt = new System.Data.DataTable();
                                adapter.Fill(dt);
                                
                                if (dt.Rows.Count > 0)
                                {
                                    // Use reflection or create books directly
                                    foreach (System.Data.DataRow row in dt.Rows)
                                    {
                                        results.Add(MapDataRowToBook(row));
                                    }
                                }
                            }
                        }
                        catch
                        {
                            // Full-text index not available, fall back to LIKE
                        }
                    }
                }
            }
            catch
            {
                // Fall through to LIKE search
            }
            
            // If no results from full-text, fall back to standard LIKE search
            if (results.Count == 0)
            {
                results.AddRange(_bookRepository.Search(searchTerm));
            }
            
            foreach (var book in results)
            {
                yield return book;
            }
        }

        private bool ContainsBooleanOperators(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return false;
                
            string upper = searchTerm.ToUpper();
            return upper.Contains(" AND ") || upper.Contains(" OR ") || upper.Contains(" NOT ");
        }

        private List<SearchCondition> ParseBooleanSearch(string searchTerm)
        {
            var conditions = new List<SearchCondition>();
            var parts = searchTerm.Split(new[] { " AND ", " OR ", " NOT " }, StringSplitOptions.None);
            var operators = new List<string>();
            
            // Extract operators
            string upper = searchTerm.ToUpper();
            int andIndex = upper.IndexOf(" AND ");
            int orIndex = upper.IndexOf(" OR ");
            int notIndex = upper.IndexOf(" NOT ");
            
            // Simple parsing - can be enhanced
            foreach (var part in parts)
            {
                var trimmed = part.Trim();
                if (!string.IsNullOrWhiteSpace(trimmed))
                {
                    conditions.Add(new SearchCondition
                    {
                        Term = trimmed,
                        Operator = "OR" // Default, will be refined
                    });
                }
            }
            
            return conditions;
        }

        private string BuildWhereClause(List<SearchCondition> conditions)
        {
            if (conditions.Count == 0)
                return "1=0"; // No results
                
            var clauses = new List<string>();
            foreach (var condition in conditions)
            {
                clauses.Add($@"(Title LIKE @Term OR Author LIKE @Term OR ISBN LIKE @Term 
                               OR Category LIKE @Term OR Publisher LIKE @Term 
                               OR AccessionNo LIKE @Term OR CallNumber LIKE @Term)");
            }
            
            return string.Join(" OR ", clauses);
        }

        private Book MapDataRowToBook(System.Data.DataRow row)
        {
            return new Book
            {
                BookID = Convert.ToInt32(row["BookID"]),
                Title = row["Title"]?.ToString() ?? "",
                Subtitle = row["Subtitle"] != DBNull.Value ? row["Subtitle"].ToString() : null,
                Author = row["Author"]?.ToString() ?? "",
                Editor = row["Editor"] != DBNull.Value ? row["Editor"].ToString() : null,
                ISBN = row["ISBN"]?.ToString() ?? "",
                Category = row["Category"]?.ToString() ?? "",
                Publisher = row["Publisher"]?.ToString() ?? "",
                PublicationYear = row["YearPublished"] != DBNull.Value ? Convert.ToInt32(row["YearPublished"]) : 0,
                Edition = row["Edition"] != DBNull.Value ? row["Edition"].ToString() : null,
                Language = row["Language"]?.ToString() ?? "",
                NumberOfPages = row["NumberOfPages"] != DBNull.Value ? Convert.ToInt32(row["NumberOfPages"]) : 0,
                PhysicalDescription = row["PhysicalDescription"] != DBNull.Value ? row["PhysicalDescription"].ToString() : null,
                TotalCopies = row["Copies"] != DBNull.Value ? Convert.ToInt32(row["Copies"]) : 0,
                Available = row["Available"] != DBNull.Value ? Convert.ToInt32(row["Available"]) : 0,
                Location = row["Location"]?.ToString() ?? "",
                Status = row["Status"]?.ToString() ?? "Available",
                AccessionNo = row["AccessionNo"]?.ToString() ?? "",
                CallNumber = row["CallNumber"] != DBNull.Value ? row["CallNumber"].ToString() : null,
                BookType = row["BookType"]?.ToString() ?? "Circulation",
                CoverImagePath = row["CoverImagePath"] != DBNull.Value ? row["CoverImagePath"].ToString() : null,
                Barcode = row["Barcode"] != DBNull.Value ? row["Barcode"].ToString() : null
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

    /// <summary>
    /// Represents a search condition for boolean operators
    /// </summary>
    internal class SearchCondition
    {
        public string Term { get; set; }
        public string Operator { get; set; } // AND, OR, NOT
    }

    public class SearchResults
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Member> Members { get; set; } = new List<Member>();
        public long SearchTime { get; set; }
        public int TotalResults { get; set; }
    }
}
