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
        // GET: Airlinecodes
        public IActionResult Index()
        {
            return View(db.Airlinecodes.ToList());
        }

        // GET: Airlinecodes/Details/5
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

        // GET: Airlinecodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Airlinecodes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Code")] Airlinecodes airlinecodes)
        {
            if (ModelState.IsValid)
            {
                db.Add(airlinecodes);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(airlinecodes);
        }
    }
}
