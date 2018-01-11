using System;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	internal static class Query
	{
		#region Aprendizaje
		internal static IQueryable<Aprendizaje> GetAprendizajes()
		{
			return
				from aprendizaje in Context.Instancia.Aprendizajes
				select aprendizaje;
		}
		#endregion

		#region AprendizajeContenido
		internal static IQueryable<AprendizajeContenido> GetAprendizajeContenidos()
		{
			return
				from aprendizajeContenido in Context.Instancia.AprendizajeContenidos
				select aprendizajeContenido;
		}
		#endregion

		#region AprendizajeIndicador
		internal static IQueryable<AprendizajeIndicador> GetAprendizajeIndicadores()
		{
			return
				from aprendizajeIndicador in Context.Instancia.AprendizajeIndicadores
				select aprendizajeIndicador;
		}
		#endregion

		#region AprendizajeObjetivoVertical
		internal static IQueryable<AprendizajeObjetivoVertical> GetAprendizajeObjetivoVerticales()
		{
			return
				from aprendizajeObjetivoVertical in Context.Instancia.AprendizajeObjetivoVerticales
				select aprendizajeObjetivoVertical;
		}
		#endregion

		#region Contenido
		internal static IQueryable<Contenido> GetContenidos()
		{
			return
				from contenido in Context.Instancia.Contenidos
				select contenido;
		}
		#endregion

		#region Eje
		internal static IQueryable<Eje> GetEjes()
		{
			return
				from eje in Context.Instancia.Ejes
				select eje;
		}
		#endregion

		#region ObjetivoTransversal
		internal static IQueryable<ObjetivoTransversal> GetObjetivoTransversales()
		{
			return
				from objetivoTransversal in Context.Instancia.ObjetivoTransversales
				select objetivoTransversal;
		}
		#endregion

		#region ObjetivoTransversalIndicador
		internal static IQueryable<ObjetivoTransversalIndicador> GetObjetivoTransversalIndicadores()
		{
			return
				from objetivoTransversalIndicador in Context.Instancia.ObjetivoTransversalIndicadores
				select objetivoTransversalIndicador;
		}
		#endregion

		#region ObjetivoVertical
		internal static IQueryable<ObjetivoVertical> GetObjetivoVerticales()
		{
			return
				from objetivoVertical in Context.Instancia.ObjetivoVerticales
				select objetivoVertical;
		}
		#endregion

		#region Unidad
		internal static IQueryable<Unidad> GetUnidades()
		{
			return
				from unidad in Context.Instancia.Unidades
				select unidad;
		}
		#endregion

		#region UnidadAprendizaje
		internal static IQueryable<UnidadAprendizaje> GetUnidadAprendizajes()
		{
			return
				from unidadAprendizaje in Context.Instancia.UnidadAprendizajes
				select unidadAprendizaje;
		}
		#endregion
	}
}