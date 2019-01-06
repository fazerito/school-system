using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int Type { get; set; }
        public int? TeacherId { get; set; }
        public int? ParentId { get; set; }
        public int? PrincipalId { get; set; }

        public Parents Parent { get; set; }
        public Principals Principal { get; set; }
        public Teachers Teacher { get; set; }
    }
}
