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
    public class OfficeAssignmentsController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/OfficeAssignments
        public IQueryable<OfficeAssignment> GetOfficeAssignments()
        {
            return db.OfficeAssignments;
        }

        // GET: api/OfficeAssignments/5
        [ResponseType(typeof(OfficeAssignment))]
        public async Task<IHttpActionResult> GetOfficeAssignment(int id)
        {
            OfficeAssignment officeAssignment = await db.OfficeAssignments.FindAsync(id);
            if (officeAssignment == null)
            {
                return NotFound();
            }

            return Ok(officeAssignment);
        }

        // PUT: api/OfficeAssignments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOfficeAssignment(int id, OfficeAssignment officeAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != officeAssignment.InstructorID)
            {
                return BadRequest();
            }

            db.Entry(officeAssignment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficeAssignmentExists(id))
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

        // POST: api/OfficeAssignments
        [ResponseType(typeof(OfficeAssignment))]
        public async Task<IHttpActionResult> PostOfficeAssignment(OfficeAssignment officeAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OfficeAssignments.Add(officeAssignment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OfficeAssignmentExists(officeAssignment.InstructorID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = officeAssignment.InstructorID }, officeAssignment);
        }

        // DELETE: api/OfficeAssignments/5
        [ResponseType(typeof(OfficeAssignment))]
        public async Task<IHttpActionResult> DeleteOfficeAssignment(int id)
        {
            OfficeAssignment officeAssignment = await db.OfficeAssignments.FindAsync(id);
            if (officeAssignment == null)
            {
                return NotFound();
            }

            db.OfficeAssignments.Remove(officeAssignment);
            await db.SaveChangesAsync();

            return Ok(officeAssignment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OfficeAssignmentExists(int id)
        {
            return db.OfficeAssignments.Count(e => e.InstructorID == id) > 0;
        }
    }
}