using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HenryUniversity.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        [Required]
        public int Credits { get; set; }

        [Required]
        public int DepartmentID { get; set; }

        public  Department Department { get; set; }
        public  ICollection<Enrollment> Enrollments { get; set; }
        public  ICollection<Instructor> Instructors { get; set; }
    }
}