using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Controllers
{
    public class UnidadController : RecursoCurricular.Web.Controller
    {
        const string Area = "BasesCurriculares";

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
        public ActionResult Unidades(RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Unidad model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }

            RecursoCurricular.BaseCurricular.Unidad unidad = new RecursoCurricular.BaseCurricular.Unidad
            {
                TipoEducacionCodigo = model.TipoEducacionCodigo,
                AnoNumero = this.CurrentAnio.Numero,
                GradoCodigo = model.GradoCodigo,
                SectorId = model.SectorId,
                Id = model.Id,
                Proposito = string.IsNullOrEmpty(model.Proposito) ? default(string) : model.Proposito.Trim(),
                ConocimientoPrevio = string.IsNullOrEmpty(model.ConocimientoPrevio) ? default(string) : model.ConocimientoPrevio.Trim(),
                PalabraClave = string.IsNullOrEmpty(model.PalabraClave) ? default(string) : model.PalabraClave.Trim(),
                Numero = model.Numero,
                Nombre = model.Nombre.Trim()
            };

            RecursoCurricular.Result resultado = RecursoCurricular.BaseCurricular.Unidad.Save(unidad, model.SubHabilidadesId, model.IndicadoresId, model.ConocimientosId, model.ActitudesId);

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

                int numero = RecursoCurricular.BaseCurricular.Unidad.Last(this.CurrentAnio, grado, sector);

                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Unidad
                {
                    TipoEducacionNombre = grado.TipoEducacion.Nombre,
                    GradoNombre = grado.Nombre,
                    SectorNombre = sector.Nombre,
                    Proposito = string.Empty,
                    ConocimientoPrevio = string.Empty,
                    PalabraClave = string.Empty,
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
                RecursoCurricular.BaseCurricular.Unidad unidad = RecursoCurricular.BaseCurricular.Unidad.Get(t, this.CurrentAnio.Numero, g, s, u);

                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Unidad
                {
                    TipoEducacionNombre = unidad.TipoEducacion.Nombre,
                    GradoNombre = unidad.Grado.Nombre,
                    SectorNombre = unidad.Sector.Nombre,
                    Id = unidad.Id,
                    Proposito = unidad.Proposito,
                    ConocimientoPrevio = unidad.ConocimientoPrevio,
                    PalabraClave = unidad.PalabraClave,
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
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    RecursoCurricular.BaseCurricular.Unidad unidad = RecursoCurricular.BaseCurricular.Unidad.Get(t, this.CurrentAnio.Numero, g, s, u);

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
                RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Unidad.Unidades unidad = new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Unidad.Unidades();

                unidad.data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Unidad>();

                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);
                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                foreach (RecursoCurricular.BaseCurricular.Unidad u in RecursoCurricular.BaseCurricular.Unidad.GetAll(this.CurrentAnio, grado, sector))
                {
                    unidad.data.Add(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Unidad
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
                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Unidad.Unidades { data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Unidad>() }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetHabilidades(string unidadId, string tipoEducacionCodigo, string gradoCodigo, string sectorId)
        {
            int t;
            int g;
            Guid s;
            Guid u;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u) && t > 0 && g > 0)
            {
                List<RecursoCurricular.Web.UI.Helpers.JsonClass.Habilidad> habilidades = RecursoCurricular.Web.UI.Helpers.JsonClass.Habilidad.GetAll(u, this.CurrentAnio.Numero, t, g, s);

                return this.Json(habilidades, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetIndicadores(string unidadId, string tipoEducacionCodigo, string gradoCodigo, string sectorId)
        {
            int t;
            int g;
            Guid s;
            Guid u;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u) && t > 0 && g > 0)
            {
                List<RecursoCurricular.Web.UI.Helpers.JsonClass.ObjetivoAprendizaje> indicadores = RecursoCurricular.Web.UI.Helpers.JsonClass.ObjetivoAprendizaje.GetAll(u, this.CurrentAnio.Numero, t, g, s);

                return this.Json(indicadores, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetActitudes(string unidadId, string tipoEducacionCodigo, string gradoCodigo, string sectorId)
        {
            int t;
            int g;
            Guid s;
            Guid u;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u) && t > 0 && g > 0)
            {
                List<RecursoCurricular.Web.UI.Helpers.JsonClass.Actitud> actitudes = RecursoCurricular.Web.UI.Helpers.JsonClass.Actitud.GetAll(u, this.CurrentAnio.Numero, t, g, s);

                return this.Json(actitudes, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetConocimientos(string unidadId, string tipoEducacionCodigo, string gradoCodigo, string sectorId)
        {
            int t;
            int g;
            Guid s;
            Guid u;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u) && t > 0 && g > 0)
            {
                List<RecursoCurricular.Web.UI.Helpers.JsonClass.Conocimiento> conocimientos = RecursoCurricular.Web.UI.Helpers.JsonClass.Conocimiento.GetAll(u, this.CurrentAnio.Numero, t, g, s);

                return this.Json(conocimientos, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
    }
}