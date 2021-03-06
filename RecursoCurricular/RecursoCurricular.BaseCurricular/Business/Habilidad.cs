using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.BaseCurricular
{
    public partial class Habilidad : RecursoCurricular.Default
    {
        const string urlSincronizacion = "/api/BasesCurriculares/Habilidades";

        public static IEnumerable<SelectListItem> Habilidades(int tipoEducacionCodigo, int anioNumero, Guid sectorId)
        {
            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.TipoEducacion tipoEducacion = RecursoCurricular.Educacion.TipoEducacion.Get(tipoEducacionCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            List<RecursoCurricular.BaseCurricular.Habilidad> habilidades = RecursoCurricular.BaseCurricular.Habilidad.GetAll(anio, tipoEducacion, sector);

            SelectList lista = new SelectList(habilidades, "Id", "Descripcion");

            return Habilidad.DefaultItem.Concat(lista);
        }

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

        public void SyncUp()
        {
            RecursoCurricular.BaseCurricular.PassingObject.Habilidad habilidad = new RecursoCurricular.BaseCurricular.PassingObject.Habilidad
            {
                Id = this.Id,
                TipoEducacionCodigo = this.TipoEducacionCodigo,
                AnoNumero = this.AnoNumero,
                SectorId = this.SectorId,
                Numero = this.Numero,
                AmbitoCodigo = 1,
                SostenedorId = default(Guid),
                EstablecimientoId = default(Guid),
                Descripcion = this.Descripcion,
                UrlSincronizacion = urlSincronizacion
            };

            RecursoCurricular.Synchronization synchronization = new RecursoCurricular.Synchronization();

            synchronization.SyncUp(habilidad);
        }
    }
}