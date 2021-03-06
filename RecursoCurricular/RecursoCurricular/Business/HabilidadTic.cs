using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular
{
	public partial class HabilidadTic
	{
        public static HabilidadTic Get(Guid id, Guid dimensionHabilidadId, int anioNumero)
        {
            return Query.GetHabilidadTICes().SingleOrDefault<RecursoCurricular.HabilidadTic>(x => x.Id == id && x.DimensionHabilidadTicId == dimensionHabilidadId && x.AnoNumero == anioNumero);
        }

        public static int Last(RecursoCurricular.Anio anio)
        {
            IQueryable<HabilidadTic> habilidadTIC = Query.GetHabilidadTICes(anio);

            return habilidadTIC.Count<HabilidadTic>() > 0 ? habilidadTIC.Count<HabilidadTic>() + 1 : 1;
        }

        public static List<HabilidadTic> GetAll()
        {
            return
                (
                from query in Query.GetHabilidadTICes()
                select query
                ).ToList<HabilidadTic>();
        }

        public static List<HabilidadTic> GetAll(DimensionHabilidadTic dimensionHabilidadTIC)
        {
            return
                (
                from query in Query.GetHabilidadTICes(dimensionHabilidadTIC)
                orderby query.Numero
                select query
                ).ToList<HabilidadTic>();
        }
    }
}