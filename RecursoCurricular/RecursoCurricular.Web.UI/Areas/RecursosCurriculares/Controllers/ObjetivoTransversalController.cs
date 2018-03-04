using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Controllers
{
    public class ObjetivoTransversalController : RecursoCurricular.Web.Controller
    {
        const string Area = "RecursosCurriculares";

        #region ObjetivosTransversales

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "ObjetivoTransversales", Area = Area)]
        public ActionResult ObjetivoTransversales()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "ObjetivoTransversales", Area = Area)]
        public ActionResult ObjetivoTransversales(RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json("500", JsonRequestBehavior.DenyGet);
            }

            try
            {
                using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                {
                    new RecursoCurricular.RecursosCurriculares.ObjetivoTransversal
                    {
                        AnoNumero = this.CurrentAnio.Numero,
                        TipoEducacionCodigo = model.TipoEducacionCodigo,
                        GradoCodigo = model.GradoCodigo,
                        SectorId = model.SectorId,
                        UnidadId = model.UnidadId,
                        Id = model.Id,
                        Numero = model.Numero,
                        Descripcion = model.Descripcion
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "ObjetivoTransversales", Area = Area)]
        public JsonResult AddObjetivoTransversal(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string unidadId)
        {
            int t;
            int g;
            Guid s;
            Guid u;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u) && t > 0 && g > 0)
            {
                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);
                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);
                RecursoCurricular.RecursosCurriculares.Unidad unidad = RecursoCurricular.RecursosCurriculares.Unidad.Get(t, this.CurrentAnio.Numero, g, s, u);

                int numero = RecursoCurricular.RecursosCurriculares.ObjetivoTransversal.Last(unidad);

                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal
                {
                    IndicadorItem = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal.Indicador(),
                    TipoEducacionNombre = grado.TipoEducacion.Nombre,
                    GradoNombre = grado.Nombre,
                    SectorNombre = sector.Nombre,
                    UnidadNombre = string.Format("{0}.- {1}", unidad.Numero, unidad.Nombre),
                    Numero = numero,
                    Descripcion = string.Empty
                }, JsonRequestBehavior.AllowGet);
            }

            return this.Json("500", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetObjetivoTransversales(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string unidadId)
        {
            int t;
            int g;
            Guid s;
            Guid u;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u) && t > 0 && g > 0)
            {
                RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal.ObjetivoTransversales objetivoTransversales = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal.ObjetivoTransversales();

                RecursoCurricular.RecursosCurriculares.Unidad unidad = RecursoCurricular.RecursosCurriculares.Unidad.Get(t, this.CurrentAnio.Numero, g, s, u);

                objetivoTransversales.data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal>();

                foreach (RecursoCurricular.RecursosCurriculares.ObjetivoTransversal o in RecursoCurricular.RecursosCurriculares.ObjetivoTransversal.GetAll(unidad))
                {
                    RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal objetivoTransversal = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal
                    {
                        Numero = o.Numero,
                        Descripcion = o.Descripcion.Length > 70 ? string.Format("{0}...", o.Descripcion.Substring(0, 70)) : o.Descripcion,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(o.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this, null, null, "editObjetivo"),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(o.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this, null, null, "deleteObjetivo"))
                    };

                    foreach (RecursoCurricular.RecursosCurriculares.ObjetivoTransversalIndicador i in RecursoCurricular.RecursosCurriculares.ObjetivoTransversalIndicador.GetAll(o))
                    {
                        objetivoTransversal.DetalleIndicadores += i.Descripcion.Length > 70 ? string.Format("<div>-{0}...</div>", i.Descripcion.Substring(0, 70)) : string.Format("<div>{0}</div>", i.Descripcion);
                    }

                    objetivoTransversales.data.Add(objetivoTransversal);
                }

                return this.Json(objetivoTransversales, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal.ObjetivoTransversales { data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal>() }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Indicadores

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "ObjetivoTransversales", Area = Area)]
        public ActionResult ObjetivoTransversalIndicador(RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal.Indicador model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json("500", JsonRequestBehavior.DenyGet);
            }

            using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
            {
                new RecursoCurricular.RecursosCurriculares.ObjetivoTransversalIndicador
                {
                    AnoNumero = this.CurrentAnio.Numero,
                    TipoEducacionCodigo = model.TipoEducacionCodigo,
                    GradoCodigo = model.GradoCodigo,
                    SectorId = model.SectorId,
                    UnidadId = model.UnidadId,
                    ObjetivoTransversalId = model.ObjetivoTransversalId,
                    Id = model.Id,
                    Numero = model.Numero,
                    Descripcion = model.Descripcion.Trim()
                }.Save(context);

                context.SubmitChanges();
            }

            return this.Json("200", JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "ObjetivoTransversales", Area = Area)]
        public JsonResult AddObjetivoTransversalIndicador(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string unidadId, string objetivoTransversalId)
        {
            int t;
            int g;
            Guid s;
            Guid u;
            Guid o;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u) && Guid.TryParse(objetivoTransversalId, out o) && t > 0 && g > 0)
            {
                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);
                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);
                RecursoCurricular.RecursosCurriculares.ObjetivoTransversal objetivoTransversal = RecursoCurricular.RecursosCurriculares.ObjetivoTransversal.Get(this.CurrentAnio.Numero, t, g, s, u, o);

                int numero = RecursoCurricular.RecursosCurriculares.ObjetivoTransversalIndicador.Last(objetivoTransversal);

                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal
                {
                    IndicadorItem = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal.Indicador
                    {
                        Numero = numero,
                        Descripcion = string.Empty
                    },
                    TipoEducacionNombre = grado.TipoEducacion.Nombre,
                    GradoNombre = grado.Nombre,
                    SectorNombre = sector.Nombre,
                    UnidadNombre = string.Format("{0}.- {1}", objetivoTransversal.Unidad.Numero, objetivoTransversal.Unidad.Nombre),
                    Descripcion = string.Format("{0}.- {1}", objetivoTransversal.Numero, objetivoTransversal.Descripcion)
                }, JsonRequestBehavior.AllowGet);
            }

            return this.Json("500", JsonRequestBehavior.AllowGet);
        }


        //[Authorize]
        [HttpGet]
        public JsonResult GetObjetivoTransversalIndicadores(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string unidadId, string objetivoTransversalId)
        {
            int t;
            int g;
            Guid s;
            Guid u;
            Guid o;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u) && Guid.TryParse(objetivoTransversalId, out o) && t > 0 && g > 0)
            {
                //RecursoCurricular.RecursosCurriculares.ObjetivoTransversal objetivoTransversal = RecursoCurricular.RecursosCurriculares.ObjetivoTransversal.Get(this.CurrentAnio.Numero, t, g, s, u, o);
                RecursoCurricular.RecursosCurriculares.ObjetivoTransversal objetivoTransversal = RecursoCurricular.RecursosCurriculares.ObjetivoTransversal.Get(2018, t, g, s, u, o);

                RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal.Indicadores indicadores = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal.Indicadores();

                indicadores.data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal.Indicador>();

                foreach (RecursoCurricular.RecursosCurriculares.ObjetivoTransversalIndicador objetivoTransversalIndicador in RecursoCurricular.RecursosCurriculares.ObjetivoTransversalIndicador.GetAll(objetivoTransversal))
                {
                    RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal.Indicador i = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal.Indicador
                    {
                        Numero = objetivoTransversalIndicador.Numero,
                        Descripcion = objetivoTransversalIndicador.Descripcion.Length > 70 ? string.Format("{0}...", objetivoTransversalIndicador.Descripcion.Substring(0, 70)) : objetivoTransversalIndicador.Descripcion,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(objetivoTransversalIndicador.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this, null, null, "editIndicador"),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(objetivoTransversalIndicador.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this, null, null, "deleteIndicador"))
                    };

                    indicadores.data.Add(i);
                }

                return this.Json(indicadores, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal.Indicadores { data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.ObjetivoTransversal.Indicador>() }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}