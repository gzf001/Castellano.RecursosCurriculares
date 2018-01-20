using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.Tic.Controllers
{
    public class HabilidadController : RecursoCurricular.Web.Controller
    {
        const string Area = "Tic";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Habilidades", Area = Area)]
        public ActionResult Habilidades()
        {
            RecursoCurricular.Web.UI.Areas.Tic.Models.HabilidadTic model = new RecursoCurricular.Web.UI.Areas.Tic.Models.HabilidadTic
            {
                Anio = this.CurrentAnio
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Habilidades", Area = Area)]
        public ActionResult Habilidades(RecursoCurricular.Web.UI.Areas.Tic.Models.HabilidadTic model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                RecursoCurricular.HabilidadTic dimensionHabilidadTic = RecursoCurricular.HabilidadTic.Get(model.Id, model.DimensionHabilidadTicId, this.CurrentAnio.Numero);

                using (RecursoCurricular.Context context = new RecursoCurricular.Context())
                {
                    new RecursoCurricular.HabilidadTic
                    {
                        Id = model.Id,
                        DimensionHabilidadTicId = model.DimensionHabilidadTicId,
                        AnoNumero = this.CurrentAnio.Numero,
                        Numero = model.Numero,
                        Nombre = model.Nombre.Trim(),
                        Descripcion = string.IsNullOrEmpty(model.Descripcion) ? default(string) : model.Descripcion.Trim()
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "Habilidades", Area = Area)]
        public ActionResult AddHabilidad(string dimensionId)
        {
            if (dimensionId == "-1")
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
            else
            {
                RecursoCurricular.DimensionHabilidadTic dimension = RecursoCurricular.DimensionHabilidadTic.Get(new Guid(dimensionId), this.CurrentAnio.Numero);

                int numero = RecursoCurricular.HabilidadTic.Last(this.CurrentAnio);

                return this.Json(new RecursoCurricular.HabilidadTic { DimensionHabilidadTIC = dimension, Numero = numero }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Habilidades", Area = Area)]
        public ActionResult EditHabilidad(Guid dimensionId, Guid id)
        {
            RecursoCurricular.HabilidadTic habilidadTic = RecursoCurricular.HabilidadTic.Get(id, dimensionId, this.CurrentAnio.Numero);

            return this.Json(new RecursoCurricular.Web.UI.Areas.Tic.Models.HabilidadTic
            {
                Id = habilidadTic.Id,
                DimensionHabilidadTIC = habilidadTic.DimensionHabilidadTIC,
                Numero = habilidadTic.Numero,
                Nombre = habilidadTic.Nombre,
                Descripcion = habilidadTic.Descripcion
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "Habilidades", Area = Area)]
        public JsonResult DeleteHabilidad(Guid dimensionId, Guid id)
        {
            RecursoCurricular.HabilidadTic habilidadTic = RecursoCurricular.HabilidadTic.Get(id, dimensionId, this.CurrentAnio.Numero);

            try
            {
                using (RecursoCurricular.Context context = new RecursoCurricular.Context())
                {
                    new RecursoCurricular.HabilidadTic
                    {
                        Id = habilidadTic.Id,
                        DimensionHabilidadTicId = habilidadTic.DimensionHabilidadTicId,
                        AnoNumero = habilidadTic.AnoNumero,
                        Numero = habilidadTic.Numero,
                        Nombre = habilidadTic.Nombre,
                        Descripcion = habilidadTic.Descripcion
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
        public JsonResult GetHabilidades(string dimensionId)
        {
            Guid id;

            if (Guid.TryParse(dimensionId, out id))
            {
                RecursoCurricular.Web.UI.Areas.Tic.Models.HabilidadTic.HabilidadTices habilidadTices = new RecursoCurricular.Web.UI.Areas.Tic.Models.HabilidadTic.HabilidadTices();

                habilidadTices.data = new List<RecursoCurricular.Web.UI.Areas.Tic.Models.HabilidadTic>();

                RecursoCurricular.DimensionHabilidadTic dimension = RecursoCurricular.DimensionHabilidadTic.Get(id, this.CurrentAnio.Numero);

                foreach (RecursoCurricular.HabilidadTic habilidadTic in RecursoCurricular.HabilidadTic.GetAll(dimension))
                {
                    habilidadTices.data.Add(new RecursoCurricular.Web.UI.Areas.Tic.Models.HabilidadTic
                    {
                        Id = habilidadTic.Id,
                        Numero = habilidadTic.Numero,
                        Nombre = habilidadTic.Nombre.Length > 50 ? string.Format("{0}...", habilidadTic.Nombre.Substring(0, 50)) : habilidadTic.Nombre,
                        Descripcion = string.IsNullOrEmpty(habilidadTic.Descripcion) ? string.Empty : habilidadTic.Descripcion.Length > 50 ? string.Format("{0}...", habilidadTic.Descripcion.Substring(0, 50)) : habilidadTic.Descripcion,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(habilidadTic.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(habilidadTic.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(habilidadTices, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.Tic.Models.HabilidadTic.HabilidadTices { data = new List<RecursoCurricular.Web.UI.Areas.Tic.Models.HabilidadTic>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}