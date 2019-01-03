using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Models
{
    public partial class Lessons
    {
        public Lessons()
        {
            Attendances = new HashSet<Attendances>();
        }

        public int LessonId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int SubjectId { get; set; }
        [Display(Name = "Day")]
        public int DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public int ClassId { get; set; }

        public Classes Class { get; set; }
        public Subjects Subject { get; set; }
        public ICollection<Attendances> Attendances { get; set; }
    }
}
