using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DAL.ViewModels
{
    public class StudentViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter last name.")]
        [MaxLength(25, ErrorMessage = "Maximum length exceeded")]
        [RegularExpression("[A-Za-z ]*", ErrorMessage = "Invalid last name! ")]
        public string LastName { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter your name.")]
        [MaxLength(30, ErrorMessage = "Maximum length exceeded")]
        [RegularExpression("[A-Za-z ]*", ErrorMessage = "Invalid first name! ")]
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
