using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Controllers
{
    public class ObjetivoAprendizajeOATController : RecursoCurricular.Web.Controller
    {
        const string Area = "BasesCurriculares";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "ObjetivosAprendizajeOAT", Area = Area)]
        public ActionResult ObjetivosAprendizajeOAT()
        {
            RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizajeOAT model = new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizajeOAT
            {
                Anio = this.CurrentAnio
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "ObjetivosAprendizajeOAT", Area = Area)]
        public ActionResult ObjetivosAprendizajeOAT(RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizajeOAT model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    new RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal
                    {
                        DimensionOATId = model.DimensionOATId,
                        AnoNumero = this.CurrentAnio.Numero,
                        Id = model.Id,
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "ObjetivosAprendizajeOAT", Area = Area)]
        public ActionResult AddObjetivoAprendizajeOAT(string dimensionId)
        {
            if (dimensionId == "-1")
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
            else
            {
                RecursoCurricular.BaseCurricular.DimensionOAT dimension = RecursoCurricular.BaseCurricular.DimensionOAT.Get(new Guid(dimensionId), this.CurrentAnio.Numero);

                int numero = RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal.Last(this.CurrentAnio);

                return this.Json(new RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal { DimensionOAT = dimension, Numero = numero }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "ObjetivosAprendizajeOAT", Area = Area)]
        public ActionResult EditObjetivoAprendizajeOAT(Guid dimensionId, Guid id)
        {
            RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal objetivoAprendizajeTransversal = RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal.Get(id, dimensionId, this.CurrentAnio.Numero);

            return this.Json(objetivoAprendizajeTransversal, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "ObjetivosAprendizajeOAT", Area = Area)]
        public JsonResult DeleteObjetivoAprendizajeOAT(Guid dimensionId, Guid id)
        {
            RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal objetivoAprendizajeTransversal = RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal.Get(id, dimensionId, this.CurrentAnio.Numero);

            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    objetivoAprendizajeTransversal.Delete(context);

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
        public JsonResult GetObjetivosAprendizajeOAT(string dimensionId)
        {
            Guid id;

            if (Guid.TryParse(dimensionId, out id))
            {
                RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizajeOAT.ObjetivosAprendizajeOAT objetivosAprendizajeOAT = new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizajeOAT.ObjetivosAprendizajeOAT();

                objetivosAprendizajeOAT.data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizajeOAT>();

                RecursoCurricular.BaseCurricular.DimensionOAT dimension = RecursoCurricular.BaseCurricular.DimensionOAT.Get(id, this.CurrentAnio.Numero);

                foreach (RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal objetivoAprendizajeTransversal in RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal.GetAll(dimension))
                {
                    objetivosAprendizajeOAT.data.Add(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizajeOAT
                    {
                        Numero = objetivoAprendizajeTransversal.Numero,
                        Nombre = objetivoAprendizajeTransversal.Nombre.Length > 50 ? string.Format("{0}...", objetivoAprendizajeTransversal.Nombre.Substring(0, 50)) : objetivoAprendizajeTransversal.Nombre,
                        Descripcion = string.IsNullOrEmpty(objetivoAprendizajeTransversal.Descripcion) ? string.Empty : objetivoAprendizajeTransversal.Descripcion.Length > 50 ? string.Format("{0}...", objetivoAprendizajeTransversal.Descripcion.Substring(0, 50)) : objetivoAprendizajeTransversal.Descripcion,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(objetivoAprendizajeTransversal.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(objetivoAprendizajeTransversal.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(objetivosAprendizajeOAT, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizajeOAT.ObjetivosAprendizajeOAT { data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizajeOAT>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}