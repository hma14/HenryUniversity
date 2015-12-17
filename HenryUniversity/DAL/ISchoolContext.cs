using HenryUniversity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HenryUniversity.DAL
{
    public interface ISchoolContext : IDisposable
    {
        IDbSet<Course> Courses { get; set; }
        IDbSet<Department> Departments { get; set; }
        DbSet<Enrollment> Enrollments { get; set; }
        DbSet<Instructor> Instructors { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        Task<int> SaveChangesAsync();

        void MarkAsModified(Course item);
        void MarkAsModified(Department item);
    }
}