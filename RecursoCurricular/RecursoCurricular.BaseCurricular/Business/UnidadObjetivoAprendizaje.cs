using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class UnidadObjetivoAprendizaje
    {
        public static UnidadObjetivoAprendizaje Get(int tipoEducacionCodigo, int anioNumero, int gradoCodigo, Guid sectorId, Guid unidadId, Guid ejeId, Guid objetivoAprendizajeId)
        {
            return Query.GetUnidadObjetivoAprendizajes().SingleOrDefault<RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.UnidadId.Equals(unidadId) && x.EjeId.Equals(ejeId) && x.ObjetivoAprendizajeId.Equals(objetivoAprendizajeId));
        }

        public static List<UnidadObjetivoAprendizaje> GetAll()
        {
            return
                (
                from query in Query.GetUnidadObjetivoAprendizajes()
                select query
                ).ToList<UnidadObjetivoAprendizaje>();
        }

        public static List<UnidadObjetivoAprendizaje> GetAll(RecursoCurricular.BaseCurricular.Unidad unidad)
        {
            return
                (
                from query in Query.GetUnidadObjetivoAprendizajes(unidad)
                orderby query.Orden
                select query
                ).ToList<UnidadObjetivoAprendizaje>();
        }
    }
}