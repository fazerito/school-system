﻿using System;
using System.Collections.Generic;

namespace SchoolProject.Models
{
    public partial class Parents
    {
        public Parents()
        {
            Students = new HashSet<Students>();
            Users = new HashSet<Users>();
        }

        public int ParentId { get; set; }
        public int PersonalDataId { get; set; }

        public PersonalDatas PersonalData { get; set; }
        public ICollection<Students> Students { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
