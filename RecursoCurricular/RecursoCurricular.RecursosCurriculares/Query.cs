using System;
using System.Linq;
using RecursoCurricular.Educacion;

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

        internal static IQueryable<Aprendizaje> GetAprendizajes(RecursoCurricular.Anio anio)
        {
            return
                 from aprendizaje in GetAprendizajes()
                 where aprendizaje.Anio == anio
                 select aprendizaje;
        }

        internal static IQueryable<Aprendizaje> GetAprendizajes(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                 from aprendizaje in GetAprendizajes(anio)
                 where aprendizaje.Grado == grado
                    && aprendizaje.Sector == sector
                 select aprendizaje;
        }

        internal static IQueryable<Aprendizaje> GetAprendizajes(RecursoCurricular.Anio anio, Unidad unidad)
        {
            return
                from aprendizaje in GetAprendizajes(anio)
                where GetUnidadAprendizajes(unidad).Select<UnidadAprendizaje, Aprendizaje>(x => x.Aprendizaje).Contains<Aprendizaje>(aprendizaje)
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

        internal static IQueryable<AprendizajeContenido> GetAprendizajeContenidos(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                from aprendizajeContenido in GetAprendizajeContenidos()
                where aprendizajeContenido.Anio == anio
                   && aprendizajeContenido.Grado == grado
                   && aprendizajeContenido.Sector == sector
                select aprendizajeContenido;
        }

        internal static IQueryable<AprendizajeContenido> GetAprendizajeContenidos(RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje)
        {
            return
                from aprendizajeContenido in GetAprendizajeContenidos()
                where aprendizajeContenido.Aprendizaje == aprendizaje
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

        internal static IQueryable<AprendizajeIndicador> GetAprendizajeIndicadores(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                from aprendizajeIndicador in GetAprendizajeIndicadores()
                where aprendizajeIndicador.Anio == anio
                   && aprendizajeIndicador.Grado == grado
                   && aprendizajeIndicador.Sector == sector
                select aprendizajeIndicador;
        }

        internal static IQueryable<AprendizajeIndicador> GetAprendizajeIndicadores(RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje)
        {
            return
                from aprendizajeIndicador in GetAprendizajeIndicadores()
                where aprendizajeIndicador.Aprendizaje == aprendizaje
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

        internal static IQueryable<AprendizajeObjetivoVertical> GetAprendizajeObjetivoVerticales(RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje)
        {
            return
                from aprendizajeObjetivoVertical in GetAprendizajeObjetivoVerticales()
                where aprendizajeObjetivoVertical.Aprendizaje == aprendizaje
                select aprendizajeObjetivoVertical;
        }
        #endregion

        #region Categoria
        internal static IQueryable<Categoria> GetCategorias()
        {
            return
                from categoria in Context.Instancia.Categorias
                select categoria;
        }
        #endregion

        #region Contenido
        internal static IQueryable<Contenido> GetContenidos()
        {
            return
                from contenido in Context.Instancia.Contenidos
                select contenido;
        }

        internal static IQueryable<Contenido> GetContenidos(RecursoCurricular.Anio anio)
        {
            return
                from contenido in GetContenidos()
                where contenido.Anio == anio
                select contenido;
        }

        internal static IQueryable<Contenido> GetContenidos(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector)
        {
            return
                from contenido in GetContenidos(anio)
                where contenido.Sector == sector
                select contenido;
        }

        internal static IQueryable<Contenido> GetContenidos(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.Educacion.Grado grado)
        {
            return
                from contenido in GetContenidos(anio, sector)
                where contenido.Grado == grado
                select contenido;
        }

        internal static IQueryable<Contenido> GetContenidos(RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje)
        {
            return
                from contenido in GetContenidos()
                where GetAprendizajeContenidos(aprendizaje).Select<RecursoCurricular.RecursosCurriculares.AprendizajeContenido, RecursoCurricular.RecursosCurriculares.Contenido>(x => x.Contenido).Contains<RecursoCurricular.RecursosCurriculares.Contenido>(contenido)
                select contenido;
        }

        internal static IQueryable<Contenido> GetContenidos(RecursoCurricular.RecursosCurriculares.Eje eje)
        {
            return
                from contenido in GetContenidos()
                where contenido.Eje == eje
                select contenido;
        }

        internal static IQueryable<Contenido> GetContenidos(RecursoCurricular.RecursosCurriculares.Eje eje, RecursoCurricular.Educacion.Sector sector)
        {
            return
                from contenido in GetContenidos(eje)
                where contenido.Sector == sector
                select contenido;
        }

        internal static IQueryable<Contenido> GetContenidos(RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.RecursosCurriculares.Eje eje)
        {
            return
                from contenido in GetContenidos(eje, sector)
                where contenido.Grado == grado
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

        internal static IQueryable<Eje> GetEjes(RecursoCurricular.Anio anio)
        {
            return
                from eje in GetEjes()
                where eje.Anio == anio
                select eje;
        }

        internal static IQueryable<Eje> GetEjes(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector)
        {
            return
                from eje in GetEjes(anio)
                where eje.Sector == sector
                select eje;
        }

        internal static IQueryable<Eje> GetEjes(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.Educacion.TipoEducacion tipoEducacion)
        {
            return
                from eje in GetEjes(anio, sector)
                where GetTipoEducacionEjes(tipoEducacion).Select<TipoEducacionEje, Eje>(x => x.Eje).Contains<Eje>(eje)
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

        internal static IQueryable<ObjetivoTransversal> GetObjetivoTransversales(RecursoCurricular.RecursosCurriculares.Unidad unidad)
        {
            return
                from objetivoTransversal in GetObjetivoTransversales()
                where objetivoTransversal.Unidad == unidad
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

        internal static IQueryable<ObjetivoTransversalIndicador> GetObjetivoTransversalIndicadores(RecursoCurricular.RecursosCurriculares.ObjetivoTransversal objetivoTransversal)
        {
            return
                from objetivoTransversalIndicador in GetObjetivoTransversalIndicadores()
                where objetivoTransversalIndicador.ObjetivoTransversal == objetivoTransversal
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

        internal static IQueryable<ObjetivoVertical> GetObjetivoVerticales(RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje)
        {
            return
                from objetivoVertical in GetObjetivoVerticales()
                where GetAprendizajeObjetivoVerticales(aprendizaje).Select<RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical, RecursoCurricular.RecursosCurriculares.ObjetivoVertical>(x => x.ObjetivoVertical).Contains<RecursoCurricular.RecursosCurriculares.ObjetivoVertical>(objetivoVertical)
                select objetivoVertical;
        }

        internal static IQueryable<ObjetivoVertical> GetObjetivoVerticales(RecursoCurricular.Anio anio)
        {
            return
                   from objetivoVertical in GetObjetivoVerticales()
                   where objetivoVertical.Anio == anio
                   select objetivoVertical;
        }

        internal static IQueryable<ObjetivoVertical> GetObjetivoVerticales(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                   from objetivoVertical in GetObjetivoVerticales(anio)
                   where objetivoVertical.Grado == grado
                      && objetivoVertical.Sector == sector
                   select objetivoVertical;
        }
        #endregion

        #region TipoEducacionEje
        internal static IQueryable<TipoEducacionEje> GetTipoEducacionEjes()
        {
            return
                from tipoEducacionEje in Context.Instancia.TipoEducacionEjes
                select tipoEducacionEje;
        }

        internal static IQueryable<TipoEducacionEje> GetTipoEducacionEjes(RecursoCurricular.RecursosCurriculares.Eje eje)
        {
            return
                from tipoEducacionEje in GetTipoEducacionEjes()
                where tipoEducacionEje.Eje == eje
                select tipoEducacionEje;
        }

        internal static IQueryable<TipoEducacionEje> GetTipoEducacionEjes(RecursoCurricular.Educacion.TipoEducacion tipoEducacion)
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

        #region UnidadAprendizaje
        internal static IQueryable<UnidadAprendizaje> GetUnidadAprendizajes()
        {
            return
                from unidadAprendizaje in Context.Instancia.UnidadAprendizajes
                select unidadAprendizaje;
        }

        internal static IQueryable<UnidadAprendizaje> GetUnidadAprendizajes(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                from unidadAprendizaje in GetUnidadAprendizajes()
                where unidadAprendizaje.Anio == anio
                   && unidadAprendizaje.Grado == grado
                   && unidadAprendizaje.Sector == sector
                select unidadAprendizaje;
        }

        internal static IQueryable<UnidadAprendizaje> GetUnidadAprendizajes(RecursoCurricular.RecursosCurriculares.Unidad unidad)
        {
            return
                from unidadAprendizaje in GetUnidadAprendizajes()
                where unidadAprendizaje.Unidad == unidad
                select unidadAprendizaje;
        }
        #endregion
    }
}