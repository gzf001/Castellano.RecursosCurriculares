﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Controllers
{
    public class SelectorController : RecursoCurricular.Web.Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Selector()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Selector(RecursoCurricular.Web.UI.Models.Selector model)
        {
            using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
            {
                new RecursoCurricular.Membresia.PerfilUsuario
                {
                    PerfilCodigo = RecursoCurricular.Membresia.Perfil.PerfilAnio.Codigo,
                    UsuarioId = this.CurrentUsuario.Id,
                    Valor = model.Numero.ToString()
                }.Save(context);

                context.SubmitChanges();
            }

            return View();
        }
    }
}