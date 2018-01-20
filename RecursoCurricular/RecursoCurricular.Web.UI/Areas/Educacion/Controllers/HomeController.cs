using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.Educacion.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public JsonResult Ciclos()
        {
            return this.Json(RecursoCurricular.Educacion.Ciclo.Ciclos, JsonRequestBehavior.AllowGet);
        }
    }
}