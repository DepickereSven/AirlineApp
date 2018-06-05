using System;
using System.Collections.Generic;

namespace AirlineApp.Entities
{
    public partial class FlightData
    {
        public int FlightId { get; set; }
        public string AirlineCode { get; set; }
        public DateTime Date { get; set; }
        public string DepatureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string DepatureState { get; set; }
        public string ArrivalState { get; set; }
        public double DepartureLatitude { get; set; }
        public double ArrivalLatitude { get; set; }
        public double DepatureLongitude { get; set; }
        public double ArrivalLongitude { get; set; }

        public Airlinecodes AirlineCodeNavigation { get; set; }
    }
}
