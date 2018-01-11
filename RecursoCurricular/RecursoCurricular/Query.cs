using System;
using System.Linq;
namespace RecursoCurricular
{
	internal static class Query
	{
		#region Ano
		internal static IQueryable<Ano> GetAnos()
		{
			return
				from ano in Context.Instancia.Anos
				select ano;
		}
		#endregion

		#region Ciudad
		internal static IQueryable<Ciudad> GetCiudades()
		{
			return
				from ciudad in Context.Instancia.Ciudades
				select ciudad;
		}
		#endregion

		#region Comuna
		internal static IQueryable<Comuna> GetComunas()
		{
			return
				from comuna in Context.Instancia.Comunas
				select comuna;
		}
		#endregion

		#region EstadoCivil
		internal static IQueryable<EstadoCivil> GetEstadoCiviles()
		{
			return
				from estadoCivil in Context.Instancia.EstadoCiviles
				select estadoCivil;
		}
		#endregion

		#region Nacionalidad
		internal static IQueryable<Nacionalidad> GetNacionalidades()
		{
			return
				from nacionalidad in Context.Instancia.Nacionalidades
				select nacionalidad;
		}
		#endregion

		#region NivelEducacional
		internal static IQueryable<NivelEducacional> GetNivelEducacionales()
		{
			return
				from nivelEducacional in Context.Instancia.NivelEducacionales
				select nivelEducacional;
		}
		#endregion

		#region Persona
		internal static IQueryable<Persona> GetPersonas()
		{
			return
				from persona in Context.Instancia.Personas
				select persona;
		}
		#endregion

		#region Region
		internal static IQueryable<Region> GetRegiones()
		{
			return
				from region in Context.Instancia.Regiones
				select region;
		}
		#endregion

		#region Sexo
		internal static IQueryable<Sexo> GetSexos()
		{
			return
				from sexo in Context.Instancia.Sexos
				select sexo;
		}
		#endregion
	}
}