using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class SubHabilidad
    {
        public static int Last(Habilidad habilidad)
        {
            IQueryable<SubHabilidad> subHabilidades = Query.GetSubHabilidades(habilidad);

            return subHabilidades.Count<SubHabilidad>() > 0 ? subHabilidades.Count<SubHabilidad>() + 1 : 1;
        }

        public static SubHabilidad Get(int tipoEducacionCodigo, int anioNumero, int gradoCodigo, Guid habilidadId, Guid sectorId, Guid id)
        {
            return Query.GetSubHabilidades().SingleOrDefault<SubHabilidad>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.GradoCodigo.Equals(gradoCodigo) && x.HabilidadId.Equals(habilidadId) && x.SectorId.Equals(sectorId) && x.Id.Equals(id));
        }

        public static List<SubHabilidad> GetAll()
        {
            return
                (
                from query in Query.GetSubHabilidades()
                select query
                ).ToList<SubHabilidad>();
        }

        public static List<SubHabilidad> GetAll(Habilidad habilidad)
        {
            return
                (
                from query in Query.GetSubHabilidades(habilidad)
                orderby query.Numero
                select query
                ).ToList<SubHabilidad>();
        }

    }
}