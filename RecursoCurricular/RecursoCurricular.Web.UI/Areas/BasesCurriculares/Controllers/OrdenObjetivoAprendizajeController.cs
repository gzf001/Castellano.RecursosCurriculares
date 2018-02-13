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
        [HttpGet]
        public PartialViewResult ObjetivosAprendizaje(string unidadId)
        {
            Guid id;

            if (Guid.TryParse(unidadId, out id))
            {
            }

            return this.PartialView("_ObjetivoAprendizaje.cshtml", new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Indicador())
        }
    }
}