using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class UnidadSubHabilidad
    {
        public static UnidadSubHabilidad Get(int tipoEducacionCodigo, int anioNumero, int gradoCodigo, Guid sectorId, Guid unidadId, Guid habilidadId, Guid subHabilidadId)
        {
            return Query.GetUnidadSubHabilidades().SingleOrDefault<RecursoCurricular.BaseCurricular.UnidadSubHabilidad>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.UnidadId.Equals(unidadId) && x.HabilidadId.Equals(habilidadId) && x.SubHabilidadId.Equals(subHabilidadId));
        }

        public static List<UnidadSubHabilidad> GetAll()
        {
            return
                (
                from query in Query.GetUnidadSubHabilidades()
                select query
                ).ToList<UnidadSubHabilidad>();
        }

        public static List<UnidadSubHabilidad> GetAll(Unidad unidad)
        {
            return
                (
                from query in Query.GetUnidadSubHabilidades(unidad)
                orderby query.SubHabilidad.Numero
                select query
                ).ToList<UnidadSubHabilidad>();
        }
    }
}