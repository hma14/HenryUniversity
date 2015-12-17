using HenryUniversity.DTO;
using HenryUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenryUniversity.UnitTests
{
    public class TestCourseDbSet : TestDbSet<Course>
    {
        public override Course Find(params object[] keyValues)
        {
            //return await this.SingleOrDefault(c => c.CourseID == (int)keyValues.Single());
            return this.SingleOrDefault(c => c.CourseID == (int)keyValues.Single());
        }

        public override async Task<Course> FindAsync(params object[] keyValues)
        {
            return this.SingleOrDefault(c => c.CourseID == (int)keyValues.Single());
        }

    }

    //public class TestCourseDtoDbSet : TestDbSet<CourseDto>
    //{
    //    public override CourseDto Find(params object[] keyValues)
    //    {
    //        return this.SingleOrDefault(c => c.CourseID == (int)keyValues.Single());
    //    }
    //    public override async Task<CourseDto> FindAsync(params object[] keyValues)
    //    {
    //        return this.SingleOrDefault(c => c.CourseID == (int)keyValues.Single());
    //    }

    //}
}
