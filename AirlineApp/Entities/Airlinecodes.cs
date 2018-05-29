using System;
using System.Collections.Generic;

namespace AirlineApp.Entities
{
    public partial class Airlinecodes
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public Locatie Locatie { get; set; }
        public Opgericht Opgericht { get; set; }
    }
}
