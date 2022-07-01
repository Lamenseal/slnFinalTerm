using System;
using System.Collections.Generic;

#nullable disable

namespace prjFinalTerm.Models
{
    public partial class CaseRecord
    {
        public int CaseId { get; set; }
        public int MemberId { get; set; }
        public string DiagnosticRecord { get; set; }
        public int ReserveId { get; set; }
        public string SyndromeDescription { get; set; }
        public int? TreatmentDetailId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Reserve Reserve { get; set; }
        public virtual TreatmentDetail TreatmentDetail { get; set; }
    }
}
