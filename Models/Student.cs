using System;
using System.Collections.Generic;

namespace WebApp4Sec001.Models
{
    public partial class Student
    {
        public Student()
        {
            CourseCodes = new HashSet<Course>();
        }

        public string StudentId { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Program { get; set; }

        public virtual Login Login { get; set; } = null!;

        public virtual ICollection<Course> CourseCodes { get; set; }
    }
}
