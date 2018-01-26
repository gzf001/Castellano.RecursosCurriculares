using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class Conocimiento
    {
        public static int Last(RecursoCurricular.Educacion.TipoEducacion tipoEducacion, RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector)
        {
            IQueryable<Conocimiento> conocimientos = Query.GetConocimientos(tipoEducacion, anio, sector);

            return conocimientos.Count<Conocimiento>() > 0 ? conocimientos.Count<Conocimiento>() + 1 : 1;
        }

        public static Conocimiento Get(int tipoEducacionCodigo, int anioNumero, Guid sectorId, Guid id)
        {
            return Query.GetConocimientos().SingleOrDefault<Conocimiento>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.SectorId.Equals(sectorId) && x.Id.Equals(id));
        }

        public static List<Conocimiento> GetAll()
        {
            return
                (
                from query in Query.GetConocimientos()
                select query
                ).ToList<Conocimiento>();
        }

        public static List<Conocimiento> GetAll(RecursoCurricular.Educacion.TipoEducacion tipoEducacion, RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector)
        {
            return
                (
                from query in Query.GetConocimientos(tipoEducacion, anio, sector)
                orderby query.Numero
                select query
                ).ToList<Conocimiento>();
        }
    }
}