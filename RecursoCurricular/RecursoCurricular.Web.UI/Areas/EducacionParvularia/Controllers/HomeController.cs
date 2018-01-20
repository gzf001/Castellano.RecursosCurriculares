using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.EducacionParvularia.Controllers
{
    public class HomeController : RecursoCurricular.Web.Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public JsonResult Nucleos(string ambitoExperienciaAprendizajeCodigo)
        {
            int ambito;

            if (int.TryParse(ambitoExperienciaAprendizajeCodigo, out ambito))
            {
                IEnumerable<SelectListItem> selectList = RecursoCurricular.BaseCurricular.NucleoAprendizaje.Nucleos(this.CurrentAnio.Numero, ambito);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                throw new Exception("Intento fallido de extracción de información");
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetEjes(string ambitoExperienciaAprendizajeCodigo, string nucleoId, string cicloCodigo)
        {
            int a;
            Guid n;
            int c;

            if (int.TryParse(ambitoExperienciaAprendizajeCodigo, out a) && Guid.TryParse(nucleoId, out n) && int.TryParse(cicloCodigo, out c) && a > 0 && c > 0)
            {
                RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje = RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.Get(this.CurrentAnio.Numero, a);
                RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleAprendizaje = BaseCurricular.NucleoAprendizaje.Get(this.CurrentAnio.Numero, a, n);
                RecursoCurricular.Educacion.Ciclo ciclo = RecursoCurricular.Educacion.Ciclo.Get(c);

                IEnumerable<SelectListItem> selectList = RecursoCurricular.BaseCurricular.EjeParvulo.Ejes(ambitoExperienciaAprendizaje, nucleAprendizaje, ciclo);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
        }
    }
}