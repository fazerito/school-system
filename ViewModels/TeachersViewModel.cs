using SchoolProject.Models;
using System.ComponentModel.DataAnnotations;

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
