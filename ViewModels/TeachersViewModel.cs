using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolProject.ViewModels
{
    public class TeachersViewModel
    {
        public Teachers Teacher { get; set; }
        public PersonalDatas PersonalData { get; set; }
        public Addresses Address { get; set; }
        public Users User { get; set; }
    }
}
