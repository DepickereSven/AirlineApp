using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirlineApp.Entities;
using AirlineApp.Models;

namespace AirlineApp.Controllers
{
    public class AirlinecodesController : Controller
    {
        private readonly AirlinesContext db;

        public AirlinecodesController(AirlinesContext context)
        {
            db = context;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View(db.Airlinecodes.ToList());
        }

        [Route("Details/{id}")]
        public IActionResult Details(string id)
        {
            if(id != null)
            {
                return View(
                    db.Airlinecodes
                               .Include(m => m.Locatie)
                               .Include(m => m.Opgericht)
                .FirstOrDefault(m => m.Code == id));
            }
            return View("Error", new ErrorViewModel());
        }
    }
}
