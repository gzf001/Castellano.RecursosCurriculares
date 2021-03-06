using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.BaseCurricular
{
    public partial class Conocimiento
    {
        const string urlSincronizacion = "/api/BasesCurriculares/Conocimientos";

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

        public void SyncUp()
        {
            RecursoCurricular.BaseCurricular.PassingObject.Conocimiento conocimiento = new RecursoCurricular.BaseCurricular.PassingObject.Conocimiento
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

            synchronization.SyncUp(conocimiento);
        }
    }
}