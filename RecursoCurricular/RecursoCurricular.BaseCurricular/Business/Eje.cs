using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.BaseCurricular
{
    public partial class Eje : RecursoCurricular.Default
    {
        const string urlSincronizacion = "/api/BasesCurriculares/EjesBaseCurricular";

        public static IEnumerable<SelectListItem> Ejes(int anioNumero, Guid sectorId)
        {
            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            List<RecursoCurricular.BaseCurricular.Eje> ejes = RecursoCurricular.BaseCurricular.Eje.GetAll(anio, sector);

            SelectList lista = new SelectList(ejes, "Id", "Nombre");

            return Eje.DefaultItem.Concat(lista);
        }

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
                orderby query.Numero
                select query
                ).ToList<Eje>();
        }

        public static List<Eje> GetAll(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.Educacion.TipoEducacion tipoEducacion)
        {
            return
                (
                from query in Query.GetEjes(anio, sector, tipoEducacion)
                orderby query.Numero
                select query
                ).ToList<Eje>();
        }

        /// <summary>
        /// Retorna todos los ejes que tengan objetivos de aprendizaje con indicadores
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="sector"></param>
        /// <param name="tipoEducacion"></param>
        /// <returns></returns>
        public static List<Eje> GetEjesObjetivoAprendizajeIndicador(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.Educacion.TipoEducacion tipoEducacion)
        {
            return Query.GetIndicadores(anio, sector, tipoEducacion).Select<RecursoCurricular.BaseCurricular.Indicador, RecursoCurricular.BaseCurricular.Eje>(x => x.ObjetivoAprendizaje.Eje).Distinct().OrderBy(x => x.Numero).ToList<RecursoCurricular.BaseCurricular.Eje>();
        }

        public void SyncUp()
        {
            List<RecursoCurricular.BaseCurricular.TipoEducacionEje> tiposEducacionEjes = RecursoCurricular.BaseCurricular.TipoEducacionEje.GetAll(this);

            foreach (RecursoCurricular.BaseCurricular.TipoEducacionEje tipoEducacionEje in tiposEducacionEjes)
            {
                RecursoCurricular.BaseCurricular.PassingObject.Eje eje = new RecursoCurricular.BaseCurricular.PassingObject.Eje
                {
                    TipoEducacionCodigo = tipoEducacionEje.TipoEducacionCodigo,
                    AnoNumero = this.AnoNumero,
                    SectorId = this.SectorId,
                    Id = this.Id,
                    AmbitoCodigo = 1,
                    SostenedorId = default(Guid),
                    EstablecimientoId = default(Guid),
                    Numero = this.Numero,
                    Nombre = this.Nombre,
                    UrlSincronizacion = urlSincronizacion
                };

                RecursoCurricular.Synchronization synchronization = new RecursoCurricular.Synchronization();

                synchronization.SyncUp(eje);
            }
        }
    }
}