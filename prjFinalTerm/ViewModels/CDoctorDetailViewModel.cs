using prjFinalTerm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace prjFinalTerm.ViewModels
{
    public class CDoctorDetailViewModel
    {
        private Doctor _doc;
        private Department _dep;
        private Experience _exp;
        private DepartmentCategory _depC;
        public CDoctorDetailViewModel()
        {
            _doc = new Doctor();
            _dep = new Department();
            _exp = new Experience();
            _depC = new DepartmentCategory();
        }
        public Doctor doctor
        {
            get { return _doc; }
            set { _doc = value; }
        }
        public Department department
        {
            get { return _dep; }
            set { _dep = value; }
        }
        public Experience experience
        {
            get { return _exp; }
            set { _exp = value; }
        }
        public DepartmentCategory departmentCategory
        {
            get { return _depC; }
            set { _depC = value; }
        }
        public int DoctorID {
            get { return _doc.DoctorId; }
            set { _doc.DoctorId = value; }
        }
        public int? MemberID
        {
            get { return _doc.MemberId; }
            set { _doc.MemberId = value; }
        }
        [DisplayName("醫生姓名")]
        public string DoctorName {
            get { return _doc.DoctorName; }
            set { _doc.DoctorName = value; }
        }
        public int? DepartmentID
        {
            get { return _doc.DepartmentId; }
            set { _doc.DepartmentId = value; }
        }
        [DisplayName("學歷")]
        public string Education {
            get { return _doc.Education; }
            set { _doc.Education = value; }
        }
        [DisplayName("職稱")]
        public string JobTitle {
            get { return _doc.JobTitle; }
            set { _doc.JobTitle = value; }
        }
        [DisplayName("大頭照")]
        public string PicturePath 
        {
            get { return _doc.PicturePath; }
            set { _doc.PicturePath = value; }
        }
        [DisplayName("經歷")]
        public string Experience {
            get { return _exp.Experience1; }
            set { _exp.Experience1 = value; }
        }
        public int ExperienceID
        {
            get { return _exp.ExperienceId; }
            set { _exp.ExperienceId = value; }
        }


        //public int DepartmentID {
        //    get { return department.DepartmentId; }
        //    set { department.DepartmentId = value; }
        //}
        [DisplayName("專長")]
        public string DepName {
            get { return _dep.DeptName; }
            set { _dep.DeptName = value; }
        }

        public int DeptCategoryID{
            get { return _depC.DeptCategoryId; }
            set { _depC.DeptCategoryId = value; }
        }
        [DisplayName("專長科別")]
        public string DeptCategoryName {
            get { return _depC.DeptCategoryName; }
            set { _depC.DeptCategoryName = value; }
        }
    }
}
