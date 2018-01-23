using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.Administracion.Controllers
{
    public class AdminController : RecursoCurricular.Web.Controller
    {
        const string Area = "Administracion";

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        #region Aplicacion

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Aplicaciones", Area = Area)]
        public ActionResult Aplicaciones()
        {
            RecursoCurricular.Web.UI.Areas.Administracion.Models.Aplicacion model = new RecursoCurricular.Web.UI.Areas.Administracion.Models.Aplicacion();

            foreach (RecursoCurricular.Membresia.Perfil perfil in RecursoCurricular.Membresia.Perfil.GetAll())
            {
                model.Perfiles.Add(new SelectListItem
                {
                    Text = perfil.Nombre,
                    Value = perfil.Codigo.ToString()
                });
            }

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Aplicaciones", Area = Area)]
        public ActionResult Aplicaciones(RecursoCurricular.Web.UI.Areas.Administracion.Models.Aplicacion model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            RecursoCurricular.Membresia.Aplicacion aplicacion = RecursoCurricular.Membresia.Aplicacion.Get(model.Id);

            using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
            {
                foreach (RecursoCurricular.Membresia.Perfil perfil in RecursoCurricular.Membresia.Perfil.GetAll())
                {
                    new RecursoCurricular.Membresia.AplicacionPerfil
                    {
                        AplicacionId = model.Id,
                        PerfilCodigo = perfil.Codigo
                    }.Delete(context);
                }

                context.SubmitChanges();
            }

            using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
            {
                new RecursoCurricular.Membresia.Aplicacion
                {
                    Id = model.Id,
                    MenuItemId = aplicacion == null ? default(Guid) : aplicacion.MenuItemId,
                    Nombre = model.Nombre,
                    Clave = model.Clave,
                    Fa_Icon = model.Fa_Icon,
                    Orden = model.Orden
                }.Save(context);

                foreach (int perfilCodigo in model.SelectedPerfil)
                {
                    new RecursoCurricular.Membresia.AplicacionPerfil
                    {
                        AplicacionId = model.Id,
                        PerfilCodigo = perfilCodigo
                    }.Save(context);
                }

                context.SubmitChanges();
            }

            if (aplicacion == null)
            {
                using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
                {
                    Guid menuItemId = Guid.NewGuid();

                    RecursoCurricular.Membresia.MenuItem menuItem = new RecursoCurricular.Membresia.MenuItem
                    {
                        AplicacionId = model.Id,
                        Id = menuItemId,
                        Nombre = "Inicio",
                        Informacion = default(string),
                        Icono = default(string),
                        Url = default(string),
                        Visible = true,
                        Orden = model.Orden * 1000
                    };

                    menuItem.Save(context);

                    new RecursoCurricular.Membresia.Aplicacion
                    {
                        Id = model.Id,
                        MenuItemId = menuItem.Id,
                        Nombre = model.Nombre,
                        Clave = model.Clave,
                        Fa_Icon = model.Fa_Icon,
                        Orden = model.Orden
                    }.Save(context);

                    context.SubmitChanges();
                }
            }

            return this.Json("ok 200", JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "Aplicaciones", Area = Area)]
        public ActionResult AddAplicacion()
        {
            return this.Json(new RecursoCurricular.Membresia.Aplicacion(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Aplicaciones", Area = Area)]
        public ActionResult EditAplicacion(Guid id)
        {
            RecursoCurricular.Membresia.Aplicacion aplicacion = RecursoCurricular.Membresia.Aplicacion.Get(id);

            List<RecursoCurricular.Membresia.AplicacionPerfil> aplicacionPerfiles = RecursoCurricular.Membresia.AplicacionPerfil.GetAll(aplicacion);

            return this.Json(new RecursoCurricular.Web.UI.Areas.Administracion.Models.Aplicacion
            {
                Id = aplicacion.Id,
                Nombre = aplicacion.Nombre,
                Clave = aplicacion.Clave,
                Fa_Icon = aplicacion.Fa_Icon,
                Orden = aplicacion.Orden,
                SelectedPerfil = aplicacionPerfiles.Any() ? aplicacionPerfiles.Select<RecursoCurricular.Membresia.AplicacionPerfil, int>(x => x.PerfilCodigo).ToList<int>() : null
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetAplicaciones()
        {
            RecursoCurricular.Web.UI.Areas.Administracion.Models.Aplicacion.Aplicaciones aplicaciones = new RecursoCurricular.Web.UI.Areas.Administracion.Models.Aplicacion.Aplicaciones();

            aplicaciones.data = new List<RecursoCurricular.Web.UI.Areas.Administracion.Models.Aplicacion>();

            foreach (RecursoCurricular.Membresia.Aplicacion aplicacion in RecursoCurricular.Membresia.Aplicacion.GetAll())
            {
                aplicaciones.data.Add(new RecursoCurricular.Web.UI.Areas.Administracion.Models.Aplicacion
                {
                    Id = aplicacion.Id,
                    Nombre = aplicacion.Nombre,
                    Clave = aplicacion.Clave,
                    Orden = aplicacion.Orden,
                    Accion = string.Format("{0}{1}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(aplicacion.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                     RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(aplicacion.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                });
            }

            return this.Json(aplicaciones, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "Aplicaciones", Area = Area)]
        public JsonResult DeleteAplicacion(Guid id)
        {
            RecursoCurricular.Membresia.Aplicacion aplicacion = RecursoCurricular.Membresia.Aplicacion.Get(id);

            using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
            {
                new RecursoCurricular.Membresia.Aplicacion
                {
                    Id = aplicacion.Id,
                    MenuItemId = aplicacion.MenuItemId,
                    Nombre = aplicacion.Nombre,
                    Clave = aplicacion.Clave,
                    Fa_Icon = aplicacion.Fa_Icon,
                    Orden = aplicacion.Orden
                }.Delete(context);

                context.SubmitChanges();
            }

            return this.Json("200 ok", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Ítems de menú

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "ItemsMenu", Area = Area)]
        public ActionResult ItemsMenu()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ItemsMenu(RecursoCurricular.Web.UI.Areas.Administracion.Models.MenuItem model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            try
            {
                RecursoCurricular.Membresia.MenuItem menuItem = RecursoCurricular.Membresia.MenuItem.Get(model.AplicacionId, model.Id);

                using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
                {
                    RecursoCurricular.Membresia.MenuItem m = new RecursoCurricular.Membresia.MenuItem
                    {
                        AplicacionId = model.AplicacionId,
                        Id = model.Id,
                        MenuItemId = model.MenuItemId,
                        Nombre = model.Nombre,
                        Informacion = model.Informacion,
                        Icono = model.Icono,
                        Url = model.Url,
                        Visible = model.Visible,
                    };

                    if (menuItem == null)
                    {
                        menuItem = RecursoCurricular.Membresia.MenuItem.Get(model.AplicacionId, model.MenuItemId.Value);

                        m.Orden = RecursoCurricular.Membresia.MenuItem.Last(menuItem);
                    }
                    else
                    {
                        m.Orden = menuItem.Orden;
                    }

                    m.Save(context);

                    foreach (RecursoCurricular.Membresia.Accion accion in RecursoCurricular.Membresia.Accion.GetAll())
                    {
                        new RecursoCurricular.Membresia.MenuItemAccion
                        {
                            AplicacionId = model.AplicacionId,
                            MenuItemId = model.Id,
                            AccionCodigo = accion.Codigo
                        }.Save(context);
                    }

                    context.SubmitChanges();
                }

                return this.View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "ItemsMenu", Area = Area)]
        public JsonResult EditItemMenu(Guid aplicacionId, Guid itemId)
        {
            RecursoCurricular.Membresia.MenuItem menuItem = RecursoCurricular.Membresia.MenuItem.Get(aplicacionId, itemId);

            RecursoCurricular.Web.UI.Areas.Administracion.Models.MenuItem m = new RecursoCurricular.Web.UI.Areas.Administracion.Models.MenuItem
            {
                Nombre = menuItem.Nombre,
                Informacion = menuItem.Informacion,
                Url = menuItem.Url,
                Visible = menuItem.Visible
            };

            return this.Json(m, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult DeleteItemsMenu(Guid aplicacionId, Guid itemId)
        {
            try
            {
                RecursoCurricular.Membresia.MenuItem menuItem = RecursoCurricular.Membresia.MenuItem.Get(aplicacionId, itemId);

                using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
                {
                    foreach (RecursoCurricular.Membresia.MenuItem m in RecursoCurricular.Membresia.MenuItem.GetAll(menuItem))
                    {
                        new RecursoCurricular.Membresia.MenuItem
                        {
                            AplicacionId = m.AplicacionId,
                            Id = m.Id,
                            MenuItemId = m.MenuItemId,
                            Nombre = m.Nombre,
                            Informacion = m.Informacion,
                            Icono = m.Icono,
                            Url = m.Url,
                            Visible = m.Visible
                        }.Delete(context);

                        context.SubmitChanges();
                    }

                    new RecursoCurricular.Membresia.MenuItem
                    {
                        AplicacionId = menuItem.AplicacionId,
                        Id = menuItem.Id,
                        MenuItemId = menuItem.MenuItemId,
                        Nombre = menuItem.Nombre,
                        Informacion = menuItem.Informacion,
                        Icono = menuItem.Icono,
                        Url = menuItem.Url,
                        Visible = menuItem.Visible
                    }.Delete(context);

                    context.SubmitChanges();
                }

                return this.Json("200 ok", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetOrder(string data)
        {
            RecursoCurricular.Membresia.MenuItem.OrderMenu(data);

            return this.Json("200 ok", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetItemsMenu(Guid aplicacionId)
        {
            RecursoCurricular.Membresia.Aplicacion aplicacion = RecursoCurricular.Membresia.Aplicacion.Get(aplicacionId);

            return this.Json(RecursoCurricular.Helpers.MenuExtension.MenuOrderable(null, aplicacion, this).ToString(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Roles

        [Authorize]
        [HttpGet]
        public ActionResult Roles()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Roles(RecursoCurricular.Web.UI.Areas.Administracion.Models.Rol model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            RecursoCurricular.Membresia.Rol rol = RecursoCurricular.Membresia.Rol.Get(model.Id);

            using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
            {
                new RecursoCurricular.Membresia.Rol
                {
                    Id = model.Id,
                    Nombre = model.Nombre.Trim(),
                    Clave = string.IsNullOrEmpty(model.Clave) ? default(string) : model.Clave.Trim()
                }.Save(context);

                context.SubmitChanges();
            }

            return this.Json("200 ok", JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetRoles()
        {
            RecursoCurricular.Web.UI.Areas.Administracion.Models.Rol.Roles rol = new RecursoCurricular.Web.UI.Areas.Administracion.Models.Rol.Roles();

            rol.data = new List<RecursoCurricular.Web.UI.Areas.Administracion.Models.Rol>();

            foreach (RecursoCurricular.Membresia.Rol r in RecursoCurricular.Membresia.Rol.GetAll())
            {
                rol.data.Add(new RecursoCurricular.Web.UI.Areas.Administracion.Models.Rol
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Clave = r.Clave,
                    Accion = string.Format("{0}{1}{2}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLink(null, null, string.Format("GetPermissions/{0}", r.Id), "Admin", "Administracion", RecursoCurricular.Helpers.TypeButton.Accept, r.Id, "btn btn-success btn-xs btn-flat", "fa-legal", "Configurar permisos", null, this),
                                                        RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(r.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                        RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(r.Id, null, RecursoCurricular.Helpers.TypeButton.Delete, this))
                });
            }

            return this.Json(rol, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "Roles", Area = Area)]
        public ActionResult AddRol()
        {
            return this.Json(new RecursoCurricular.Web.UI.Areas.Administracion.Models.Rol(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult EditRol(Guid id)
        {
            RecursoCurricular.Membresia.Rol rol = RecursoCurricular.Membresia.Rol.Get(id);

            return this.Json(new RecursoCurricular.Web.UI.Areas.Administracion.Models.Rol
            {
                Id = rol.Id,
                Nombre = rol.Nombre,
                Clave = rol.Clave
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult DeleteRol(Guid id)
        {
            RecursoCurricular.Membresia.Rol rol = RecursoCurricular.Membresia.Rol.Get(id);

            using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
            {
                new RecursoCurricular.Membresia.Rol
                {
                    Id = rol.Id,
                    Nombre = rol.Nombre,
                    Clave = rol.Clave,
                }.Delete(context);

                context.SubmitChanges();
            }

            return this.Json("200 ok", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetPermissions(Guid rolId)
        {
            RecursoCurricular.Membresia.Rol rol = RecursoCurricular.Membresia.Rol.Get(rolId);

            return this.View(new RecursoCurricular.Web.UI.Areas.Administracion.Models.RolAccion
            {
                RolId = rol.Id,
                Rol = rol,
                AplicacionId = default(Guid),
                MenuItemId = default(Guid),
                AccionCodigo = default(int)
            });
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Roles", Area = Area)]
        public ActionResult GetPermissions(List<RecursoCurricular.Web.UI.Areas.Administracion.Models.RolAccion> model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            RecursoCurricular.Membresia.Aplicacion aplicacion = RecursoCurricular.Membresia.Aplicacion.Get(model.First<RecursoCurricular.Web.UI.Areas.Administracion.Models.RolAccion>().AplicacionId);

            RecursoCurricular.Membresia.Rol rol = RecursoCurricular.Membresia.Rol.Get(model.First<RecursoCurricular.Web.UI.Areas.Administracion.Models.RolAccion>().RolId);

            using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
            {
                foreach (RecursoCurricular.Membresia.RolAccion rolAccion in RecursoCurricular.Membresia.RolAccion.GetAll(aplicacion, rol).Where<RecursoCurricular.Membresia.RolAccion>(x => !x.MenuItem.Equals(RecursoCurricular.Membresia.MenuItem.RolPermiso)))
                {
                    new RecursoCurricular.Membresia.RolAccion
                    {
                        RolId = rolAccion.RolId,
                        AplicacionId = rolAccion.AplicacionId,
                        MenuItemId = rolAccion.MenuItemId,
                        AccionCodigo = rolAccion.AccionCodigo
                    }.Delete(context);
                }

                context.SubmitChanges();
            }

            using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
            {
                foreach (RecursoCurricular.Web.UI.Areas.Administracion.Models.RolAccion rolAccion in model.Where<RecursoCurricular.Web.UI.Areas.Administracion.Models.RolAccion>(x => x.AccionCodigo != default(int)))
                {
                    new RecursoCurricular.Membresia.RolAccion
                    {
                        RolId = rolAccion.RolId,
                        AplicacionId = rolAccion.AplicacionId,
                        MenuItemId = rolAccion.MenuItemId,
                        AccionCodigo = rolAccion.AccionCodigo
                    }.Save(context);
                    context.SubmitChanges();

                }
            }

            return this.Json("200 ok", JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetRolAccion(Guid rolId, string aplicacionId)
        {
            Guid apId;

            RecursoCurricular.Web.UI.Areas.Administracion.Models.RolAccion.RolAcciones rolAcciones = new RecursoCurricular.Web.UI.Areas.Administracion.Models.RolAccion.RolAcciones();

            if (Guid.TryParse(aplicacionId, out apId))
            {
                RecursoCurricular.Membresia.Rol rol = RecursoCurricular.Membresia.Rol.Get(rolId);

                RecursoCurricular.Membresia.Aplicacion aplicacion = RecursoCurricular.Membresia.Aplicacion.Get(apId);

                List<RecursoCurricular.Membresia.Accion> acciones = RecursoCurricular.Membresia.Accion.GetAll();

                foreach (RecursoCurricular.Membresia.MenuItem menuItem in RecursoCurricular.Membresia.MenuItem.GetAll(aplicacion).FindAll(x => !x.Equals(aplicacion.Inicio)))
                {
                    RecursoCurricular.Web.UI.Areas.Administracion.Models.RolAccion model = new RecursoCurricular.Web.UI.Areas.Administracion.Models.RolAccion
                    {
                        RolId = rol.Id,
                        AplicacionId = aplicacion.Id,
                        MenuItemId = menuItem.Id,
                        MenuItemNombre = menuItem.Nombre
                    };

                    model.AccionNombre += "<div class='option-group field'>";

                    if (!menuItem.RootNode)
                    {
                        foreach (RecursoCurricular.Membresia.Accion accion in acciones)
                        {
                            bool exists = RecursoCurricular.Membresia.RolAccion.Exists(rol, menuItem, accion);

                            model.AccionNombre += string.Format("<label class='option'><input type='checkbox' {0} name='Accion' data-parent={1} data-value={2}><span class='checkbox'></span>{3}</label></label>", exists ? "checked= '{0}'" : string.Empty, menuItem.Id, accion.Codigo, accion.Nombre);

                            model.Acciones.Add(new SelectListItem
                            {
                                Text = accion.Nombre,
                                Value = accion.Codigo.ToString()
                            });
                        }
                    }

                    model.AccionNombre += "</div>";

                    rolAcciones.data.Add(model);
                }

                return this.Json(rolAcciones, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(rolAcciones, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Usuarios

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Usuarios", Area = Area)]
        public ActionResult Usuarios()
        {
            RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario usuario = new RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario();

            usuario.FindType = RecursoCurricular.FindType.Equals;

            return this.View(usuario);
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Usuarios", Area = Area)]
        public ActionResult Usuarios(RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            //try
            //{
            RecursoCurricular.Membresia.Usuario usuario = RecursoCurricular.Membresia.Usuario.Get(model.Id);

            string textoRun = model.Persona.Run.Replace(".", string.Empty).Replace("-", string.Empty);

            int runCuerpo = int.Parse(textoRun.Substring(0, textoRun.Length - 1));
            char runDigito = char.Parse(textoRun.Replace(runCuerpo.ToString(), string.Empty));

            using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
            {
                RecursoCurricular.Persona persona = new RecursoCurricular.Persona
                {
                    Id = model.Id,
                    RunCuerpo = runCuerpo,
                    RunDigito = runDigito,
                    Nombres = model.Persona.Nombres,
                    ApellidoPaterno = model.Persona.ApellidoPaterno,
                    ApellidoMaterno = model.Persona.ApellidoMaterno,
                    Email = model.Persona.Email,
                    SexoCodigo = model.Persona.SexoCodigo,
                    FechaNacimiento = model.Persona.FechaNacimiento,
                    NacionalidadCodigo = model.Persona.NacionalidadCodigo,
                    EstadoCivilCodigo = model.Persona.EstadoCivilCodigo,
                    NivelEducacionalCodigo = model.Persona.NivelEducacionalCodigo,
                    RegionCodigo = model.Persona.ComunaCodigo.HasValue && model.Persona.ComunaCodigo.Value > 0 ? model.Persona.RegionCodigo.Value : default(short),
                    CiudadCodigo = model.Persona.ComunaCodigo.HasValue && model.Persona.ComunaCodigo.Value > 0 ? model.Persona.CiudadCodigo.Value : default(short),
                    ComunaCodigo = model.Persona.ComunaCodigo.HasValue && model.Persona.ComunaCodigo.Value > 0 ? model.Persona.ComunaCodigo.Value : default(short),
                    VillaPoblacion = model.Persona.VillaPoblacion,
                    Direccion = model.Persona.Direccion,
                    Telefono = model.Persona.Telefono,
                    Celular = model.Persona.Celular,
                    Observaciones = default(string)
                };

                persona.Save(context);

                context.SubmitChanges();

                if (usuario == null)
                {
                    RecursoCurricular.Membresia.Account.RegisterLogin(persona);
                }
                else
                {
                    new RecursoCurricular.Membresia.Usuario
                    {
                        Id = usuario.Id,
                        Password = usuario.Password,
                        Aprobado = usuario.Aprobado,
                        Bloqueado = usuario.Bloqueado,
                        Creacion = usuario.Creacion,
                        UltimaActividad = usuario.UltimaActividad,
                        UltimoAcceso = usuario.UltimoAcceso,
                        UltimoCambioPassword = usuario.UltimoCambioPassword,
                        UltimoDesbloqueo = usuario.UltimoDesbloqueo,
                        NumeroIntentosFallidos = usuario.NumeroIntentosFallidos,
                        FechaIntentoFallido = usuario.FechaIntentoFallido
                    }.Save(context);

                    context.SubmitChanges();
                }
            }

            return this.Json("200", JsonRequestBehavior.DenyGet);
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.Contains("RunCuerpoIndex"))
            //    {
            //        return this.Json("El R.U.N. se encuentra registrado con otro usuario", JsonRequestBehavior.DenyGet);
            //    }
            //    else
            //    {
            //        return this.Json(ex.Message, JsonRequestBehavior.DenyGet);
            //    }
            //}
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetAllUsuarios(RecursoCurricular.FindType findType, string filter)
        {
            RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario.Usuarios usuarios = this.UsuarioGridView(findType, filter);

            return this.Json(usuarios, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Usuarios", Area = Area)]
        public JsonResult GetUsuarios(string run)
        {
            string textoRun = run.Replace(".", string.Empty).Replace("-", string.Empty);

            int runCuerpo = int.Parse(textoRun.Substring(0, textoRun.Length - 1));
            char runDigito = char.Parse(textoRun.Replace(runCuerpo.ToString(), string.Empty));

            if (!RecursoCurricular.Helper.ValidaRun(runCuerpo, runDigito))
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }

            RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario.Usuarios usuarios = this.UsuarioGridView(null, null, runCuerpo, runDigito);

            return this.Json(usuarios, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Add }, Root = "Usuarios", Area = Area)]
        public JsonResult AddUsuario()
        {
            return this.Json(new RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario
            {
                Id = Guid.NewGuid(),
                Password = string.Empty,
                Aprobado = true,
                Bloqueado = true,
                Creacion = DateTime.Now,
                UltimaActividad = DateTime.Now,
                UltimoAcceso = DateTime.Now,
                UltimoCambioPassword = default(DateTime),
                UltimoDesbloqueo = default(DateTime),
                NumeroIntentosFallidos = default(int),
                FechaIntentoFallido = default(DateTime),
                FechaNacimientoString = string.Empty,
                Persona = new RecursoCurricular.Persona()
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Edit }, Root = "Usuarios", Area = Area)]
        public JsonResult EditUsuario(Guid id)
        {
            RecursoCurricular.Membresia.Usuario usuario = RecursoCurricular.Membresia.Usuario.Get(id);

            return this.Json(new RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario
            {
                Id = usuario.Id,
                Password = string.Empty,
                Aprobado = usuario.Aprobado,
                Bloqueado = usuario.Bloqueado,
                Creacion = usuario.Creacion,
                UltimaActividad = usuario.UltimaActividad,
                UltimoAcceso = usuario.UltimoAcceso,
                UltimoCambioPassword = usuario.UltimoCambioPassword,
                UltimoDesbloqueo = usuario.UltimoDesbloqueo,
                NumeroIntentosFallidos = usuario.NumeroIntentosFallidos,
                FechaIntentoFallido = usuario.FechaIntentoFallido,
                FechaNacimientoString = usuario.Persona.FechaNacimiento.HasValue ? usuario.Persona.FechaNacimiento.Value.ToShortDateString() : string.Empty,
                Persona = usuario.Persona
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Delete }, Root = "Usuarios", Area = Area)]
        public JsonResult DeleteUsuario(Guid id)
        {
            try
            {
                RecursoCurricular.Membresia.Usuario usuario = RecursoCurricular.Membresia.Usuario.Get(id);

                using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
                {
                    new RecursoCurricular.Membresia.Usuario
                    {
                        Id = usuario.Id,
                        Password = usuario.Password,
                        Aprobado = usuario.Aprobado,
                        Bloqueado = usuario.Bloqueado,
                        Creacion = usuario.Creacion,
                        UltimaActividad = usuario.UltimaActividad,
                        UltimoAcceso = usuario.UltimoAcceso,
                        UltimoCambioPassword = usuario.UltimoCambioPassword,
                        UltimoDesbloqueo = usuario.UltimoDesbloqueo,
                        NumeroIntentosFallidos = usuario.NumeroIntentosFallidos,
                        FechaIntentoFallido = usuario.FechaIntentoFallido
                    }.Delete(context);

                    context.SubmitChanges();
                }

                return this.Json("200", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "Usuarios", Area = Area)]
        public JsonResult Usuario(string run)
        {
            string textoRun = run.Replace(".", string.Empty).Replace("-", string.Empty);

            int runCuerpo = int.Parse(textoRun.Substring(0, textoRun.Length - 1));
            char runDigito = char.Parse(textoRun.Replace(runCuerpo.ToString(), string.Empty));

            if (!RecursoCurricular.Helper.ValidaRun(runCuerpo, runDigito))
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }

            RecursoCurricular.Membresia.Usuario usuario = RecursoCurricular.Membresia.Usuario.Get(runCuerpo, runDigito);

            if (usuario == null)
            {
                RecursoCurricular.Persona persona = RecursoCurricular.Persona.Get(runCuerpo, runDigito);

                if (persona == null)
                {
                    persona = new RecursoCurricular.Persona();

                    persona.RunCuerpo = runCuerpo;
                    persona.RunDigito = runDigito;
                    persona.Run = string.Format("{0}-{1}", runCuerpo.ToString("N0"), runDigito);
                }

                usuario = new RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario
                {
                    Id = persona.Id,
                    FechaNacimientoString = persona.FechaNacimiento.HasValue ? persona.FechaNacimiento.Value.ToShortDateString() : string.Empty,
                    Persona = persona
                };

                return this.Json(usuario, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario
            {
                Id = usuario.Id,
                Aprobado = usuario.Aprobado,
                Bloqueado = usuario.Bloqueado,
                Creacion = usuario.Creacion,
                UltimaActividad = usuario.UltimaActividad,
                UltimoAcceso = usuario.UltimoAcceso,
                UltimoCambioPassword = usuario.UltimoCambioPassword,
                UltimoDesbloqueo = usuario.UltimoDesbloqueo,
                NumeroIntentosFallidos = usuario.NumeroIntentosFallidos,
                FechaIntentoFallido = usuario.FechaIntentoFallido,
                FechaNacimientoString = usuario.Persona.FechaNacimiento.HasValue ? usuario.Persona.FechaNacimiento.Value.ToShortDateString() : string.Empty,
                Persona = usuario.Persona
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Accept }, Root = "Usuarios", Area = Area)]
        public ActionResult ChangePass(RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                RecursoCurricular.Membresia.Usuario usuario = RecursoCurricular.Membresia.Usuario.Get(model.Id);

                RecursoCurricular.Membresia.Account.DoChangePassword(usuario, model.ChangePass.Password1, model.ChangePass.Password2);

                return this.Json("200", JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                return this.Json(ex.Message, JsonRequestBehavior.DenyGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult UsuarioRol(Guid personaId)
        {
            RecursoCurricular.Persona persona = RecursoCurricular.Persona.Get(personaId);

            RecursoCurricular.Web.UI.Areas.Administracion.Models.Rol.Roles rol = new RecursoCurricular.Web.UI.Areas.Administracion.Models.Rol.Roles();

            rol.data = new List<RecursoCurricular.Web.UI.Areas.Administracion.Models.Rol>();

            foreach (RecursoCurricular.Membresia.Rol r in RecursoCurricular.Membresia.Rol.GetAll())
            {
                bool exists = RecursoCurricular.Membresia.RolPersona.Exists(persona, r); ;

                rol.data.Add(new RecursoCurricular.Web.UI.Areas.Administracion.Models.Rol
                {
                    Nombre = r.Nombre,
                    Accion = string.Format("<label class='option'><input type='checkbox' {0} name='asignacion' data-value={1}><span class='checkbox'></span></label>", exists ? "checked" : string.Empty, r.Id)
                });
            }

            return this.Json(rol, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult UsuarioRol(List<RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario.RolPersona> model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json("501", JsonRequestBehavior.AllowGet);
            }

            List<RecursoCurricular.Membresia.Account.Rol> roles = (from m in model
                                                                   select new RecursoCurricular.Membresia.Account.Rol
                                                                   {
                                                                       PersonaId = m.PersonaId,
                                                                       RolId = m.RolId
                                                                   }).ToList<RecursoCurricular.Membresia.Account.Rol>();

            return this.Json(RecursoCurricular.Membresia.Account.Rol.Asignar(roles), JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult DeshabilitarUsuario(Guid usuarioId)
        {
            RecursoCurricular.Membresia.Usuario usuario = RecursoCurricular.Membresia.Usuario.Get(usuarioId);

            RecursoCurricular.Membresia.Account.Lock(usuario);

            return this.Json("200", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult HabilitarUsuario(Guid usuarioId)
        {
            RecursoCurricular.Membresia.Usuario usuario = RecursoCurricular.Membresia.Usuario.Get(usuarioId);

            RecursoCurricular.Membresia.Account.UnLock(usuario);

            return this.Json("200", JsonRequestBehavior.AllowGet);
        }

        private RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario.Usuarios UsuarioGridView(RecursoCurricular.FindType? findType, string filter, int? runCuerpo = null, char? runDigito = null)
        {
            RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario.Usuarios usuarios = new RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario.Usuarios();

            usuarios.data = new List<RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario>();

            List<RecursoCurricular.Membresia.Usuario> lista = new List<RecursoCurricular.Membresia.Usuario>();

            if (runCuerpo.HasValue && runDigito.HasValue)
            {
                RecursoCurricular.Membresia.Usuario usuario = RecursoCurricular.Membresia.Usuario.Get(runCuerpo.Value, runDigito.Value);

                if (usuario != null)
                {
                    lista.Add(usuario);
                }
            }
            else
            {
                lista = RecursoCurricular.Membresia.Usuario.GetAll(findType.Value, filter);
            }

            foreach (RecursoCurricular.Membresia.Usuario usuario in lista)
            {
                MvcHtmlString botonHabilitado;

                if (usuario.Bloqueado)
                {
                    botonHabilitado = RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(usuario.Id, null, RecursoCurricular.Helpers.TypeButton.OtherAction, this, "fa-unlock", "Habilitar cuenta", "enableAccount");
                }
                else
                {
                    botonHabilitado = RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(usuario.Id, null, RecursoCurricular.Helpers.TypeButton.OtherAction, this, "fa-lock", "Deshabilitar cuenta", "disableAccount");
                }

                usuarios.data.Add(new RecursoCurricular.Web.UI.Areas.Administracion.Models.Usuario
                {
                    Nombre = usuario.Persona.Nombre,
                    Run = usuario.Persona.Run,
                    Estado = usuario.Bloqueado ? "Bloquedo" : "Activo",
                    UltimoLogin = usuario.UltimoAcceso.ToString(),
                    Accion = string.Format("{0}{1}{2}{3}", RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(usuario.Id, null, RecursoCurricular.Helpers.TypeButton.Edit, this),
                                                              RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(usuario.Id, null, RecursoCurricular.Helpers.TypeButton.OtherAction, this, "fa-key", "Establecer contraseña", "changePass"),
                                                              RecursoCurricular.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(usuario.Id, null, RecursoCurricular.Helpers.TypeButton.OtherAction, this, "fa-wrench", "Asignar roles", "assignRole"),
                                                              botonHabilitado)
                });
            }

            return usuarios;
        }

        #endregion

        #region Usuarios conectados

        [Authorize]
        [HttpGet]
        [RecursoCurricular.Web.Authorization(ActionType = new RecursoCurricular.Web.ActionType[] { RecursoCurricular.Web.ActionType.Access }, Root = "UsuarioConectados", Area = Area)]
        public ActionResult UsuarioConectados()
        {
            RecursoCurricular.Web.UI.Areas.Administracion.Models.UsuariosConectados usuariosConectados = new RecursoCurricular.Web.UI.Areas.Administracion.Models.UsuariosConectados();

            usuariosConectados.NumeroUsuarios = (int)System.Web.HttpContext.Current.Application["NumberUsers"];

            return this.View(usuariosConectados);
        }

        [Authorize]
        [HttpGet]
        public JsonResult CountUsuarioConectados()
        {
            return this.Json((int)System.Web.HttpContext.Current.Application["NumberUsers"], JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}