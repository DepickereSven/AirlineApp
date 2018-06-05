using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.Controllers
{
    [Route("ListAPI")]
    public class ListAPIController : Controller
    {
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}