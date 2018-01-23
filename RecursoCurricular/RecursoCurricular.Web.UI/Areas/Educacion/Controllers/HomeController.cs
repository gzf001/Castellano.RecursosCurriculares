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

        [Authorize]
        [HttpGet]
        public JsonResult Grados(string tipoEducacionCodigo)
        {
            int t;

            if (int.TryParse(tipoEducacionCodigo, out t))
            {
                return this.Json(RecursoCurricular.Educacion.Grado.Grados(t), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult Sectores()
        {
            return this.Json(RecursoCurricular.Educacion.Sector.Sectores, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult TiposEducacion()
        {
            return this.Json(RecursoCurricular.Educacion.TipoEducacion.TiposEducacion, JsonRequestBehavior.AllowGet);
        }
    }
}