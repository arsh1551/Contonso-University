using DAL.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DAL.ViewModels;
using System.Collections.Generic;
using System;
using ServiceLayer.Interfaces;

namespace ContosoUni.Controllers
{
    public class StudentController : Controller
    {
        #region initialize
        IStudentService _StudentService = null;
        public StudentController(IStudentService objStudentService)
        {
            _StudentService = objStudentService;
        }
        #endregion

        #region CRUD

        /// <summary>
        /// Display Student listing along with Enrollments.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            List<StudentViewModel> listStudentViewModel = _StudentService.GetStudents();
            CreateSummaryOfNames(listStudentViewModel);
            return View(listStudentViewModel);
        }

        ///// <summary>
        /////  Save Student form 
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult Create()
        //{
        //    StudentViewModel objStudentViewModel = new StudentViewModel();
        //    objStudentViewModel.Enrollments = _StudentService.GetEnrollmentsAll();
        //    return View(objStudentViewModel);
        //}

        ///// <summary>
        ///// Save new Student.
        ///// </summary>
        ///// <param name="StudentViewModel"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "StudentId,StudentName,StudentPhone,StudentAddress,specializationIds")] StudentViewModel StudentViewModel)
        //{
        //    if (StudentViewModel.specializationIds == null)
        //    {
        //        ModelState.AddModelError("Enrollments", "Please select specialization");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        _StudentService.SaveStudent(StudentViewModel);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        StudentViewModel.Enrollments = _StudentService.GetEnrollmentsAll();
        //        return View(StudentViewModel);
        //    }
        //}

        ///// <summary>
        ///// Edit Student form
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return View("BadRequest");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            StudentViewModel StudentViewModel = _StudentService.GetStudentById(id.Value);
        //            CreateSummaryOfIds(StudentViewModel);
        //            return View(StudentViewModel);
        //        }
        //        catch
        //        {
        //            throw;
        //        }

        //    }

        //}

        ///// <summary>
        ///// Update Student alongwith Enrollments.
        ///// </summary>
        ///// <param name="StudentViewModel"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "StudentId,StudentName,StudentPhone,StudentAddress,specializationIds")] StudentViewModel StudentViewModel)
        //{
        //    if (StudentViewModel.specializationIds == null)
        //    {
        //        ModelState.AddModelError("Enrollments", "Please select specialization");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _StudentService.SaveStudent(StudentViewModel);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        StudentViewModel.Enrollments = _StudentService.GetEnrollmentsAll();
        //        return View(StudentViewModel);
        //    }
        //}

        ///// <summary>
        ///// Delete an Student
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {

            try
            {
                _StudentService.DeleteStudent(id);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }


        }
        #endregion

        #region Common

        public void CreateSummaryOfNames(List<StudentViewModel> listStudentViewModel)
        {
            foreach (StudentViewModel objStudentViewModel in listStudentViewModel)
            {
                objStudentViewModel.CourseSummary = (objStudentViewModel.Enrollments.Count > 0) ? string.Join(",", objStudentViewModel.Enrollments.Select(e => e.Course.Title).ToList()) : "Not specified";

            }
        }
        public void CreateSummaryOfIds(StudentViewModel StudentViewModel)
        {
            StudentViewModel.CourseSummary = (StudentViewModel.Enrollments.Count > 0) ? string.Join(",", StudentViewModel.Enrollments.Select(e => e.Course.CourseID).ToList()) : "Not specified";
        }
        #endregion

    }
}
