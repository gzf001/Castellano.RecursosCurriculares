using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Controllers
{
    public class ContenidoController : RecursoCurricular.Web.Controller
    {
        const string Area = "RecursosCurriculares";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Contenidos", Area = Area)]
        public ActionResult Contenidos()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Contenidos", Area = Area)]
        public ActionResult Contenidos(RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Contenido model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                {
                    RecursoCurricular.RecursosCurriculares.Contenido contenido = new RecursoCurricular.RecursosCurriculares.Contenido
                    {
                        AnoNumero = this.CurrentAnio.Numero,
                        TipoEducacionCodigo = model.TipoEducacionCodigo,
                        SectorId = model.SectorId,
                        EjeId = model.EjeId,
                        GradoCodigo = model.GradoCodigo,
                        Id = model.Id,
                        Numero = model.Numero,
                        Descripcion = model.Descripcion.Trim(),
                        Transversal = model.Transversal
                    };

                    contenido.Save(context);

                    context.SubmitChanges();

                    contenido.SyncUp();
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "Contenidos", Area = Area)]
        public JsonResult AddContenido(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string ejeId)
        {
            int t;
            int g;
            Guid s;
            Guid e;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(ejeId, out e) && t > 0 && g > 0)
            {
                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);
                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);
                RecursoCurricular.RecursosCurriculares.Eje eje = RecursoCurricular.RecursosCurriculares.Eje.Get(this.CurrentAnio.Numero, s, e);

                int numero = RecursoCurricular.RecursosCurriculares.Contenido.Last(grado, sector, eje);

                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Contenido
                {
                    TipoEducacion = grado.TipoEducacion,
                    Sector = sector,
                    Eje = eje,
                    Grado = grado,
                    Numero = numero,
                    Descripcion = string.Empty,
                    Transversal = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Contenidos", Area = Area)]
        public JsonResult EditContenido(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string ejeId, string id)
        {
            int t;
            int g;
            Guid s;
            Guid e;
            Guid i;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(ejeId, out e) && Guid.TryParse(id, out i) && t > 0 && g > 0)
            {
                RecursoCurricular.RecursosCurriculares.Contenido contenido = RecursoCurricular.RecursosCurriculares.Contenido.Get(t, this.CurrentAnio.Numero, g, s, e, i);

                return this.Json(contenido, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "Contenidos", Area = Area)]
        public JsonResult DeleteContenido(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string ejeId, string id)
        {
            try
            {
                int t;
                int g;
                Guid s;
                Guid e;
                Guid i;

                if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(ejeId, out e) && Guid.TryParse(id, out i) && t > 0 && g > 0)
                {
                    RecursoCurricular.RecursosCurriculares.Contenido contenido = RecursoCurricular.RecursosCurriculares.Contenido.Get(t, this.CurrentAnio.Numero, g, s, e, i);

                    using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                    {
                        contenido.Delete(context);

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
        public JsonResult GetContenidos(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string ejeId)
        {
            int t;
            int g;
            Guid s;
            Guid e;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(ejeId, out e) && t > 0 && g > 0)
            {
                RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Contenido.Contenidos contenidos = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Contenido.Contenidos();

                contenidos.data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Contenido>();

                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);
                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);
                RecursoCurricular.RecursosCurriculares.Eje eje = RecursoCurricular.RecursosCurriculares.Eje.Get(this.CurrentAnio.Numero, s, e);

                foreach (RecursoCurricular.RecursosCurriculares.Contenido contenido in RecursoCurricular.RecursosCurriculares.Contenido.GetAll(grado, sector, eje))
                {
                    contenidos.data.Add(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Contenido
                    {
                        Numero = contenido.Numero,
                        Descripcion = contenido.Descripcion.Length > 100 ? string.Format("{0}...", contenido.Descripcion.Substring(0, 100)) : contenido.Descripcion,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(contenido.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(contenido.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(contenidos, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Contenido.Contenidos { data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Contenido>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}