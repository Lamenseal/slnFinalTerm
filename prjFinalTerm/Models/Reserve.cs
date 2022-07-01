using System;
using System.Collections.Generic;

#nullable disable

namespace prjFinalTerm.Models
{
    public partial class Reserve
    {
        public Reserve()
        {
            CaseRecords = new HashSet<CaseRecord>();
        }

        public int ReserveId { get; set; }
        public int ClinicDetailId { get; set; }
        public int? StateId { get; set; }
        public int MemberId { get; set; }
        public DateTime ReserveDate { get; set; }
        public string RemarkPatient { get; set; }
        public string RemarkAdmin { get; set; }
        public int? SourceId { get; set; }

        public virtual ClinicDetail ClinicDetail { get; set; }
        public virtual Member Member { get; set; }
        public virtual Source Source { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<CaseRecord> CaseRecords { get; set; }
    }
}
