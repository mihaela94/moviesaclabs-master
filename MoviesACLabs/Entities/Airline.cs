﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesACLabs.Entities
{
    public class Airline
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CountryOfOrigin { get; set; }

        //should have list of Planes
    }
}