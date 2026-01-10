using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Project5LMS.Data;
using Project5LMS.Interfaces;
using Project5LMS.Models;
using MySql.Data.MySqlClient;
namespace Project5LMS.Services
{
    public class BulkImportService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IBookService _bookService;
        public BulkImportService(DatabaseContext dbContext, IBookService bookService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }
        public BulkImportResult ImportFromFile(string filePath, bool skipHeader = true)
        {
            string extension = System.IO.Path.GetExtension(filePath).ToLower();
            if (extension == ".xlsx" || extension == ".xls")
            {
                return ImportFromExcel(filePath, skipHeader);
            }
            else
            {
                return ImportFromCSV(filePath, skipHeader);
            }
        }
        public BulkImportResult ImportFromCSV(string filePath, bool skipHeader = true)
        {
            var result = new BulkImportResult();
            try
            {
                var books = ParseCSV(filePath, skipHeader);
                result.TotalRecords = books.Count;
                foreach (var book in books)
                {
                    try
                    {
                        if (ValidateBook(book))
                        {
                            if (string.IsNullOrWhiteSpace(book.AccessionNo))
                            {
                                book.AccessionNo = GenerateAccessionNumber();
                            }
                            if (string.IsNullOrWhiteSpace(book.Barcode))
                            {
                                book.Barcode = Helpers.BarcodeGenerator.GenerateFromAccession(book.AccessionNo);
                            }
                            if (_bookService.AddBook(book))
                            {
                                result.SuccessCount++;
                            }
                            else
                            {
                                result.FailedCount++;
                                result.Errors.Add($"Failed to add book: {book.Title}");
                            }
                        }
                        else
                        {
                            result.FailedCount++;
                            result.Errors.Add($"Invalid book data: {book.Title ?? "Unknown"}");
                        }
                    }
                    catch (Exception ex)
                    {
                        result.FailedCount++;
                        result.Errors.Add($"Error importing {book.Title ?? "Unknown"}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Error reading CSV file: {ex.Message}");
            }
            return result;
        }
        private List<Book> ParseCSV(string filePath, bool skipHeader)
        {
            var books = new List<Book>();
            var lines = File.ReadAllLines(filePath);
            int startIndex = skipHeader ? 1 : 0;
            for (int i = startIndex; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (string.IsNullOrEmpty(line))
                    continue;
                var fields = ParseCSVLine(line);
                if (fields.Count < 3)
                    continue;
                var book = new Book
                {
                    Title = GetField(fields, 0, "Title"),
                    Author = GetField(fields, 1, "Author"),
                    ISBN = GetField(fields, 2, "ISBN"),
                    Subtitle = GetField(fields, 3, ""),
                    Editor = GetField(fields, 4, ""),
                    Publisher = GetField(fields, 5, ""),
                    PublicationYear = ParseInt(GetField(fields, 6, "0")),
                    Edition = GetField(fields, 7, ""),
                    Category = GetField(fields, 8, ""),
                    Language = GetField(fields, 9, "English"),
                    NumberOfPages = ParseInt(GetField(fields, 10, "0")),
                    PhysicalDescription = GetField(fields, 11, ""),
                    Location = GetField(fields, 12, ""),
                    CallNumber = GetField(fields, 13, ""),
                    AccessionNo = GetField(fields, 14, ""),
                    BookType = GetField(fields, 15, "Circulation"),
                    TotalCopies = ParseInt(GetField(fields, 16, "1")),
                    Available = ParseInt(GetField(fields, 16, "1")),
                    Status = "Available"
                };
                books.Add(book);
            }
            return books;
        }
        private List<string> ParseCSVLine(string line)
        {
            var fields = new List<string>();
            bool inQuotes = false;
            string currentField = "";
            foreach (char c in line)
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(currentField.Trim());
                    currentField = "";
                }
                else
                {
                    currentField += c;
                }
            }
            fields.Add(currentField.Trim());
            return fields;
        }
        private string GetField(List<string> fields, int index, string defaultValue)
        {
            return index < fields.Count && !string.IsNullOrWhiteSpace(fields[index])
                ? fields[index]
                : defaultValue;
        }
        private int ParseInt(string value, int defaultValue = 0)
        {
            return int.TryParse(value, out int result) ? result : defaultValue;
        }
        private bool ValidateBook(Book book)
        {
            return !string.IsNullOrWhiteSpace(book.Title) &&
                   !string.IsNullOrWhiteSpace(book.Author) &&
                   book.TotalCopies > 0;
        }
        private string GenerateAccessionNumber()
        {
            try
            {
                using (var conn = _dbContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT MAX(BookID) as MaxID FROM Books";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int maxId = result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
                        return $"ACC-{(maxId + 1).ToString().PadLeft(4, '0')}";
                    }
                }
            }
            catch
            {
                return $"ACC-{DateTime.Now:yyyyMMddHHmmss}";
            }
        }
        public BulkImportResult ImportFromExcel(string filePath, bool skipHeader = true)
        {
            var result = new BulkImportResult();
            try
            {
                var closedXmlType = Type.GetType("ClosedXML.Excel.XLWorkbook, ClosedXML");
                if (closedXmlType != null)
                {
                    return ImportFromExcelWithClosedXML(filePath, skipHeader);
                }
                var interopType = Type.GetType("Microsoft.Office.Interop.Excel.ApplicationClass, Microsoft.Office.Interop.Excel");
                if (interopType != null)
                {
                    return ImportFromExcelWithInterop(filePath, skipHeader);
                }
                result.Errors.Add("Excel import libraries not found. Please install ClosedXML NuGet package or use CSV format.");
                result.Errors.Add("To install ClosedXML: Install-Package ClosedXML -Version 0.95.4");
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Error reading Excel file: {ex.Message}");
            }
            return result;
        }
        private BulkImportResult ImportFromExcelWithClosedXML(string filePath, bool skipHeader)
        {
            var result = new BulkImportResult();
            try
            {
                var workbookType = Type.GetType("ClosedXML.Excel.XLWorkbook, ClosedXML");
                var worksheetType = Type.GetType("ClosedXML.Excel.IXLWorksheet, ClosedXML");
                if (workbookType == null || worksheetType == null)
                {
                    result.Errors.Add("ClosedXML library not properly loaded.");
                    return result;
                }
                var workbook = Activator.CreateInstance(workbookType, filePath);
                var worksheetsProperty = workbookType.GetProperty("Worksheets");
                var worksheets = worksheetsProperty.GetValue(workbook);
                var firstMethod = worksheets.GetType().GetMethod("First");
                var worksheet = firstMethod.Invoke(worksheets, null);
                var usedRangeProperty = worksheetType.GetProperty("RangeUsed");
                var usedRange = usedRangeProperty.GetValue(worksheet);
                if (usedRange == null)
                {
                    result.Errors.Add("Excel file is empty.");
                    return result;
                }
                var rowsProperty = usedRange.GetType().GetProperty("Rows");
                var rows = rowsProperty.GetValue(usedRange);
                var countProperty = rows.GetType().GetProperty("Count");
                int rowCount = (int)countProperty.GetValue(rows);
                int startRow = skipHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= rowCount; rowNum++)
                {
                    try
                    {
                        var cellMethod = worksheetType.GetMethod("Cell", new[] { typeof(int), typeof(int) });
                        var cell1 = cellMethod.Invoke(worksheet, new object[] { rowNum, 1 });
                        var cell2 = cellMethod.Invoke(worksheet, new object[] { rowNum, 2 });
                        var cell3 = cellMethod.Invoke(worksheet, new object[] { rowNum, 3 });
                        var valueProperty = cell1.GetType().GetProperty("Value");
                        string title = valueProperty.GetValue(cell1)?.ToString() ?? "";
                        string author = valueProperty.GetValue(cell2)?.ToString() ?? "";
                        string isbn = valueProperty.GetValue(cell3)?.ToString() ?? "";
                        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
                            continue;
                        var book = new Book
                        {
                            Title = title,
                            Author = author,
                            ISBN = isbn,
                            Subtitle = GetExcelCellValue(worksheet, worksheetType, rowNum, 4),
                            Editor = GetExcelCellValue(worksheet, worksheetType, rowNum, 5),
                            Publisher = GetExcelCellValue(worksheet, worksheetType, rowNum, 6),
                            PublicationYear = ParseInt(GetExcelCellValue(worksheet, worksheetType, rowNum, 7)),
                            Edition = GetExcelCellValue(worksheet, worksheetType, rowNum, 8),
                            Category = GetExcelCellValue(worksheet, worksheetType, rowNum, 9),
                            Language = GetExcelCellValue(worksheet, worksheetType, rowNum, 10) ?? "English",
                            NumberOfPages = ParseInt(GetExcelCellValue(worksheet, worksheetType, rowNum, 11)),
                            PhysicalDescription = GetExcelCellValue(worksheet, worksheetType, rowNum, 12),
                            Location = GetExcelCellValue(worksheet, worksheetType, rowNum, 13),
                            CallNumber = GetExcelCellValue(worksheet, worksheetType, rowNum, 14),
                            AccessionNo = GetExcelCellValue(worksheet, worksheetType, rowNum, 15),
                            BookType = GetExcelCellValue(worksheet, worksheetType, rowNum, 16) ?? "Circulation",
                            TotalCopies = ParseInt(GetExcelCellValue(worksheet, worksheetType, rowNum, 17)),
                            Available = ParseInt(GetExcelCellValue(worksheet, worksheetType, rowNum, 17)),
                            Status = "Available"
                        };
                        result.TotalRecords++;
                        if (ValidateBook(book))
                        {
                            if (string.IsNullOrWhiteSpace(book.AccessionNo))
                            {
                                book.AccessionNo = GenerateAccessionNumber();
                            }
                            if (string.IsNullOrWhiteSpace(book.Barcode))
                            {
                                book.Barcode = Helpers.BarcodeGenerator.GenerateFromAccession(book.AccessionNo);
                            }
                            if (_bookService.AddBook(book))
                            {
                                result.SuccessCount++;
                            }
                            else
                            {
                                result.FailedCount++;
                                result.Errors.Add($"Failed to add book: {book.Title}");
                            }
                        }
                        else
                        {
                            result.FailedCount++;
                            result.Errors.Add($"Invalid book data: {book.Title ?? "Unknown"}");
                        }
                    }
                    catch (Exception ex)
                    {
                        result.FailedCount++;
                        result.Errors.Add($"Error importing row {rowNum}: {ex.Message}");
                    }
                }
                var disposeMethod = workbookType.GetMethod("Dispose");
                disposeMethod?.Invoke(workbook, null);
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Error reading Excel file with ClosedXML: {ex.Message}");
            }
            return result;
        }
        private string GetExcelCellValue(object worksheet, Type worksheetType, int row, int col)
        {
            try
            {
                var cellMethod = worksheetType.GetMethod("Cell", new[] { typeof(int), typeof(int) });
                var cell = cellMethod.Invoke(worksheet, new object[] { row, col });
                var valueProperty = cell.GetType().GetProperty("Value");
                return valueProperty.GetValue(cell)?.ToString() ?? "";
            }
            catch
            {
                return "";
            }
        }
        private BulkImportResult ImportFromExcelWithInterop(string filePath, bool skipHeader)
        {
            var result = new BulkImportResult();
            result.Errors.Add("Microsoft Office Interop Excel import not yet implemented. Please use ClosedXML or CSV format.");
            return result;
        }
    }
    public class BulkImportResult
    {
        public int TotalRecords { get; set; }
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool HasErrors => Errors.Count > 0;
        public double SuccessRate => TotalRecords > 0 ? (SuccessCount * 100.0 / TotalRecords) : 0;
    }
}