﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Models
{
    public partial class Parents
    {
        public Parents()
        {
            Students = new HashSet<Students>();
        }

        public int ParentId { get; set; }
        [Required]
        public int PersonalDataId { get; set; }

        public PersonalDatas PersonalData { get; set; }
        public ICollection<Students> Students { get; set; }
    }
}
