﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesACLabs.Entities
{
    public class Award
    {
        public int Id { get; set; }

        public int ActorId { get; set; }

        /*public ActorModel Actor { get; set; }*/

        public string AwardTitle { get; set; }

        public DateTime Date { get; set; }
    }
}