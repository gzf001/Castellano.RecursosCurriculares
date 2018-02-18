using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Controllers
{
    public class OrdenObjetivoAprendizajeController : RecursoCurricular.Web.Controller
    {
        const string Area = "BasesCurriculares";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "OrdenObjetivoAprendizaje", Area = Area)]
        public ActionResult OrdenObjetivoAprendizaje()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "OrdenObjetivoAprendizaje", Area = Area)]
        public JsonResult OrdenObjetivoAprendizaje(RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.OrdenObjetivoAprendizaje model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }

            model.AnioNumero = this.CurrentAnio.Numero;

            try
            {
                RecursoCurricular.BaseCurricular.OrdenObjetivoAprendizaje.Ordenar(model);

                return this.Json("200", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public PartialViewResult ObjetivosAprendizaje(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string unidadId)
        {
            int t;
            int g;
            Guid s;
            Guid u;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u))
            {
                return this.PartialView("_ObjetivoAprendizaje", new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.OrdenObjetivoAprendizaje
                {
                    TipoEducacionCodigo = t,
                    AnioNumero = this.CurrentAnio.Numero,
                    GradoCodigo = g,
                    SectorId = s,
                    UnidadId = u
                });
            }

            return this.PartialView("_ObjetivoAprendizaje", new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.OrdenObjetivoAprendizaje());
        }
    }
}