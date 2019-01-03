using System;
using System.Collections.Generic;

namespace SchoolProject.Models
{
    public partial class Qualifications
    {
        public Qualifications()
        {
            QualificationTeachers = new HashSet<QualificationTeachers>();
        }

        public int QualificationId { get; set; }
        public string Name { get; set; }

        public ICollection<QualificationTeachers> QualificationTeachers { get; set; }
    }
}
