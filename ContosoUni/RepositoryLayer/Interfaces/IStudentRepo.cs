using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Models;
using DAL.ViewModels;


namespace RepositoryLayer.Interfaces
{
    public interface IStudentRepo
    {
        Student GetStudentById(int StudentId);
        List<Student> GetStudents();
        
        void SaveStudent(Student Student);       
        void DeleteStudent(int studentId);
        List<Enrollment> GetStudentEnrollments(StudentViewModel StudentViewModel);
        List<Course> GetEnrollmentsAll();

    }
}