﻿using Microsoft.AspNetCore.Hosting;
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
        private readonly MedicalContext _db;
        public DoctorController(IWebHostEnvironment p,MedicalContext db)
        {
            _enviroment = p;
            _db = db;
        }
        public IActionResult Index(CKeyWordViewModel vModel)
        {
            IEnumerable<Doctor> datas = null;
            if (string.IsNullOrEmpty(vModel.txtKeyword))
            {
                datas = from t in _db.Doctors
                        join d in _db.Departments on t.DepartmentId equals d.DepartmentId
                        select t;
            }
            else
            {
                datas = _db.Doctors.Where(t => t.DoctorName.Contains(vModel.txtKeyword) ||
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
            if (d.photo != null)
            {
                string pName = Guid.NewGuid().ToString() + ".jpg";
                d.photo.CopyTo(new FileStream((_enviroment.WebRootPath + "/images/" + pName), FileMode.Create));
                d.PicturePath = pName;
            }
            _db.Members.Add(d.member);
            _db.SaveChanges();
            d.doctor.MemberId = d.member.MemberId;
            _db.Doctors.Add(d.doctor);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CDoctorDetailViewModel d)
        {

            if (d.photo != null)
            {
                string pName = Guid.NewGuid().ToString() + ".jpg";
                d.photo.CopyTo(new FileStream((_enviroment.WebRootPath + "/images/" + pName), FileMode.Create));
                d.PicturePath = pName;
            }
            _db.Doctors.Add(d.doctor);
            _db.Members.Add(d.member);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int? id)
        {
            
            CDoctorDetailViewModel prod = new CDoctorDetailViewModel();
            prod.doctor = _db.Doctors.FirstOrDefault(t => t.DoctorId == id);
            prod.department = _db.Departments.FirstOrDefault(t => t.DepartmentId == prod.doctor.DepartmentId);
            prod.departmentCategory = _db.DepartmentCategories.FirstOrDefault(t => t.DeptCategoryId == prod.department.DeptCategoryId);
            prod.experience = _db.Experiences.FirstOrDefault(t => t.DoctorId == prod.doctor.DoctorId);
            prod.member = _db.Members.FirstOrDefault(t => t.MemberId == prod.doctor.MemberId);
            if (prod != null)
            {
                if (prod.departmentCategory != null)
                {
                    _db.DepartmentCategories.Remove(prod.departmentCategory);
                }
                if (prod.department != null) 
                {
                    _db.Departments.Remove(prod.department);
                }
                if (prod.experience != null) 
                {
                    _db.Experiences.Remove(prod.experience);
                }
                if (prod.member != null)
                {
                    _db.Members.Remove(prod.member);
                }
                _db.Doctors.Remove(prod.doctor);
                
                

                _db.SaveChanges();
            }
            
            return RedirectToAction("Index");

        }
        public IActionResult EditDetail(int? id)
        {
            CDoctorDetailViewModel prod = new CDoctorDetailViewModel();
            prod.doctor = _db.Doctors.FirstOrDefault(t => t.DoctorId == id);
            Department dep =  _db.Departments.FirstOrDefault(t => t.DepartmentId == prod.doctor.DepartmentId);
            DepartmentCategory depC = null;
            if (dep != null)
            {
                depC = _db.DepartmentCategories.FirstOrDefault(t => t.DeptCategoryId == dep.DeptCategoryId);
                prod.department = dep;
            }
            Experience exp = _db.Experiences.FirstOrDefault(t => t.DoctorId == prod.doctor.DoctorId);
            if (depC != null)
                prod.departmentCategory = depC;
            if (exp != null)
                prod.experience = exp;
            if (prod == null)
                return RedirectToAction("Index");
            return View(prod);
        }
        [HttpPost]
        public IActionResult EditDetail(CDoctorDetailViewModel p)
        {
            Doctor doc = _db.Doctors.FirstOrDefault(t => t.DoctorId == p.DoctorID);
            Department dep = _db.Departments.FirstOrDefault(s => s.DepartmentId == p.doctor.DepartmentId);
            DepartmentCategory depC = _db.DepartmentCategories.FirstOrDefault(u => u.DeptCategoryId == p.departmentCategory.DeptCategoryId);
            Experience exp = _db.Experiences.FirstOrDefault(v => v.DoctorId == p.DoctorID);
            Member mem = _db.Members.FirstOrDefault(x => x.MemberId == doc.MemberId);
            if (doc != null)
            {
                if (p.photo != null)
                {
                    string pName = Guid.NewGuid().ToString() + ".jpg";
                    p.photo.CopyTo(new FileStream((_enviroment.WebRootPath + "/images/" + pName), FileMode.Create));
                    doc.PicturePath = pName;
                }
                doc.DoctorName = p.DoctorName;
                mem.MemberName = p.DoctorName;
                doc.DepartmentId = p.DepartmentID;
                doc.Education = p.Education;
                doc.JobTitle = p.JobTitle;
            }
            if (depC != null && depC.DeptCategoryName != p.DeptCategoryName)
                depC.DeptCategoryName = p.DeptCategoryName;
            if (p.DeptCategoryName != null && depC == null) 
            {
                _db.DepartmentCategories.Add(p.departmentCategory);
                _db.SaveChanges();
                depC = _db.DepartmentCategories.FirstOrDefault(s => s.DeptCategoryName == p.DeptCategoryName);
            }

            if (dep != null && dep.DeptName != p.DepName)
                dep.DeptName = p.DepName;
            if (p.DepName != null && dep == null)
            {
                p.department.DeptCategoryId = depC.DeptCategoryId;
                _db.Departments.Add(p.department);
                _db.SaveChanges();
                dep = _db.Departments.FirstOrDefault(s => s.DeptName == p.DepName);
                doc.DepartmentId= dep.DepartmentId;
                dep.DeptCategoryId = depC.DeptCategoryId;
            }


            if (exp != null && exp.Experience1 != p.Experience)
            {
                exp.Experience1 = p.Experience;
            }
            if (p.Experience != null && exp == null)
            {
                p.experience.DoctorId = p.doctor.DoctorId;
                _db.Experiences.Add(p.experience);
            }

            _db.SaveChanges();
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
            IEnumerable<Doctor> datas = null;
            if (string.IsNullOrEmpty(vModel.txtKeyword))
            {
                datas = from t in _db.Doctors
                        select t;
                        
                
            }
            else
            {
                datas = _db.Doctors.Where(t => t.DoctorName.Contains(vModel.txtKeyword) ||
                t.Education.Contains(vModel.txtKeyword) || t.JobTitle.Contains(vModel.txtKeyword));
            }
            return View(datas);
        }
        public IActionResult Details(int? id)
        {
           
            CDoctorDetailViewModel prod =new CDoctorDetailViewModel();
            Doctor DD = _db.Doctors.FirstOrDefault(t => t.DoctorId == id);
            Experience exp = _db.Experiences.FirstOrDefault(t => t.DoctorId == id);
            prod.doctor = DD;
            if(DD.DepartmentId!=null)
                prod.department = _db.Departments.FirstOrDefault(t => t.DepartmentId == prod.doctor.DepartmentId);
            if (exp!= null)
                prod.experience = _db.Experiences.FirstOrDefault(t => t.DoctorId == prod.doctor.DoctorId);
            if (DD.DepartmentId!= null)
                prod.departmentCategory = _db.DepartmentCategories.FirstOrDefault(t => t.DeptCategoryId == prod.department.DeptCategoryId);
           
            if (prod == null)
                return RedirectToAction("Index");
            return View(prod);
        }
    }
}
