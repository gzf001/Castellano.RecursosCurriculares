using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Controllers
{
    public class DimensionOATController : RecursoCurricular.Web.Controller
    {
        const string Area = "BasesCurriculares";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "DimensionesOAT", Area = Area)]
        public ActionResult DimensionesOAT()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "DimensionesOAT", Area = Area)]
        public ActionResult DimensionesOAT(RecursoCurricular.Web.UI.Areas.Tic.Models.Dimension model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    RecursoCurricular.BaseCurricular.DimensionOAT dimensionOAT = new RecursoCurricular.BaseCurricular.DimensionOAT
                    {
                        Id = model.Id,
                        AnoNumero = this.CurrentAnio.Numero,
                        Numero = model.Numero,
                        Nombre = model.Nombre.Trim(),
                        Descripcion = string.IsNullOrEmpty(model.Descripcion) ? default(string) : model.Descripcion.Trim()
                    };

                    dimensionOAT.Save(context);

                    context.SubmitChanges();

                    dimensionOAT.SyncUp();
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "DimensionesOAT", Area = Area)]
        public ActionResult AddDimensionOAT()
        {
            int numero = RecursoCurricular.BaseCurricular.DimensionOAT.Last(this.CurrentAnio);

            return this.Json(new RecursoCurricular.BaseCurricular.DimensionOAT { Numero = numero }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "DimensionesOAT", Area = Area)]
        public ActionResult EditDimensionOAT(Guid id)
        {
            RecursoCurricular.BaseCurricular.DimensionOAT dimensionOAT = RecursoCurricular.BaseCurricular.DimensionOAT.Get(id, this.CurrentAnio.Numero);

            return this.Json(new Tic.Models.Dimension
            {
                Id = dimensionOAT.Id,
                Numero = dimensionOAT.Numero,
                Nombre = dimensionOAT.Nombre,
                Descripcion = dimensionOAT.Descripcion
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "DimensionesOAT", Area = Area)]
        public JsonResult DeleteDimensionOAT(Guid id)
        {
            RecursoCurricular.BaseCurricular.DimensionOAT dimensionOAT = RecursoCurricular.BaseCurricular.DimensionOAT.Get(id, this.CurrentAnio.Numero);

            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    new RecursoCurricular.BaseCurricular.DimensionOAT
                    {
                        Id = dimensionOAT.Id,
                        AnoNumero = dimensionOAT.AnoNumero,
                        Numero = dimensionOAT.Numero,
                        Nombre = dimensionOAT.Nombre,
                        Descripcion = dimensionOAT.Descripcion
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
        public JsonResult GetDimensionesOAT()
        {
            RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.DimensionOAT.DimensionesOAT dimensiones = new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.DimensionOAT.DimensionesOAT();

            dimensiones.data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.DimensionOAT>();

            foreach (RecursoCurricular.BaseCurricular.DimensionOAT dimensionOAT in RecursoCurricular.BaseCurricular.DimensionOAT.GetAll(this.CurrentAnio))
            {
                dimensiones.data.Add(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.DimensionOAT
                {
                    Numero = dimensionOAT.Numero,
                    Nombre = dimensionOAT.Nombre,
                    Descripcion = string.IsNullOrEmpty(dimensionOAT.Descripcion) ? string.Empty : dimensionOAT.Descripcion.Length > 70 ? string.Format("{0}...", dimensionOAT.Descripcion.Substring(0, 70)) : dimensionOAT.Descripcion,
                    Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(dimensionOAT.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                     RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(dimensionOAT.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                });
            }

            return this.Json(dimensiones, JsonRequestBehavior.AllowGet);
        }
    }
}