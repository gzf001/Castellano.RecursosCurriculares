using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
    public partial class ObjetivoTransversal
    {
        public static int Last(RecursoCurricular.RecursosCurriculares.Unidad unidad)
        {
            IQueryable<ObjetivoTransversal> objetivosTransversales = Query.GetObjetivoTransversales(unidad);

            return objetivosTransversales.Count<ObjetivoTransversal>() > 0 ? objetivosTransversales.Count<ObjetivoTransversal>() + 1 : 1;
        }

        public static ObjetivoTransversal Get(int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid unidadId, Guid id)
        {
            return Query.GetObjetivoTransversales().SingleOrDefault<RecursoCurricular.RecursosCurriculares.ObjetivoTransversal>(x => x.AnoNumero.Equals(anioNumero) && x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.UnidadId.Equals(unidadId) && x.Id.Equals(id));
        }

        public static List<ObjetivoTransversal> GetAll()
        {
            return
                (
                from query in Query.GetObjetivoTransversales()
                select query
                ).ToList<ObjetivoTransversal>();
        }

        public static List<ObjetivoTransversal> GetAll(RecursoCurricular.RecursosCurriculares.Unidad unidad)
        {
            return
                (
                from query in Query.GetObjetivoTransversales(unidad)
                orderby query.Numero
                select query
                ).ToList<ObjetivoTransversal>();
        }
    }
}