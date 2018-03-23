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
                // List<Student> userList = uow.Repository<Student>().GetAll().ToList();
                List<Student> listStudent = schoolContext.Students.Include(a => a.Enrollments).ToList();
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
                    //Student objStudent = GetStudentById(Student.StudentId);


                    //objStudent.StudentId = Student.StudentId;
                    //objStudent.StudentName = Student.StudentName;
                    //objStudent.StudentAddress = Student.StudentAddress;
                    //objStudent.StudentPhone = Student.StudentPhone;
                    //objStudent.Enrollments = Student.Enrollments;
                    //uow.SaveChanges();

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
                Student Student = schoolContext.Students.Where(a => a.ID == studentId).Include(x => x.Enrollments).FirstOrDefault();
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
                schoolContext.Students.Remove(Student);
                schoolContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public List<Enrollment> GetStudentEnrollments(StudentViewModel StudentViewModel)
        {
            List<Course> listCourses = schoolContext.Courses.Where(c => StudentViewModel.SelectedCourses.Contains(c.CourseID)).ToList();
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
            return schoolContext.Courses.ToList();
        }

    }
}
