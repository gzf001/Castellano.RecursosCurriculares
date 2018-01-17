using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RecursoCurricular.Web
{
    public class Authorization : ActionFilterAttribute
    {
        public RecursoCurricular.Web.ActionType[] ActionType
        {
            get;
            set;
        }

        public string Root
        {
            get;
            set;
        }

        public string Area
        {
            get;
            set;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RecursoCurricular.Persona persona = (filterContext.Controller as RecursoCurricular.Web.Controller).CurrentPersona;

            RecursoCurricular.Membresia.Usuario usuario = (filterContext.Controller as RecursoCurricular.Web.Controller).CurrentUsuario;

            if (filterContext.Controller.ControllerContext.RequestContext.HttpContext.Request.UrlReferrer == null)
            {
                throw new Exception(CustomError.SinPermiso_403.ToString());
            }

            string controller = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();

            RecursoCurricular.Membresia.MenuItem menuItem = RecursoCurricular.Membresia.MenuItem.Get(string.Format("/{0}/{1}/{2}", this.Area, controller, this.Root));

            RecursoCurricular.Membresia.AplicacionPerfil aplicacionPerfil = RecursoCurricular.Membresia.AplicacionPerfil.Get(menuItem == null ? default(Guid) : menuItem.AplicacionId, RecursoCurricular.Membresia.Perfil.PerfilAnio.Codigo);

            if (aplicacionPerfil != null)
            {
                RecursoCurricular.Membresia.PerfilUsuario perfilUsuario = RecursoCurricular.Membresia.PerfilUsuario.Get(RecursoCurricular.Membresia.Perfil.PerfilAnio, usuario);

                if (perfilUsuario == null)
                {


                    throw new Exception(CustomError.SinPerfilEstablecido_500.ToString());
                }
            }

            #region Permisos Efectivos

            if (this.ActionType.Contains<RecursoCurricular.Web.ActionType>(RecursoCurricular.Web.ActionType.Access))
            {
                #region Acceder

                if (!RecursoCurricular.Membresia.RolAccion.Exists(persona, menuItem, RecursoCurricular.Membresia.Accion.Acceder))
                {
                    throw new Exception(CustomError.SinPermiso_403.ToString());
                }

                #endregion
            }
            else if (this.ActionType.Contains<RecursoCurricular.Web.ActionType>(RecursoCurricular.Web.ActionType.Accept))
            {
                #region Aceptar

                if (!RecursoCurricular.Membresia.RolAccion.Exists(persona, menuItem, RecursoCurricular.Membresia.Accion.Aceptar))
                {
                    throw new Exception("No tiene permisos para grabar");
                }

                #endregion
            }
            else if (this.ActionType.Contains<RecursoCurricular.Web.ActionType>(RecursoCurricular.Web.ActionType.Add))
            {
                #region Agregar

                if (!RecursoCurricular.Membresia.RolAccion.Exists(persona, menuItem, RecursoCurricular.Membresia.Accion.Agregar))
                {
                    throw new Exception("No tiene permisos para agregar");
                }

                #endregion
            }
            else if (this.ActionType.Contains<RecursoCurricular.Web.ActionType>(RecursoCurricular.Web.ActionType.Edit))
            {
                #region Editar

                if (!RecursoCurricular.Membresia.RolAccion.Exists(persona, menuItem, RecursoCurricular.Membresia.Accion.Editar))
                {
                    throw new Exception("No tiene permisos para editar");
                }

                #endregion
            }
            else if (this.ActionType.Contains<RecursoCurricular.Web.ActionType>(RecursoCurricular.Web.ActionType.Delete))
            {
                #region Eliminar

                if (!RecursoCurricular.Membresia.RolAccion.Exists(persona, menuItem, RecursoCurricular.Membresia.Accion.Eliminar))
                {
                    throw new Exception("No tiene permisos para eliminar");
                }

                #endregion
            }

            #endregion

            base.OnActionExecuting(filterContext);
        }
    }
}