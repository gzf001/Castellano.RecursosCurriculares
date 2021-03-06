using System;
using System.Linq;
namespace RecursoCurricular
{
    internal static class Query
    {
        #region Ano
        internal static IQueryable<Anio> GetAnios()
        {
            return
                from ano in Context.Instancia.Anios
                select ano;
        }

        internal static IQueryable<Anio> GetAnios(bool activos)
        {
            return activos ?
                from ano in GetAnios()
                where ano.Activo
                select ano
                :
                from ano in GetAnios()
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

        internal static IQueryable<Ciudad> GetCiudades(Region region)
        {
            return
                from ciudad in GetCiudades()
                where ciudad.Region == region
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

        internal static IQueryable<Comuna> GetComunas(Ciudad ciudad)
        {
            return
                from comuna in GetComunas()
                where comuna.Ciudad == ciudad
                select comuna;
        }
        #endregion

        #region DimensionHabilidadTIC
        internal static IQueryable<DimensionHabilidadTic> GetDimensionHabilidadTices()
        {
            return
                from dimensionHabilidadTIC in Context.Instancia.DimensionHabilidadTices
                select dimensionHabilidadTIC;
        }

        internal static IQueryable<DimensionHabilidadTic> GetDimensionHabilidadTices(Anio anio)
        {
            return
                from dimensionHabilidadTIC in GetDimensionHabilidadTices()
                where dimensionHabilidadTIC.Anio == anio
                select dimensionHabilidadTIC;
        }
        #endregion

        #region EstadoSincronizacion
        internal static IQueryable<EstadoSincronizacion> GetEstadoSincronizaciones()
        {
            return
                from estadoSincronizacion in Context.Instancia.EstadoSincronizaciones
                select estadoSincronizacion;
        }
        #endregion

        #region HabilidadTIC
        internal static IQueryable<HabilidadTic> GetHabilidadTICes()
        {
            return
                from habilidadTIC in Context.Instancia.HabilidadTices
                select habilidadTIC;
        }

        internal static IQueryable<HabilidadTic> GetHabilidadTICes(Anio anio)
        {
            return
                from habilidadTIC in GetHabilidadTICes()
                where habilidadTIC.Anio == anio
                select habilidadTIC;
        }

        internal static IQueryable<HabilidadTic> GetHabilidadTICes(DimensionHabilidadTic dimensionHabilidadTIC)
        {
            return
               from habilidadTIC in GetHabilidadTICes()
               where habilidadTIC.DimensionHabilidadTIC == dimensionHabilidadTIC
               select habilidadTIC;
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

        internal static IQueryable<Persona> GetPersonas(FindType findType, string filter)
        {
            switch (findType)
            {
                case FindType.StartsWith: return from usuario in GetPersonas() where (usuario.Nombre.StartsWith(filter)) select usuario;
                case FindType.Contains: return from usuario in GetPersonas() where (usuario.Nombre.Contains(filter)) select usuario;
                case FindType.EndsWith: return from usuario in GetPersonas() where (usuario.Nombre.EndsWith(filter)) select usuario;
                default: return from usuario in GetPersonas() where (usuario.Nombre == filter) select usuario;
            }
        }

        internal static IQueryable<Persona> GetPersonas(int cuerpo, char digito)
        {
            return
                from persona in GetPersonas()
                where persona.RunCuerpo == cuerpo
                && persona.RunDigito == digito
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

        #region Sincronizacion
        internal static IQueryable<Sincronizacion> GetSincronizaciones()
        {
            return
                from sincronizacion in Context.Instancia.Sincronizaciones
                select sincronizacion;
        }
        #endregion

        #region Sistemas
        internal static IQueryable<Sistema> GetSistemas()
        {
            return
                from sistema in Context.Instancia.Sistemas
                select sistema;
        }
        #endregion
    }
}