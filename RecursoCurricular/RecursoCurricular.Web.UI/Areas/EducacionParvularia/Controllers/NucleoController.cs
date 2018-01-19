using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.EducacionParvularia.Controllers
{
    public class NucleoController : RecursoCurricular.Web.Controller
    {
        const string Area = "EducacionParvularia";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Nucleos", Area = Area)]
        public ActionResult Nucleos()
        {
            RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Nucleo model = new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Nucleo
            {
                Anio = this.CurrentAnio
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Nucleos", Area = Area)]
        public ActionResult Nucleos(RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Nucleo model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    new RecursoCurricular.BaseCurricular.NucleoAprendizaje
                    {
                        AnoNumero = this.CurrentAnio.Numero,
                        AmbitoExperienciaAprendizajeCodigo = model.AmbitoExperienciaAprendizajeCodigo,
                        Id = model.Id,
                        Numero = model.Numero,
                        Nombre = model.Nombre.Trim()
                    }.Save(context);

                    context.SubmitChanges();
                }

                return this.Json("200", JsonRequestBehavior.DenyGet);
            }
            catch
            {
                return this.Json("500", JsonRequestBehavior.DenyGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "Nucleos", Area = Area)]
        public ActionResult AddNucleo(int ambitoExperienciaAprendizajeCodigo)
        {
            if (ambitoExperienciaAprendizajeCodigo < 0)
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }

            RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje = RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.Get(this.CurrentAnio.Numero, ambitoExperienciaAprendizajeCodigo);

            int numero = RecursoCurricular.BaseCurricular.NucleoAprendizaje.Last(this.CurrentAnio);

            return this.Json(new RecursoCurricular.BaseCurricular.NucleoAprendizaje { AmbitoExperienciaAprendizaje = ambitoExperienciaAprendizaje, Numero = numero }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Nucleos", Area = Area)]
        public ActionResult EditNucleo(int ambitoExperienciaAprendizajeCodigo, Guid id)
        {
            RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleoAprendizaje = RecursoCurricular.BaseCurricular.NucleoAprendizaje.Get(this.CurrentAnio.Numero, ambitoExperienciaAprendizajeCodigo, id);

            return this.Json(new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Nucleo
            {
                Id = nucleoAprendizaje.Id,
                AmbitoExperienciaAprendizaje = nucleoAprendizaje.AmbitoExperienciaAprendizaje,
                Numero = nucleoAprendizaje.Numero,
                Nombre = nucleoAprendizaje.Nombre
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "Nucleos", Area = Area)]
        public JsonResult DeleteNucleo(int ambitoExperienciaAprendizajeCodigo, Guid id)
        {
            RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleoAprendizaje = RecursoCurricular.BaseCurricular.NucleoAprendizaje.Get(this.CurrentAnio.Numero, ambitoExperienciaAprendizajeCodigo, id);

            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    new RecursoCurricular.BaseCurricular.NucleoAprendizaje
                    {
                        AnoNumero = nucleoAprendizaje.AnoNumero,
                        AmbitoExperienciaAprendizajeCodigo = nucleoAprendizaje.AmbitoExperienciaAprendizajeCodigo,
                        Id = nucleoAprendizaje.Id,
                        Numero = nucleoAprendizaje.Numero,
                        Nombre = nucleoAprendizaje.Nombre
                    }.Delete(context);

                    context.SubmitChanges();
                }

                return this.Json("200", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetNucleos(int ambitoExperienciaAprendizajeCodigo)
        {
            RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje = RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.Get(this.CurrentAnio.Numero, ambitoExperienciaAprendizajeCodigo);

            RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Nucleo.Nucleos nucleos = new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Nucleo.Nucleos();

            nucleos.data = new List<RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Nucleo>();

            foreach (RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleoAprendizaje in RecursoCurricular.BaseCurricular.NucleoAprendizaje.GetAll(ambitoExperienciaAprendizaje))
            {
                nucleos.data.Add(new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Nucleo
                {
                    Id = nucleoAprendizaje.Id,
                    Numero = nucleoAprendizaje.Numero,
                    Nombre = nucleoAprendizaje.Nombre,
                    Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(nucleoAprendizaje.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                     RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(nucleoAprendizaje.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                });
            }

            return this.Json(nucleos, JsonRequestBehavior.AllowGet);
        }
    }
}