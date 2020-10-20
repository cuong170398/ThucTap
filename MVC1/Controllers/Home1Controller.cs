using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC1.Controllers
{
    public class Home1Controller : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}