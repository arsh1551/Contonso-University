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
        //AssociateViewModel GetAssociateById(int associateId);
        List<StudentViewModel> GetStudents();
        //void SaveAssociate(AssociateViewModel associateViewModel);
        void DeleteStudent(int studentId);
        
        //List<SpecializationViewModel> GetSpecializationsAll();

    }
}