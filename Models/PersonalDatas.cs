using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SchoolProject.Models
{
    public partial class PersonalDatas
    {
        public PersonalDatas()
        {
            Parents = new HashSet<Parents>();
            Students = new HashSet<Students>();
            Teachers = new HashSet<Teachers>();
        }

        public int PersonalDataId { get; set; }
        [Required]
        [RegularExpression("^[A-Z][a-z]{2,}$")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("^[A-Z][a-z]{2,}$")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Full name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int Gender { get; set; }
        [RegularExpression("^[0-9 | a-z | _ | -]+@[0-9 | a-z | _ | -]+\\.[a-z]{2,3}$")]
        public string Email { get; set; }
        [Required]
        public int AddressId { get; set; }
        [Required]
        [RegularExpression("^[0-9]{11}$")]
        public string Pesel { get; set; }

        public Addresses Address { get; set; }
        public ICollection<Parents> Parents { get; set; }
        public ICollection<Students> Students { get; set; }
        public ICollection<Teachers> Teachers { get; set; }
    }
}
