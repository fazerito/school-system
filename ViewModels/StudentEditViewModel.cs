using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SchoolProject.Models;

namespace SchoolProject.ViewModels
{
    public class StudentEditViewModel
    {
        public Students Student { get; set; }
        [RegularExpression("^[A-Z][a-z]{2,}$")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [RegularExpression("^[A-Z][a-z]{2,}$")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Full name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [RegularExpression("^[0-9 | a-z | _ | -]+@[0-9 | a-z | _ | -]+\\.[a-z]{2,3}$")]
        public string Email { get; set; }

        [Display(Name = "Street name")]
        public string StreetName { get; set; }

        [RegularExpression("^[a-zA-Z]+$")]
        public string City { get; set; }

        [Range(1, 999)]
        [Display(Name = "Street number")]
        public int StreetNumber { get; set; }
        [Display(Name = "Apartment number")]
        [Range(1, 999)]
        public int? AptNumber { get; set; }

        [RegularExpression("^[0-9]{2}-[0-9]{3}")]
        [Display(Name = "Zip code")]
        public string ZipCode { get; set; }

        public Classes Class { get; set; }
    }
}
