using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
	public partial class ObjetivoAprendizaje
    {
        public static int Last(RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.BaseCurricular.Eje eje)
        {
            IQueryable<ObjetivoAprendizaje> objetivosAprendizaje = Query.GetObjetivoAprendizajes(grado, sector, eje);

            return objetivosAprendizaje.Count<ObjetivoAprendizaje>() > 0 ? objetivosAprendizaje.Count<ObjetivoAprendizaje>() + 1 : 1;
        }

        public static ObjetivoAprendizaje Get(int tipoEducacionCodigo, int anioNumero, int gradoCodigo, Guid sectorId, Guid ejeId, Guid id)
        {
            return Query.GetObjetivoAprendizajes().SingleOrDefault<RecursoCurricular.BaseCurricular.ObjetivoAprendizaje>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.EjeId.Equals(ejeId) && x.Id.Equals(id));
        }

        public static List<ObjetivoAprendizaje> GetAll()
		{
			return
				(
				from query in Query.GetObjetivoAprendizajes()
				select query
				).ToList<ObjetivoAprendizaje>();
		}

        public static List<ObjetivoAprendizaje> GetAll(RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.BaseCurricular.Eje eje)
        {
            return
                (
                from query in Query.GetObjetivoAprendizajes(grado, sector, eje)
                orderby query.Numero
                select query
                ).ToList<ObjetivoAprendizaje>();
        }
    }
}