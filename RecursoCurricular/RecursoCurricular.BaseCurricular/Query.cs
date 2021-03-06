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

        internal static IQueryable<Actitud> GetActitudes(RecursoCurricular.Anio anio)
        {
            return
                   from actitud in GetActitudes()
                   where actitud.Anio == anio
                   select actitud;
        }

        internal static IQueryable<Actitud> GetActitudes(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.TipoEducacion tipoEducacion, RecursoCurricular.Educacion.Sector sector)
        {
            return
                   from actitud in GetActitudes(anio)
                   where actitud.TipoEducacion == tipoEducacion
                      && actitud.Sector == sector
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

        internal static IQueryable<AprendizajeEsperadoParvulo> GetAprendizajeEsperadoParvulos(RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje, RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleoAprendizaje)
        {
            return
                from aprendizajeEsperadoParvulo in GetAprendizajeEsperadoParvulos()
                where aprendizajeEsperadoParvulo.NucleoAprendizaje.AmbitoExperienciaAprendizaje == ambitoExperienciaAprendizaje &&
                      aprendizajeEsperadoParvulo.NucleoAprendizaje == nucleoAprendizaje
                select aprendizajeEsperadoParvulo;
        }

        internal static IQueryable<AprendizajeEsperadoParvulo> GetAprendizajeEsperadoParvulos(RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje, RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleoAprendizaje, RecursoCurricular.Educacion.Ciclo ciclo)
        {
            return
                from aprendizajeEsperadoParvulo in GetAprendizajeEsperadoParvulos(ambitoExperienciaAprendizaje, nucleoAprendizaje)
                where aprendizajeEsperadoParvulo.Ciclo == ciclo
                select aprendizajeEsperadoParvulo;
        }

        internal static IQueryable<AprendizajeEsperadoParvulo> GetAprendizajeEsperadoParvulos(RecursoCurricular.BaseCurricular.AmbitoExperienciaAprendizaje ambitoExperienciaAprendizaje, RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleoAprendizaje, RecursoCurricular.Educacion.Ciclo ciclo, RecursoCurricular.BaseCurricular.EjeParvulo eje)
        {
            return
               from aprendizajeEsperadoParvulo in GetAprendizajeEsperadoParvulos(ambitoExperienciaAprendizaje, nucleoAprendizaje, ciclo)
               where aprendizajeEsperadoParvulo.EjeParvulo == eje
               select aprendizajeEsperadoParvulo;
        }

        internal static IQueryable<AprendizajeEsperadoParvulo> GetAprendizajeEsperadoParvulos(RecursoCurricular.Anio anio)
        {
            return
                from aprendizajeEsperadoParvulo in GetAprendizajeEsperadoParvulos()
                where aprendizajeEsperadoParvulo.Anio == anio
                select aprendizajeEsperadoParvulo;
        }

        internal static IQueryable<AprendizajeEsperadoParvulo> GetAprendizajeEsperadoParvulos(RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleo)
        {
            return
                from aprendizajeEsperadoParvulo in GetAprendizajeEsperadoParvulos()
                where aprendizajeEsperadoParvulo.NucleoAprendizaje == nucleo
                select aprendizajeEsperadoParvulo;
        }

        internal static IQueryable<AprendizajeEsperadoParvulo> GetAprendizajeEsperadoParvulos(RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleo, RecursoCurricular.Educacion.Ciclo ciclo)
        {
            return
                from aprendizajeEsperadoParvulo in GetAprendizajeEsperadoParvulos(nucleo)
                where aprendizajeEsperadoParvulo.Ciclo == ciclo
                select aprendizajeEsperadoParvulo;
        }

        internal static IQueryable<AprendizajeEsperadoParvulo> GetAprendizajeEsperadoParvulos(RecursoCurricular.BaseCurricular.NucleoAprendizaje nucleo, RecursoCurricular.Educacion.Ciclo ciclo, RecursoCurricular.BaseCurricular.EjeParvulo eje)
        {
            if (eje == null)
            {
                return GetAprendizajeEsperadoParvulos(nucleo, ciclo).Where<RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo>(x => !x.EjeParvuloId.HasValue);
            }
            else
            {
                return
                    from aprendizajeEsperadoParvulo in GetAprendizajeEsperadoParvulos(nucleo, ciclo)
                    where aprendizajeEsperadoParvulo.EjeParvulo == eje
                    select aprendizajeEsperadoParvulo;
            }
        }
        #endregion

        #region Conocimiento
        internal static IQueryable<Conocimiento> GetConocimientos()
        {
            return
                from conocimiento in Context.Instancia.Conocimientos
                select conocimiento;
        }

        internal static IQueryable<Conocimiento> GetConocimientos(RecursoCurricular.Anio anio)
        {
            return
                   from conocimiento in GetConocimientos()
                   where conocimiento.Anio == anio
                   select conocimiento;
        }

        internal static IQueryable<Conocimiento> GetConocimientos(RecursoCurricular.Educacion.TipoEducacion tipoEducacion, RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector)
        {
            return
                   from conocimiento in GetConocimientos(anio)
                   where conocimiento.TipoEducacion == tipoEducacion
                      && conocimiento.Sector == sector
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

        internal static IQueryable<DimensionOAT> GetDimensionOATes(Anio anio)
        {
            return
                from dimensionOAT in GetDimensionOATes()
                where dimensionOAT.Anio == anio
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

        internal static IQueryable<Eje> GetEjes(Anio anio)
        {
            return
                from eje in GetEjes()
                where eje.Anio == anio
                select eje;
        }

        internal static IQueryable<Eje> GetEjes(Anio anio, Sector sector)
        {
            return
                from eje in GetEjes(anio)
                where eje.Sector == sector
                select eje;
        }

        internal static IQueryable<Eje> GetEjes(Anio anio, Sector sector, TipoEducacion tipoEducacion)
        {
            return
                from eje in GetEjes(anio, sector)
                where GetTipoEducacionEjes(tipoEducacion).Select<TipoEducacionEje, Eje>(x => x.Eje).Contains<Eje>(eje)
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

        internal static IQueryable<Habilidad> GetHabilidades(RecursoCurricular.Anio anio)
        {
            return
                   from habilidad in GetHabilidades()
                   where habilidad.Anio == anio
                   select habilidad;
        }

        internal static IQueryable<Habilidad> GetHabilidades(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.TipoEducacion tipoEducacion, RecursoCurricular.Educacion.Sector sector)
        {
            return
                   from habilidad in GetHabilidades(anio)
                   where habilidad.TipoEducacion == tipoEducacion
                      && habilidad.Sector == sector
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

        internal static IQueryable<Indicador> GetIndicadores(RecursoCurricular.Anio anio)
        {
            return
                from indicador in GetIndicadores()
                where indicador.Anio == anio
                select indicador;
        }

        internal static IQueryable<Indicador> GetIndicadores(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector)
        {
            return
                from indicador in GetIndicadores(anio)
                where indicador.Sector == sector
                select indicador;
        }

        internal static IQueryable<Indicador> GetIndicadores(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.Educacion.Grado grado)
        {
            return
                from indicador in GetIndicadores(anio, sector)
                where indicador.Grado == grado
                select indicador;
        }

        internal static IQueryable<Indicador> GetIndicadores(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.BaseCurricular.Eje eje)
        {
            return
                from indicador in GetIndicadores(anio, sector, grado)
                where indicador.ObjetivoAprendizaje.Eje == eje
                select indicador;
        }

        internal static IQueryable<Indicador> GetIndicadores(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.Educacion.TipoEducacion tipoEducacion)
        {
            return
                from indicador in GetIndicadores(anio, sector)
                where indicador.TipoEducacion == tipoEducacion
                select indicador;
        }

        internal static IQueryable<Indicador> GetIndicadores(RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje)
        {
            return
                from indicador in GetIndicadores()
                where indicador.ObjetivoAprendizaje == objetivoAprendizaje
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

        internal static IQueryable<ObjetivoAprendizaje> GetObjetivoAprendizajes(Eje eje)
        {
            return
                from objetivoAprendizaje in GetObjetivoAprendizajes()
                where objetivoAprendizaje.Eje == eje
                select objetivoAprendizaje;
        }

        internal static IQueryable<ObjetivoAprendizaje> GetObjetivoAprendizajes(Eje eje, Sector sector)
        {
            return
                from objetivoAprendizaje in GetObjetivoAprendizajes(eje)
                where objetivoAprendizaje.Sector == sector
                select objetivoAprendizaje;
        }

        internal static IQueryable<ObjetivoAprendizaje> GetObjetivoAprendizajes(Grado grado, Sector sector, Eje eje)
        {
            return
                from objetivoAprendizaje in GetObjetivoAprendizajes(eje, sector)
                where objetivoAprendizaje.Grado == grado
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

        internal static IQueryable<ObjetivoAprendizajeTransversal> GetObjetivoAprendizajeTransversales(Anio anio)
        {
            return
                from objetivoAprendizajeTransversal in GetObjetivoAprendizajeTransversales()
                where objetivoAprendizajeTransversal.Anio == anio
                select objetivoAprendizajeTransversal;
        }

        internal static IQueryable<ObjetivoAprendizajeTransversal> GetObjetivoAprendizajeTransversales(DimensionOAT dimensionOAT)
        {
            return
                from objetivoAprendizajeTransversal in GetObjetivoAprendizajeTransversales()
                where objetivoAprendizajeTransversal.DimensionOAT == dimensionOAT
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

        internal static IQueryable<PrincipioPedagogico> GetPrincipioPedagogicos(RecursoCurricular.Anio anio)
        {
            return
                from principioPedagogico in GetPrincipioPedagogicos()
                where principioPedagogico.Anio == anio
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

        internal static IQueryable<SubHabilidad> GetSubHabilidades(RecursoCurricular.BaseCurricular.Habilidad habilidad)
        {
            return
                from subHabilidad in GetSubHabilidades()
                where subHabilidad.Habilidad == habilidad
                select subHabilidad;
        }

        internal static IQueryable<SubHabilidad> GetSubHabilidades(RecursoCurricular.BaseCurricular.Habilidad habilidad, RecursoCurricular.Educacion.Grado grado)
        {
            return
                from subHabilidad in GetSubHabilidades(habilidad)
                where subHabilidad.Grado == grado
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

        internal static IQueryable<TipoEducacionEje> GetTipoEducacionEjes(Eje eje)
        {
            return
                from tipoEducacionEje in GetTipoEducacionEjes()
                where tipoEducacionEje.Eje == eje
                select tipoEducacionEje;
        }

        internal static IQueryable<TipoEducacionEje> GetTipoEducacionEjes(TipoEducacion tipoEducacion)
        {
            return
                from tipoEducacionEje in GetTipoEducacionEjes()
                where tipoEducacionEje.TipoEducacion == tipoEducacion
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

        internal static IQueryable<Unidad> GetUnidades(RecursoCurricular.Anio anio)
        {
            return
                from unidad in GetUnidades()
                where unidad.Anio == anio
                select unidad;
        }

        public static IQueryable<Unidad> GetUnidades(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                from unidad in GetUnidades(anio)
                where unidad.Grado == grado
                   && unidad.Sector == sector
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

        internal static IQueryable<UnidadActitud> GetUnidadActitudes(RecursoCurricular.BaseCurricular.Unidad unidad)
        {
            return
                from unidadActitud in GetUnidadActitudes()
                where unidadActitud.Unidad == unidad
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

        internal static IQueryable<UnidadConocimiento> GetUnidadConocimientos(RecursoCurricular.BaseCurricular.Unidad unidad)
        {
            return
                from unidadConocimiento in GetUnidadConocimientos()
                where unidadConocimiento.Unidad == unidad
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

        internal static IQueryable<UnidadIndicador> GetUnidadIndicadores(RecursoCurricular.BaseCurricular.Unidad unidad)
        {
            return
                from unidadIndicador in GetUnidadIndicadores()
                where unidadIndicador.Unidad == unidad
                select unidadIndicador;
        }

        internal static IQueryable<UnidadIndicador> GetUnidadIndicadores(RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje unidadObjetivoAprendizaje)
        {
            return
                from unidadIndicador in GetUnidadIndicadores()
                where unidadIndicador.UnidadObjetivoAprendizaje == unidadObjetivoAprendizaje
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

        internal static IQueryable<UnidadObjetivoAprendizaje> GetUnidadObjetivoAprendizajes(RecursoCurricular.BaseCurricular.Unidad unidad)
        {
            return
                from unidadObjetivoAprendizaje in GetUnidadObjetivoAprendizajes()
                where unidadObjetivoAprendizaje.Unidad == unidad
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

        internal static IQueryable<UnidadSubHabilidad> GetUnidadSubHabilidades(RecursoCurricular.BaseCurricular.Unidad unidad)
        {
            return
                from unidadSubHabilidad in GetUnidadSubHabilidades()
                where unidadSubHabilidad.Unidad == unidad
                select unidadSubHabilidad;
        }
        #endregion
    }
}