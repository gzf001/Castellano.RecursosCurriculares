using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.BaseCurricular
{
    public partial class DimensionOAT : RecursoCurricular.Default
    {
        public static IEnumerable<SelectListItem> Dimensiones(RecursoCurricular.Anio anio)
        {
            SelectList lista = new SelectList(RecursoCurricular.BaseCurricular.DimensionOAT.GetAll(anio), "Id", "Nombre");

            return DefaultItem.Concat(lista);
        }

        public static int Last(RecursoCurricular.Anio anio)
        {
            IQueryable<DimensionOAT> dimensionOAT = Query.GetDimensionOATes(anio);

            return dimensionOAT.Count<DimensionOAT>() > 0 ? dimensionOAT.Count<DimensionOAT>() + 1 : 1;
        }

        public static DimensionOAT Get(Guid id, int anioNumero)
        {
            return Query.GetDimensionOATes().SingleOrDefault<DimensionOAT>(x => x.Id.Equals(id) && x.AnoNumero.Equals(anioNumero));
        }

        public static List<DimensionOAT> GetAll()
        {
            return
                (
                from query in Query.GetDimensionOATes()
                select query
                ).ToList<DimensionOAT>();
        }

        public static List<DimensionOAT> GetAll(Anio anio)
        {
            return
                (
                from query in Query.GetDimensionOATes(anio)
                orderby query.Numero
                select query
                ).ToList<DimensionOAT>();
        }
    }
}