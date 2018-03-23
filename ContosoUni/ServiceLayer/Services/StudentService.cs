using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using ServiceLayer.Interfaces;
using RepositoryLayer.Interfaces;
using System.Data.Entity;
using DAL.ViewModels;

namespace ServiceLayer.Services
{
    public class StudentService : IStudentService
    {
        public IStudentRepo _StudentRepo = null;
        public StudentService(IStudentRepo objStudentRepo)
        {
            _StudentRepo = objStudentRepo;

        }

        public List<StudentViewModel> GetStudents()
        {

            try
            {
                List<StudentViewModel> listStudentViewModel = _StudentRepo.GetStudents().Select(a => new StudentViewModel
                {
                    ID = a.ID,
                    FirstMidName = a.FirstMidName,
                    LastName = a.LastName,
                    EnrollmentDate = a.EnrollmentDate,
                    Enrollments = a.Enrollments
                  .Select(e => new EnrollmentViewModel { EnrollmentID = e.EnrollmentID, CourseID = e.CourseID, StudentID = e.StudentID,
                      Course = new CourseViewModel { CourseID = e.Course.CourseID, Title = e.Course.Title, Credits = e.Course.Credits } }).ToList()
                    // Enrollments = a.Enrollments
                }).ToList();

                return listStudentViewModel;
            }
            catch
            {
                throw;

            }

        }

        public  void SaveStudent(StudentViewModel student)
        {
            try
            {
                Student objStudent = new Student()
                {

                    ID = student.ID,
                    LastName = student.LastName,
                    FirstMidName = student.FirstMidName,
                    Enrollments = _StudentRepo.GetStudentEnrollments(student)
                };
                _StudentRepo.SaveStudent(objStudent);
            }
            catch (Exception e)
            {

            }

        }

        //public StudentViewModel GetStudentById(int StudentId)
        //{

        //    Student Student = _StudentRepo.GetStudentById(StudentId);
        //    if (Student != null)
        //    {
        //        StudentViewModel StudentViewModel = new StudentViewModel
        //        {
        //            StudentId = Student.StudentId,
        //            StudentName = Student.StudentName,
        //            StudentAddress = Student.StudentAddress,
        //            StudentPhone = Student.StudentPhone,
        //            specializationIds = Student.Specializations.Select(x => x.SpecializationId).ToList(),
        //            Specializations = _StudentRepo.GetSpecializaionsAll().Select(s => new SpecializationViewModel { SpecializationId = s.SpecializationId, SpecializationName = s.SpecializationName }).ToList()

        //        };
        //        return StudentViewModel;
        //    }
        //    //else part later
        //    else
        //    {
        //        return null;
        //    }
        //}

        public void DeleteStudent(int StudentId)
        {

            try
            {
                _StudentRepo.DeleteStudent(StudentId);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public List<CourseViewModel> GetEnrollmentsAll()
        {
            List<CourseViewModel> listCoursesAll = _StudentRepo.GetEnrollmentsAll().Select(e=>           
                new CourseViewModel { CourseID = e.CourseID, Title = e.Title, Credits = e.Credits }
            ).ToList();
            return listCoursesAll;
        }
    }
}
