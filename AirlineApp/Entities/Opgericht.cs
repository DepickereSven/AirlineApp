using System;
using System.Collections.Generic;

namespace AirlineApp.Entities
{
    public partial class Opgericht
    {
        public string AirlineCode { get; set; }
        public string Opgericht1 { get; set; }
        public string Gestopt { get; set; }

        public Airlinecodes AirlineCodeNavigation { get; set; }
    }
}
