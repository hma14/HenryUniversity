using HenryUniversity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HenryUniversity.DAL
{
    public class SchoolContext : DbContext, ISchoolContext
    {
        public SchoolContext() : base("name=SchoolContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public virtual IDbSet<Course> Courses { get; set; }
        public virtual IDbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Fluent API, The API is "fluent" because it's often used by 
            // stringing a series of method calls together into a single statement, 
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("InstructorID")
                    .ToTable("CourseInstructor"));
        }

       
        public override async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public void MarkAsModified(Course item)
        {
            Entry(item).State = EntityState.Modified;
        }
        public void MarkAsModified(Department item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}