using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly AirlinesContext db;

        public FlightDataController(AirlinesContext context)
        {
            db = context;
        }

        [HttpGet("code/{id}")]
        public IActionResult Get(string id)
        {
            if (id == "all")
            {
                var item = db.Airlinecodes.Select(x => new Airlines { Code = x.Code, Name = x.Name }).ToArray();
                return Ok(item);

            } else
            {
                var item = db.Airlinecodes.Find(id);
                var newItem = (new Airlines { Code = item.Code, Name = item.Name });
                if (item == null)
                {
                    return Ok($"Airline with like {id} don't exist");
                }
                return Ok(newItem);
            }
        }

        [HttpGet("")]
        
        
    }
}
