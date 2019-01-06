using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Models
{
    public partial class Attendances
    {
        public int AttendanceId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int LessonId { get; set; }
        [Required]
        public int AttendanceType { get; set; }

        public Lessons Lesson { get; set; }
        public Students Student { get; set; }
    }
}
