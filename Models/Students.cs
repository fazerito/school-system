using System;
using System.Collections.Generic;

namespace SchoolProject.Models
{
    public partial class Students
    {
        public Students()
        {
            Attendances = new HashSet<Attendances>();
            Grades = new HashSet<Grades>();
        }

        public int StudentId { get; set; }
        public int ParentId { get; set; }
        public int ClassId { get; set; }
        public int PersonalDataId { get; set; }

        public Classes Class { get; set; }
        public Parents Parent { get; set; }
        public PersonalDatas PersonalData { get; set; }
        public ICollection<Attendances> Attendances { get; set; }
        public ICollection<Grades> Grades { get; set; }
    }
}
