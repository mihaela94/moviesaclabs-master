using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoviesACLabs.Models;
using MoviesACLabs.Data;
using AutoMapper;
using MoviesACLabs.Entities;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MoviesACLabs.Controllers
{
    public class AwardsController : ApiController
    {
        // valoarea nu ramane modificata dupa apel (stateless)
        //private int counter1 = 0;
        // valoarea ramane modificata
        //private static int counter2 = 0;

        //private static int id = 0;

        private MoviesContext db = new MoviesContext();

        /*private static IList<AwardModel> awards = new List<AwardModel>{
            new AwardModel
            {
                Date = DateTime.Now,
                Id = 1,
                ActorId = 1,
                AwardTitle = "Oscar"
            },
            new AwardModel
            {
                Date = DateTime.Now,
                Id = 2,
                ActorId = 2,
                AwardTitle = "Oscar"
            },
            new AwardModel
            {
                Date = DateTime.Now,
                Id = 3,
                ActorId = 1,
                AwardTitle = "Bla"
            }
        };*/

        // GET /myawards 
        [Route("~/myawards")]
        public IList<AwardModel> GetAwards()
        {
            var awards = db.Awards;
            var awardsModel = Mapper.Map<IList<AwardModel>>(awards);
            return awardsModel;
        }

        // GET /myaward/id
        [Route("~/myaward/{id}")]
        public IHttpActionResult GetAward(int id)
        {
            Award award = db.Awards.Find(id);
            if(award == null)
            {
                return NotFound();
            }

            var awardModel = Mapper.Map<AwardModel>(award);

            return Ok(awardModel);
        }

        // GET /filter/name
        [HttpGet]
        [Route("~/filter/{name}")]
        public IHttpActionResult GetAward(String name)
        {
            var awards = db.Awards.Where((x) => x.AwardTitle == name);
            var awardsModel = Mapper.Map<IList<AwardModel>>(awards.ToList());

            //daca nu se gaseste nimic?

            return Ok(awardsModel);
        }

        // PUT /api/Awards/id
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAward(int id, AwardModel awardModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != awardModel.Id)
            {
                return BadRequest();
            }

            var award = Mapper.Map<Award>(awardModel);

            db.Entry(award).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Awards.Any(e => e.Id == id))
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

        [ResponseType(typeof(AwardModel))]
        public IHttpActionResult PostAward(AwardModel awardModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var award = Mapper.Map<Award>(awardModel);

            db.Awards.Add(award);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = award.Id }, award);
        }

        public IHttpActionResult DeleteAward(int id)
        {
            Award award = db.Awards.Find(id);
            if (award == null)
            {
                return NotFound();
            }

            db.Awards.Remove(award);
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
    }
}
