using System;
using System.Collections.Generic;

namespace SchoolProject.Models
{
    public partial class Subjects
    {
        public Subjects()
        {
            Grades = new HashSet<Grades>();
            Lessons = new HashSet<Lessons>();
        }

        public int SubjectId { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }

        public Teachers Teacher { get; set; }
        public ICollection<Grades> Grades { get; set; }
        public ICollection<Lessons> Lessons { get; set; }
    }
}
