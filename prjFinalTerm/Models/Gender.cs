using System;
using System.Collections.Generic;

#nullable disable

namespace prjFinalTerm.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Members = new HashSet<Member>();
        }

        public int GenderId { get; set; }
        public string Gender1 { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
