using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Controllers
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
        public JsonResult Ejes(string sectorId)
        {
            Guid id;

            if (Guid.TryParse(sectorId, out id))
            {
                return this.Json(RecursoCurricular.RecursosCurriculares.Eje.Ejes(this.CurrentAnio.Numero, id), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
        }
    }
}