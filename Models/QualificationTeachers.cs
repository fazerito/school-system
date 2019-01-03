using System;
using System.Collections.Generic;

namespace SchoolProject.Models
{
    public partial class QualificationTeachers
    {
        public int QualificationQualificationId { get; set; }
        public int TeacherTeacherId { get; set; }

        public Qualifications QualificationQualification { get; set; }
        public Teachers TeacherTeacher { get; set; }
    }
}
