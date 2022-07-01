using System;
using System.Collections.Generic;

#nullable disable

namespace prjFinalTerm.Models
{
    public partial class TreatmentDetail
    {
        public TreatmentDetail()
        {
            CaseRecords = new HashSet<CaseRecord>();
            Treatments = new HashSet<Treatment>();
        }

        public int TreatmentDetailId { get; set; }
        public string TreatmentDetail1 { get; set; }
        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<CaseRecord> CaseRecords { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
