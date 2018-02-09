using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Controllers
{
    public class HabilidadController : RecursoCurricular.Web.Controller
    {
        const string Area = "BasesCurriculares";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Habilidades", Area = Area)]
        public ActionResult Habilidades()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Habilidades", Area = Area)]
        public ActionResult Habilidades(RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Habilidad model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    new RecursoCurricular.BaseCurricular.Habilidad
                    {
                        Id = model.Id,
                        TipoEducacionCodigo = model.TipoEducacionCodigo,
                        AnoNumero = this.CurrentAnio.Numero,
                        SectorId = model.SectorId,
                        Numero = model.Numero,
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "Habilidades", Area = Area)]
        public JsonResult AddHabilidad(string tipoEducacionCodigo, string sectorId)
        {
            int t;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && Guid.TryParse(sectorId, out s) && t > 0)
            {
                RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(t);

                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                int numero = RecursoCurricular.BaseCurricular.Habilidad.Last(this.CurrentAnio, tipoEducacion, sector);

                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Habilidad
                {
                    TipoEducacionNombre = tipoEducacion.Nombre,
                    SectorNombre = sector.Nombre,
                    Numero = numero,
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Habilidades", Area = Area)]
        public JsonResult EditHabilidad(string tipoEducacionCodigo, string sectorId, string id)
        {
            int t;
            Guid s;
            Guid i;

            if (int.TryParse(tipoEducacionCodigo, out t) && Guid.TryParse(sectorId, out s) && Guid.TryParse(id, out i) && t > 0)
            {
                RecursoCurricular.BaseCurricular.Habilidad habilidad = RecursoCurricular.BaseCurricular.Habilidad.Get(i, t, this.CurrentAnio.Numero, s);

                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Habilidad
                {
                    Id = habilidad.Id,
                    TipoEducacionNombre = habilidad.TipoEducacion.Nombre,
                    SectorNombre = habilidad.Sector.Nombre,
                    Numero = habilidad.Numero,
                    Descripcion = habilidad.Descripcion
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "Habilidades", Area = Area)]
        public JsonResult DeleteHabilidad(string tipoEducacionCodigo, string sectorId, string id)
        {
            try
            {
                int t;
                Guid s;
                Guid i;

                if (int.TryParse(tipoEducacionCodigo, out t) && Guid.TryParse(sectorId, out s) && Guid.TryParse(id, out i) && t > 0)
                {
                    RecursoCurricular.BaseCurricular.Habilidad habilidad = RecursoCurricular.BaseCurricular.Habilidad.Get(i, t, this.CurrentAnio.Numero, s);

                    using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                    {
                        habilidad.Delete(context);

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
        public JsonResult GetHabilidades(string tipoEducacionCodigo, string sectorId)
        {
            int t;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && Guid.TryParse(sectorId, out s) && t > 0)
            {
                RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Habilidad.Habilidades habilidades = new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Habilidad.Habilidades();

                RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(t);

                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                habilidades.data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Habilidad>();

                foreach (RecursoCurricular.BaseCurricular.Habilidad habilidad in RecursoCurricular.BaseCurricular.Habilidad.GetAll(this.CurrentAnio, tipoEducacion, sector))
                {
                    habilidades.data.Add(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Habilidad
                    {
                        Numero = habilidad.Numero,
                        Descripcion = habilidad.Descripcion.Length > 70 ? string.Format("{0}...", habilidad.Descripcion.Substring(0, 70)) : habilidad.Descripcion,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(habilidad.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(habilidad.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(habilidades, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Habilidad.Habilidades { data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Habilidad>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}