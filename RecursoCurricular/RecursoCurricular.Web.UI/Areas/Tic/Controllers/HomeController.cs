using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.Tic.Controllers
{
    public class HomeController : RecursoCurricular.Web.Controller
    {
        const string Area = "Tic";

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}