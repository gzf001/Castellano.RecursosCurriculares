using System;
using System.Linq;
namespace RecursoCurricular.Membresia
{
    internal static class Query
    {
        #region Auditoria
        internal static IQueryable<Auditoria> GetAuditorias()
        {
            return
                from auditoria in Context.Instancia.Auditorias
                select auditoria;
        }

        internal static IQueryable<Auditoria> GetAuditorias(Persona persona, DateTime fecha)
        {
            return
                from auditoria in GetAuditorias()
                where auditoria.Usuario.Persona == persona && auditoria.Fecha.Date == fecha
                select auditoria;
        }
        #endregion

        #region Accion
        internal static IQueryable<Accion> GetAcciones()
        {
            return
                from accion in Context.Instancia.Acciones
                select accion;
        }

        internal static IQueryable<Accion> GetAcciones(MenuItem menuItem)
        {
            return
                from accion in GetAcciones()
                where (GetMenuItemAcciones(menuItem).Select<MenuItemAccion, Accion>(x => x.Accion).Contains<Accion>(accion))
                select accion;
        }
        #endregion

        #region Aplicacion
        internal static IQueryable<Aplicacion> GetAplicaciones()
        {
            return
                from aplicacion in Context.Instancia.Aplicaciones
                select aplicacion;
        }

        internal static IQueryable<Aplicacion> GetAplicaciones(MenuItem menuItem)
        {
            return
                (
                from query in GetMenuItemes()
                where query == menuItem
                select query.Aplicacion
                );
        }

        internal static IQueryable<Aplicacion> GetAplicaciones(Persona persona)
        {
            return
                from aplicacion in GetAplicaciones()
                where
                (
                from rol in GetRolPersonas(persona).Select<RolPersona, Rol>(x => x.Rol)
                join rolAccion in GetRolAcciones() on rol equals rolAccion.Rol
                select rolAccion.MenuItemAccion.MenuItem.Aplicacion
                ).Contains<Aplicacion>(aplicacion)
                select aplicacion;
        }
        #endregion

        #region AplicacionPerfil
        internal static IQueryable<AplicacionPerfil> GetAplicacionPerfiles()
        {
            return
                from aplicacionPerfil in Context.Instancia.AplicacionPerfiles
                select aplicacionPerfil;
        }

        internal static IQueryable<AplicacionPerfil> GetAplicacionPerfiles(Aplicacion aplicacion)
        {
            return
                from aplicacionPerfil in GetAplicacionPerfiles()
                where (aplicacionPerfil.Aplicacion == aplicacion)
                select aplicacionPerfil;
        }
        #endregion

        #region MenuItem
        internal static IQueryable<MenuItem> GetMenuItemes()
        {
            return
                from menuItem in Context.Instancia.MenuItemes
                select menuItem;
        }

        internal static IQueryable<MenuItem> GetMenuItemes(Aplicacion aplicacion)
        {
            return
                from menuItem in GetMenuItemes()
                where menuItem.Aplicacion == aplicacion
                select menuItem;
        }

        internal static IQueryable<MenuItem> GetMenuItemes(Aplicacion aplicacion, Persona persona)
        {
            return
                from menuItem in GetMenuItemes(aplicacion)
                where GetMenuItemAcciones(persona).Select<MenuItemAccion, MenuItem>(x => x.MenuItem).Contains<MenuItem>(menuItem)
                select menuItem;
        }

        internal static IQueryable<MenuItem> GetMenuItemes(Persona persona)
        {
            return
                from menuItem in GetMenuItemes()
                where GetMenuItemAcciones(persona).Select<MenuItemAccion, MenuItem>(x => x.MenuItem).Contains<MenuItem>(menuItem)
                select menuItem;
        }
        #endregion

        #region MenuItemAccion
        internal static IQueryable<MenuItemAccion> GetMenuItemAcciones()
        {
            return
                from menuItemAccion in Context.Instancia.MenuItemAcciones
                select menuItemAccion;
        }

        internal static IQueryable<MenuItemAccion> GetMenuItemAcciones(MenuItem menuItem)
        {
            return
                from menuItemAccion in GetMenuItemAcciones()
                where menuItemAccion.MenuItem == menuItem
                select menuItemAccion;
        }

        internal static IQueryable<MenuItemAccion> GetMenuItemAcciones(Persona persona)
        {
            return
                from menuItemAccion in GetMenuItemAcciones()
                where (GetRolAcciones(persona).Select<RolAccion, MenuItemAccion>(x => x.MenuItemAccion).Contains<MenuItemAccion>(menuItemAccion))
                select menuItemAccion;
        }
        #endregion

        #region Perfil
        internal static IQueryable<Perfil> GetPerfiles()
        {
            return
                from perfil in Context.Instancia.Perfiles
                select perfil;
        }

        internal static IQueryable<Perfil> GetPerfiles(Aplicacion aplicacion)
        {
            return
                from aplicacionPerfil in GetAplicacionPerfiles()
                where aplicacionPerfil.Aplicacion == aplicacion
                select aplicacionPerfil.Perfil;
        }
        #endregion

        #region PerfilUsuario
        internal static IQueryable<PerfilUsuario> GetPerfilUsuarios()
        {
            return
                from perfilUsuario in Context.Instancia.PerfilUsuarios
                select perfilUsuario;
        }
        #endregion

        #region Rol
        internal static IQueryable<Rol> GetRoles()
        {
            return
                from rol in Context.Instancia.Roles
                select rol;
        }

        internal static IQueryable<Rol> GetRoles(Aplicacion aplicacion)
        {
            return
                from rol in GetRoles()
                where GetRolAcciones(aplicacion).Select<RolAccion, Rol>(x => x.Rol).Contains(rol)
                select rol;
        }

        internal static IQueryable<Rol> GetRoles(Persona persona)
        {
            return
                from rol in GetRoles()
                where GetRolPersonas(persona).Select<RolPersona, Rol>(x => x.Rol).Contains<Rol>(rol)
                select rol;
        }

        internal static IQueryable<Rol> GetRoles(Persona persona, Aplicacion aplicacion)
        {
            return
                from rol in GetRoles(persona)
                where GetRolAcciones(aplicacion).Select<RolAccion, Rol>(x => x.Rol).Contains<Rol>(rol)
                select rol;
        }
        #endregion

        #region RolAccion
        internal static IQueryable<RolAccion> GetRolAcciones()
        {
            return
                from rolAccion in Context.Instancia.RolAcciones
                select rolAccion;
        }

        internal static IQueryable<RolAccion> GetRolAcciones(Aplicacion aplicacion)
        {
            return
                from rolAccion in GetRolAcciones()
                where rolAccion.MenuItemAccion.MenuItem.Aplicacion == aplicacion
                select rolAccion;
        }

        internal static IQueryable<RolAccion> GetRolAcciones(Aplicacion aplicacion, Rol rol)
        {
            return
                from rolAccion in GetRolAcciones(aplicacion)
                where rolAccion.Rol == rol
                select rolAccion;
        }

        internal static IQueryable<RolAccion> GetRolAcciones(Persona persona)
        {
            return
                from rolAccion in GetRolAcciones()
                join rolPersona in GetRoles(persona) on rolAccion.Rol equals rolPersona
                select rolAccion;
        }

        internal static IQueryable<RolAccion> GetRolAcciones(Persona persona, MenuItem menuItem)
        {
            return
                from rolAccion in GetRolAcciones(persona)
                where rolAccion.MenuItemAccion.MenuItem == menuItem
                select rolAccion;
        }

        internal static IQueryable<RolAccion> GetRolAcciones(Rol rol)
        {
            return
                from rolAccion in GetRolAcciones()
                where rolAccion.Rol == rol
                select rolAccion;
        }

        internal static IQueryable<RolAccion> GetRolAcciones(Rol rol, MenuItem menuItem)
        {
            return
                from rolAccion in GetRolAcciones(rol)
                where rolAccion.MenuItem == menuItem
                select rolAccion;
        }
        #endregion

        #region RolPersona
        internal static IQueryable<RolPersona> GetRolPersonas()
        {
            return
                from rolPersona in Context.Instancia.RolPersonas
                select rolPersona;
        }

        internal static IQueryable<RolPersona> GetRolPersonas(Persona persona)
        {
            return
                from rolPersona in GetRolPersonas()
                where rolPersona.Persona == persona
                select rolPersona;
        }

        internal static IQueryable<RolPersona> GetRolPersonas(Persona persona, Aplicacion aplicacion)
        {
            return
                from rolPersona in GetRolPersonas(persona)
                where
                (
                from rol in GetRoles()
                join rolAccion in GetRolAcciones() on rol equals rolAccion.Rol
                where rolAccion.MenuItemAccion.MenuItem.Aplicacion == aplicacion
                select rol
                ).Contains<Rol>(rolPersona.Rol)
                select rolPersona;
        }
        #endregion

        #region Usuario
        internal static IQueryable<Usuario> GetUsuarios()
        {
            return
                from usuario in Context.Instancia.Usuarios
                select usuario;
        }

        internal static IQueryable<Usuario> GetUsuarios(FindType findType, string filter)
        {
            switch (findType)
            {
                case FindType.StartsWith: return from usuario in GetUsuarios() where (usuario.Persona.Nombre.StartsWith(filter)) select usuario;
                case FindType.Contains: return from usuario in GetUsuarios() where (usuario.Persona.Nombre.Contains(filter)) select usuario;
                case FindType.EndsWith: return from usuario in GetUsuarios() where (usuario.Persona.Nombre.EndsWith(filter)) select usuario;
                default: return from usuario in GetUsuarios() where (usuario.Persona.Nombre == filter) select usuario;
            }
        }
        #endregion
    }
}