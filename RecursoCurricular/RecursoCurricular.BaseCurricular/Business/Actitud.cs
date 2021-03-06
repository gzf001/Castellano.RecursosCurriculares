using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.BaseCurricular
{
    public partial class Actitud : RecursoCurricular.Default
    {
        const string urlSincronizacion = "/api/BasesCurriculares/Actitudes";

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

        public void SyncUp()
        {
            RecursoCurricular.BaseCurricular.PassingObject.Actitud actitud = new RecursoCurricular.BaseCurricular.PassingObject.Actitud
            {
                TipoEducacionCodigo = this.TipoEducacionCodigo,
                AnoNumero = this.AnoNumero,
                SectorId = this.SectorId,
                Id = this.Id,
                AmbitoCodigo = 1,
                SostenedorId = default(Guid),
                EstablecimientoId = default(Guid),
                Numero = this.Numero,
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                UrlSincronizacion = urlSincronizacion
            };

            RecursoCurricular.Synchronization synchronization = new RecursoCurricular.Synchronization();

            synchronization.SyncUp(actitud);
        }
    }
}