using System;
using System.Collections.Generic;

namespace AirlineApp.Entities
{
    public partial class Locatie
    {
        public string AirlineCode { get; set; }
        public string StadHoofkwartier { get; set; }
        public string StaatHoofkwartier { get; set; }
        public string MainHub { get; set; }
        public string StaatMainHub { get; set; }

        public Airlinecodes AirlineCodeNavigation { get; set; }
    }
}
