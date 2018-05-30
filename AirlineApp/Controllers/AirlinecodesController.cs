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
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airlinecodes = await db.Airlinecodes
                .SingleOrDefaultAsync(m => m.Code == id);
            if (airlinecodes == null)
            {
                return NotFound();
            }

            return View(airlinecodes);
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

        // GET: Airlinecodes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airlinecodes = await db.Airlinecodes.SingleOrDefaultAsync(m => m.Code == id);
            if (airlinecodes == null)
            {
                return NotFound();
            }
            return View(airlinecodes);
        }

        // POST: Airlinecodes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Code")] Airlinecodes airlinecodes)
        {
            if (id != airlinecodes.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(airlinecodes);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirlinecodesExists(airlinecodes.Code))
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
            return View(airlinecodes);
        }

        // GET: Airlinecodes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airlinecodes = await db.Airlinecodes
                .SingleOrDefaultAsync(m => m.Code == id);
            if (airlinecodes == null)
            {
                return NotFound();
            }

            return View(airlinecodes);
        }

        // POST: Airlinecodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var airlinecodes = await db.Airlinecodes.SingleOrDefaultAsync(m => m.Code == id);
            db.Airlinecodes.Remove(airlinecodes);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirlinecodesExists(string id)
        {
            return db.Airlinecodes.Any(e => e.Code == id);
        }
    }
}
