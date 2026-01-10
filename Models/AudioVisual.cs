using System;

namespace Project5LMS.Models
{
    /// <summary>
    /// Represents audio-visual materials (DVDs, CDs, etc.)
    /// </summary>
    public class AudioVisual : Book
    {
        public string MediaType { get; set; } // DVD, CD, Blu-ray, VHS, etc.
        public int DurationMinutes { get; set; }
        public string Format { get; set; } // NTSC, PAL, etc.
        public string AudioLanguage { get; set; } // Audio language (different from Book.Language which is publication language)
        public bool HasSubtitles { get; set; }
        public string Rating { get; set; } // G, PG, PG-13, R, etc.
    }
}

