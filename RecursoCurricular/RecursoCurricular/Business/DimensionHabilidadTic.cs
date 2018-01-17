using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular
{
	public partial class DimensionHabilidadTic
	{
        public static int Last(RecursoCurricular.Anio anio)
        {
            IQueryable<DimensionHabilidadTic> dimensionHabilidadTIC = Query.GetDimensionHabilidadTices(anio);

            return dimensionHabilidadTIC.Count<DimensionHabilidadTic>() > 0 ? dimensionHabilidadTIC.Count<DimensionHabilidadTic>() + 1 : 1;
        }

        public static DimensionHabilidadTic Get(Guid id, int anoNumero)
        {
            return Query.GetDimensionHabilidadTices().SingleOrDefault<RecursoCurricular.DimensionHabilidadTic>(x => x.Id == id && x.AnoNumero == anoNumero);
        }

        public static List<DimensionHabilidadTic> GetAll()
        {
            return
                (
                from query in Query.GetDimensionHabilidadTices()
                select query
                ).ToList<RecursoCurricular.DimensionHabilidadTic>();
        }

        public static List<DimensionHabilidadTic> GetAll(RecursoCurricular.Anio anio)
        {
            return
                (
                from query in Query.GetDimensionHabilidadTices(anio)
                orderby query.Numero
                select query
                ).ToList<RecursoCurricular.DimensionHabilidadTic>();
        }
    }
}