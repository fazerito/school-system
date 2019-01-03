using System;
using System.Collections.Generic;

namespace SchoolProject.Models
{
    public partial class Classes
    {
        public Classes()
        {
            Lessons = new HashSet<Lessons>();
            Students = new HashSet<Students>();
        }

        public int ClassId { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }

        public Teachers Teacher { get; set; }
        public ICollection<Lessons> Lessons { get; set; }
        public ICollection<Students> Students { get; set; }
    }
}
