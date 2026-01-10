using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Project5LMS.Models;
namespace Project5LMS.Helpers
{
    public static class DataTableHelper
    {
        public static DataTable BooksToDataTable(IEnumerable<Book> books)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("BookID", typeof(int));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Author", typeof(string));
            dt.Columns.Add("ISBN", typeof(string));
            dt.Columns.Add("Publisher", typeof(string));
            dt.Columns.Add("YearPublished", typeof(int));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Copies", typeof(int));
            dt.Columns.Add("Available", typeof(int));
            dt.Columns.Add("Barcode", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            foreach (var book in books)
            {
                DataRow row = dt.NewRow();
                row["BookID"] = book.BookID;
                row["Title"] = book.Title ?? string.Empty;
                row["Author"] = book.Author ?? string.Empty;
                row["ISBN"] = book.ISBN ?? string.Empty;
                row["Publisher"] = book.Publisher ?? string.Empty;
                row["YearPublished"] = book.PublicationYear;
                row["Category"] = book.Category ?? string.Empty;
                row["Copies"] = book.TotalCopies;
                row["Available"] = book.Available;
                row["Barcode"] = book.AccessionNo ?? string.Empty;
                row["Location"] = book.Location ?? string.Empty;
                row["Status"] = book.Status ?? string.Empty;
                dt.Rows.Add(row);
            }
            return dt;
        }
        public static DataTable MembersToDataTable(IEnumerable<Member> members, Func<Member, int> getBorrowingCount = null)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MemberID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Contact", typeof(string));
            dt.Columns.Add("MemberType", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Books", typeof(int));
            dt.Columns.Add("Expires", typeof(DateTime));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("RegistrationDate", typeof(DateTime));
            dt.Columns.Add("Address", typeof(string));
            foreach (var member in members)
            {
                DataRow row = dt.NewRow();
                row["MemberID"] = member.MemberID;
                row["Name"] = member.FullName;
                row["Contact"] = member.Contact ?? string.Empty;
                row["MemberType"] = member.Type ?? string.Empty;
                row["Status"] = member.Status ?? "Active";
                row["Books"] = getBorrowingCount != null ? getBorrowingCount(member) : 0;
                row["Expires"] = member.ExpirationDate;
                row["Email"] = member.Email ?? string.Empty;
                row["RegistrationDate"] = member.RegistrationDate;
                row["Address"] = member.Address ?? string.Empty;
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}