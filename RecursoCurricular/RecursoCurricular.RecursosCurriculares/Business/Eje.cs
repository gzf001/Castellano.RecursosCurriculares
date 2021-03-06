using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace RecursoCurricular.RecursosCurriculares
{
    public partial class Eje : RecursoCurricular.Default
    {
        const string urlSincronizacion = "/api/RecursosCurriculares/Ejes";

        public static IEnumerable<SelectListItem> Ejes(int anioNumero, Guid sectorId)
        {
            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            List<RecursoCurricular.RecursosCurriculares.Eje> ejes = RecursoCurricular.RecursosCurriculares.Eje.GetAll(anio, sector);

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
            return Query.GetEjes().SingleOrDefault<RecursoCurricular.RecursosCurriculares.Eje>(x => x.AnoNumero.Equals(anioNumero) && x.SectorId.Equals(sectorId) && x.Id.Equals(id));
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
        /// Retorna todos los ejes que tengan al menos un contenido asociado
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="sector"></param>
        /// <param name="tipoEducacion"></param>
        /// <returns></returns>
        public static List<Eje> GetEjesContenidos(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Sector sector, RecursoCurricular.Educacion.Grado grado)
        {
            return Query.GetContenidos(anio, sector, grado).Select<RecursoCurricular.RecursosCurriculares.Contenido, RecursoCurricular.RecursosCurriculares.Eje>(x => x.Eje).Distinct().OrderBy(x => x.Numero).ToList<RecursoCurricular.RecursosCurriculares.Eje>();
        }

        public void SyncUp()
        {
            List<RecursoCurricular.RecursosCurriculares.TipoEducacionEje> tiposEducacionEjes = RecursoCurricular.RecursosCurriculares.TipoEducacionEje.GetAll(this);

            foreach (RecursoCurricular.RecursosCurriculares.TipoEducacionEje tipoEducacionEje in tiposEducacionEjes)
            {
                RecursoCurricular.RecursosCurriculares.PassingObject.EjeAno ejeaAnio = new RecursoCurricular.RecursosCurriculares.PassingObject.EjeAno
                {
                    TipoEducacionCodigo = tipoEducacionEje.TipoEducacionCodigo,
                    SectorId = this.SectorId,
                    EjeId = this.Id,
                    AnoNumero = this.AnoNumero,
                    Eje = new RecursoCurricular.RecursosCurriculares.PassingObject.Eje
                    {
                        TipoEducacionCodigo = tipoEducacionEje.TipoEducacionCodigo,
                        SectorId = this.SectorId,
                        Id = this.Id,
                        AmbitoCodigo = 1,
                        SostenedorId = default(Guid),
                        EstablecimientoId = default(Guid),
                        Numero = this.Numero,
                        Nombre = this.Nombre
                    },
                    UrlSincronizacion = urlSincronizacion
                };

                RecursoCurricular.Synchronization synchronization = new RecursoCurricular.Synchronization();

                synchronization.SyncUp(ejeaAnio);
            }
        }
    }
}