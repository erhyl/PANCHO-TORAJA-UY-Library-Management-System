using System;
namespace Project5LMS.Models
{
    public class Periodical : Book
    {
        public string IssueNumber { get; set; }
        public string VolumeNumber { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Frequency { get; set; }
        public string ISSN { get; set; }
    }
}