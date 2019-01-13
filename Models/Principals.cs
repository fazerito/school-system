using System;
using System.Collections.Generic;

namespace SchoolProject.Models
{
    public partial class Principals
    {
        public Principals()
        {
        }

        public int PrincipalId { get; set; }
        public int TeacherId { get; set; }

        public Teachers Principal { get; set; }
    }
}
