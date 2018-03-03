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

        #region Aprendizajes

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "AprendizajesEsperados", Area = Area)]
        public ActionResult AprendizajesEsperados()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "AprendizajesEsperados", Area = Area)]
        public ActionResult AprendizajesEsperados(RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json("500", JsonRequestBehavior.DenyGet);
            }

            RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje = new RecursoCurricular.RecursosCurriculares.Aprendizaje
            {
                AnoNumero = this.CurrentAnio.Numero,
                TipoEducacionCodigo = model.TipoEducacionCodigo,
                GradoCodigo = model.GradoCodigo,
                SectorId = model.SectorId,
                Id = model.Id,
                Numero = model.Numero,
                Descripcion = model.Descripcion
            };

            RecursoCurricular.Result resultado = RecursoCurricular.RecursosCurriculares.Aprendizaje.Save(aprendizaje, model.Contenidos, model.ObjetivosVerticales);

            return this.Json(resultado.Status, JsonRequestBehavior.AllowGet);
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "AprendizajesEsperados", Area = Area)]
        public JsonResult EditAprendizaje(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string aprendizajeId)
        {
            int t;
            int g;
            Guid s;
            Guid a;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(aprendizajeId, out a) && t > 0 && g > 0)
            {
                RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje = RecursoCurricular.RecursosCurriculares.Aprendizaje.Get(this.CurrentAnio.Numero, t, g, s, a);

                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado
                {
                    IndicadorItem = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicador(),
                    TipoEducacionNombre = aprendizaje.TipoEducacion.Nombre,
                    GradoNombre = aprendizaje.Grado.Nombre,
                    SectorNombre = aprendizaje.Sector.Nombre,
                    Id = aprendizaje.Id,
                    Numero = aprendizaje.Numero,
                    Descripcion = aprendizaje.Descripcion
                }, JsonRequestBehavior.AllowGet);
            }

            return this.Json("500", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "AprendizajesEsperados", Area = Area)]
        public JsonResult DeleteAprendizaje(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string aprendizajeId)
        {
            int t;
            int g;
            Guid s;
            Guid a;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(aprendizajeId, out a) && t > 0 && g > 0)
            {
                RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje = RecursoCurricular.RecursosCurriculares.Aprendizaje.Get(this.CurrentAnio.Numero, t, g, s, a);

                try
                {
                    using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                    {
                        aprendizaje.Delete(context);

                        context.SubmitChanges();
                    };

                    return this.Json("200", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return this.Json("500", JsonRequestBehavior.AllowGet);
                }
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
                RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.AprendizajesEsperados aprendizajesEsperados = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.AprendizajesEsperados();

                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);

                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                aprendizajesEsperados.data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado>();

                foreach (RecursoCurricular.RecursosCurriculares.Aprendizaje a in RecursoCurricular.RecursosCurriculares.Aprendizaje.GetAll(this.CurrentAnio, grado, sector))
                {
                    RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado aprendizaje = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado
                    {
                        Numero = a.Numero,
                        Descripcion = a.Descripcion.Length > 70 ? string.Format("{0}...", a.Descripcion.Substring(0, 70)) : a.Descripcion,
                        CMO = RecursoCurricular.RecursosCurriculares.Contenido.Count(a),
                        OFV = RecursoCurricular.RecursosCurriculares.ObjetivoVertical.Count(a),
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(a.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this, null, null, "editAprendizaje"),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(a.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this, null, null, "deleteAprendizaje"))
                    };

                    foreach (RecursoCurricular.RecursosCurriculares.AprendizajeIndicador i in RecursoCurricular.RecursosCurriculares.AprendizajeIndicador.GetAll(a))
                    {
                        aprendizaje.DetalleIndicadores += i.Descripcion.Length > 70 ? string.Format("<div>-{0}...</div>", i.Descripcion.Substring(0, 70)) : string.Format("<div>{0}</div>", i.Descripcion);
                    }

                    aprendizajesEsperados.data.Add(aprendizaje);
                }

                return this.Json(aprendizajesEsperados, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.AprendizajesEsperados { data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado>() }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Indicadores

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "AprendizajesEsperados", Area = Area)]
        public ActionResult Indicadores(RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicador model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json("500", JsonRequestBehavior.DenyGet);
            }

            using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
            {
                new RecursoCurricular.RecursosCurriculares.AprendizajeIndicador
                {
                    AnoNumero = this.CurrentAnio.Numero,
                    TipoEducacionCodigo = model.TipoEducacionCodigo,
                    GradoCodigo = model.GradoCodigo,
                    SectorId = model.SectorId,
                    AprendizajeId = model.AprendizajeId,
                    Id = model.Id,
                    CategoriaCodigo = model.CategoriaCodigo > -1 ? model.CategoriaCodigo.Value : default(int),
                    Numero = model.Numero,
                    Descripcion = model.Descripcion
                }.Save(context);

                context.SubmitChanges();
            }

            return this.Json("200", JsonRequestBehavior.DenyGet);
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

                int numero = RecursoCurricular.RecursosCurriculares.AprendizajeIndicador.Last(aprendizaje);

                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado
                {
                    IndicadorItem = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicador
                    {
                        Numero = numero,
                        Descripcion = string.Empty
                    },
                    TipoEducacionNombre = grado.TipoEducacion.Nombre,
                    GradoNombre = grado.Nombre,
                    SectorNombre = sector.Nombre,
                    Descripcion = string.Format("{0}.- {1}", aprendizaje.Numero, aprendizaje.Descripcion)
                }, JsonRequestBehavior.AllowGet);
            }

            return this.Json("500", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "AprendizajesEsperados", Area = Area)]
        public JsonResult EditIndicador(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string aprendizajeId, string indicadorId)
        {
            int t;
            int g;
            Guid s;
            Guid a;
            Guid i;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(aprendizajeId, out a) && Guid.TryParse(indicadorId, out i) && t > 0 && g > 0)
            {
                RecursoCurricular.RecursosCurriculares.AprendizajeIndicador aprendizajeIndicador = RecursoCurricular.RecursosCurriculares.AprendizajeIndicador.Get(this.CurrentAnio.Numero, t, g, s, a, i);

                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado
                {
                    IndicadorItem = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicador
                    {
                        Id = aprendizajeIndicador.Id,
                        Numero = aprendizajeIndicador.Numero,
                        CategoriaCodigo = aprendizajeIndicador.CategoriaCodigo.HasValue ? aprendizajeIndicador.CategoriaCodigo.Value : -1,
                        Descripcion = aprendizajeIndicador.Descripcion
                    },
                    TipoEducacionNombre = aprendizajeIndicador.TipoEducacion.Nombre,
                    GradoNombre = aprendizajeIndicador.Grado.Nombre,
                    SectorNombre = aprendizajeIndicador.Sector.Nombre,
                    Descripcion = string.Format("{0}.- {1}", aprendizajeIndicador.Aprendizaje.Numero, aprendizajeIndicador.Aprendizaje.Descripcion)
                }, JsonRequestBehavior.AllowGet);
            }

            return this.Json("500", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "AprendizajesEsperados", Area = Area)]
        public JsonResult DeleteIndicador(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string aprendizajeId, string indicadorId)
        {
            int t;
            int g;
            Guid s;
            Guid a;
            Guid i;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(aprendizajeId, out a) && Guid.TryParse(indicadorId, out i) && t > 0 && g > 0)
            {
                RecursoCurricular.RecursosCurriculares.AprendizajeIndicador aprendizajeIndicador = RecursoCurricular.RecursosCurriculares.AprendizajeIndicador.Get(this.CurrentAnio.Numero, t, g, s, a, i);

                using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                {
                    aprendizajeIndicador.Delete(context);

                    context.SubmitChanges();
                }

                return this.Json("200", JsonRequestBehavior.AllowGet);
            }

            return this.Json("500", JsonRequestBehavior.AllowGet);
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
                RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje = RecursoCurricular.RecursosCurriculares.Aprendizaje.Get(2018, t, g, s, a);

                RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicadores indicadores = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicadores();

                indicadores.data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicador>();

                foreach (RecursoCurricular.RecursosCurriculares.AprendizajeIndicador aprendizajeIndicador in RecursoCurricular.RecursosCurriculares.AprendizajeIndicador.GetAll(aprendizaje))
                {
                    RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicador i = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicador
                    {
                        Numero = aprendizajeIndicador.Numero,
                        Descripcion = aprendizajeIndicador.Descripcion.Length > 70 ? string.Format("{0}...", aprendizajeIndicador.Descripcion.Substring(0, 70)) : aprendizajeIndicador.Descripcion,
                        Habilidad = aprendizajeIndicador.CategoriaCodigo.HasValue ? aprendizajeIndicador.Categoria.Nombre : string.Empty,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(aprendizajeIndicador.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this, null, null, "editIndicador"),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(aprendizajeIndicador.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this, null, null, "deleteIndicador"))
                    };

                    indicadores.data.Add(i);
                }

                return this.Json(indicadores, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicadores { data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.AprendizajeEsperado.Indicador>() }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

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