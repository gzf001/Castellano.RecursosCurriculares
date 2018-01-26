using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Controllers
{
    public class ActitudController : Controller
    {
        const string Area = "BasesCurriculares";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Actitudes", Area = Area)]
        public ActionResult Actitudes()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Actitudes", Area = Area)]
        public ActionResult Actitudes(RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Actitud model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    new RecursoCurricular.BaseCurricular.Actitud
                    {
                        TipoEducacionCodigo = model.TipoEducacionCodigo,
                        AnoNumero = this.CurrentAnio.Numero,
                        SectorId = model.SectorId,
                        Id = model.Id,
                        Numero = model.Numero,
                        Nombre = model.Nombre.Trim(),
                        Descripcion = model.Descripcion.Trim()
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "Actitudes", Area = Area)]
        public JsonResult AddActitud(string tipoEducacionCodigo, string sectorId)
        {
            int t;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && Guid.TryParse(sectorId, out s) && t > 0)
            {
                RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(t);

                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                int numero = RecursoCurricular.BaseCurricular.Actitud.Last(this.CurrentAnio, tipoEducacion, sector);

                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Actitud
                {
                    TipoEducacionNombre = tipoEducacion.Nombre,
                    SectorNombre = sector.Nombre,
                    Numero = numero,
                    Nombre = string.Empty,
                    Descripcion = string.Empty
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Actitudes", Area = Area)]
        public JsonResult EditActitud(string tipoEducacionCodigo, string sectorId, string id)
        {
            int t;
            Guid s;
            Guid i;

            if (int.TryParse(tipoEducacionCodigo, out t) && Guid.TryParse(sectorId, out s) && Guid.TryParse(id, out i) && t > 0)
            {
                RecursoCurricular.BaseCurricular.Actitud actitud = RecursoCurricular.BaseCurricular.Actitud.Get(t, this.CurrentAnio.Numero, s, i);

                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Actitud
                {
                    TipoEducacionNombre = actitud.TipoEducacion.Nombre,
                    SectorNombre = actitud.Sector.Nombre,
                    Id = actitud.Id,
                    Numero = actitud.Numero,
                    Nombre = actitud.Nombre,
                    Descripcion = actitud.Descripcion
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "Actitudes", Area = Area)]
        public JsonResult DeleteActitud(string tipoEducacionCodigo, string sectorId, string id)
        {
            try
            {
                int t;
                Guid s;
                Guid i;

                if (int.TryParse(tipoEducacionCodigo, out t) && Guid.TryParse(sectorId, out s) && Guid.TryParse(id, out i) && t > 0)
                {
                    RecursoCurricular.BaseCurricular.Actitud actitud = RecursoCurricular.BaseCurricular.Actitud.Get(t, this.CurrentAnio.Numero, s, i);

                    using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                    {
                        actitud.Delete(context);

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
        public JsonResult GetActitudes(string tipoEducacionCodigo, string sectorId)
        {
            int t;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && Guid.TryParse(sectorId, out s) && t > 0)
            {
                RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Actitud.Actitudes actitudes = new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Actitud.Actitudes();

                RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(t);

                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                actitudes.data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Actitud>();

                foreach (RecursoCurricular.BaseCurricular.Actitud actitud in RecursoCurricular.BaseCurricular.Actitud.GetAll(tipoEducacion, this.CurrentAnio, sector))
                {
                    actitudes.data.Add(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Actitud
                    {
                        Numero = actitud.Numero,
                        Nombre = actitud.Nombre.Length > 70 ? string.Format("{0}...", actitud.Nombre.Substring(0, 70)) : actitud.Nombre,
                        Descripcion = actitud.Descripcion.Length > 70 ? string.Format("{0}...", actitud.Descripcion.Substring(0, 70)) : actitud.Descripcion,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(actitud.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(actitud.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(actitudes, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Actitud.Actitudes { data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Actitud>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}