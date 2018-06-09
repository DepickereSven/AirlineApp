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
    }
}
