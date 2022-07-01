﻿using System;
using System.Collections.Generic;

#nullable disable

namespace prjFinalTerm.Models
{
    public partial class RatingDoctor
    {
        public int RatingDoctorId { get; set; }
        public int DoctorId { get; set; }
        public int RatingTypeId { get; set; }
        public string Rating { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual RatingType RatingType { get; set; }
    }
}
