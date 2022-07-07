using System;
using System.Collections.Generic;

namespace WebApp4Sec001.Models
{
    public partial class Course
    {
        public Course()
        {
            Students = new HashSet<Student>();
        }

        public string CourseCode { get; set; } = null!;
        public string CourseTitle { get; set; } = null!;
        public int TotalCourseHours { get; set; }
        public string School { get; set; } = null!;
        public string Department { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; }
    }
}
