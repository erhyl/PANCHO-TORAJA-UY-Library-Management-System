using System;

namespace Project5LMS.Models
{

    public class Book
    {

        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Category { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public string Language { get; set; }
        public int TotalCopies { get; set; }
        public int Available { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string AccessionNo { get; set; }
        public string CallNumber { get; set; }
        public string BookType { get; set; }

        public bool IsAvailable => Available > 0;

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Title) && 
                   !string.IsNullOrWhiteSpace(Author) &&
                   TotalCopies > 0 &&
                   Available >= 0 &&
                   Available <= TotalCopies;
        }

        public static Book Create(string title, string author, int totalCopies)
        {
            var book = new Book
            {
                Title = title,
                Author = author,
                TotalCopies = totalCopies,
                Available = totalCopies,
                Status = "Available"
            };

            if (!book.IsValid())
            {
                throw new ArgumentException("Invalid book data");
            }

            return book;
        }
    }
}
