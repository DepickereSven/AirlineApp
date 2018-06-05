using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.Controllers
{
    public class ListAPIController : Controller
    {
        // GET: ListAPI
        public ActionResult Index()
        {
            return View();
        }
    }
}