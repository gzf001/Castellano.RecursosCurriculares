using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.BaseCurricular
{
    public partial class Eje : RecursoCurricular.Default
    {
        public static int Last(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector)
        {
            IQueryable<Eje> ejes = Query.GetEjes(anio, sector);

            return ejes.Count<Eje>() > 0 ? ejes.Count<Eje>() + 1 : 1;
        }

        public static Eje Get(int anioNumero, Guid sectorId, Guid id)
        {
            return Query.GetEjes().SingleOrDefault<RecursoCurricular.BaseCurricular.Eje>(x => x.AnoNumero.Equals(anioNumero) && x.SectorId.Equals(sectorId) && x.Id.Equals(id));
        }

        public static List<Eje> GetAll()
        {
            return
                (
                from query in Query.GetEjes()
                select query
                ).ToList<Eje>();
        }

        public static List<Eje> GetAll(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector)
        {
            return
                (
                from query in Query.GetEjes(anio, sector)
                select query
                ).ToList<Eje>();
        }
    }
}