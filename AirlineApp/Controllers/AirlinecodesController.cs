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
        private readonly AirlinesContext _context;

        public AirlinecodesController(AirlinesContext context)
        {
            _context = context;
        }

        [Route("")]
        // GET: Airlinecodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Airlinecodes.ToListAsync());
        }

        // GET: Airlinecodes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airlinecodes = await _context.Airlinecodes
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Code")] Airlinecodes airlinecodes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airlinecodes);
                await _context.SaveChangesAsync();
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

            var airlinecodes = await _context.Airlinecodes.SingleOrDefaultAsync(m => m.Code == id);
            if (airlinecodes == null)
            {
                return NotFound();
            }
            return View(airlinecodes);
        }

        // POST: Airlinecodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(airlinecodes);
                    await _context.SaveChangesAsync();
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

            var airlinecodes = await _context.Airlinecodes
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
            var airlinecodes = await _context.Airlinecodes.SingleOrDefaultAsync(m => m.Code == id);
            _context.Airlinecodes.Remove(airlinecodes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirlinecodesExists(string id)
        {
            return _context.Airlinecodes.Any(e => e.Code == id);
        }
    }
}
