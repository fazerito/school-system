using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.ViewModels
{
    public class StudentViewModel
    {
        public Students Student { get; set; }
        public PersonalDatas PersonalData { get; set; }
        public Addresses Address { get; set; }
        public Classes Classes { get; set; }
    }
}
