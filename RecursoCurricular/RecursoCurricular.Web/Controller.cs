using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace RecursoCurricular.Web
{
    //[AuthorizeSessionAttribute]
    public abstract class Controller : System.Web.Mvc.Controller
    {
        public RecursoCurricular.Membresia.MenuItem CurrentMenuItem
        {
            get
            {
                if (this.Request == null)
                {
                    return null;
                }
                else
                {
                    RecursoCurricular.Membresia.MenuItem menuItem;

                    if (this.Request.UrlReferrer == null)
                    {
                        menuItem = RecursoCurricular.Membresia.MenuItem.Get(this.Request.CurrentExecutionFilePath);
                    }
                    else
                    {
                        menuItem = RecursoCurricular.Membresia.MenuItem.Get(this.Request.UrlReferrer.AbsolutePath);
                    }

                    return menuItem;
                }
            }
        }

        public RecursoCurricular.Persona CurrentPersona
        {
            get
            {
                if (string.IsNullOrEmpty(this.User.Identity.Name))
                {
                    return null;
                }

                RecursoCurricular.Persona persona = RecursoCurricular.Persona.Get(new Guid(this.User.Identity.Name));

                return persona;
            }
        }

        public RecursoCurricular.Membresia.Usuario CurrentUsuario
        {
            get
            {
                if (string.IsNullOrEmpty(this.User.Identity.Name))
                {
                    return null;
                }

                RecursoCurricular.Membresia.Usuario usuario = RecursoCurricular.Membresia.Usuario.Get(new Guid(this.User.Identity.Name));

                return usuario;
            }
        }

        public string GetError()
        {
            string text = string.Empty;

            foreach (ModelError error in this.ModelState.Values.Where<ModelState>(x => x.Errors.Any<ModelError>()).SelectMany<ModelState, ModelError>(x => x.Errors).Distinct())
            {
                if (error.ErrorMessage.Contains("El valor '-1' no es válido para"))
                {
                    text += error.ErrorMessage.Replace("El valor '-1' no es válido para", "Debe seleccionar ") + Environment.NewLine;
                }
                else
                {
                    text += error.ErrorMessage + Environment.NewLine;
                }
            }

            return text;
        }
    }
}