using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Models
{
    public partial class Grades
    {
        public int GradeId { get; set; }
        [Required]
        [Range(1, 6)]
        public int Value { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int StudentId { get; set; }

        public Students Student { get; set; }
        public Subjects Subject { get; set; }
        public Teachers Teacher { get; set; }
    }
}
