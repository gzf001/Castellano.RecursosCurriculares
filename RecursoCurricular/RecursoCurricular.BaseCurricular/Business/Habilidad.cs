using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class Habilidad
    {
        public static int Last(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.TipoEducacion tipoEducacion, RecursoCurricular.Educacion.Sector sector)
        {
            IQueryable<Habilidad> habilidades = Query.GetHabilidades(anio, tipoEducacion, sector);

            return habilidades.Count<Habilidad>() > 0 ? habilidades.Count<Habilidad>() + 1 : 1;
        }

        public static Habilidad Get(Guid id, int tipoEducacionCodigo, int anioNumero, Guid sectorId)
        {
            return Query.GetHabilidades().SingleOrDefault<Habilidad>(x => x.Id.Equals(id) && x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.SectorId.Equals(sectorId));
        }

        public static List<Habilidad> GetAll()
        {
            return
                (
                from query in Query.GetHabilidades()
                select query
                ).ToList<Habilidad>();
        }

        public static List<Habilidad> GetAll(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.TipoEducacion tipoEducacion, RecursoCurricular.Educacion.Sector sector)
        {
            return
                (
                from query in Query.GetHabilidades(anio, tipoEducacion, sector)
                orderby query.Numero
                select query
                ).ToList<Habilidad>();
        }
    }
}