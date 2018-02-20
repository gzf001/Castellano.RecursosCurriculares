using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Controllers
{
    public class AprendizajeEsperadoController : RecursoCurricular.Web.Controller
    {
        const string Area = "RecursosCurriculares";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "AprendizajesEsperados", Area = Area)]
        public ActionResult AprendizajesEsperados()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "AprendizajesEsperados", Area = Area)]
        public JsonResult AddAprendizaje(string tipoEducacionCodigo, string gradoCodigo, string sectorId)
        {
            int t;
            int g;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && t > 0 && g > 0)
            {
                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);
                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                int numero = RecursoCurricular.RecursosCurriculares.Aprendizaje.Last(this.CurrentAnio, grado, sector);

                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado
                {
                    IndicadorItem = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicador(),
                    TipoEducacionNombre = grado.TipoEducacion.Nombre,
                    GradoNombre = grado.Nombre,
                    SectorNombre = sector.Nombre,
                    Numero = numero,
                    Descripcion = string.Empty
                }, JsonRequestBehavior.AllowGet);
            }

            return this.Json("500", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "AprendizajesEsperados", Area = Area)]
        public JsonResult AddIndicador(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string aprendizajeId)
        {
            int t;
            int g;
            Guid s;
            Guid a;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(aprendizajeId, out a) && t > 0 && g > 0)
            {
                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);
                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);
                RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje = RecursoCurricular.RecursosCurriculares.Aprendizaje.Get(this.CurrentAnio.Numero, t, g, s, a);

                int numero = RecursoCurricular.RecursosCurriculares.AprendizajeIndicador.Last(this.CurrentAnio, grado, sector);

                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado
                {
                    IndicadorItem = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicador
                    {

                    },
                    TipoEducacionNombre = grado.TipoEducacion.Nombre,
                    GradoNombre = grado.Nombre,
                    SectorNombre = sector.Nombre,
                    Numero = numero,
                    Descripcion = string.Empty
                }, JsonRequestBehavior.AllowGet);
            }

            return this.Json("500", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetAprendizajesEsperados(string tipoEducacionCodigo, string gradoCodigo, string sectorId)
        {
            int t;
            int g;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && t > 0 && g > 0)
            {
                RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.AprendizajesEsperados aprendizajeEsperado = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.AprendizajesEsperados();

                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);

                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                aprendizajeEsperado.data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado>();

                foreach (RecursoCurricular.RecursosCurriculares.Aprendizaje a in RecursoCurricular.RecursosCurriculares.Aprendizaje.GetAll(this.CurrentAnio, grado, sector))
                {
                    RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado aprendizaje = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado
                    {
                        Numero = a.Numero,
                        Descripcion = a.Descripcion.Length > 70 ? string.Format("{0}...", a.Descripcion.Substring(0, 70)) : a.Descripcion,
                        CMO = RecursoCurricular.RecursosCurriculares.Contenido.Count(a),
                        OFV = RecursoCurricular.RecursosCurriculares.ObjetivoVertical.Count(a),
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(a.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(a.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    };

                    foreach (RecursoCurricular.RecursosCurriculares.AprendizajeIndicador i in RecursoCurricular.RecursosCurriculares.AprendizajeIndicador.GetAll(a))
                    {
                        aprendizaje.DetalleIndicadores += i.Descripcion.Length > 70 ? string.Format("<div>{0}...</div>", i.Descripcion.Substring(0, 70)) : string.Format("<div>{0}</div>", i.Descripcion);
                    }
                }

                return this.Json(aprendizajeEsperado, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.AprendizajesEsperados { data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado>() }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetAprendizajesIndicadores(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string aprendizajeId)
        {
            int t;
            int g;
            Guid s;
            Guid a;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(aprendizajeId, out a) && t > 0 && g > 0)
            {
                RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje = RecursoCurricular.RecursosCurriculares.Aprendizaje.Get(this.CurrentAnio.Numero, t, g, s, a);

                RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicadores indicadores = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicadores();

                indicadores.data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicador>();

                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);

                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                foreach (RecursoCurricular.RecursosCurriculares.AprendizajeIndicador aprendizajeIndicador in RecursoCurricular.RecursosCurriculares.AprendizajeIndicador.GetAll(aprendizaje))
                {
                    RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicador i = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicador
                    {
                        Numero = aprendizajeIndicador.Numero,
                        Descripcion = aprendizajeIndicador.Descripcion.Length > 70 ? string.Format("{0}...", aprendizajeIndicador.Descripcion.Substring(0, 70)) : aprendizajeIndicador.Descripcion,
                        Categoria = aprendizajeIndicador.Categoria.Nombre,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(aprendizajeIndicador.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(aprendizajeIndicador.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    };
                }

                return this.Json(indicadores, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicadores { data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicador>() }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetAprendizajeContenidos(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string aprendizajeId)
        {
            int t;
            int g;
            Guid s;
            Guid a;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(aprendizajeId, out a) && t > 0 && g > 0)
            {
                List<RecursoCurricular.Web.UI.Helpers.JsonClass.Contenido> contenidos = RecursoCurricular.Web.UI.Helpers.JsonClass.Contenido.GetAll(this.CurrentAnio.Numero, t, g, s, a);

                return this.Json(contenidos, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetAprendizajeObjetivosVerticales(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string aprendizajeId)
        {
            int t;
            int g;
            Guid s;
            Guid a;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(aprendizajeId, out a) && t > 0 && g > 0)
            {
                List<RecursoCurricular.Web.UI.Helpers.JsonClass.ObjetivoVertical> objetivosVerticales = RecursoCurricular.Web.UI.Helpers.JsonClass.ObjetivoVertical.GetAll(this.CurrentAnio.Numero, t, g, s, a);

                return this.Json(objetivosVerticales, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
    }
}