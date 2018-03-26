using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
using RepositoryLayer.Interfaces;
using System.Data.Entity;

namespace RepositoryLayer.Repositories
{
    public class StudentRepository : IStudentRepo
    {
        #region Initialize
        UnityOfWork uow = null;
        SchoolContext schoolContext;
        public StudentRepository()
        {
            if (uow == null)
            {
                schoolContext = new SchoolContext();
                uow = new UnityOfWork(schoolContext);
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                this.uow.Dispose();
                this.schoolContext.Database.Connection.Close();
                this.schoolContext.Dispose();
            }
            // free native resources
        }

        #endregion


        public List<Student> GetStudents()
        {

            try
            {
                List<Student> listStudent = uow.Repository<Student>().GetAll().ToList();
                return listStudent;
            }
            catch
            {
                throw;

            }

        }


        public void SaveStudent(Student Student)
        {
            try
            {
                if (Student.ID == 0)
                {
                    Student.EnrollmentDate = DateTime.Now;
                    uow.Repository<Student>().Add(Student);
                    uow.SaveChanges();
                }

                else
                {
                    var studentDB = uow.Repository<Student>().AsQuerable().Where(s => s.ID == Student.ID)
            .Include(s => s.Enrollments)
            .SingleOrDefault();
                    studentDB.LastName = Student.LastName;
                    studentDB.FirstMidName = Student.FirstMidName;
                    foreach (var enrollment in Student.Enrollments)
                    {
                        var studentEnrollmentDB = studentDB.Enrollments
                            .Where(c => c.CourseID == enrollment.CourseID && c.StudentID == enrollment.StudentID && c.EnrollmentID != 0)
                            .SingleOrDefault();
                        if (studentEnrollmentDB != null)
                        {
                            //Add according to future requirements.
                        }
                        else
                        {
                            enrollment.EnrollmentID = 0;
                            studentDB.EnrollmentDate = DateTime.Now;
                            studentDB.Enrollments.Add(enrollment);
                        }
                    }
                    foreach (var enrollmentDB in
                                 studentDB.Enrollments.Where(c => c.EnrollmentID != 0).ToList())
                    {

                        if (!Student.Enrollments.Any(e => e.CourseID == enrollmentDB.CourseID && e.StudentID == enrollmentDB.StudentID))
                            uow.Repository<Enrollment>().Delete(enrollmentDB);

                    }
                    uow.SaveChanges();

                }
            }
            catch (Exception e)
            {

            }

        }

        public Student GetStudentById(int studentId)
        {
            try
            {
                Student Student = uow.Repository<Student>().AsQuerable().Where(s => s.ID == studentId)
            .Include(s => s.Enrollments)
            .SingleOrDefault();
                return Student;
            }
            catch
            {
                throw;

            }

        }


        public void DeleteStudent(int studentId)
        {
            try
            {
                Student Student = GetStudentById(studentId);
                uow.Repository<Student>().Delete(Student);
                uow.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public List<Enrollment> GetStudentEnrollments(StudentViewModel StudentViewModel)
        {
            List<Course> listCourses = uow.Repository<Course>().AsQuerable().Where(c => StudentViewModel.SelectedCourses.Contains(c.CourseID)).ToList();
            List<Enrollment> listEnrollments = new List<Enrollment>();
            foreach (Course course in listCourses)
            {
                listEnrollments.Add(new Enrollment
                {
                    StudentID = StudentViewModel.ID,
                    CourseID = course.CourseID,
                    Course = course
                });
            }
            return listEnrollments;

        }

        public List<Course> GetEnrollmentsAll()
        {
            return uow.Repository<Course>().GetAll().ToList();
        }

    }
}
