using System;
using System.Collections.Generic;

namespace SchoolProject.Models
{
    public partial class Principals
    {
        public Principals()
        {
            Users = new HashSet<Users>();
        }

        public int PrincipalId { get; set; }
        public int TeacherId { get; set; }

        public Teachers Principal { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
