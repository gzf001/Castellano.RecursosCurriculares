using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Controllers
{
    public class ObjetivoAprendizajeController : RecursoCurricular.Web.Controller
    {
        const string Area = "BasesCurriculares";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "ObjetivosAprendizaje", Area = Area)]
        public ActionResult ObjetivosAprendizaje()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "ObjetivosAprendizaje", Area = Area)]
        public ActionResult ObjetivosAprendizaje(RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizaje model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    new RecursoCurricular.BaseCurricular.ObjetivoAprendizaje
                    {
                        TipoEducacionCodigo = model.TipoEducacionCodigo,
                        AnoNumero = this.CurrentAnio.Numero,
                        GradoCodigo = model.GradoCodigo,
                        SectorId = model.SectorId,
                        EjeId = model.EjeId,
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "ObjetivosAprendizaje", Area = Area)]
        public JsonResult AddObjetivoAprendizaje(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string ejeId)
        {
            int t;
            int g;
            Guid s;
            Guid e;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(ejeId, out e) && t > 0 && g > 0)
            {
                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);
                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);
                RecursoCurricular.BaseCurricular.Eje eje = RecursoCurricular.BaseCurricular.Eje.Get(this.CurrentAnio.Numero, s, e);

                int numero = RecursoCurricular.BaseCurricular.ObjetivoAprendizaje.Last(grado, sector, eje);

                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizaje
                {
                    TipoEducacion = grado.TipoEducacion,
                    Grado = grado,
                    Sector = sector,
                    Eje = eje,
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "ObjetivosAprendizaje", Area = Area)]
        public JsonResult EditObjetivoAprendizaje(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string ejeId, string id)
        {
            int t;
            int g;
            Guid s;
            Guid e;
            Guid i;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(ejeId, out e) && Guid.TryParse(id, out i) && t > 0 && g > 0)
            {
                RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje = RecursoCurricular.BaseCurricular.ObjetivoAprendizaje.Get(t, this.CurrentAnio.Numero, g, s, e, i);

                return this.Json(objetivoAprendizaje, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "ObjetivosAprendizaje", Area = Area)]
        public JsonResult DeleteObjetivoAprendizaje(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string ejeId, string id)
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
                    RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje = RecursoCurricular.BaseCurricular.ObjetivoAprendizaje.Get(t, this.CurrentAnio.Numero, g, s, e, i);

                    using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                    {
                        objetivoAprendizaje.Delete(context);

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
        public JsonResult GetObjetivosAprendizaje(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string ejeId)
        {
            int t;
            int g;
            Guid s;
            Guid e;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(ejeId, out e) && t > 0 && g > 0)
            {
                RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizaje.ObjetivosAprendizaje objetivosAprendizaje = new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizaje.ObjetivosAprendizaje();

                objetivosAprendizaje.data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizaje>();

                RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(t, g);
                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);
                RecursoCurricular.BaseCurricular.Eje eje = RecursoCurricular.BaseCurricular.Eje.Get(this.CurrentAnio.Numero, s, e);

                foreach (RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje in RecursoCurricular.BaseCurricular.ObjetivoAprendizaje.GetAll(grado, sector, eje))
                {
                    objetivosAprendizaje.data.Add(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizaje
                    {
                        Numero = objetivoAprendizaje.Numero,
                        Descripcion = objetivoAprendizaje.Descripcion.Length > 250 ? string.Format("{0}...", objetivoAprendizaje.Descripcion.Substring(0, 200)) : objetivoAprendizaje.Descripcion,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(objetivoAprendizaje.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(objetivoAprendizaje.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(objetivosAprendizaje, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizaje.ObjetivosAprendizaje { data = new List<RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizaje>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}