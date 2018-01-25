using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Controllers
{
    public class IndicadorController : RecursoCurricular.Web.Controller
    {
        const string Area = "BasesCurriculares";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Indicadores", Area = Area)]
        public ActionResult Indicadores()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Indicadores", Area = Area)]
        public ActionResult Indicadores(RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Indicador model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    new RecursoCurricular.BaseCurricular.Indicador
                    {
                        TipoEducacionCodigo = model.TipoEducacionCodigo,
                        AnoNumero = this.CurrentAnio.Numero,
                        GradoCodigo = model.GradoCodigo,
                        SectorId = model.SectorId,
                        EjeId = model.EjeId,
                        ObjetivoAprendizajeId = model.ObjetivoAprendizajeId,
                        Id = model.Id,
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Indicadores", Area = Area)]
        public JsonResult SelectObjetivo(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string ejeId, string objetivoAprendizajeId)
        {
            int t;
            int g;
            Guid s;
            Guid e;
            Guid o;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(ejeId, out e) && Guid.TryParse(objetivoAprendizajeId, out o) && t > 0 && g > 0)
            {
                RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje = RecursoCurricular.BaseCurricular.ObjetivoAprendizaje.Get(t, this.CurrentAnio.Numero, g, s, e, o);

                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Indicador.Objetivo
                {
                    Id = objetivoAprendizaje.Id,
                    TipoEducacionNombre = objetivoAprendizaje.TipoEducacion.Nombre,
                    GradoNombre = objetivoAprendizaje.Grado.Nombre,
                    SectorNombre = objetivoAprendizaje.Sector.Nombre,
                    EjeNombre = string.Format("{0}.- {1}", objetivoAprendizaje.Eje.Numero, objetivoAprendizaje.Eje.Nombre),
                    Descripcion = string.Format("{0}.- {1}", objetivoAprendizaje.Numero, objetivoAprendizaje.Descripcion)
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "Indicadores", Area = Area)]
        public JsonResult AddIndicador(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string ejeId, string objetivoAprendizajeId)
        {
            int t;
            int g;
            Guid s;
            Guid e;
            Guid o;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(ejeId, out e) && Guid.TryParse(objetivoAprendizajeId, out o) && t > 0 && g > 0)
            {
                RecursoCurricular.BaseCurricular.ObjetivoAprendizaje ob = BaseCurricular.ObjetivoAprendizaje.Get(t, this.CurrentAnio.Numero, g, s, e, o);

                int numero = RecursoCurricular.BaseCurricular.Indicador.Last(ob);

                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Indicador
                {
                    ObjetivoAprendizaje = ob,
                    Numero = numero
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Indicadores", Area = Area)]
        public JsonResult EditIndicador(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string ejeId, string objetivoAprendizajeId, string id)
        {
            int t;
            int g;
            Guid s;
            Guid e;
            Guid o;
            Guid i;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(ejeId, out e) && Guid.TryParse(objetivoAprendizajeId, out o) && Guid.TryParse(id, out i) && t > 0 && g > 0)
            {
                RecursoCurricular.BaseCurricular.Indicador indicador = RecursoCurricular.BaseCurricular.Indicador.Get(t, this.CurrentAnio.Numero, g, s, e, o, i);

                return this.Json(indicador, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "Indicadores", Area = Area)]
        public JsonResult DeleteIndicador(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string ejeId, string objetivoAprendizajeId, string id)
        {
            try
            {
                int t;
                int g;
                Guid s;
                Guid e;
                Guid o;
                Guid i;

                if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(ejeId, out e) && Guid.TryParse(objetivoAprendizajeId, out o) && Guid.TryParse(id, out i) && t > 0 && g > 0)
                {
                    RecursoCurricular.BaseCurricular.Indicador indicador = RecursoCurricular.BaseCurricular.Indicador.Get(t, this.CurrentAnio.Numero, g, s, e, o, i);

                    using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                    {
                        indicador.Delete(context);

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
        public JsonResult GetObjetivos(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string ejeId)
        {
            int t;
            int g;
            Guid s;
            Guid e;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(ejeId, out e) && t > 0 && g > 0)
            {
                RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Indicador.Objetivo.ObjetivosAprendizaje objetivosAprendizaje = new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Indicador.Objetivo.ObjetivosAprendizaje();

                objetivosAprendizaje.data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizaje>();

                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);
                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);
                RecursoCurricular.BaseCurricular.Eje eje = RecursoCurricular.BaseCurricular.Eje.Get(this.CurrentAnio.Numero, s, e);

                foreach (RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje in RecursoCurricular.BaseCurricular.ObjetivoAprendizaje.GetAll(grado, sector, eje))
                {
                    RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Indicador.Objetivo o = new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Indicador.Objetivo();

                    o.Numero = objetivoAprendizaje.Numero;
                    o.Descripcion = objetivoAprendizaje.Descripcion.Length > 250 ? string.Format("{0}...", objetivoAprendizaje.Descripcion.Substring(0, 200)) : objetivoAprendizaje.Descripcion;
                    o.Accion = string.Format("{0}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLink(null, null, null, null, null, RecursoCurricular.Helpers.TypeButton.OtherAction, objetivoAprendizaje.Id, "btn btn-success btn-xs btn-flat", "fa-check", "Seleccionar", null, this));
                    //fa-check
                    foreach (RecursoCurricular.BaseCurricular.Indicador indicador in RecursoCurricular.BaseCurricular.Indicador.GetAll(objetivoAprendizaje))
                    {
                        o.Indicadores += string.Format("<div>{0}.- {1}</div>", indicador.Numero, indicador.Descripcion.Length > 70 ? string.Format("{0}...", indicador.Descripcion.Substring(0, 70)) : indicador.Descripcion);
                    }

                    objetivosAprendizaje.data.Add(o);
                }

                return this.Json(objetivosAprendizaje, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizaje.ObjetivosAprendizaje { data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizaje>() }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetIndicadores(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string ejeId, string objetivoAprendizajeId)
        {
            int t;
            int g;
            Guid s;
            Guid e;
            Guid o;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(ejeId, out e) && Guid.TryParse(objetivoAprendizajeId, out o) && t > 0 && g > 0)
            {
                RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Indicador.Indicadores indicadores = new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Indicador.Indicadores();

                indicadores.data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Indicador>();

                RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje = RecursoCurricular.BaseCurricular.ObjetivoAprendizaje.Get(t, this.CurrentAnio.Numero, g, s, e, o);

                foreach (RecursoCurricular.BaseCurricular.Indicador indicador in RecursoCurricular.BaseCurricular.Indicador.GetAll(objetivoAprendizaje))
                {
                    indicadores.data.Add(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Indicador
                    {
                        Numero = indicador.Numero,
                        Descripcion = indicador.Descripcion.Length > 250 ? string.Format("{0}...", indicador.Descripcion.Substring(0, 200)) : indicador.Descripcion,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(indicador.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(indicador.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(indicadores, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Indicador.Indicadores { data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.Indicador>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}