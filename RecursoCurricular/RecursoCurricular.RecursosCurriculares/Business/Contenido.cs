using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
	public partial class Contenido
	{
        public static bool Exists(RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.RecursosCurriculares.Eje eje)
        {
            return Query.GetContenidos(grado, sector, eje).Any<RecursoCurricular.RecursosCurriculares.Contenido>();
        }

        public static int Last(RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.RecursosCurriculares.Eje eje)
        {
            IQueryable<Contenido> objetivosAprendizaje = Query.GetContenidos(grado, sector, eje);

            return objetivosAprendizaje.Count<Contenido>() > 0 ? objetivosAprendizaje.Count<Contenido>() + 1 : 1;
        }

        public static Contenido Get(int tipoEducacionCodigo, int anioNumero, int gradoCodigo, Guid sectorId, Guid ejeId, Guid id)
        {
            return Query.GetContenidos().SingleOrDefault<RecursoCurricular.RecursosCurriculares.Contenido>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.EjeId.Equals(ejeId) && x.Id.Equals(id));
        }

        public static List<Contenido> GetAll()
        {
            return
                (
                from query in Query.GetContenidos()
                select query
                ).ToList<Contenido>();
        }

        public static List<Contenido> GetAll(RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.RecursosCurriculares.Eje eje)
        {
            return
                (
                from query in Query.GetContenidos(grado, sector, eje)
                orderby query.Numero
                select query
                ).ToList<Contenido>();
        }
    }
}