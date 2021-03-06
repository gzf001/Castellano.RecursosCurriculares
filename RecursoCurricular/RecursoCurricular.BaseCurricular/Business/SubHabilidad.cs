using System;
using System.Collections.Generic;
using System.Linq;
using RecursoCurricular.Educacion;

namespace RecursoCurricular.BaseCurricular
{
    public partial class SubHabilidad
    {
        const string urlSincronizacion = "/api/BasesCurriculares/SubHabilidades";

        public static bool Exists(Habilidad habilidad, Grado grado)
        {
            return Query.GetSubHabilidades(habilidad, grado).Any<SubHabilidad>();
        }

        public static int Last(Habilidad habilidad)
        {
            IQueryable<SubHabilidad> subHabilidades = Query.GetSubHabilidades(habilidad);

            return subHabilidades.Count<SubHabilidad>() > 0 ? subHabilidades.Count<SubHabilidad>() + 1 : 1;
        }

        public static SubHabilidad Get(int tipoEducacionCodigo, int anioNumero, int gradoCodigo, Guid habilidadId, Guid sectorId, Guid id)
        {
            return Query.GetSubHabilidades().SingleOrDefault<SubHabilidad>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.GradoCodigo.Equals(gradoCodigo) && x.HabilidadId.Equals(habilidadId) && x.SectorId.Equals(sectorId) && x.Id.Equals(id));
        }

        public static List<SubHabilidad> GetAll()
        {
            return
                (
                from query in Query.GetSubHabilidades()
                select query
                ).ToList<SubHabilidad>();
        }

        public static List<SubHabilidad> GetAll(Habilidad habilidad)
        {
            return
                (
                from query in Query.GetSubHabilidades(habilidad)
                orderby query.Numero
                select query
                ).ToList<SubHabilidad>();
        }

        public static List<SubHabilidad> GetAll(Habilidad habilidad, RecursoCurricular.Educacion.Grado grado)
        {
            return
                (
                from query in Query.GetSubHabilidades(habilidad, grado)
                orderby query.Numero
                select query
                ).ToList<SubHabilidad>();
        }

        public void SyncUp()
        {
            RecursoCurricular.BaseCurricular.PassingObject.SubHabilidad subHabilidad = new RecursoCurricular.BaseCurricular.PassingObject.SubHabilidad
            {
                TipoEducacionCodigo = this.TipoEducacionCodigo,
                AnoNumero = this.AnoNumero,
                GradoCodigo = this.GradoCodigo,
                HabilidadId = this.HabilidadId,
                SectorId = this.SectorId,
                Id = this.Id,
                Numero = this.Numero,
                AmbitoCodigo = 1,
                SostenedorId = default(Guid),
                EstablecimientoId = default(Guid),
                Descripcion = this.Descripcion,
                UrlSincronizacion = urlSincronizacion
            };

            RecursoCurricular.Synchronization synchronization = new RecursoCurricular.Synchronization();

            synchronization.SyncUp(subHabilidad);
        }
    }
}