using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.EducacionParvularia.Controllers
{
    public class EjeController : RecursoCurricular.Web.Controller
    {
        const string Area = "EducacionParvularia";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Ejes", Area = Area)]
        public ActionResult Ejes()
        {
            RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje model = new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje
            {
                Anio = this.CurrentAnio
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Ejes", Area = Area)]
        public ActionResult Ejes(RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    new RecursoCurricular.BaseCurricular.EjeParvulo
                    {
                        AnoNumero = this.CurrentAnio.Numero,
                        AmbitoExperienciaAprendizajeCodigo = model.AmbitoExperienciaAprendizajeCodigo,
                        NucleoId = model.NucleoId,
                        CicloCodigo = model.CicloCodigo,
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "Ejes", Area = Area)]
        public JsonResult AddEje(string ambitoExperienciaAprendizajeCodigo, string nucleoId, string cicloCodigo)
        {
            int a;
            Guid n;
            short c;

            if (int.TryParse(ambitoExperienciaAprendizajeCodigo, out a) && Guid.TryParse(nucleoId, out n) && short.TryParse(cicloCodigo, out c) && a > 0 && c > 0)
            {
                RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje = RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.Get(this.CurrentAnio.Numero, a);
                RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleAprendizaje = BaseCurricular.NucleoAprendizaje.Get(this.CurrentAnio.Numero, a, n);
                RecursoCurricular.Educacion.Ciclo ciclo = RecursoCurricular.Educacion.Ciclo.Get(c);

                int numero = RecursoCurricular.BaseCurricular.EjeParvulo.Last(ambitoExperienciaAprendizaje, nucleAprendizaje, ciclo);

                return this.Json(new RecursoCurricular.BaseCurricular.EjeParvulo
                {
                    AmbitoExperienciaAprendizaje = ambitoExperienciaAprendizaje,
                    NucleoAprendizaje = nucleAprendizaje,
                    Ciclo = ciclo,
                    Numero = numero
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Ejes", Area = Area)]
        public JsonResult EditEje(string ambitoExperienciaAprendizajeCodigo, string nucleoId, string cicloCodigo, string id)
        {
            int a;
            Guid n;
            int c;
            Guid e;

            if (int.TryParse(ambitoExperienciaAprendizajeCodigo, out a) && Guid.TryParse(nucleoId, out n) && int.TryParse(cicloCodigo, out c) && Guid.TryParse(id, out e))
            {
                RecursoCurricular.BaseCurricular.EjeParvulo eje = RecursoCurricular.BaseCurricular.EjeParvulo.Get(this.CurrentAnio.Numero, a, n, c, e);

                return this.Json(eje, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "Ejes", Area = Area)]
        public JsonResult DeleteEje(string ambitoExperienciaAprendizajeCodigo, string nucleoId, string cicloCodigo, string id)
        {
            try
            {
                int a;
                Guid n;
                int c;
                Guid e;

                if (int.TryParse(ambitoExperienciaAprendizajeCodigo, out a) && Guid.TryParse(nucleoId, out n) && int.TryParse(cicloCodigo, out c) && Guid.TryParse(id, out e))
                {
                    RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje = RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.Get(this.CurrentAnio.Numero, a);
                    RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleAprendizaje = BaseCurricular.NucleoAprendizaje.Get(this.CurrentAnio.Numero, a, n);
                    RecursoCurricular.Educacion.Ciclo ciclo = RecursoCurricular.Educacion.Ciclo.Get(c);

                    RecursoCurricular.BaseCurricular.EjeParvulo eje = RecursoCurricular.BaseCurricular.EjeParvulo.Get(this.CurrentAnio.Numero, a, n, c, e);

                    using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                    {
                        eje.Delete(context);

                        context.SubmitChanges();
                    }

                    return this.Json("200", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return this.Json("500", JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetEjes(string ambitoExperienciaAprendizajeCodigo, string nucleoId, string cicloCodigo)
        {
            int a;
            Guid n;
            int c;

            if (int.TryParse(ambitoExperienciaAprendizajeCodigo, out a) && Guid.TryParse(nucleoId, out n) && int.TryParse(cicloCodigo, out c))
            {
                RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje.Ejes ejes = new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje.Ejes();

                ejes.data = new List<RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje>();

                RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje = RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.Get(this.CurrentAnio.Numero, a);
                RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleAprendizaje = BaseCurricular.NucleoAprendizaje.Get(this.CurrentAnio.Numero, a, n);
                RecursoCurricular.Educacion.Ciclo ciclo = RecursoCurricular.Educacion.Ciclo.Get(c);

                foreach (RecursoCurricular.BaseCurricular.EjeParvulo ejeParvulo in RecursoCurricular.BaseCurricular.EjeParvulo.GetAll(ambitoExperienciaAprendizaje, nucleAprendizaje, ciclo))
                {
                    ejes.data.Add(new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje
                    {
                        Id = ejeParvulo.Id,
                        Numero = ejeParvulo.Numero,
                        Nombre = ejeParvulo.Nombre,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(ejeParvulo.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(ejeParvulo.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(ejes, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje.Ejes { data = new List<RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}