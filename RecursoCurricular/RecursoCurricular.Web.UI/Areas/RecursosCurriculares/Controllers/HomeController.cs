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

        [Authorize]
        [HttpGet]
        public JsonResult Unidades(string tipoEducacionCodigo, string gradoCodigo, string sectorId)
        {
            int t;
            int g;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && t > 0 && g > 0)
            {
                return this.Json(RecursoCurricular.RecursosCurriculares.Unidad.Unidades(this.CurrentAnio.Numero, t, g, s), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
        }
    }
}