using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesACLabs.Entities
{
    public class Plane
    {
        public int Id { get; set; }

        public int AvailableSeats { get; set; }

        public string Model { get; set; }
    }
}