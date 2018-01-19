using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.EducacionParvularia.Controllers
{
    public class EjeController : Controller
    {
        const string Area = "Tic";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Dimensiones", Area = Area)]
        public ActionResult Dimensiones()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Dimensiones", Area = Area)]
        public ActionResult Dimensiones(RecursoCurricular.Web.UI.Areas.Tic.Models.Dimension model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                RecursoCurricular.DimensionHabilidadTic dimensionHabilidadTic = RecursoCurricular.DimensionHabilidadTic.Get(model.Id, this.CurrentAnio.Numero);

                using (RecursoCurricular.Context context = new RecursoCurricular.Context())
                {
                    new RecursoCurricular.DimensionHabilidadTic
                    {
                        Id = model.Id,
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "Dimensiones", Area = Area)]
        public ActionResult AddDimension()
        {
            int numero = RecursoCurricular.DimensionHabilidadTic.Last(this.CurrentAnio);

            return this.Json(new RecursoCurricular.DimensionHabilidadTic { Numero = numero }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Dimensiones", Area = Area)]
        public ActionResult EditDimension(Guid id)
        {
            RecursoCurricular.DimensionHabilidadTic dimensionHabilidadTic = RecursoCurricular.DimensionHabilidadTic.Get(id, this.CurrentAnio.Numero);

            return this.Json(new RecursoCurricular.Web.UI.Areas.Tic.Models.Dimension
            {
                Id = dimensionHabilidadTic.Id,
                Numero = dimensionHabilidadTic.Numero,
                Nombre = dimensionHabilidadTic.Nombre,
                Descripcion = dimensionHabilidadTic.Descripcion
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "Dimensiones", Area = Area)]
        public JsonResult DeleteDimension(Guid id)
        {
            RecursoCurricular.DimensionHabilidadTic dimensionHabilidadTic = RecursoCurricular.DimensionHabilidadTic.Get(id, this.CurrentAnio.Numero);

            try
            {
                using (RecursoCurricular.Context context = new RecursoCurricular.Context())
                {
                    new RecursoCurricular.DimensionHabilidadTic
                    {
                        Id = dimensionHabilidadTic.Id,
                        AnoNumero = dimensionHabilidadTic.AnoNumero,
                        Numero = dimensionHabilidadTic.Numero,
                        Nombre = dimensionHabilidadTic.Nombre,
                        Descripcion = dimensionHabilidadTic.Descripcion
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
        public JsonResult GetDimensiones()
        {
            RecursoCurricular.Web.UI.Areas.Tic.Models.Dimension.DimensionHabilidadTices dimensiones = new RecursoCurricular.Web.UI.Areas.Tic.Models.Dimension.DimensionHabilidadTices();

            dimensiones.data = new List<RecursoCurricular.Web.UI.Areas.Tic.Models.Dimension>();

            foreach (RecursoCurricular.DimensionHabilidadTic dimensionHabilidadTic in RecursoCurricular.DimensionHabilidadTic.GetAll(this.CurrentAnio))
            {
                dimensiones.data.Add(new RecursoCurricular.Web.UI.Areas.Tic.Models.Dimension
                {
                    Id = dimensionHabilidadTic.Id,
                    Numero = dimensionHabilidadTic.Numero,
                    Nombre = dimensionHabilidadTic.Nombre,
                    Descripcion = string.IsNullOrEmpty(dimensionHabilidadTic.Descripcion) ? string.Empty : dimensionHabilidadTic.Descripcion.Length > 70 ? string.Format("{0}...", dimensionHabilidadTic.Descripcion.Substring(0, 70)) : dimensionHabilidadTic.Descripcion,
                    Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(dimensionHabilidadTic.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                     RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(dimensionHabilidadTic.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                });
            }

            return this.Json(dimensiones, JsonRequestBehavior.AllowGet);
        }
    }
}