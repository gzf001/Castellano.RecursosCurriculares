using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
    public partial class AprendizajeContenido
    {
        public static AprendizajeContenido Get(int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid aprendizajeId, Guid ejeId, Guid contenidoId)
        {
            return Query.GetAprendizajeContenidos().SingleOrDefault<RecursoCurricular.RecursosCurriculares.AprendizajeContenido>(x => x.AnoNumero.Equals(anioNumero) && x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.AprendizajeId.Equals(aprendizajeId) && x.EjeId.Equals(ejeId) && x.ContenidoId.Equals(contenidoId));
        }

        public static List<AprendizajeContenido> GetAll()
        {
            return
                (
                from query in Query.GetAprendizajeContenidos()
                select query
                ).ToList<AprendizajeContenido>();
        }

        public static List<AprendizajeContenido> GetAll(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                (
                from query in Query.GetAprendizajeContenidos(anio, grado, sector)
                orderby query.Contenido.Numero
                select query
                ).ToList<AprendizajeContenido>();
        }

        public static List<AprendizajeContenido> GetAll(RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje)
        {
            return
                (
                from query in Query.GetAprendizajeContenidos(aprendizaje)
                orderby query.Contenido.Numero
                select query
                ).ToList<AprendizajeContenido>();
        }
    }
}