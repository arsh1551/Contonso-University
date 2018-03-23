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
        public IStudentRepo _studentRepo = null;
        public StudentService(IStudentRepo objStudentRepo)
        {
            _studentRepo = objStudentRepo;

        }

        public List<StudentViewModel> GetStudents()
        {

            try
            {
                List<StudentViewModel> listStudentViewModel = _studentRepo.GetStudents().Select(a => new StudentViewModel
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

        //public void SaveAssociate(AssociateViewModel associateViewModel)
        //{
        //    try
        //    {
        //        Associate objAssociate = new Associate()
        //        {

        //            AssociateId = associateViewModel.AssociateId,
        //            AssociateName = associateViewModel.AssociateName,
        //            AssociateAddress = associateViewModel.AssociateAddress,
        //            AssociatePhone = associateViewModel.AssociatePhone,
        //            Specializations = _associateRepo.GetSpecializaions(associateViewModel)
        //        };
        //        _associateRepo.SaveAssociate(objAssociate);
        //    }
        //    catch (Exception e)
        //    {

        //    }

        //}

        //public AssociateViewModel GetAssociateById(int associateId)
        //{

        //    Associate associate = _associateRepo.GetAssociateById(associateId);
        //    if (associate != null)
        //    {
        //        AssociateViewModel associateViewModel = new AssociateViewModel
        //        {
        //            AssociateId = associate.AssociateId,
        //            AssociateName = associate.AssociateName,
        //            AssociateAddress = associate.AssociateAddress,
        //            AssociatePhone = associate.AssociatePhone,
        //            specializationIds = associate.Specializations.Select(x => x.SpecializationId).ToList(),
        //            Specializations = _associateRepo.GetSpecializaionsAll().Select(s => new SpecializationViewModel { SpecializationId = s.SpecializationId, SpecializationName = s.SpecializationName }).ToList()

        //        };
        //        return associateViewModel;
        //    }
        //    //else part later
        //    else
        //    {
        //        return null;
        //    }
        //}
        
        public void DeleteStudent(int studentId)
        {

            try
            {
                _studentRepo.DeleteStudent(studentId);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        //public List<SpecializationViewModel> GetSpecializationsAll()
        //{
        //    List<SpecializationViewModel> listSpecializations= _associateRepo.GetSpecializaionsAll().Select(s => new SpecializationViewModel { SpecializationId = s.SpecializationId, SpecializationName = s.SpecializationName }).ToList();
        //    return listSpecializations;
        //}
    }
}
