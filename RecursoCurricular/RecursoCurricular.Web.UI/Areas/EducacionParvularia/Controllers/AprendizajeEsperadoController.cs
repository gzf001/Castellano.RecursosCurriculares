using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.EducacionParvularia.Controllers
{
    public class AprendizajeEsperadoController : RecursoCurricular.Web.Controller
    {
        const string Area = "EducacionParvularia";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "AprendizajesEsperados", Area = Area)]
        public ActionResult AprendizajesEsperados()
        {
            RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.AprendizajeEsperado model = new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.AprendizajeEsperado
            {
                Anio = this.CurrentAnio
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "AprendizajesEsperados", Area = Area)]
        public ActionResult AprendizajesEsperados(RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.AprendizajeEsperado model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                {
                    new RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo
                    {
                        AnoNumero = this.CurrentAnio.Numero,
                        AmbitoExperienciaAprendizajeCodigo = model.AmbitoExperienciaAprendizajeCodigo,
                        NucleoAprendizajeId = model.NucleoAprendizajeId,
                        CicloCodigo = model.CicloCodigo,
                        Id = model.Id,
                        EjeParvuloId = model.IdEje == "-1" ? default(Guid) : new Guid(model.IdEje),
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "AprendizajesEsperados", Area = Area)]
        public JsonResult AddAprendizajeEsperado(string ambitoExperienciaAprendizajeCodigo, string nucleoId, string cicloCodigo)
        {
            int a;
            Guid n;
            short c;

            if (int.TryParse(ambitoExperienciaAprendizajeCodigo, out a) && Guid.TryParse(nucleoId, out n) && short.TryParse(cicloCodigo, out c) && a > 0 && c > 0)
            {
                RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje = RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.Get(this.CurrentAnio.Numero, a);
                RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleAprendizaje = BaseCurricular.NucleoAprendizaje.Get(this.CurrentAnio.Numero, a, n);
                RecursoCurricular.Educacion.Ciclo ciclo = RecursoCurricular.Educacion.Ciclo.Get(c);

                return this.Json(new RecursoCurricular.BaseCurricular.EjeParvulo
                {
                    AmbitoExperienciaAprendizaje = ambitoExperienciaAprendizaje,
                    NucleoAprendizaje = nucleAprendizaje,
                    Ciclo = ciclo,
                    Numero = 1
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "AprendizajesEsperados", Area = Area)]
        public JsonResult EditAprendizajeEsperado(string ambitoExperienciaAprendizajeCodigo, string nucleoId, string cicloCodigo, string id)
        {
            int a;
            Guid n;
            int c;
            Guid i;

            if (int.TryParse(ambitoExperienciaAprendizajeCodigo, out a) && Guid.TryParse(nucleoId, out n) && int.TryParse(cicloCodigo, out c) && Guid.TryParse(id, out i))
            {
                RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo aprendizajeEsperadoParvulo = RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo.Get(this.CurrentAnio.Numero, a, n, c, i);

                return this.Json(new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.AprendizajeEsperado
                {
                    AmbitoExperienciaAprendizajeCodigo = aprendizajeEsperadoParvulo.AmbitoExperienciaAprendizajeCodigo,
                    AmbitoExperienciaAprendizaje = aprendizajeEsperadoParvulo.AmbitoExperienciaAprendizaje,
                    NucleoAprendizajeId = aprendizajeEsperadoParvulo.NucleoAprendizajeId,
                    NucleoAprendizaje = aprendizajeEsperadoParvulo.NucleoAprendizaje,
                    CicloCodigo = aprendizajeEsperadoParvulo.CicloCodigo,
                    Ciclo = aprendizajeEsperadoParvulo.Ciclo,
                    Id = aprendizajeEsperadoParvulo.Id,
                    IdEje = aprendizajeEsperadoParvulo.EjeParvuloId.HasValue ? aprendizajeEsperadoParvulo.EjeParvuloId.Value.ToString() : "-1",
                    Numero = aprendizajeEsperadoParvulo.Numero,
                    Descripcion = aprendizajeEsperadoParvulo.Descripcion
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "AprendizajesEsperados", Area = Area)]
        public JsonResult DeleteAprendizajeEsperado(string ambitoExperienciaAprendizajeCodigo, string nucleoId, string cicloCodigo, string id)
        {
            try
            {
                int a;
                Guid n;
                int c;
                Guid i;

                if (int.TryParse(ambitoExperienciaAprendizajeCodigo, out a) && Guid.TryParse(nucleoId, out n) && int.TryParse(cicloCodigo, out c) && Guid.TryParse(id, out i))
                {
                    RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje = RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.Get(this.CurrentAnio.Numero, a);
                    RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleAprendizaje = BaseCurricular.NucleoAprendizaje.Get(this.CurrentAnio.Numero, a, n);
                    RecursoCurricular.Educacion.Ciclo ciclo = RecursoCurricular.Educacion.Ciclo.Get(c);

                    RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo aprendizajeEsperadoParvulo = RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo.Get(this.CurrentAnio.Numero, a, n, c, i);

                    using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
                    {
                        aprendizajeEsperadoParvulo.Delete(context);

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
        public JsonResult GetAprendizajesEsperados(string ambitoExperienciaAprendizajeCodigo, string nucleoId, string cicloCodigo)
        {
            int a;
            Guid n;
            int c;

            if (int.TryParse(ambitoExperienciaAprendizajeCodigo, out a) && Guid.TryParse(nucleoId, out n) && int.TryParse(cicloCodigo, out c) && a > 0 && c > 0)
            {
                RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.AprendizajeEsperado.AprendizajesEsperados aprendizajesEsperados = new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.AprendizajeEsperado.AprendizajesEsperados();

                aprendizajesEsperados.data = new List<RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.AprendizajeEsperado>();

                RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje = RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.Get(this.CurrentAnio.Numero, a);
                RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleAprendizaje = BaseCurricular.NucleoAprendizaje.Get(this.CurrentAnio.Numero, a, n);
                RecursoCurricular.Educacion.Ciclo ciclo = RecursoCurricular.Educacion.Ciclo.Get(c);

                foreach (RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo aprendizajeEsperadoParvulo in RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo.GetAll(ambitoExperienciaAprendizaje, nucleAprendizaje, ciclo))
                {
                    aprendizajesEsperados.data.Add(new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.AprendizajeEsperado
                    {
                        Numero = aprendizajeEsperadoParvulo.Numero,
                        EjeNombre = aprendizajeEsperadoParvulo.EjeParvuloId.HasValue ? aprendizajeEsperadoParvulo.EjeParvulo.Nombre : "-----",
                        Descripcion = aprendizajeEsperadoParvulo.Descripcion.Length > 70 ? string.Format("{0}...", aprendizajeEsperadoParvulo.Descripcion.Substring(0, 70)) : aprendizajeEsperadoParvulo.Descripcion,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(aprendizajeEsperadoParvulo.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(aprendizajeEsperadoParvulo.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(aprendizajesEsperados, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.AprendizajeEsperado.AprendizajesEsperados { data = new List<RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.AprendizajeEsperado>() }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "AprendizajesEsperados", Area = Area)]
        public JsonResult NumeroAprendizajeEsperado(string ambitoExperienciaAprendizajeCodigo, string nucleoId, string cicloCodigo, string ejeId)
        {
            int a;
            Guid n;
            int c;
            Guid e;

            if (int.TryParse(ambitoExperienciaAprendizajeCodigo, out a) && Guid.TryParse(nucleoId, out n) && int.TryParse(cicloCodigo, out c) && a > 0 && c > 0)
            {
                RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje = RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.Get(this.CurrentAnio.Numero, a);
                RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleAprendizaje = BaseCurricular.NucleoAprendizaje.Get(this.CurrentAnio.Numero, a, n);
                RecursoCurricular.Educacion.Ciclo ciclo = RecursoCurricular.Educacion.Ciclo.Get(c);
                RecursoCurricular.BaseCurricular.EjeParvulo eje = null;

                if (Guid.TryParse(ejeId, out e))
                {
                    eje = BaseCurricular.EjeParvulo.Get(this.CurrentAnio.Numero, a, n, c, e);
                }

                //int numero = RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo.Last(ambitoExperienciaAprendizaje, nucleAprendizaje, ciclo, eje);
                int numero = RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo.Last(ambitoExperienciaAprendizaje, nucleAprendizaje, ciclo, eje);

                return this.Json(numero, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
        }
    }
}