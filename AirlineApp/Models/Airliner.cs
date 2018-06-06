using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineApp.Models
{
    public class Airliner
    {
        public Airlines[] Airline { get; set; }
        public class Airlines
        {

            public string AirlineName { get; set; }
            public string AirlineCode { get; set; }
        }
    }
}
