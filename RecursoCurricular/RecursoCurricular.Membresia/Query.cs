using System;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	internal static class Query
	{
		#region Accion
		internal static IQueryable<Accion> GetAcciones()
		{
			return
				from accion in Context.Instancia.Acciones
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
		#endregion

		#region AplicacionPerfil
		internal static IQueryable<AplicacionPerfil> GetAplicacionPerfiles()
		{
			return
				from aplicacionPerfil in Context.Instancia.AplicacionPerfiles
				select aplicacionPerfil;
		}
		#endregion

		#region Auditoria
		internal static IQueryable<Auditoria> GetAuditorias()
		{
			return
				from auditoria in Context.Instancia.Auditorias
				select auditoria;
		}
		#endregion

		#region MenuItem
		internal static IQueryable<MenuItem> GetMenuItemes()
		{
			return
				from menuItem in Context.Instancia.MenuItemes
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
		#endregion

		#region Perfil
		internal static IQueryable<Perfil> GetPerfiles()
		{
			return
				from perfil in Context.Instancia.Perfiles
				select perfil;
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
		#endregion

		#region RolAccion
		internal static IQueryable<RolAccion> GetRolAcciones()
		{
			return
				from rolAccion in Context.Instancia.RolAcciones
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
		#endregion

		#region Usuario
		internal static IQueryable<Usuario> GetUsuarios()
		{
			return
				from usuario in Context.Instancia.Usuarios
				select usuario;
		}
		#endregion
	}
}