using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.EducacionParvularia.Controllers
{
    public class PrincipioPedagogicoController : RecursoCurricular.Web.Controller
    {
        const string Area = "EducacionParvularia";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "PrincipiosPedagogicos", Area = Area)]
        public ActionResult PrincipiosPedagogicos()
        {
            RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.PrincipioPedagogico model = new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.PrincipioPedagogico
            {
                Anio = this.CurrentAnio
            };

            return this.View(model);
        }
    }
}