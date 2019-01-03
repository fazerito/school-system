using System;
using System.Collections.Generic;

namespace SchoolProject.Models
{
    public partial class Attendances
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public int AttendanceType { get; set; }

        public Lessons Lesson { get; set; }
        public Students Student { get; set; }
    }
}
