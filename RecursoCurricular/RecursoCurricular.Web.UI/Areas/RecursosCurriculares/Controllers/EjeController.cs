﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Controllers
{
    public class EjeController : RecursoCurricular.Web.Controller
    {
        const string Area = "RecursosCurriculares";

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Ejes", Area = Area)]
        public ActionResult Ejes()
        {
            RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Eje model = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Eje();

            foreach (RecursoCurricular.Educacion.TipoEducacion tipoEducacion in RecursoCurricular.Educacion.TipoEducacion.GetAll().FindAll(x => x.Codigo > 10))
            {
                model.TiposEducacion.Add(new SelectListItem
                {
                    Text = tipoEducacion.Nombre,
                    Value = tipoEducacion.Codigo.ToString()
                });
            }

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Ejes", Area = Area)]
        public ActionResult Ejes(RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Eje model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(this.GetError());
            }

            try
            {
                RecursoCurricular.RecursosCurriculares.Eje eje = new RecursoCurricular.RecursosCurriculares.Eje
                {
                    AnoNumero = this.CurrentAnio.Numero,
                    SectorId = model.SectorId,
                    Id = model.Id,
                    Numero = model.Numero,
                    Nombre = model.Nombre.Trim()
                };

                using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                {
                    foreach (RecursoCurricular.RecursosCurriculares.TipoEducacionEje tipoEducacionEje in RecursoCurricular.RecursosCurriculares.TipoEducacionEje.GetAll(eje))
                    {
                        tipoEducacionEje.Delete(context);
                    }

                    context.SubmitChanges();
                }

                using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                {
                    eje.Save(context);

                    foreach (int tipoEducacionCodigo in model.SelectedTipoEducacion)
                    {
                        new RecursoCurricular.RecursosCurriculares.TipoEducacionEje
                        {
                            TipoEducacionCodigo = tipoEducacionCodigo,
                            AnoNumero = this.CurrentAnio.Numero,
                            SectorId = model.SectorId,
                            EjeId = model.Id
                        }.Save(context);
                    }

                    context.SubmitChanges();

                    eje.SyncUp();
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
        public JsonResult AddEje(string sectorId)
        {
            Guid s;

            if (Guid.TryParse(sectorId, out s))
            {
                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                int numero = RecursoCurricular.RecursosCurriculares.Eje.Last(this.CurrentAnio, sector);

                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Eje
                {
                    Sector = sector,
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
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Ejes", Area = Area)]
        public JsonResult EditEje(string sectorId, string id)
        {
            Guid s;
            Guid i;

            if (Guid.TryParse(sectorId, out s) && Guid.TryParse(id, out i))
            {
                RecursoCurricular.RecursosCurriculares.Eje eje = RecursoCurricular.RecursosCurriculares.Eje.Get(this.CurrentAnio.Numero, s, i);

                List<RecursoCurricular.RecursosCurriculares.TipoEducacionEje> tiposEducacionEjes = RecursoCurricular.RecursosCurriculares.TipoEducacionEje.GetAll(eje);

                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Eje
                {
                    Sector = eje.Sector,
                    Id = eje.Id,
                    Numero = eje.Numero,
                    Nombre = eje.Nombre,
                    SelectedTipoEducacion = tiposEducacionEjes.Any() ? tiposEducacionEjes.Select<RecursoCurricular.RecursosCurriculares.TipoEducacionEje, int>(x => x.TipoEducacionCodigo).ToList<int>() : null
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "Ejes", Area = Area)]
        public JsonResult DeleteEje(string sectorId, string id)
        {
            try
            {
                Guid s;
                Guid i;

                if (Guid.TryParse(sectorId, out s) && Guid.TryParse(id, out i))
                {
                    RecursoCurricular.RecursosCurriculares.Eje eje = RecursoCurricular.RecursosCurriculares.Eje.Get(this.CurrentAnio.Numero, s, i);

                    using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                    {
                        eje.Delete(context);

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
        public JsonResult GetEjes(string sectorId)
        {
            Guid s;

            if (Guid.TryParse(sectorId, out s))
            {
                RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Eje.Ejes ejes = new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Eje.Ejes();

                ejes.data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Eje>();

                RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(s);

                foreach (RecursoCurricular.RecursosCurriculares.Eje eje in RecursoCurricular.RecursosCurriculares.Eje.GetAll(this.CurrentAnio, sector))
                {
                    string tiposEducacion = string.Empty;

                    foreach (RecursoCurricular.RecursosCurriculares.TipoEducacionEje tipoEducacionEje in RecursoCurricular.RecursosCurriculares.TipoEducacionEje.GetAll(eje))
                    {
                        tiposEducacion += string.Format("<div>{0}</div>", tipoEducacionEje.TipoEducacion.Nombre);
                    }

                    ejes.data.Add(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Eje
                    {
                        Numero = eje.Numero,
                        Nombre = eje.Nombre,
                        TipoEducacionNombre = tiposEducacion,
                        Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(eje.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                         RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(eje.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                    });
                }

                return this.Json(ejes, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Eje.Ejes { data = new List<RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models.Eje>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}