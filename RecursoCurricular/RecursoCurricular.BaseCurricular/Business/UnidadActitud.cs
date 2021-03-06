using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class UnidadActitud
    {
        public static UnidadActitud Get(int tipoEducacionCodigo, int anioNumero, int gradoCodigo, Guid sectorId, Guid unidadId, Guid actitudId)
        {
            return Query.GetUnidadActitudes().SingleOrDefault<RecursoCurricular.BaseCurricular.UnidadActitud>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.UnidadId.Equals(unidadId) && x.ActitudId.Equals(actitudId));
        }

        public static List<UnidadActitud> GetAll()
        {
            return
                (
                from query in Query.GetUnidadActitudes()
                select query
                ).ToList<UnidadActitud>();
        }

        public static List<UnidadActitud> GetAll(RecursoCurricular.BaseCurricular.Unidad unidad)
        {
            return
                (
                from query in Query.GetUnidadActitudes(unidad)
                orderby query.Actitud.Numero
                select query
                ).ToList<UnidadActitud>();
        }
    }
}