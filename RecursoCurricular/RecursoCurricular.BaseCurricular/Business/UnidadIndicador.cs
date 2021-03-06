using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class UnidadIndicador
    {
        public static UnidadIndicador Get(int tipoEducacionCodigo, int anioNumero, int gradoCodigo, Guid sectorId, Guid unidadId, Guid ejeId, Guid objetivoAprendizajeId, Guid indicadorId)
        {
            return Query.GetUnidadIndicadores().SingleOrDefault<RecursoCurricular.BaseCurricular.UnidadIndicador>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.UnidadId.Equals(unidadId) && x.EjeId.Equals(ejeId) && x.ObjetivoAprendizajeId.Equals(objetivoAprendizajeId) && x.IndicadorId.Equals(indicadorId));
        }

        public static List<UnidadIndicador> GetAll()
        {
            return
                (
                from query in Query.GetUnidadIndicadores()
                select query
                ).ToList<UnidadIndicador>();
        }

        public static List<UnidadIndicador> GetAll(RecursoCurricular.BaseCurricular.Unidad unidad)
        {
            return
                (
                from query in Query.GetUnidadIndicadores(unidad)
                orderby query.Orden
                select query
                ).ToList<UnidadIndicador>();
        }

        public static List<UnidadIndicador> GetAll(RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje unidadObjetivoAprendizaje)
        {
            return
                (
                from query in Query.GetUnidadIndicadores(unidadObjetivoAprendizaje)
                orderby query.Orden
                select query
                ).ToList<UnidadIndicador>();
        }
    }
}