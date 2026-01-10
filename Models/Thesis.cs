using System;

namespace Project5LMS.Models
{
    /// <summary>
    /// Represents a thesis or dissertation
    /// </summary>
    public class Thesis : Book
    {
        public string StudentName { get; set; }
        public string StudentID { get; set; }
        public string Degree { get; set; } // Bachelor, Master, PhD
        public string Department { get; set; }
        public string Advisor { get; set; }
        public DateTime DefenseDate { get; set; }
        public string Abstract { get; set; }
        public bool IsRestricted { get; set; } // Some theses may have restricted access
    }
}

