using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.Membresia
{
	public partial class Auditoria
	{
		public static void Auditar(Usuario usuario, MenuItem menuItem)
		{
			using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
			{
				new RecursoCurricular.Membresia.Auditoria
				{
					UsuarioId = usuario.Id,
					AplicacionId = menuItem.AplicacionId,
					MenuItemId = menuItem.Id,
					Actividad = string.Format("Navegación: {0}", menuItem.Nombre),
					Fecha = DateTime.Now
				}.Save(context);

				context.SubmitChanges();
			}
		}

		public static void Auditar(Usuario usuario, MenuItem menuItem, string actividad)
		{
			using (RecursoCurricular.Membresia.Context context = new RecursoCurricular.Membresia.Context())
			{
				new RecursoCurricular.Membresia.Auditoria
				{
					UsuarioId = usuario.Id,
					AplicacionId = menuItem.AplicacionId,
					MenuItemId = menuItem.Id,
					Actividad = actividad,
					Fecha = DateTime.Now
				}.Save(context);

				context.SubmitChanges();
			}
		}

		public static List<Auditoria> GetAll()
		{
			return
				(
				from query in Query.GetAuditorias()
				select query
				).ToList<Auditoria>();
		}

		public static List<Auditoria> GetAll(Persona persona, DateTime fecha)
		{
			return
				(
				from query in Query.GetAuditorias(persona, fecha)
				orderby query.Fecha descending
				select query
				).ToList<Auditoria>();
		}
	}
}