using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HenryUniversity.DAL;
using HenryUniversity.Models;
using HenryUniversity.DTO;


namespace HenryUniversity.Controllers
{
    public class CoursesController : ApiController
    {
        private ISchoolContext db = new SchoolContext();
        public CoursesController()
        {
                
        }
        public CoursesController(ISchoolContext context)
        {
            this.db = context;
        }

        // GET: api/Courses
        public IQueryable<CourseDto> GetCourses()
        {
#if true
            var result = (from c in db.Courses.Include(d => d.Department)                         
                          select new CourseDto
                          {
                              CourseID = c.CourseID,
                              Title = c.Title,
                              Credits = c.Credits,
                              DepartmentName = c.Department.Name
                          }).AsQueryable();
#else
            var result = (from c in db.Courses.Include(d => d.Department)
                          from d in db.Departments
                          where c.DepartmentID == d.DepartmentID
                          select new CourseDto
                          {
                              CourseID = c.CourseID,
                              Title = c.Title,
                              Credits = c.Credits,
                              DepartmentName = d.Name
                          }).AsQueryable();
#endif
            return result;
        }

        // GET: api/Courses/5
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> GetCourse(int id)
        {
            Course course = await ((DbSet<Course>)db.Courses).FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // PUT: api/Courses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCourse(int id, Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course.CourseID)
            {
                return BadRequest();
            }

            db.MarkAsModified(course);
            //db.Entry(course).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Courses
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> PostCourse(Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Courses.Add(course);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseExists(course.CourseID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = course.CourseID }, course);
        }

        // DELETE: api/Courses/5
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> DeleteCourse(int id)
        {
            Course course = await ((DbSet<Course>)db.Courses).FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            db.Courses.Remove(course);
            await db.SaveChangesAsync();

            return Ok(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourseExists(int id)
        {
            return db.Courses.Count(e => e.CourseID == id) > 0;
        }
    }
}