using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineApp.Models
{
    public class Airline
    {
        public Airlines[] Airline { get; set; }
        public class Airlines
        { 

            public string Name { get; set; }
            public string Code { get; set; }
        }
    }
}
