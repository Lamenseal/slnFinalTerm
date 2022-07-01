﻿using System;
using System.Collections.Generic;

#nullable disable

namespace prjFinalTerm.Models
{
    public partial class ClinicRoom
    {
        public ClinicRoom()
        {
            ClinicDetails = new HashSet<ClinicDetail>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; }

        public virtual ICollection<ClinicDetail> ClinicDetails { get; set; }
    }
}
