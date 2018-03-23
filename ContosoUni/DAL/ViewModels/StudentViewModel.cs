using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.ViewModels
{
    public class StudentViewModel
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        //student specific
        public virtual ICollection<EnrollmentViewModel> Enrollments { get; set; }

        public string CourseSummary { get; set; }

        //posted course id's from dropdown
        public List<int> SelectedCourses { get; set; }

        //All avialble courses in db: bind with dropdown
        public List<CourseViewModel> AvailableCourses { get; set; }




    }
}
