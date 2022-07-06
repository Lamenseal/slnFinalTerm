using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using prjFinalTerm.Models;
using prjFinalTerm.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace prjFinalTerm.Controllers
{
    public class DoctorController : Controller
    {
        private IWebHostEnvironment _enviroment;
        public DoctorController(IWebHostEnvironment p)
        {
            _enviroment = p;
        }
        public IActionResult Index(CKeyWordViewModel vModel)
        {
            MedicalContext db = new MedicalContext();
            IEnumerable<Doctor> datas = null;
            if (string.IsNullOrEmpty(vModel.txtKeyword))
            {
                datas = from t in db.Doctors
                        join d in db.Departments on t.DepartmentId equals d.DepartmentId
                        select t;
                
            }
            else
            {
                datas = db.Doctors.Where(t => t.DoctorName.Contains(vModel.txtKeyword) ||
                t.Education.Contains(vModel.txtKeyword) || t.JobTitle.Contains(vModel.txtKeyword));
            }
            return View(datas);
        }
        public IActionResult CreateDetail()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateDetail(CDoctorDetailViewModel d)
        {

            MedicalContext db = new MedicalContext();
            if (d.photo != null)
            {
                string pName = Guid.NewGuid().ToString() + ".jpg";
                d.photo.CopyTo(new FileStream((_enviroment.WebRootPath + "/images/" + pName), FileMode.Create));
                d.PicturePath = pName;
            }
            db.Members.Add(d.member);
            db.SaveChanges();
            d.doctor.MemberId = d.member.MemberId;
            db.Doctors.Add(d.doctor);            
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CDoctorDetailViewModel d)
        {

            MedicalContext db = new MedicalContext();
            if (d.photo != null)
            {
                string pName = Guid.NewGuid().ToString() + ".jpg";
                d.photo.CopyTo(new FileStream((_enviroment.WebRootPath + "/images/" + pName), FileMode.Create));
                d.PicturePath = pName;
            }
            db.Doctors.Add(d.doctor);
            db.Members.Add(d.member);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int? id)
        {
            MedicalContext db = new MedicalContext();
            CDoctorDetailViewModel prod = new CDoctorDetailViewModel();
            prod.doctor = db.Doctors.FirstOrDefault(t => t.DoctorId == id);
            //prod.department = db.Departments.FirstOrDefault(t => t.DepartmentId == prod.doctor.DepartmentId);
            //prod.departmentCategory = db.DepartmentCategories.FirstOrDefault(t => t.DeptCategoryId == prod.department.DeptCategoryId);
            //prod.experience = db.Experiences.FirstOrDefault(t => t.DoctorId == prod.doctor.DoctorId);
            prod.member = db.Members.FirstOrDefault(t => t.MemberId == prod.doctor.MemberId);
            if (prod != null)
            {
                db.Doctors.Remove(prod.doctor);
                if (prod.member != null)
                {
                    db.Members.Remove(prod.member);
                }

                db.SaveChanges();
            }
            
            return RedirectToAction("Index");

        }
        public IActionResult EditDetail(int? id)
        {
            MedicalContext db = new MedicalContext();
            CDoctorDetailViewModel prod = new CDoctorDetailViewModel();
            prod.doctor = db.Doctors.FirstOrDefault(t => t.DoctorId == id);
            prod.department = db.Departments.FirstOrDefault(t => t.DepartmentId == prod.doctor.DepartmentId);
            prod.departmentCategory = db.DepartmentCategories.FirstOrDefault(t => t.DeptCategoryId == prod.department.DeptCategoryId);
            prod.experience = db.Experiences.FirstOrDefault(t => t.DoctorId == prod.doctor.DoctorId);
            if (prod == null)
                return RedirectToAction("Index");
            return View(prod);
        }
        [HttpPost]
        public IActionResult EditDetail(CDoctorDetailViewModel p)
        {

            MedicalContext db = new MedicalContext();
            Doctor dcDb = db.Doctors.FirstOrDefault(t => t.DoctorId == p.DoctorID);
            Department dpDb = db.Departments.FirstOrDefault(s => s.DepartmentId == p.DepartmentID);
            DepartmentCategory dpcDb = db.DepartmentCategories.FirstOrDefault(u => u.DeptCategoryId == p.DeptCategoryID);
            Experience expDb = db.Experiences.FirstOrDefault(v => v.ExperienceId == p.ExperienceID);
            if (dcDb != null)
            {
                if (p.photo != null)
                {
                    string pName = Guid.NewGuid().ToString() + ".jpg";
                    p.photo.CopyTo(new FileStream((_enviroment.WebRootPath + "/images/" + pName), FileMode.Create));
                    dcDb.PicturePath = pName;
                }
                dcDb.DoctorName = p.DoctorName;
                dcDb.DepartmentId = p.DepartmentID;
                dcDb.Education = p.Education;
                dcDb.JobTitle = p.JobTitle;
            }
            if (dpDb != null)
            {
                dpDb.DeptName = p.DepName;
                dpDb.DeptCategoryId = p.DeptCategoryID;
            }
            if (dpcDb != null)
            {
                dpcDb.DeptCategoryName = p.DeptCategoryName;
            }
            if (expDb != null)
            {
                expDb.Experience1 = p.Experience;
            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        //public IActionResult Edit(int? id)
        //{
        //    MedicalContext db = new MedicalContext();
        //    Doctor prod = db.Doctors.FirstOrDefault(t => t.DoctorId == id);
        //    if (prod == null)
        //        return RedirectToAction("Index");
        //    return View(prod);
        //}
        //[HttpPost]
        //public IActionResult Edit(CDoctorViewModel p)
        //{

        //    MedicalContext db = new MedicalContext();
        //    Doctor prod = db.Doctors.FirstOrDefault(t => t.DoctorId == p.DoctorId);
        //    if (prod != null)
        //    {
        //        if (p.photo != null)
        //        {
        //            string pName = Guid.NewGuid().ToString() + ".jpg";
        //            p.photo.CopyTo(new FileStream((_enviroment.WebRootPath + "/images/" + pName), FileMode.Create));
        //            prod.PicturePath = pName;
        //        }
        //        prod.DoctorName = p.DoctorName;
        //        prod.DepartmentId = p.DepartmentId;
        //        prod.Education = p.Education;
        //        prod.JobTitle = p.JobTitle;

        //    }
        //    db.SaveChanges();
        //    return RedirectToAction("Index");

        //}
        public IActionResult List(CKeyWordViewModel vModel)
        {
            MedicalContext db = new MedicalContext();
            IEnumerable<Doctor> datas = null;
            if (string.IsNullOrEmpty(vModel.txtKeyword))
            {
                datas = from t in db.Doctors
                        select t;
                        
                
            }
            else
            {
                datas = db.Doctors.Where(t => t.DoctorName.Contains(vModel.txtKeyword) ||
                t.Education.Contains(vModel.txtKeyword) || t.JobTitle.Contains(vModel.txtKeyword));
            }
            return View(datas);
        }
        public IActionResult Details(int? id)
        {
            MedicalContext db = new MedicalContext();
            CDoctorDetailViewModel prod =new CDoctorDetailViewModel();
            Doctor DD = db.Doctors.FirstOrDefault(t => t.DoctorId == id);
            prod.doctor = DD;
            prod.department = db.Departments.FirstOrDefault(t => t.DepartmentId == prod.doctor.DepartmentId);
            prod.experience = db.Experiences.FirstOrDefault(t => t.DoctorId == prod.doctor.DoctorId);
            prod.departmentCategory = db.DepartmentCategories.FirstOrDefault(t => t.DeptCategoryId == prod.department.DeptCategoryId);
            if (prod == null)
                return RedirectToAction("Index");
            return View(prod);
        }
    }
}
