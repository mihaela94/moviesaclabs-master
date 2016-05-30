using AutoMapper;
using MoviesACLabs.Data;
using MoviesACLabs.Entities;
using MoviesACLabs.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MoviesACLabs.Controllers
{
    public class PlanesController : ApiController
    {

        private MoviesContext db = new MoviesContext();

        // GET: api/Planes
        public IList<PlaneModel> GetPlanes()
        {
            var planes = db.Planes;
            var planesModel = Mapper.Map<IList<PlaneModel>>(planes);
            return planesModel;
        }

        // GET: api/Planes/5
        [ResponseType(typeof(PlaneModel))]
        public IHttpActionResult GetPlane(int id)
        {
            Plane plane = db.Planes.Find(id);
            if (plane == null)
            {
                return NotFound();
            }

            var planeModel = Mapper.Map<PlaneModel>(plane);

            return Ok(planeModel);
        }

        // PUT: api/Planes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlane(int id, PlaneModel planeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != planeModel.Id)
            {
                return BadRequest();
            }

            var plane = Mapper.Map<Plane>(planeModel);

            db.Entry(plane).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaneExists(id))
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

        // POST: api/Planes
        [ResponseType(typeof(PlaneModel))]
        public IHttpActionResult PostPlane(PlaneModel planeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var plane = Mapper.Map<Plane>(planeModel);

            db.Planes.Add(plane);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = plane.Id }, plane);
        }

        // DELETE: api/Planes/5
        public IHttpActionResult DeletePlane(int id)
        {
            Plane plane = db.Planes.Find(id);
            if (plane == null)
            {
                return NotFound();
            }

            db.Planes.Remove(plane);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlaneExists(int id)
        {
            return db.Planes.Any(e => e.Id == id);
        }

    }
}
