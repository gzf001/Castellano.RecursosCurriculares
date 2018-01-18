using System;
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
            RecursoCurricular.Membresia.PerfilUsuario perfilUsuario = RecursoCurricular.Membresia.PerfilUsuario.Get(RecursoCurricular.Membresia.Perfil.PerfilAnio.Codigo, new Guid(this.User.Identity.Name));

            using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
            {
                new RecursoCurricular.Membresia.PerfilUsuario
                {
                    PerfilCodigo = RecursoCurricular.Membresia.Perfil.PerfilAnio.Codigo,
                    UsuarioId = this.CurrentUsuario.Id,
                    Url = perfilUsuario.Url,
                    Valor = model.Numero.ToString()
                }.Save(context);

                context.SubmitChanges();
            }

            return this.Redirect(perfilUsuario.Url);

            //return View();
        }
    }
}