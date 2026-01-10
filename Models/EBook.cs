using System;

namespace Project5LMS.Models
{
    /// <summary>
    /// Represents an electronic book with download link
    /// </summary>
    public class EBook : Book
    {
        public string DownloadLink { get; set; }
        public string FileFormat { get; set; } // PDF, EPUB, MOBI, etc.
        public long FileSizeBytes { get; set; }
        public int MaxDownloads { get; set; }
        public int CurrentDownloads { get; set; }
        public DateTime? ExpirationDate { get; set; } // For time-limited access
        public bool RequiresAuthentication { get; set; }
        public string AccessKey { get; set; } // Optional access key
    }
}

