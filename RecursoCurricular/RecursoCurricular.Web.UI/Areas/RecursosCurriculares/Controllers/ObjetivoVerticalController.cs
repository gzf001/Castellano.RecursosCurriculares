using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Controllers
{
    public class ObjetivoVerticalController : RecursoCurricular.Web.Controller
    {
        const string Area = "RecursosCurriculares";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "ObjetivosVerticales", Area = Area)]
        public ActionResult ObjetivosVerticales()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "ObjetivosVerticales", Area = Area)]
        public ActionResult ObjetivosVerticales(RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoVertical model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                {
                    new RecursoCurricular.RecursosCurriculares.ObjetivoVertical
                    {
                        AnoNumero = this.CurrentAnio.Numero,
                        TipoEducacionCodigo = model.TipoEducacionCodigo,
                        GradoCodigo = model.GradoCodigo,
                        SectorId = model.SectorId,
                        Id = model.Id,
                        Numero = model.Numero,
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "ObjetivosVerticales", Area = Area)]
        public JsonResult AddObjetivoVertical(string tipoEducacionCodigo, string gradoCodigo, string sectorId)
        {
            int t;
            int g;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && t > 0 && g > 0)
            {
                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);

                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                int numero = RecursoCurricular.RecursosCurriculares.ObjetivoVertical.Last(this.CurrentAnio, grado, sector);

                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoVertical
                {
                    TipoEducacionNombre = grado.TipoEducacion.Nombre,
                    GradoNombre = grado.Nombre,
                    SectorNombre = sector.Nombre,
                    Id = Guid.NewGuid(),
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "ObjetivosVerticales", Area = Area)]
        public JsonResult EditObjetivoVertical(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string id)
        {
            int t;
            int g;
            Guid s;
            Guid i;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(id, out i) && t > 0 && g > 0)
            {
                RecursoCurricular.RecursosCurriculares.ObjetivoVertical objetivoVertical = RecursoCurricular.RecursosCurriculares.ObjetivoVertical.Get(this.CurrentAnio.Numero, t, g, s, i);

                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoVertical
                {
                    TipoEducacionNombre = objetivoVertical.Grado.TipoEducacion.Nombre,
                    GradoNombre = objetivoVertical.Grado.Nombre,
                    SectorNombre = objetivoVertical.Sector.Nombre,
                    Id = objetivoVertical.Id,
                    Numero = objetivoVertical.Numero,
                    Descripcion = objetivoVertical.Descripcion
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "ObjetivosVerticales", Area = Area)]
        public JsonResult DeleteObjetivoVertical(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string id)
        {
            try
            {
                int t;
                int g;
                Guid s;
                Guid i;

                if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(id, out i) && t > 0 && g > 0)
                {
                    RecursoCurricular.RecursosCurriculares.ObjetivoVertical objetivoVertical = RecursoCurricular.RecursosCurriculares.ObjetivoVertical.Get(this.CurrentAnio.Numero, t, g, s, i);

                    using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                    {
                        objetivoVertical.Delete(context);

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
        public JsonResult GetObjetivosVerticales(string tipoEducacionCodigo, string gradoCodigo, string sectorId)
        {
            int t;
            int g;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && t > 0 && g > 0)
            {
                RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoVertical.ObjetivosVerticales objetivoVertical = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoVertical.ObjetivosVerticales();

                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);

                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                objetivoVertical.data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoVertical>();

                foreach (RecursoCurricular.RecursosCurriculares.ObjetivoVertical o in RecursoCurricular.RecursosCurriculares.ObjetivoVertical.GetAll(this.CurrentAnio, grado, sector))
                {
                    objetivoVertical.data.Add(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoVertical
                    {
                        Numero = o.Numero,
                        Descripcion = o.Descripcion.Length > 70 ? string.Format("{0}...", o.Descripcion.Substring(0, 70)) : o.Descripcion,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(o.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(o.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(objetivoVertical, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoVertical.ObjetivosVerticales { data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoVertical>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}