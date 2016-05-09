using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoviesACLabs.Models;

namespace MoviesACLabs.Controllers
{
    public class AwardsController : ApiController
    {
        // valoarea nu ramane modificata dupa apel (stateless)
        //private int counter1 = 0;
        // valoarea ramane modificata
        //private static int counter2 = 0;

        private static int id = 0;

        private static IList<AwardModel> awards = new List<AwardModel>{
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
        };

        [Route("~/myawards")]
        public IList<AwardModel> GetAwards()
        {
            return awards;
        }

        public void PostAward(AwardModel awardModel)
        {
            awardModel.Id = id++;
            awards.Add(awardModel);
        }

        public void PutAward(AwardModel award)
        {
            for (int i = 0; i < awards.Count; i++)
            {
                if (award.Id == awards.ElementAt(i).Id)
                {
                    awards.RemoveAt(i);
                    awards.Insert(i, award);
                    break;
                }
            }
        }

        [Route("~/myaward/{id}")]
        public AwardModel GetAward(int id)
        {
            foreach (AwardModel am in awards)
            {
                if(am.Id == id) { return am; }
            }

            return null;

            //counter1++;
            //counter2++;
        }

        public void DeleteAward(int id)
        {
            for(int i = 0; i < awards.Count; i++)
            {
                if(awards.ElementAt(i).Id == id)
                {
                    awards.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
