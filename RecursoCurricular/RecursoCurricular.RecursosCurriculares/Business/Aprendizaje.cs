using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
    public partial class Aprendizaje
    {
        public static int Last(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            IQueryable<Aprendizaje> aprendizajes = Query.GetAprendizajes(anio, grado, sector);

            return aprendizajes.Count<Aprendizaje>() > 0 ? aprendizajes.Count<Aprendizaje>() + 1 : 1;
        }

        public static Aprendizaje Get(int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid id)
        {
            return Query.GetAprendizajes().SingleOrDefault<RecursoCurricular.RecursosCurriculares.Aprendizaje>(x => x.AnoNumero.Equals(anioNumero) && x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.Id.Equals(id));
        }

        public static List<Aprendizaje> GetAll()
        {
            return
                (
                from query in Query.GetAprendizajes()
                select query
                ).ToList<Aprendizaje>();
        }

        public static List<Aprendizaje> GetAll(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                 (
                 from query in Query.GetAprendizajes(anio, grado, sector)
                 orderby query.Numero
                 select query
                 ).ToList<Aprendizaje>();
        }
    }
}