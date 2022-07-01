using System;
using System.Collections.Generic;

#nullable disable

namespace prjFinalTerm.Models
{
    public partial class City
    {
        public City()
        {
            Members = new HashSet<Member>();
            Orders = new HashSet<Order>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }

        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
