using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.ViewModels;

namespace DAL.ViewModels
{
    public class CourseViewModel
    {
       
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        //public virtual ICollection<EnrollmentViewModel> Enrollments { get; set; }
    }
}
