using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class UnidadConocimiento
    {
        public static UnidadConocimiento Get(int tipoEducacionCodigo, int anioNumero, int gradoCodigo, Guid sectorId, Guid unidadId, Guid conocimientoId)
        {
            return Query.GetUnidadConocimientos().SingleOrDefault<RecursoCurricular.BaseCurricular.UnidadConocimiento>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.UnidadId.Equals(unidadId) && x.ConocimientoId.Equals(conocimientoId));
        }

        public static List<UnidadConocimiento> GetAll()
        {
            return
                (
                from query in Query.GetUnidadConocimientos()
                select query
                ).ToList<UnidadConocimiento>();
        }

        public static List<UnidadConocimiento> GetAll(RecursoCurricular.BaseCurricular.Unidad unidad)
        {
            return
                (
                from query in Query.GetUnidadConocimientos(unidad)
                orderby query.Conocimiento.Numero
                select query
                ).ToList<UnidadConocimiento>();
        }
    }
}