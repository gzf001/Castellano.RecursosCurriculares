using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.Home.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(RecursoCurricular.Web.UI.Areas.Home.Models.Login model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string textoRun = model.Run.Replace(".", string.Empty).Replace("-", string.Empty);

            int runCuerpo = int.Parse(textoRun.Substring(0, textoRun.Length - 1));
            char runDigito = char.Parse(textoRun.Replace(runCuerpo.ToString(), string.Empty));

            RecursoCurricular.Persona persona = RecursoCurricular.Persona.Get(runCuerpo, runDigito);

            RecursoCurricular.Membresia.LoginStatus loginStatus = RecursoCurricular.Membresia.Account.DoLogin(persona, model.Password);

            if (loginStatus == RecursoCurricular.Membresia.LoginStatus.InvalidRunOrPassword)
            {
                this.ModelState.AddModelError("loginError", "R.U.N. o contraseña incorrectos. Verifique sus datos e inténte acceder nuevamente.");

                return this.View(model);
            }
            else if (loginStatus == RecursoCurricular.Membresia.LoginStatus.NotAccessAllowed)
            {
                this.ModelState.AddModelError("loginError", "Usted no tiene suficientes permisos para ingresar a la aplicación. Por favor contacte al administrador.");

                return this.View(model);
            }
            else if (loginStatus == RecursoCurricular.Membresia.LoginStatus.UserApprovedOut)
            {
                this.ModelState.AddModelError("loginError", "Su cuenta de acceso a sido caducada. Por favor contacte al administrador del sistema.");

                return this.View(model);
            }
            else if (loginStatus == RecursoCurricular.Membresia.LoginStatus.UserLocked)
            {
                this.ModelState.AddModelError("loginError", "Su cuenta de acceso a sido bloqueada por exceder el máximo de intentos fallidos permitidos.");

                return this.View(model);
            }

            System.Web.Security.FormsAuthenticationTicket ticket = new System.Web.Security.FormsAuthenticationTicket(1, persona.Id.ToString(), DateTime.Now, DateTime.Now.AddYears(1), model.RememberMe, "RecursoCurricular_AUTHENTICATE");

            string cookie = System.Web.Security.FormsAuthentication.Encrypt(ticket);

            HttpCookie httpCookie = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, cookie);

            System.Web.Security.FormsAuthentication.SetAuthCookie(persona.Id.ToString(), model.RememberMe);

            this.Response.Cookies.Add(httpCookie);

            return this.RedirectToAction("Index", "Admin", new { area = "Administracion" });
        }

        public ActionResult RecoveryPass()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RecoveryPass(RecursoCurricular.Web.UI.Areas.Home.Models.RecoveryPass model)
        {
            string textoRun = model.RUN.Replace(".", string.Empty).Replace("-", string.Empty);

            int runCuerpo = int.Parse(textoRun.Substring(0, textoRun.Length - 1));
            char runDigito = char.Parse(textoRun.Replace(runCuerpo.ToString(), string.Empty));

            RecursoCurricular.Membresia.PasswordRecoveryStatus passwordRecoveryStatus = RecursoCurricular.Membresia.Account.DoPasswordRecovery(runCuerpo, runDigito);

            if (passwordRecoveryStatus == RecursoCurricular.Membresia.PasswordRecoveryStatus.EmailNotRegistered)
            {
                this.ModelState.AddModelError("recoveryError", "El usuario no tiene un correo electrónico registrado. Comuniquese con soporte@netcore.cl para asistirle en la restitución de su contraseña.");

                return this.View(model);
            }
            else if (passwordRecoveryStatus == RecursoCurricular.Membresia.PasswordRecoveryStatus.UserNotFound)
            {
                this.ModelState.AddModelError("recoveryError", "No se ha encontrado el usuario especificado. Verifique que haya escrito correctamente su R.U.N. o contacte al administrador del sistema.");

                return this.View(model);
            }

            this.ViewBag.Message = "Sus datos de acceso han sido enviados a su casilla de correo. Por favor revise el mensaje enviado y siga las intrucciones especificadas.";

            return this.View();

        }
    }
}