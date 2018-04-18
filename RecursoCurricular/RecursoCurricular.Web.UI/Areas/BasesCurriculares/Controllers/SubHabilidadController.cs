using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Controllers
{
    public class SubHabilidadController : RecursoCurricular.Web.Controller
    {
        const string Area = "BasesCurriculares";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "SubHabilidades", Area = Area)]
        public ActionResult SubHabilidades()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "SubHabilidades", Area = Area)]
        public ActionResult SubHabilidades(RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.SubHabilidad model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    RecursoCurricular.BaseCurricular.SubHabilidad subHabilidad = new RecursoCurricular.BaseCurricular.SubHabilidad
                    {
                        TipoEducacionCodigo = model.TipoEducacionCodigo,
                        AnoNumero = this.CurrentAnio.Numero,
                        GradoCodigo = model.GradoCodigo,
                        HabilidadId = model.HabilidadId,
                        SectorId = model.SectorId,
                        Id = model.Id,
                        Numero = model.Numero,
                        Descripcion = model.Descripcion.Trim()
                    };

                    subHabilidad.Save(context);

                    context.SubmitChanges();

                    subHabilidad.SyncUp();
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "SubHabilidades", Area = Area)]
        public JsonResult AddSubHabilidad(string tipoEducacionCodigo, string gradoCodigo, string habilidadId, string sectorId)
        {
            int t;
            int g;
            Guid h;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(habilidadId, out h) && Guid.TryParse(sectorId, out s) && t > 0 && g > 0)
            {
                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);

                RecursoCurricular.BaseCurricular.Habilidad habilidad = RecursoCurricular.BaseCurricular.Habilidad.Get(h, t, this.CurrentAnio.Numero, s);

                int numero = RecursoCurricular.BaseCurricular.SubHabilidad.Last(habilidad);

                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.SubHabilidad
                {
                    TipoEducacionNombre = habilidad.TipoEducacion.Nombre,
                    GradoNombre = grado.Nombre,
                    HabilidadNombre = habilidad.Descripcion,
                    SectorNombre = habilidad.Sector.Nombre,
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "SubHabilidades", Area = Area)]
        public JsonResult EditSubHabilidad(string tipoEducacionCodigo, string gradoCodigo, string habilidadId, string sectorId, string id)
        {
            int t;
            int g;
            Guid h;
            Guid s;
            Guid i;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(habilidadId, out h) && Guid.TryParse(sectorId, out s) && Guid.TryParse(id, out i) && t > 0 && g > 0)
            {
                RecursoCurricular.BaseCurricular.SubHabilidad subHabilidad = RecursoCurricular.BaseCurricular.SubHabilidad.Get(t, this.CurrentAnio.Numero, g, h, s, i);

                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.SubHabilidad
                {
                    TipoEducacionNombre = subHabilidad.TipoEducacion.Nombre,
                    GradoNombre = subHabilidad.Grado.Nombre,
                    HabilidadNombre = subHabilidad.Descripcion,
                    SectorNombre = subHabilidad.Sector.Nombre,
                    Id = subHabilidad.Id,
                    Numero = subHabilidad.Numero,
                    Descripcion = subHabilidad.Descripcion
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "SubHabilidades", Area = Area)]
        public JsonResult DeleteSubHabilidad(string tipoEducacionCodigo, string gradoCodigo, string habilidadId, string sectorId, string id)
        {
            try
            {
                int t;
                int g;
                Guid h;
                Guid s;
                Guid i;

                if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(habilidadId, out h) && Guid.TryParse(sectorId, out s) && Guid.TryParse(id, out i) && t > 0 && g > 0)
                {
                    RecursoCurricular.BaseCurricular.SubHabilidad subHabilidad = RecursoCurricular.BaseCurricular.SubHabilidad.Get(t, this.CurrentAnio.Numero, g, h, s, i);

                    using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                    {
                        subHabilidad.Delete(context);

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
        public JsonResult GetSubHabilidades(string tipoEducacionCodigo, string gradoCodigo, string habilidadId, string sectorId)
        {
            int t;
            int g;
            Guid h;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(habilidadId, out h) && Guid.TryParse(sectorId, out s) && t > 0 && g > 0)
            {
                RecursoCurricular.BaseCurricular.Habilidad habilidad = RecursoCurricular.BaseCurricular.Habilidad.Get(h, t, this.CurrentAnio.Numero, s);

                RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.SubHabilidad.SubHabilidades subHabilidades = new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.SubHabilidad.SubHabilidades();

                subHabilidades.data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.SubHabilidad>();

                foreach (RecursoCurricular.BaseCurricular.SubHabilidad subHabilidad in RecursoCurricular.BaseCurricular.SubHabilidad.GetAll(habilidad))
                {
                    subHabilidades.data.Add(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.SubHabilidad
                    {
                        Numero = subHabilidad.Numero,
                        Descripcion = subHabilidad.Descripcion.Length > 250 ? string.Format("{0}...", subHabilidad.Descripcion.Substring(0, 200)) : subHabilidad.Descripcion,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(subHabilidad.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(subHabilidad.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(subHabilidades, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizaje.ObjetivosAprendizaje { data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizaje>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}