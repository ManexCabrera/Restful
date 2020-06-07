using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using RESTful.Models;

namespace RESTful.Controllers
{
    public class ProfesoresController : ApiController
    {
        private DAM_Compartido_DEVEntities1 db = new DAM_Compartido_DEVEntities1();
        // GET: api/Profesores
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Profesores> Get()
        {
            return db.Profesores;
        }

        // GET: api/Profesores/5
        [ResponseType(typeof(Profesores))]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult GetProfesor(int id)
        {
            Profesores profesor = db.Profesores.Find(id);
            if (profesor == null)
            {
                return NotFound();
            }

            return Ok(profesor);
        }

        // POST: api/Profesores
        public IHttpActionResult Post([FromBody]Profesores profesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Profesores.Add(profesor);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProfesorExists(profesor.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = profesor.ID }, profesor);
        }

        // PUT: api/Profesores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, [FromBody]Profesores profesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profesor.ID)
            {
                return BadRequest();
            }

            db.Entry(profesor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(id))
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

        // DELETE: api/Profesores/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [ResponseType(typeof(Profesores))]
        public IHttpActionResult Delete(int id)
        {
            Profesores profesor = db.Profesores.Find(id);
            if (profesor == null)
            {
                return NotFound();
            }

            db.Profesores.Remove(profesor);
            db.SaveChanges();

            return Ok(profesor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfesorExists(int id)
        {
            return db.Profesores.Count(e => e.ID == id) > 0;
        }
    }
}
