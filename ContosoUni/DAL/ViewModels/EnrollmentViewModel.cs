using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class EnrollmentViewModel
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        public virtual CourseViewModel Course { get; set; }
       // public virtual StudentViewModel Student { get; set; }
    }
}
