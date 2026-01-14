using System;
using System.Collections.Generic;
using System.Data;
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
        private IEnumerable<Book> AdvancedSearch(string searchTerm)
        {
            var results = new List<Book>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    var conditions = ParseBooleanSearch(searchTerm);
                    var whereClause = BuildWhereClause(conditions);
                    string query = $@"SELECT * FROM Books WHERE {whereClause} LIMIT 100";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        // Add parameters for each condition
                        for (int i = 0; i < conditions.Count; i++)
                        {
                            string paramName = $"@Term{i}";
                            string searchPattern = $"%{conditions[i].Term}%";
                            cmd.Parameters.AddWithValue(paramName, searchPattern);
                        }
                        
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
                // Fallback to simple search
                results.AddRange(_bookRepository.Search(searchTerm));
            }
            foreach (var book in results)
            {
                yield return book;
            }
        }
        private IEnumerable<Book> FullTextSearch(string searchTerm)
        {
            var results = new List<Book>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
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
                                    foreach (System.Data.DataRow row in dt.Rows)
                                    {
                                        results.Add(MapDataRowToBook(row));
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
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
            if (string.IsNullOrWhiteSpace(searchTerm))
                return conditions;
            
            // Normalize the search term for parsing
            string normalized = " " + searchTerm.Trim() + " ";
            string upper = normalized.ToUpper();
            
            // Find all operator positions
            var operatorPositions = new List<Tuple<int, string, int>>();
            
            int pos = 0;
            while ((pos = upper.IndexOf(" AND ", pos)) != -1)
            {
                operatorPositions.Add(new Tuple<int, string, int>(pos, "AND", 5));
                pos += 5;
            }
            pos = 0;
            while ((pos = upper.IndexOf(" OR ", pos)) != -1)
            {
                operatorPositions.Add(new Tuple<int, string, int>(pos, "OR", 4));
                pos += 4;
            }
            pos = 0;
            while ((pos = upper.IndexOf(" NOT ", pos)) != -1)
            {
                operatorPositions.Add(new Tuple<int, string, int>(pos, "NOT", 5));
                pos += 5;
            }
            
            // Sort by position
            operatorPositions.Sort((a, b) => a.Item1.CompareTo(b.Item1));
            
            // If no operators found, treat entire term as single condition
            if (operatorPositions.Count == 0)
            {
                conditions.Add(new SearchCondition
                {
                    Term = searchTerm.Trim(),
                    Operator = "OR"
                });
                return conditions;
            }
            
            // Parse terms and operators
            int startPos = 0;
            string lastOperator = "OR"; // Default to OR for first term
            
            for (int i = 0; i < operatorPositions.Count; i++)
            {
                var op = operatorPositions[i];
                int termStart = startPos;
                int termEnd = op.Item1;
                
                // Extract term before operator
                string term = normalized.Substring(termStart, termEnd - termStart).Trim();
                if (!string.IsNullOrWhiteSpace(term))
                {
                    conditions.Add(new SearchCondition
                    {
                        Term = term,
                        Operator = lastOperator
                    });
                }
                
                lastOperator = op.Item2;
                startPos = op.Item1 + op.Item3;
            }
            
            // Add final term
            if (startPos < normalized.Length)
            {
                string finalTerm = normalized.Substring(startPos).Trim();
                if (!string.IsNullOrWhiteSpace(finalTerm))
                {
                    conditions.Add(new SearchCondition
                    {
                        Term = finalTerm,
                        Operator = lastOperator
                    });
                }
            }
            
            return conditions;
        }
        private string BuildWhereClause(List<SearchCondition> conditions)
        {
            if (conditions.Count == 0)
                return "1=0";
            
            var clauses = new List<string>();
            var parameters = new Dictionary<string, object>();
            
            for (int i = 0; i < conditions.Count; i++)
            {
                var condition = conditions[i];
                string paramName = $"@Term{i}";
                
                // Build field search clause for this term
                string fieldClause = $@"(Title LIKE {paramName} OR Author LIKE {paramName} OR ISBN LIKE {paramName}
                               OR Category LIKE {paramName} OR Publisher LIKE {paramName}
                               OR AccessionNo LIKE {paramName} OR CallNumber LIKE {paramName})";
                
                // Apply operator
                if (condition.Operator == "NOT")
                {
                    clauses.Add($"NOT ({fieldClause})");
                }
                else if (i == 0)
                {
                    // First condition - no operator prefix
                    clauses.Add(fieldClause);
                }
                else
                {
                    // Subsequent conditions - add operator
                    clauses.Add($"{condition.Operator} {fieldClause}");
                }
            }
            
            return string.Join(" ", clauses);
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
            
            // Comprehensive search for books - search all relevant fields
            var books = SearchBooksComprehensive(searchTerm).Take(50).ToList();
            
            // Comprehensive search for members - search all relevant fields
            var members = SearchMembersComprehensive(searchTerm).Take(50).ToList();
            
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
        private IEnumerable<Book> SearchBooksComprehensive(string searchTerm)
        {
            List<Book> books = new List<Book>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string searchPattern = $"%{searchTerm}%";
                    
                    // Check which optional columns exist
                    bool hasSubtitle = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Subtitle");
                    bool hasEditor = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Editor");
                    bool hasBarcode = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "Barcode");
                    bool hasCallNumber = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "CallNumber");
                    bool hasAccessionNo = DatabaseSchemaHelper.CheckColumnExists(conn, "Books", "AccessionNo");
                    
                    // Build WHERE clause with only existing columns
                    var conditions = new List<string>
                    {
                        "Title LIKE @SearchTerm",
                        "Author LIKE @SearchTerm",
                        "ISBN LIKE @SearchTerm",
                        "Category LIKE @SearchTerm",
                        "Publisher LIKE @SearchTerm",
                        "Language LIKE @SearchTerm",
                        "Location LIKE @SearchTerm",
                        "Status LIKE @SearchTerm"
                    };
                    
                    if (hasAccessionNo)
                        conditions.Add("AccessionNo LIKE @SearchTerm");
                    if (hasCallNumber)
                        conditions.Add("CallNumber LIKE @SearchTerm");
                    if (hasBarcode)
                        conditions.Add("Barcode LIKE @SearchTerm");
                    if (hasSubtitle)
                        conditions.Add("Subtitle LIKE @SearchTerm");
                    if (hasEditor)
                        conditions.Add("Editor LIKE @SearchTerm");
                    
                    // Try to parse as numeric ID
                    int parsedBookId = 0;
                    bool isNumeric = int.TryParse(searchTerm.Trim(), out parsedBookId);
                    
                    if (isNumeric)
                    {
                        conditions.Insert(0, "BookID = @BookID");
                    }
                    
                    string whereClause = string.Join(" OR ", conditions);
                    string query = $@"SELECT * FROM Books
                                    WHERE {whereClause}
                                    LIMIT 100";
                    
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        if (isNumeric)
                        {
                            cmd.Parameters.AddWithValue("@BookID", parsedBookId);
                        }
                        cmd.Parameters.AddWithValue("@SearchTerm", searchPattern);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                books.Add(MapDataRowToBook(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in comprehensive book search: {ex.Message}");
                // Fallback to repository search
                books.AddRange(_bookRepository.Search(searchTerm));
            }
            return books;
        }
        private IEnumerable<Member> SearchMembersComprehensive(string searchTerm)
        {
            List<Member> members = new List<Member>();
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    
                    // Check if optional columns exist
                    bool hasMemberCardNumber = DatabaseSchemaHelper.CheckColumnExists(conn, "Members", "MemberCardNumber");
                    bool hasContact = DatabaseSchemaHelper.CheckColumnExists(conn, "Members", "Contact");
                    bool hasAddress = DatabaseSchemaHelper.CheckColumnExists(conn, "Members", "Address");
                    
                    // Try to parse member ID from formatted strings like "MEM-000005"
                    int parsedMemberId = IDFormatter.ParseMemberID(searchTerm);
                    
                    string searchPattern = $"%{searchTerm}%";
                    
                    // Build comprehensive search query
                    string memberCardNumberCondition = hasMemberCardNumber 
                        ? "OR MemberCardNumber LIKE @SearchTerm " 
                        : "";
                    
                    string contactCondition = hasContact 
                        ? "OR Contact LIKE @SearchTerm " 
                        : "";
                    
                    string addressCondition = hasAddress 
                        ? "OR Address LIKE @SearchTerm " 
                        : "";
                    
                    // Search in FirstName, LastName, and concatenated FullName for better name matching
                    string query = $@"SELECT * FROM Members
                                    WHERE (FirstName LIKE @SearchTerm
                                    OR LastName LIKE @SearchTerm
                                    OR CONCAT(FirstName, ' ', LastName) LIKE @SearchTerm
                                    OR CONCAT(LastName, ' ', FirstName) LIKE @SearchTerm
                                    OR Email LIKE @SearchTerm
                                    OR CAST(MemberID AS CHAR) LIKE @SearchTerm
                                    OR Type LIKE @SearchTerm
                                    OR Status LIKE @SearchTerm
                                    {memberCardNumberCondition}
                                    {contactCondition}
                                    {addressCondition}
                                    {(parsedMemberId > 0 ? "OR MemberID = @ParsedMemberID" : "")})
                                    LIMIT 100";
                    
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", searchPattern);
                        if (parsedMemberId > 0)
                        {
                            cmd.Parameters.AddWithValue("@ParsedMemberID", parsedMemberId);
                        }
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                members.Add(MapDataRowToMember(row));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in comprehensive member search: {ex.Message}");
                // Fallback to repository search
                members.AddRange(_memberRepository.Search(searchTerm));
            }
            return members;
        }
        private Member MapDataRowToMember(System.Data.DataRow row)
        {
            string memberType = string.Empty;
            if (row.Table.Columns.Contains("Type") && row["Type"] != DBNull.Value)
            {
                memberType = row["Type"].ToString();
            }
            else if (row.Table.Columns.Contains("MemberType") && row["MemberType"] != DBNull.Value)
            {
                memberType = row["MemberType"].ToString();
            }
            return new Member
            {
                MemberID = Convert.ToInt32(row["MemberID"]),
                FirstName = row["FirstName"]?.ToString() ?? string.Empty,
                LastName = row["LastName"]?.ToString() ?? string.Empty,
                Email = row["Email"]?.ToString() ?? string.Empty,
                Type = memberType,
                RegistrationDate = row["RegistrationDate"] != DBNull.Value ? Convert.ToDateTime(row["RegistrationDate"]) : DateTime.Now,
                ExpirationDate = row["ExpirationDate"] != DBNull.Value ? Convert.ToDateTime(row["ExpirationDate"]) : DateTime.Now,
                Status = row["Status"]?.ToString() ?? string.Empty,
                Contact = row.Table.Columns.Contains("Contact") && row["Contact"] != DBNull.Value ? row["Contact"].ToString() : string.Empty,
                Address = row.Table.Columns.Contains("Address") && row["Address"] != DBNull.Value ? row["Address"].ToString() : string.Empty,
                PhotoPath = row.Table.Columns.Contains("PhotoPath") && row["PhotoPath"] != DBNull.Value ? row["PhotoPath"].ToString() : null,
                ValidIDPath = row.Table.Columns.Contains("ValidIDPath") && row["ValidIDPath"] != DBNull.Value ? row["ValidIDPath"].ToString() : null,
                MemberCardNumber = row.Table.Columns.Contains("MemberCardNumber") && row["MemberCardNumber"] != DBNull.Value ? row["MemberCardNumber"].ToString() : null
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
    internal class SearchCondition
    {
        public string Term { get; set; }
        public string Operator { get; set; }
    }
    public class SearchResults
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Member> Members { get; set; } = new List<Member>();
        public long SearchTime { get; set; }
        public int TotalResults { get; set; }
    }
}