using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
    public partial class TipoEducacionEje
    {
        public static TipoEducacionEje Get(int tipoEducacionCodigo, int anioNumero, Guid sectorId, Guid ejeId)
        {
            return Query.GetTipoEducacionEjes().SingleOrDefault<RecursoCurricular.RecursosCurriculares.TipoEducacionEje>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.SectorId.Equals(sectorId) && x.EjeId.Equals(ejeId));
        }

        public static List<TipoEducacionEje> GetAll()
        {
            return
                (
                from query in Query.GetTipoEducacionEjes()
                select query
                ).ToList<TipoEducacionEje>();
        }

        public static List<TipoEducacionEje> GetAll(Eje eje)
        {
            return
                (
                from query in Query.GetTipoEducacionEjes(eje)
                orderby query.TipoEducacionCodigo
                select query
                ).ToList<TipoEducacionEje>();
        }
    }
}