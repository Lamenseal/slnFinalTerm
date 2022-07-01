using System;
using System.Collections.Generic;

#nullable disable

namespace prjFinalTerm.Models
{
    public partial class Department
    {
        public Department()
        {
            ClinicDetails = new HashSet<ClinicDetail>();
            Doctors = new HashSet<Doctor>();
            TreatmentDetails = new HashSet<TreatmentDetail>();
        }

        public int DepartmentId { get; set; }
        public string DeptName { get; set; }
        public int DeptCategoryId { get; set; }

        public virtual DepartmentCategory DeptCategory { get; set; }
        public virtual ICollection<ClinicDetail> ClinicDetails { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<TreatmentDetail> TreatmentDetails { get; set; }
    }
}
