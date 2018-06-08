using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AirlineApp.Data;
using AirlineApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static AirlineApp.Models.Airliner;

namespace AirlineApp.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class FlightDataController : Controller
    {
        private readonly ApplicationDbContext db;

        public FlightDataController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet("code/{id}")]
        public IActionResult Get(string id)
        {
            if (id == "all")
            {
                var item = db.Airlinecodes
                    .Select(x => new
                    {
                        AirlineCode = x.Code,
                        AirlineName = x.Name
                    })
                    .ToArray();
                return Ok(item);
            }
            else
            {
                var item = db.Airlinecodes
                    .Find(id);
                var newItem = (new Airlines
                {
                    AirlineCode = item.Code,
                    AirlineName = item.Name
                });
                if (item == null)
                {
                    return Ok($"Airline with like {id} don't exist");
                }
                return Ok(newItem);
            }
        }

        [HttpGet("date/{dateStamp}")]
        public IActionResult GetStamp(string dateStamp)
        {
            var item = db.FlightData
                .Where(x => x.Date == DateTime.Parse(dateStamp))
                .Select(x => new
                {
                    date = x.Date.ToString("d", CultureInfo.CreateSpecificCulture("ja-JP")),
                    AirlineCode = x.AirlineCode,
                    Departure = new
                    {
                        Airport = x.DepatureAirport,
                        State = x.DepatureState,
                        Latitude = x.DepartureLatitude,
                        Longitude = x.DepatureLongitude
                    },
                    Arrival = new
                    {
                        Airport = x.ArrivalAirport,
                        State = x.ArrivalState,
                        Latitude = x.ArrivalLatitude,
                        Longitude = x.ArrivalLongitude
                    }
                });
            if (item.Count() == 0)
            {
                return Ok($"The date you give enterd: {dateStamp} isn't between: 2011-01-01 - 2011-01-03");
            }
            return Ok(item);
        }


        [HttpGet("state/{depatureOrArrival}/{stateName}")]
        public IActionResult GetStates(string depatureOrArrival, string stateName)
        {
            if (depatureOrArrival == "DS" | depatureOrArrival == "ds")
            {
                var item = db.FlightData
                    .Where(x => x.DepatureState == stateName)
                    .Select(x => new
                    {
                        date = x.Date.ToString("d", CultureInfo.CreateSpecificCulture("ja-JP")),
                        AirlineCode = x.AirlineCode,
                        Departure = new
                        {
                            Airport = x.DepatureAirport,
                            State = x.DepatureState,
                            Latitude = x.DepartureLatitude,
                            Longitude = x.DepatureLongitude
                        },
                        Arrival = new
                        {
                            Airport = x.ArrivalAirport,
                            State = x.ArrivalState,
                            Latitude = x.ArrivalLatitude,
                            Longitude = x.ArrivalLongitude
                        }
                    });
                if (item.Count() == 0)
                {
                    return Ok($"The state for depature: {stateName} you gave up don't have any records");
                }
                return Ok(item);
            }
            else
            {
                if (depatureOrArrival == "AS" | depatureOrArrival == "as")
                {
                    var item = db.FlightData
                    .Where(x => x.ArrivalState == stateName)
                    .Select(x => new
                    {
                        date = x.Date.ToString("d", CultureInfo.CreateSpecificCulture("ja-JP")),
                        AirlineCode = x.AirlineCode,
                        Departure = new
                        {
                            Airport = x.DepatureAirport,
                            State = x.DepatureState,
                            Latitude = x.DepartureLatitude,
                            Longitude = x.DepatureLongitude
                        },
                        Arrival = new
                        {
                            Airport = x.ArrivalAirport,
                            State = x.ArrivalState,
                            Latitude = x.ArrivalLatitude,
                            Longitude = x.ArrivalLongitude
                        }
                    });
                    if (item.Count() == 0)
                    {
                        return Ok($"The state for arrival: {stateName} you gave up don't have any records");
                    }
                    return Ok(item);
                }
                else
                {
                    return Ok($"You entered {depatureOrArrival} that isn't equal to DS or AS");
                }
            }
        }

        [HttpGet("info/{airlineCode}")]
        public IActionResult GEtInfo(string airlineCode)
        {
            var item = db.Airlinecodes
                .Where(x => x.Code == airlineCode)
                .Select(x => new
                {
                    AirlineCode = x.Code,
                    AirlineName = x.Name,
                    Location = db.Locatie
                    .Where(i => i.AirlineCode == airlineCode)
                    .Select(i => new
                    {
                        Hoofdkwartier = new
                        {
                            City = i.StadHoofkwartier,
                            State = i.StaatHoofkwartier
                        },
                        Hub = new
                        {
                            Airport = i.MainHub,
                            State = i.StaatMainHub
                        }
                    }), 
                    History = db.Opgericht
                    .Where(p => p.AirlineCode == airlineCode)
                    .Select(p => new
                    {
                        FoundedInTheYear = p.Opgericht1,
                        CeasedOperationsInTheYear = p.Gestopt
                    }),

                });
            if (item == null)
            {
                return Ok($"Airline with like {airlineCode} don't exist");
            }
            return Ok(item);
        }
    }
}
