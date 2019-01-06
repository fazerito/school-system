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
        [RegularExpression("^[a-zA-Z]+$")]
        public string StreetName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]+$")]
        public string City { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]+$")]
        [Display(Name = "Street number")]
        public int StreetNumber { get; set; }
        [Display(Name = "Apartment number")]
        public int? AptNumber { get; set; }
        [Required]
        [RegularExpression("^[0-9]{2}-[0-9]{3}")]
        [Display(Name = "Zip code")]
        public string ZipCode { get; set; }

        public ICollection<PersonalDatas> PersonalDatas { get; set; }
    }
}
