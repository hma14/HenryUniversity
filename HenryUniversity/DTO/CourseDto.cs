using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HenryUniversity.DTO
{
    public class CourseDto
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public string DepartmentName { get; set; }
    }
}