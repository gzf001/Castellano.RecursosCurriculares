using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Controllers
{
    public class ConocimientoController : Controller
    {
        const string Area = "BasesCurriculares";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Conocimientos", Area = Area)]
        public ActionResult Conocimientos()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Conocimientos", Area = Area)]
        public ActionResult Conocimientos(RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Conocimiento model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    RecursoCurricular.BaseCurricular.Conocimiento conocimiento = new RecursoCurricular.BaseCurricular.Conocimiento
                    {
                        TipoEducacionCodigo = model.TipoEducacionCodigo,
                        AnoNumero = this.CurrentAnio.Numero,
                        SectorId = model.SectorId,
                        Id = model.Id,
                        Numero = model.Numero,
                        Nombre = model.Nombre.Trim(),
                        Descripcion = string.IsNullOrEmpty(model.Descripcion) ? default(string) : model.Descripcion.Trim()
                    };

                    conocimiento.Save(context);

                    context.SubmitChanges();

                    conocimiento.SyncUp();
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "Conocimientos", Area = Area)]
        public JsonResult AddConocimiento(string tipoEducacionCodigo, string sectorId)
        {
            int t;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && Guid.TryParse(sectorId, out s) && t > 0)
            {
                RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(t);

                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                int numero = RecursoCurricular.BaseCurricular.Conocimiento.Last(tipoEducacion, this.CurrentAnio, sector);

                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Conocimiento
                {
                    TipoEducacionNombre = tipoEducacion.Nombre,
                    SectorNombre = sector.Nombre,
                    Id = Guid.NewGuid(),
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Conocimientos", Area = Area)]
        public JsonResult EditConocimiento(string tipoEducacionCodigo, string sectorId, string id)
        {
            int t;
            Guid s;
            Guid i;

            if (int.TryParse(tipoEducacionCodigo, out t) && Guid.TryParse(sectorId, out s) && Guid.TryParse(id, out i) && t > 0)
            {
                RecursoCurricular.BaseCurricular.Conocimiento conocimiento = RecursoCurricular.BaseCurricular.Conocimiento.Get(t, this.CurrentAnio.Numero, s, i);

                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Conocimiento
                {
                    TipoEducacionNombre = conocimiento.TipoEducacion.Nombre,
                    SectorNombre = conocimiento.Sector.Nombre,
                    Id = conocimiento.Id,
                    Numero = conocimiento.Numero,
                    Nombre = conocimiento.Nombre,
                    Descripcion = conocimiento.Descripcion
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "Conocimientos", Area = Area)]
        public JsonResult DeleteConocimiento(string tipoEducacionCodigo, string sectorId, string id)
        {
            try
            {
                int t;
                Guid s;
                Guid i;

                if (int.TryParse(tipoEducacionCodigo, out t) && Guid.TryParse(sectorId, out s) && Guid.TryParse(id, out i) && t > 0)
                {
                    RecursoCurricular.BaseCurricular.Conocimiento conocimiento = RecursoCurricular.BaseCurricular.Conocimiento.Get(t, this.CurrentAnio.Numero, s, i);

                    using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                    {
                        conocimiento.Delete(context);

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
        public JsonResult GetConocimientos(string tipoEducacionCodigo, string sectorId)
        {
            int t;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && Guid.TryParse(sectorId, out s) && t > 0)
            {
                RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Conocimiento.Conocimientos conocimiento = new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Conocimiento.Conocimientos();

                RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(t);

                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                conocimiento.data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Conocimiento>();

                foreach (RecursoCurricular.BaseCurricular.Conocimiento c in RecursoCurricular.BaseCurricular.Conocimiento.GetAll(tipoEducacion, this.CurrentAnio, sector))
                {
                    conocimiento.data.Add(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Conocimiento
                    {
                        Numero = c.Numero,
                        Nombre = string.IsNullOrEmpty(c.Nombre) ? string.Empty : c.Nombre.Length > 70 ? string.Format("{0}...", c.Nombre.Substring(0, 70)) : c.Nombre,
                        Descripcion = c.Descripcion.Length > 70 ? string.Format("{0}...", c.Descripcion.Substring(0, 70)) : c.Descripcion,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(c.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(c.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(conocimiento, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Conocimiento.Conocimientos { data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Conocimiento>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}