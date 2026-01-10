using System;

namespace Project5LMS.Models
{
    /// <summary>
    /// Represents a periodical/magazine resource
    /// </summary>
    public class Periodical : Book
    {
        public string IssueNumber { get; set; }
        public string VolumeNumber { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Frequency { get; set; } // Monthly, Weekly, Quarterly, etc.
        public string ISSN { get; set; }
    }
}

