using System;
namespace Project5LMS.Models
{
    public class EBook : Book
    {
        public string DownloadLink { get; set; }
        public string FileFormat { get; set; }
        public long FileSizeBytes { get; set; }
        public int MaxDownloads { get; set; }
        public int CurrentDownloads { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool RequiresAuthentication { get; set; }
        public string AccessKey { get; set; }
    }
}