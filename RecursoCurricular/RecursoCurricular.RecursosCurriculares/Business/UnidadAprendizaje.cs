using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
    public partial class UnidadAprendizaje
    {
        public static UnidadAprendizaje Get(int anoNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid unidadId, Guid aprendizajeId)
        {
            return Query.GetUnidadAprendizajes().SingleOrDefault<RecursoCurricular.RecursosCurriculares.UnidadAprendizaje>(x => x.AnoNumero.Equals(anoNumero) && x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.UnidadId.Equals(unidadId) && x.AprendizajeId.Equals(aprendizajeId));
        }

        public static List<UnidadAprendizaje> GetAll()
        {
            return
                (
                from query in Query.GetUnidadAprendizajes()
                select query
                ).ToList<UnidadAprendizaje>();
        }

        public static List<UnidadAprendizaje> GetAll(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                (
                from query in Query.GetUnidadAprendizajes(anio, grado, sector)
                orderby query.Aprendizaje.Numero
                select query
                ).ToList<UnidadAprendizaje>();
        }

        public static List<UnidadAprendizaje> GetAll(RecursoCurricular.RecursosCurriculares.Unidad unidad)
        {
            return
                (
                from query in Query.GetUnidadAprendizajes(unidad)
                orderby query.Aprendizaje.Numero
                select query
                ).ToList<UnidadAprendizaje>();
        }
    }
}