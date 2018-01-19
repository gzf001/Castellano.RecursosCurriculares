using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.EducacionParvularia.Controllers
{
    public class EjeController : Controller
    {
        const string Area = "EducacionParvularia";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Ejes", Area = Area)]
        public ActionResult Ejes()
        {
            RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje model = new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje
            {
                Anio = this.CurrentAnio
            };

            return this.View(model);
        }

        [Authorize]
        [HttpGet]
        public JsonResult Nucleos(string ambitoExperienciaAprendizajeCodigo)
        {
            int ambito;

            if (int.TryParse(ambitoExperienciaAprendizajeCodigo, out ambito))
            {
                IEnumerable<SelectListItem> selectList = RecursoCurricular.BaseCurricular.NucleoAprendizaje.Nucleos(this.CurrentAnio.Numero, ambito);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                throw new Exception("Intento fallido de extracción de información");
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult Ciclos()
        {
            return this.Json(RecursoCurricular.Educacion.Ciclo.Ciclos, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Ejes", Area = Area)]
        public ActionResult Dimensiones(RecursoCurricular.Web.UI.Areas.Tic.Models.Dimension model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                RecursoCurricular.DimensionHabilidadTic dimensionHabilidadTic = RecursoCurricular.DimensionHabilidadTic.Get(model.Id, this.CurrentAnio.Numero);

                using (RecursoCurricular.Context context = new RecursoCurricular.Context())
                {
                    new RecursoCurricular.DimensionHabilidadTic
                    {
                        Id = model.Id,
                        AnoNumero = this.CurrentAnio.Numero,
                        Numero = model.Numero,
                        Nombre = model.Nombre.Trim(),
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "Ejes", Area = Area)]
        public ActionResult AddDimension()
        {
            int numero = RecursoCurricular.DimensionHabilidadTic.Last(this.CurrentAnio);

            return this.Json(new RecursoCurricular.DimensionHabilidadTic { Numero = numero }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Ejes", Area = Area)]
        public ActionResult EditDimension(Guid id)
        {
            RecursoCurricular.DimensionHabilidadTic dimensionHabilidadTic = RecursoCurricular.DimensionHabilidadTic.Get(id, this.CurrentAnio.Numero);

            return this.Json(new RecursoCurricular.Web.UI.Areas.Tic.Models.Dimension
            {
                Id = dimensionHabilidadTic.Id,
                Numero = dimensionHabilidadTic.Numero,
                Nombre = dimensionHabilidadTic.Nombre,
                Descripcion = dimensionHabilidadTic.Descripcion
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "Ejes", Area = Area)]
        public JsonResult DeleteDimension(Guid id)
        {
            RecursoCurricular.DimensionHabilidadTic dimensionHabilidadTic = RecursoCurricular.DimensionHabilidadTic.Get(id, this.CurrentAnio.Numero);

            try
            {
                using (RecursoCurricular.Context context = new RecursoCurricular.Context())
                {
                    new RecursoCurricular.DimensionHabilidadTic
                    {
                        Id = dimensionHabilidadTic.Id,
                        AnoNumero = dimensionHabilidadTic.AnoNumero,
                        Numero = dimensionHabilidadTic.Numero,
                        Nombre = dimensionHabilidadTic.Nombre,
                        Descripcion = dimensionHabilidadTic.Descripcion
                    }.Delete(context);

                    context.SubmitChanges();
                }

                return this.Json("200", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetEjes(string ambitoExperienciaAprendizajeCodigo, string nucleoId, string cicloCodigo)
        {
            int a;
            Guid n;
            int c;

            if (int.TryParse(ambitoExperienciaAprendizajeCodigo, out a) && Guid.TryParse(nucleoId, out n) && int.TryParse(cicloCodigo, out c))
            {
                RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje.Ejes ejes = new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje.Ejes();

                ejes.data = new List<RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje>();

                RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje = RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje.Get(this.CurrentAnio.Numero, a);
                RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleAprendizaje = BaseCurricular.NucleoAprendizaje.Get(this.CurrentAnio.Numero, a, n);
                RecursoCurricular.Educacion.Ciclo ciclo = RecursoCurricular.Educacion.Ciclo.Get(c);

                foreach (RecursoCurricular.BaseCurricular.EjeParvulo ejeParvulo in RecursoCurricular.BaseCurricular.EjeParvulo.GetAll(ambitoExperienciaAprendizaje, nucleAprendizaje, ciclo))
                {
                    ejes.data.Add(new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje
                    {
                        Id = ejeParvulo.Id,
                        Numero = ejeParvulo.Numero,
                        Nombre = ejeParvulo.Nombre,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(ejeParvulo.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(ejeParvulo.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(ejes, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje.Ejes { data = new List<RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models.Eje>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}