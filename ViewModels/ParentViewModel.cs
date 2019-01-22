using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolProject.Models;

namespace SchoolProject.ViewModels
{
    public class ParentViewModel
    {
        public Parents Parent { get; set; }
        public PersonalDatas PersonalData { get; set; }
        public Addresses Address { get; set; }
        public Users User { get; set; }
        public List<Students> Students { get; set; }
    }
}
