using System;
namespace Project5LMS.Models
{
    public class AudioVisual : Book
    {
        public string MediaType { get; set; }
        public int DurationMinutes { get; set; }
        public string Format { get; set; }
        public string AudioLanguage { get; set; }
        public bool HasSubtitles { get; set; }
        public string Rating { get; set; }
    }
}