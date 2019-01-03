using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Models
{
    public partial class Addresses
    {
        public Addresses()
        {
            PersonalDatas = new HashSet<PersonalDatas>();
        }

        public int AddressId { get; set; }
        [Display(Name = "Street name")]
        public string StreetName { get; set; }
        public string City { get; set; }
        [Display(Name = "Street number")]
        public int StreetNumber { get; set; }
        [Display(Name = "Apartment number")]
        public int? AptNumber { get; set; }
        [Display(Name = "Zip code")]
        public string ZipCode { get; set; }

        public ICollection<PersonalDatas> PersonalDatas { get; set; }
    }
}
