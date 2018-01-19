using System;
using System.Linq;
using RecursoCurricular.Educacion;

namespace RecursoCurricular.BaseCurricular
{
	internal static class Query
	{
		#region Actitud
		internal static IQueryable<Actitud> GetActitudes()
		{
			return
				from actitud in Context.Instancia.Actitudes
				select actitud;
		}
		#endregion

		#region AmbitoExperienciaAprendizaje
		internal static IQueryable<AmbitoExperienciaAprendizaje> GetAmbitoExperienciaAprendizajes()
		{
			return
				from ambitoExperienciaAprendizaje in Context.Instancia.AmbitoExperienciaAprendizajes
				select ambitoExperienciaAprendizaje;
		}

        internal static IQueryable<AmbitoExperienciaAprendizaje> GetAmbitoExperienciaAprendizajes(RecursoCurricular.Anio anio)
        {
            return
                from ambitoExperienciaAprendizaje in GetAmbitoExperienciaAprendizajes()
                where ambitoExperienciaAprendizaje.Anio == anio
                select ambitoExperienciaAprendizaje;
        }
        #endregion

        #region AprendizajeEsperadoParvulo
        internal static IQueryable<AprendizajeEsperadoParvulo> GetAprendizajeEsperadoParvulos()
		{
			return
				from aprendizajeEsperadoParvulo in Context.Instancia.AprendizajeEsperadoParvulos
				select aprendizajeEsperadoParvulo;
		}
		#endregion

		#region Conocimiento
		internal static IQueryable<Conocimiento> GetConocimientos()
		{
			return
				from conocimiento in Context.Instancia.Conocimientos
				select conocimiento;
		}
		#endregion

		#region DimensionOAT
		internal static IQueryable<DimensionOAT> GetDimensionOATes()
		{
			return
				from dimensionOAT in Context.Instancia.DimensionOATes
				select dimensionOAT;
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

		#region EjeParvulo
		internal static IQueryable<EjeParvulo> GetEjeParvulos()
		{
			return
				from ejeParvulo in Context.Instancia.EjeParvulos
				select ejeParvulo;
		}

        internal static IQueryable<EjeParvulo> GetEjeParvulos(AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje)
        {
            return
                from ejeParvulo in GetEjeParvulos()
                where ejeParvulo.AmbitoExperienciaAprendizaje == ambitoExperienciaAprendizaje
                select ejeParvulo;
        }

        internal static IQueryable<EjeParvulo> GetEjeParvulos(AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje, NucleoAprendizaje nucleAprendizaje, Ciclo ciclo)
        {
            return
                from ejeParvulo in GetEjeParvulos(ambitoExperienciaAprendizaje)
                where ejeParvulo.NucleoAprendizaje == nucleAprendizaje
                   && ejeParvulo.Ciclo == ciclo
                select ejeParvulo;
        }
        #endregion

        #region Habilidad
        internal static IQueryable<Habilidad> GetHabilidades()
		{
			return
				from habilidad in Context.Instancia.Habilidades
				select habilidad;
		}
		#endregion

		#region Indicador
		internal static IQueryable<Indicador> GetIndicadores()
		{
			return
				from indicador in Context.Instancia.Indicadores
				select indicador;
		}
		#endregion

		#region NucleoAprendizaje
		internal static IQueryable<NucleoAprendizaje> GetNucleoAprendizajes()
		{
			return
				from nucleoAprendizaje in Context.Instancia.NucleoAprendizajes
				select nucleoAprendizaje;
		}

        internal static IQueryable<NucleoAprendizaje> GetNucleoAprendizajes(AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje)
        {
            return
                from nucleoAprendizaje in GetNucleoAprendizajes()
                where nucleoAprendizaje.AmbitoExperienciaAprendizaje == ambitoExperienciaAprendizaje
                select nucleoAprendizaje;
        }

        internal static IQueryable<NucleoAprendizaje> GetNucleoAprendizajes(Anio anio)
        {
            return
                from nucleoAprendizaje in GetNucleoAprendizajes()
                where nucleoAprendizaje.Anio == anio
                select nucleoAprendizaje;
        }
        #endregion

        #region ObjetivoAprendizaje
        internal static IQueryable<ObjetivoAprendizaje> GetObjetivoAprendizajes()
		{
			return
				from objetivoAprendizaje in Context.Instancia.ObjetivoAprendizajes
				select objetivoAprendizaje;
		}
		#endregion

		#region ObjetivoAprendizajeTransversal
		internal static IQueryable<ObjetivoAprendizajeTransversal> GetObjetivoAprendizajeTransversales()
		{
			return
				from objetivoAprendizajeTransversal in Context.Instancia.ObjetivoAprendizajeTransversales
				select objetivoAprendizajeTransversal;
		}
		#endregion

		#region PrincipioPedagogico
		internal static IQueryable<PrincipioPedagogico> GetPrincipioPedagogicos()
		{
			return
				from principioPedagogico in Context.Instancia.PrincipioPedagogicos
				select principioPedagogico;
		}
		#endregion

		#region SubHabilidad
		internal static IQueryable<SubHabilidad> GetSubHabilidades()
		{
			return
				from subHabilidad in Context.Instancia.SubHabilidades
				select subHabilidad;
		}
		#endregion

		#region TipoEducacionEje
		internal static IQueryable<TipoEducacionEje> GetTipoEducacionEjes()
		{
			return
				from tipoEducacionEje in Context.Instancia.TipoEducacionEjes
				select tipoEducacionEje;
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

		#region UnidadActitud
		internal static IQueryable<UnidadActitud> GetUnidadActitudes()
		{
			return
				from unidadActitud in Context.Instancia.UnidadActitudes
				select unidadActitud;
		}
		#endregion

		#region UnidadConocimiento
		internal static IQueryable<UnidadConocimiento> GetUnidadConocimientos()
		{
			return
				from unidadConocimiento in Context.Instancia.UnidadConocimientos
				select unidadConocimiento;
		}
		#endregion

		#region UnidadIndicador
		internal static IQueryable<UnidadIndicador> GetUnidadIndicadores()
		{
			return
				from unidadIndicador in Context.Instancia.UnidadIndicadores
				select unidadIndicador;
		}
		#endregion

		#region UnidadObjetivoAprendizaje
		internal static IQueryable<UnidadObjetivoAprendizaje> GetUnidadObjetivoAprendizajes()
		{
			return
				from unidadObjetivoAprendizaje in Context.Instancia.UnidadObjetivoAprendizajes
				select unidadObjetivoAprendizaje;
		}
		#endregion

		#region UnidadSubHabilidad
		internal static IQueryable<UnidadSubHabilidad> GetUnidadSubHabilidades()
		{
			return
				from unidadSubHabilidad in Context.Instancia.UnidadSubHabilidades
				select unidadSubHabilidad;
		}
		#endregion
	}
}