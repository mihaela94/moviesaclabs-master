using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MoviesACLabs.Data;
using MoviesACLabs.Models;
using AutoMapper;
using MoviesACLabs.Entities;

namespace MoviesACLabs.Controllers
{
    public class ActorsController : ApiController
    {
        private MoviesContext db = new MoviesContext();

        // GET: api/Actors
        public IList<ActorModel> GetActors()
        {
            var actors = db.Actors;
            var actorsModel = Mapper.Map<IList<ActorModel>>(actors);
            return actorsModel;
        }
        
        // GET: api/Actors/5
        [ResponseType(typeof(ActorModel))]
        public IHttpActionResult GetActor(int id)
        {
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return NotFound();
            }

            var actorModel = Mapper.Map<ActorModel>(actor);

            return Ok(actorModel);
        }

        [ResponseType(typeof(ActorModel))]
        [Route("~/actorsWithLongestNameAward")]
        public IHttpActionResult GetActorHavingAwardWithLongestName()
        {
            var actors = db.Actors.Include(a => a.Awards);
            var actorsList = actors.ToList();

            var x = actorsList.Select(e => new { Name = e.Name, AwardName = e.Awards.Where(a => a.AwardTitle.Length == e.Awards.Max(at => at.AwardTitle.Length)).Select(a => a.AwardTitle).FirstOrDefault()});

            var actorsModel = Mapper.Map<IList<ActorModel>>(actorsList);
            

            //daca nu se gaseste nimic?

            return Ok(actorsModel);
        }

        // PUT: api/Actors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutActor(int id, ActorModel actorModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actorModel.Id)
            {
                return BadRequest();
            }

            var actor = Mapper.Map<Actor>(actorModel);

            db.Entry(actor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
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

        // POST: api/Actors
        [ResponseType(typeof(ActorModel))]
        public IHttpActionResult PostActor(ActorModel actorModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actor = Mapper.Map<Actor>(actorModel);

            db.Actors.Add(actor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = actor.Id }, actor);
        }

        // DELETE: api/Actors/5
        public IHttpActionResult DeleteActor(int id)
        {
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return NotFound();
            }

            db.Actors.Remove(actor);
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

        private bool ActorExists(int id)
        {
            return db.Actors.Any(e => e.Id == id);
        }
    }
}