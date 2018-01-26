using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.BaseCurricular
{
    public partial class Actitud : RecursoCurricular.Default
    {
        public static int Last(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.TipoEducacion tipoEducacion, RecursoCurricular.Educacion.Sector sector)
        {
            IQueryable<Actitud> actitudes = Query.GetActitudes(anio, tipoEducacion, sector);

            return actitudes.Count<Actitud>() > 0 ? actitudes.Count<Actitud>() + 1 : 1;
        }

        public static Actitud Get(int tipoEducacionCodigo, int anioNumero, Guid sectorId, Guid id)
        {
            return Query.GetActitudes().SingleOrDefault<Actitud>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.SectorId.Equals(sectorId) && x.Id.Equals(id));
        }

        public static List<Actitud> GetAll()
        {
            return
                (
                from query in Query.GetActitudes()
                select query
                ).ToList<Actitud>();
        }

        public static List<Actitud> GetAll(RecursoCurricular.Educacion.TipoEducacion tipoEducacion, RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector)
        {
            return
                (
                from query in Query.GetActitudes(anio, tipoEducacion, sector)
                orderby query.Numero
                select query
                ).ToList<Actitud>();
        }
    }
}