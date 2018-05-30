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
    public class HistoryController : Controller
    {
        private readonly AirlinesContext _context;

        public HistoryController(AirlinesContext context)
        {
            _context = context;
        }

        // GET: History
        public IActionResult Index()
        {
            var airlinesContext = _context.Opgericht.Include(o => o.AirlineCodeNavigation);
            return View(airlinesContext.ToList());
        }
       
        // GET: History/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opgericht = await _context.Opgericht.SingleOrDefaultAsync(m => m.AirlineCode == id);
            if (opgericht == null)
            {
                return NotFound();
            }
            ViewData["AirlineCode"] = new SelectList(_context.Airlinecodes, "Code", "Code", opgericht.AirlineCode);
            return View(opgericht);
        }

        // POST: History/Edit/5
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
                    _context.Update(opgericht);
                    await _context.SaveChangesAsync();
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
            ViewData["AirlineCode"] = new SelectList(_context.Airlinecodes, "Code", "Code", opgericht.AirlineCode);
            return View(opgericht);
        }

        private bool OpgerichtExists(string id)
        {
            return _context.Opgericht.Any(e => e.AirlineCode == id);
        }
    }
}
