using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirlineApp.Entities;

namespace AirlineApp.Controllers
{
    public class LocatiesController : Controller
    {
        private readonly AirlinesContext db;

        public LocatiesController(AirlinesContext context)
        {
            db = context;
        }

        // GET: Locaties
        public IActionResult Index()
        {
            var airlinesContext = db.Locatie.Include(l => l.AirlineCodeNavigation);
            return View(airlinesContext.ToList());
        }

        // GET: Locaties/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locatie = await db.Locatie.SingleOrDefaultAsync(m => m.AirlineCode == id);
            if (locatie == null)
            {
                return NotFound();
            }
            ViewData["AirlineCode"] = new SelectList(db.Airlinecodes, "Code", "Code", locatie.AirlineCode);
            return View(locatie);
        }

        // POST: Locaties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AirlineCode,StadHoofkwartier,StaatHoofkwartier,MainHub,StaatMainHub")] Locatie locatie)
        {
            if (id != locatie.AirlineCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(locatie);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocatieExists(locatie.AirlineCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirlineCode"] = new SelectList(db.Airlinecodes, "Code", "Code", locatie.AirlineCode);
            return View(locatie);
        }

        private bool LocatieExists(string id)
        {
            return db.Locatie.Any(e => e.AirlineCode == id);
        }
    }
}
