using System.ComponentModel.DataAnnotations;
using SchoolProject.Models;

namespace SchoolProject.ViewModels
{
    public class StudentViewModel
    {
        public Students Student { get; set; }
        public PersonalDatas PersonalData { get; set; }
        public Addresses Address { get; set; }
        public Classes Classes { get; set; }
        public Users User { get; set; }
    }
}
