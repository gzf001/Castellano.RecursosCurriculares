using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Controllers
{
    public class UnidadController : RecursoCurricular.Web.Controller
    {
        const string Area = "RecursosCurriculares";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Unidades", Area = Area)]
        public ActionResult Unidades()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Unidades", Area = Area)]
        public ActionResult Unidades(RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Unidad model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json("500", JsonRequestBehavior.DenyGet);
            }

            RecursoCurricular.RecursosCurriculares.Unidad unidad = new RecursoCurricular.RecursosCurriculares.Unidad
            {
                AnoNumero = this.CurrentAnio.Numero,
                TipoEducacionCodigo = model.TipoEducacionCodigo,
                GradoCodigo = model.GradoCodigo,
                SectorId = model.SectorId,
                Id = model.Id,
                Numero = model.Numero,
                Nombre = model.Nombre
            };

            RecursoCurricular.Result resultado = RecursoCurricular.RecursosCurriculares.Unidad.Save(unidad, model.Aprendizajes);

            return this.Json(resultado.Status, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "Unidades", Area = Area)]
        public JsonResult AddUnidad(string tipoEducacionCodigo, string gradoCodigo, string sectorId)
        {
            int t;
            int g;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && t > 0 && g > 0)
            {
                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);
                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                int numero = RecursoCurricular.RecursosCurriculares.Unidad.Last(this.CurrentAnio, grado, sector);

                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Unidad
                {
                    TipoEducacionNombre = grado.TipoEducacion.Nombre,
                    GradoNombre = grado.Nombre,
                    SectorNombre = sector.Nombre,
                    Numero = numero,
                    Nombre = string.Empty
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Unidades", Area = Area)]
        public JsonResult EditUnidad(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string unidadId)
        {
            int t;
            int g;
            Guid s;
            Guid u;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u) && t > 0 && g > 0)
            {
                RecursoCurricular.RecursosCurriculares.Unidad unidad = RecursoCurricular.RecursosCurriculares.Unidad.Get(t, this.CurrentAnio.Numero, g, s, u);

                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Unidad
                {
                    TipoEducacionNombre = unidad.TipoEducacion.Nombre,
                    GradoNombre = unidad.Grado.Nombre,
                    SectorNombre = unidad.Sector.Nombre,
                    Id = unidad.Id,
                    Numero = unidad.Numero,
                    Nombre = unidad.Nombre
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "Unidades", Area = Area)]
        public JsonResult DeleteUnidad(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string unidadId)
        {
            int t;
            int g;
            Guid s;
            Guid u;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u) && t > 0 && g > 0)
            {
                using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                {
                    RecursoCurricular.RecursosCurriculares.Unidad unidad = RecursoCurricular.RecursosCurriculares.Unidad.Get(t, this.CurrentAnio.Numero, g, s, u);

                    unidad.Delete(context);

                    context.SubmitChanges();
                }

                return this.Json("200", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetUnidades(string tipoEducacionCodigo, string gradoCodigo, string sectorId)
        {
            int t;
            int g;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && t > 0 && g > 0)
            {
                RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Unidad.Unidades unidad = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Unidad.Unidades();

                unidad.data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Unidad>();

                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);
                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                foreach (RecursoCurricular.RecursosCurriculares.Unidad u in RecursoCurricular.RecursosCurriculares.Unidad.GetAll(this.CurrentAnio, grado, sector))
                {
                    unidad.data.Add(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Unidad
                    {
                        Numero = u.Numero,
                        Nombre = u.Nombre.Length > 70 ? string.Format("{0}...", u.Nombre.Substring(0, 70)) : u.Nombre,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(u.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(u.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(unidad, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Unidad.Unidades { data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Unidad>() }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetAprendizajes(string unidadId, string tipoEducacionCodigo, string gradoCodigo, string sectorId)
        {
            int t;
            int g;
            Guid s;
            Guid u;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u) && t > 0 && g > 0)
            {
                List<RecursoCurricular.Web.UI.Helpers.JsonClass.Aprendizaje> aprendizajes = RecursoCurricular.Web.UI.Helpers.JsonClass.Aprendizaje.GetAll(this.CurrentAnio.Numero, t, g, s, u);

                return this.Json(aprendizajes, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
    }
}