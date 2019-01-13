using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SchoolProject.Models
{
    public class Roles 
    {
        [Key, Required]
        public int RoleId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        

    }
}
