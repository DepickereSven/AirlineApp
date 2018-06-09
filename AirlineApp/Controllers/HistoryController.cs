using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirlineApp.Entities;
using AirlineApp.Data;
using AirlineApp.Models;

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
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return View("Error", new ErrorViewModel());
            }
            else
            {
                Opgericht opgericht = db.Opgericht.SingleOrDefault(m => m.AirlineCode == id);
                if (opgericht == null)
                {
                    return View("Error", new ErrorViewModel());
                }
                return View(opgericht);
            }
        }

        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Opgericht opgericht)
        {
            if (ModelState.IsValid)
            {
                db.Opgericht.Update(opgericht);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(opgericht);
        }

        [Route("Sort/{object?}")]
        public IActionResult Sort(string field)
        {
            List<Opgericht> historieData;
            switch (field)
            {
                default:
                case "code":
                    historieData = db.Opgericht
                        .OrderBy(m => m.AirlineCode)
                        .Include(l => l.AirlineCodeNavigation)
                        .ToList();
                    break;
                case "Opgericht":
                    historieData = db.Opgericht
                        .OrderBy(m => m.Opgericht1)
                        .Include(l => l.AirlineCodeNavigation)
                        .ToList();
                    break;
                case "Gestopt":
                    historieData = db.Opgericht
                        .OrderBy(m => m.Gestopt)
                        .Include(l => l.AirlineCodeNavigation)
                        .ToList();
                    break;
            }
            return View("Index", historieData);
        }
    }
}
