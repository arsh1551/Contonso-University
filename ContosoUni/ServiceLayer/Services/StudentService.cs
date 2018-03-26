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

        public void UpdateStudent(List<int> selectedCourses, StudentViewModel studentViewModel)
        {
          StudentViewModel updatedStudentViewModel=  UpdateStudentEnrollments(selectedCourses, studentViewModel);


            try
            {
                Student objStudent = new Student()
                {

                    ID = updatedStudentViewModel.ID,
                    LastName = updatedStudentViewModel.LastName,
                    FirstMidName = updatedStudentViewModel.FirstMidName,
                    Enrollments = updatedStudentViewModel.Enrollments.Select(e => new Enrollment { EnrollmentID = e.EnrollmentID, CourseID = e.CourseID, StudentID = e.StudentID,
                        Course = new Course { CourseID = e.Course.CourseID, Title = e.Course.Title, Credits = e.Course.Credits } }).ToList()
                    // Enrollments = a.Enrollments
                };
                
                _StudentRepo.SaveStudent(objStudent);
            }
            catch (Exception e)
            {

            }

        }

        private StudentViewModel UpdateStudentEnrollments(List<int> selectedCourses, StudentViewModel studentViewModel)
        {
            Student student = _StudentRepo.GetStudentById(studentViewModel.ID);
            List<EnrollmentViewModel> studentExistingEnrollments = student.Enrollments.Select(e => new EnrollmentViewModel
            {
                EnrollmentID = e.EnrollmentID,
                CourseID = e.CourseID,
                StudentID = e.StudentID,
                Course = new CourseViewModel { CourseID = e.Course.CourseID, Title = e.Course.Title, Credits = e.Course.Credits }
            }).ToList();
            studentViewModel.Enrollments= studentExistingEnrollments;

            if (selectedCourses == null)
            {
                studentViewModel.Enrollments = new List<EnrollmentViewModel>();
                return studentViewModel;
            }
            var AvailableCourses = _StudentRepo.GetEnrollmentsAll();
            var selectedCoursesHS = new HashSet<int>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (studentExistingEnrollments.Select(e => e.Course.CourseID));
            foreach (var course in AvailableCourses)
            {
                if (selectedCoursesHS.Contains(course.CourseID))
                {
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        EnrollmentViewModel newEnrollment = new EnrollmentViewModel();
                        newEnrollment.Course = new CourseViewModel { CourseID = course.CourseID, Title = course.Title, Credits = course.Credits };
                        newEnrollment.CourseID = course.CourseID;
                        newEnrollment.StudentID = student.ID;
                        


                        studentViewModel.Enrollments.Add(newEnrollment);
                    }
                }
                else
                {

                    if (instructorCourses.Contains(course.CourseID))
                    {
                        EnrollmentViewModel deleteEnrollmentViewModel = studentViewModel.Enrollments.Where(e => e.Course.CourseID == course.CourseID).FirstOrDefault();
                        studentViewModel.Enrollments.Remove(deleteEnrollmentViewModel);

                    }
                }
                
            }
            return studentViewModel;
        }

        public StudentViewModel GetStudentById(int studentId)
        {

            Student Student = _StudentRepo.GetStudentById(studentId);
            if (Student != null)
            {
                StudentViewModel StudentViewModel = new StudentViewModel
                {
                    ID = Student.ID,
                    FirstMidName = Student.FirstMidName,
                    LastName = Student.LastName,

                   
                    SelectedCourses = Student.Enrollments.Select(x => x.Course.CourseID).ToList(),
                    AvailableCourses = _StudentRepo.GetEnrollmentsAll().Select(s => new CourseViewModel { CourseID = s.CourseID, Title = s.Title,Credits=s.Credits }).ToList()

                };
                return StudentViewModel;
            }
            //else part later
            else
            {
                return null;
            }
        }

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
