using HenryUniversity.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HenryUniversity.Models;
using System.Data.Entity;

namespace HenryUniversity.UnitTests
{
    public class TestSchoolContext : ISchoolContext
    {
        public TestSchoolContext()
        {
            this.Courses = new TestCourseDbSet();
            this.Departments = new TestDepartmentDbSet();
        }
        public System.Data.Entity.IDbSet<Course> Courses { get; set; }

        public System.Data.Entity.IDbSet<Department> Departments { get; set; }


        public System.Data.Entity.DbSet<Enrollment> Enrollments { get; set; }


        public System.Data.Entity.DbSet<Instructor> Instructors { get; set; }

        public System.Data.Entity.DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        public System.Data.Entity.DbSet<Student> Students { get; set; }

        public Task<int> SaveChangesAsync() { return null;  }
        public void Dispose() { }

        public async Task<Course> FindAsync(int id)
        {
            return await ((DbSet<Course>)this.Courses).FindAsync(id);
        }

        public void MarkAsModified(Course item) { }
        public void MarkAsModified(Department item) { }
    }
}
