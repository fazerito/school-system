using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Models
{
    public partial class Teachers
    {
        public Teachers()
        {
            Classes = new HashSet<Classes>();
            Grades = new HashSet<Grades>();
            QualificationTeachers = new HashSet<QualificationTeachers>();
            Subjects = new HashSet<Subjects>();
        }

        public int TeacherId { get; set; }
        public int PersonalDataId { get; set; }
        [Display(Name = "Educator")]
        public bool IsEducator { get; set; }

        public PersonalDatas PersonalData { get; set; }
        public Principals Principals { get; set; }
        public ICollection<Classes> Classes { get; set; }
        public ICollection<Grades> Grades { get; set; }
        public ICollection<QualificationTeachers> QualificationTeachers { get; set; }
        public ICollection<Subjects> Subjects { get; set; }
    }
}
