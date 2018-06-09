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
    [Route("Locaties")]
    public class LocatiesController : Controller
    {
        private readonly ApplicationDbContext db;

        public LocatiesController(ApplicationDbContext context)
        {
            db = context;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            var airlinesContext = db.Locatie.Include(l => l.AirlineCodeNavigation);
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
                Locatie locatie = db.Locatie.SingleOrDefault(m => m.AirlineCode == id);
                if (locatie == null)
                {
                    return View("Error", new ErrorViewModel());
                }
                return View(locatie);
            }
        }

        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Locatie locatie)
        {
            if (ModelState.IsValid)
            {
                db.Locatie.Update(locatie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locatie);
        }


        [Route("Sort/{object?}")]
        public IActionResult Sort(string field)
        {
            List<Locatie> locaties;
            switch (field)
            {
                default:
                case "code":
                    locaties = db.Locatie
                        .OrderBy(m => m.AirlineCode)
                        .Include(l => l.AirlineCodeNavigation)
                        .ToList();
                    break;
                case "StadHoofkwartier":
                    locaties = db.Locatie
                        .OrderBy(m => m.StadHoofkwartier)
                        .Include(l => l.AirlineCodeNavigation)
                        .ToList();
                    break;
                case "StaatHoofkwartier":
                    locaties = db.Locatie
                        .OrderBy(m => m.StaatHoofkwartier)
                        .Include(l => l.AirlineCodeNavigation)
                        .ToList();
                    break;
                case "MainHub":
                    locaties = db.Locatie
                        .OrderBy(m => m.MainHub)
                        .Include(l => l.AirlineCodeNavigation)
                        .ToList();
                    break;
                case "StaatMainHub":
                    locaties = db.Locatie
                        .OrderBy(m => m.StaatMainHub)
                        .Include(l => l.AirlineCodeNavigation)
                        .ToList();
                    break;


            }
            return View("Index", locaties);
        }

        [Route("Find/{keyword?}")]
        public IActionResult Find(string keyword)
        {
            return View("Index",
                  db.Locatie
                  .Where(m => 
                  m.AirlineCode.Contains(keyword ?? "")
                  ||
                  m.StadHoofkwartier.Contains(keyword ?? "")
                  ||
                  m.StaatHoofkwartier.Contains(keyword ?? "")
                  ||
                  m.MainHub.Contains(keyword ?? "")
                  ||
                  m.StaatMainHub.Contains(keyword ?? "")
                  )
                  .Select(m => m)
                  .Include(l => l.AirlineCodeNavigation)
                  .ToList());
        }
    }
}
