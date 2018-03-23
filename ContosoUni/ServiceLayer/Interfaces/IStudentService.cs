using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Models;
using DAL.ViewModels;


namespace ServiceLayer.Interfaces
{
    public interface IStudentService
    {
        StudentViewModel GetStudentById(int studentId);
        List<StudentViewModel> GetStudents();
       
        void SaveStudent(StudentViewModel student);
        void DeleteStudent(int studentId);

        List<CourseViewModel> GetEnrollmentsAll();

    }
}