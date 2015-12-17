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

namespace HenryUniversity.Controllers
{
    public class InstructorsController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/Instructors
        public IQueryable<Instructor> GetInstructors()
        {
            return db.Instructors;
        }

        // GET: api/Instructors/5
        [ResponseType(typeof(Instructor))]
        public async Task<IHttpActionResult> GetInstructor(int id)
        {
            Instructor instructor = await db.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            return Ok(instructor);
        }

        // PUT: api/Instructors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInstructor(int id, Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != instructor.ID)
            {
                return BadRequest();
            }

            db.Entry(instructor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(id))
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

        // POST: api/Instructors
        [ResponseType(typeof(Instructor))]
        public async Task<IHttpActionResult> PostInstructor(Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Instructors.Add(instructor);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = instructor.ID }, instructor);
        }

        // DELETE: api/Instructors/5
        [ResponseType(typeof(Instructor))]
        public async Task<IHttpActionResult> DeleteInstructor(int id)
        {
            Instructor instructor = await db.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            db.Instructors.Remove(instructor);
            await db.SaveChangesAsync();

            return Ok(instructor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstructorExists(int id)
        {
            return db.Instructors.Count(e => e.ID == id) > 0;
        }
    }
}