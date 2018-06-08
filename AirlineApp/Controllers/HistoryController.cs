using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirlineApp.Entities;
using AirlineApp.Data;

namespace AirlineApp.Controllers
{
    [Route("History")]
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext db;

        public HistoryController(ApplicationDbContext context)
        {
            db = context;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            var airlinesContext = db.Opgericht.Include(o => o.AirlineCodeNavigation);
            return View(airlinesContext.ToList());
        }

        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opgericht = await db.Opgericht.SingleOrDefaultAsync(m => m.AirlineCode == id);
            if (opgericht == null)
            {
                return NotFound();
            }
            ViewData["AirlineCode"] = new SelectList(db.Airlinecodes, "Code", "Code", opgericht.AirlineCode);
            return View(opgericht);
        }

        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AirlineCode,Opgericht1,Gestopt")] Opgericht opgericht)
        {
            if (id != opgericht.AirlineCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(opgericht);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpgerichtExists(opgericht.AirlineCode))
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
            ViewData["AirlineCode"] = new SelectList(db.Airlinecodes, "Code", "Code", opgericht.AirlineCode);
            return View(opgericht);
        }

        private bool OpgerichtExists(string id)
        {
            return db.Opgericht.Any(e => e.AirlineCode == id);
        }
    }
}
