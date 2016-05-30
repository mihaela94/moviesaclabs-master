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
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Description;

namespace MoviesACLabs.Controllers
{
    public class AirlinesController : ApiController
    {

        private MoviesContext db = new MoviesContext();

        // GET: api/Airlines
        public IList<AirlineModel> GetAirlines()
        {
            var airlines = db.Airlines;
            var airlinesModel = Mapper.Map<IList<AirlineModel>>(airlines);
            return airlinesModel;
        }

        // GET: api/Airlines/5
        [ResponseType(typeof(AirlineModel))]
        public IHttpActionResult GetAirline(int id)
        {
            Airline airline = db.Airlines.Find(id);
            if (airline == null)
            {
                return NotFound();
            }

            var airlineModel = Mapper.Map<AirlineModel>(airline);

            return Ok(airlineModel);
        }

        [ResponseType(typeof(IList<AirlineModel>))]
        [Route("~/airlinesWith2Vowels")]
        public IHttpActionResult GetAirlinesWith2Vowels()
        {
            //exactly 2 vowels
            var rx = new Regex("[^aeiyou]*[aeiyou][^aeiyou]*[aeiyou][^aeiyou]*", RegexOptions.IgnoreCase);
            var airlines = db.Airlines.Where((a) => rx.IsMatch(a.Name));
            // exception thrown here
            var x = airlines.ToList();
            var airlinesModel = Mapper.Map<IList<AirlineModel>>(airlines.ToList());

            return Ok(airlinesModel);

            /*
            Should try something like this (for counting vowels) and then == 2 :

            var vowels = new List<string> { "a", "e", "i", "o", "u" };
            var query = words.Select(s => new
            {
                Text = s,
                Count = s.Count(c => vowels.Exists(vowel => 
                    vowel.Equals(c.ToString(), 
                        StringComparison.InvariantCultureIgnoreCase)))
            });
            foreach (var item in query)
            {
            Console.WriteLine("String {0} contains {1} vowels", item.Text, item.Count);
            }
            */
        }

        // PUT: api/Airlines/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAirline(int id, AirlineModel airlineModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != airlineModel.Id)
            {
                return BadRequest();
            }

            var airline = Mapper.Map<Airline>(airlineModel);

            db.Entry(airline).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirlineExists(id))
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

        // POST: api/Airlines
        [ResponseType(typeof(AirlineModel))]
        public IHttpActionResult PostAirline(AirlineModel airlineModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var airline = Mapper.Map<Airline>(airlineModel);

            db.Airlines.Add(airline);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = airline.Id }, airline);
        }

        // DELETE: api/Airlines/5
        public IHttpActionResult DeleteAirline(int id)
        {
            Airline airline = db.Airlines.Find(id);
            if (airline == null)
            {
                return NotFound();
            }

            db.Airlines.Remove(airline);
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

        private bool AirlineExists(int id)
        {
            return db.Airlines.Any(e => e.Id == id);
        }

    }
}
