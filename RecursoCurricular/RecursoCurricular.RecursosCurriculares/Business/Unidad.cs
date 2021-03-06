using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RecursoCurricular.RecursosCurriculares
{
    public partial class Unidad : RecursoCurricular.Default
    {
        const string urlSincronizacion = "/api/RecursosCurriculares/Unidades";

        public static IEnumerable<SelectListItem> Unidades(int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId)
        {
            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(tipoEducacionCodigo, gradoCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            List<RecursoCurricular.RecursosCurriculares.Unidad> unidades = RecursoCurricular.RecursosCurriculares.Unidad.GetAll(anio, grado, sector);

            SelectList lista = new SelectList(unidades, "Id", "Nombre");

            return Unidad.DefaultItem.Concat(lista);
        }

        public static int Last(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            IQueryable<Unidad> unidades = Query.GetUnidades(anio, grado, sector);

            return unidades.Count<Unidad>() > 0 ? unidades.Count<Unidad>() + 1 : 1;
        }

        public static Unidad Get(int tipoEducacionCodigo, int anioNumero, int gradoCodigo, Guid SectorId, Guid id)
        {
            return Query.GetUnidades().SingleOrDefault<RecursoCurricular.RecursosCurriculares.Unidad>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.AnoNumero.Equals(anioNumero) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(SectorId) && x.Id.Equals(id));
        }

        public static List<Unidad> GetAll()
        {
            return
                (
                from query in Query.GetUnidades()
                select query
                ).ToList<Unidad>();
        }

        public static List<Unidad> GetAll(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                (
                from query in Query.GetUnidades(anio, grado, sector)
                orderby query.Numero
                select query
                ).ToList<Unidad>();
        }

        public static Result Save(RecursoCurricular.RecursosCurriculares.Unidad unidad, List<RecursoCurricular.RecursosCurriculares.Aprendizaje> aprendizajes)
        {
            try
            {
                using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                {
                    foreach (RecursoCurricular.RecursosCurriculares.UnidadAprendizaje unidadAprendizaje in RecursoCurricular.RecursosCurriculares.UnidadAprendizaje.GetAll(unidad))
                    {
                        unidadAprendizaje.Delete(context);
                    }

                    context.SubmitChanges();
                }

                using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                {
                    RecursoCurricular.RecursosCurriculares.Unidad uni = new RecursoCurricular.RecursosCurriculares.Unidad
                    {
                        AnoNumero = unidad.AnoNumero,
                        TipoEducacionCodigo = unidad.TipoEducacionCodigo,
                        GradoCodigo = unidad.GradoCodigo,
                        SectorId = unidad.SectorId,
                        Id = unidad.Id,
                        Numero = unidad.Numero,
                        Nombre = unidad.Nombre.Trim()
                    };

                    uni.Save(context);

                    if (aprendizajes != null)
                    {
                        foreach (RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje in aprendizajes)
                        {
                            new RecursoCurricular.RecursosCurriculares.UnidadAprendizaje
                            {
                                AnoNumero = uni.AnoNumero,
                                TipoEducacionCodigo = uni.TipoEducacionCodigo,
                                GradoCodigo = uni.GradoCodigo,
                                SectorId = uni.SectorId,
                                UnidadId = uni.Id,
                                AprendizajeId = aprendizaje.Id
                            }.Save(context);
                        }
                    }

                    context.SubmitChanges();

                    uni.SyncUp();
                }

                return new RecursoCurricular.Result
                {
                    Status = "200"
                };
            }
            catch
            {
                return new RecursoCurricular.Result
                {
                    Status = "500"
                };
            }
        }

        public void SyncUp()
        {
            RecursoCurricular.RecursosCurriculares.PassingObject.UnidadAno unidadAnio = new RecursoCurricular.RecursosCurriculares.PassingObject.UnidadAno
            {
                TipoEducacionCodigo = this.TipoEducacionCodigo,
                GradoCodigo = this.GradoCodigo,
                SectorId = this.SectorId,
                UnidadId = this.Id,
                AnoNumero = this.AnoNumero,
                Unidad = new RecursoCurricular.RecursosCurriculares.PassingObject.Unidad
                {
                    TipoEducacionCodigo = this.TipoEducacionCodigo,
                    GradoCodigo = this.GradoCodigo,
                    SectorId = this.SectorId,
                    Id = this.Id,
                    AmbitoCodigo = 1,
                    SostenedorId = default(Guid),
                    EstablecimientoId = default(Guid),
                    Numero = this.Numero,
                    Nombre = this.Nombre,
                    Aprendizajes = new List<RecursoCurricular.RecursosCurriculares.PassingObject.Aprendizaje>()
                },
                UrlSincronizacion = urlSincronizacion
            };

            this.Attach();

            foreach (RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje in RecursoCurricular.RecursosCurriculares.Aprendizaje.GetAll(this.Anio, this))
            {
                unidadAnio.Unidad.Aprendizajes.Add(new RecursoCurricular.RecursosCurriculares.PassingObject.Aprendizaje
                {
                    TipoEducacionCodigo = aprendizaje.TipoEducacionCodigo,
                    GradoCodigo = aprendizaje.GradoCodigo,
                    SectorId = aprendizaje.SectorId,
                    Id = aprendizaje.Id,
                    AmbitoCodigo = 1,
                    SostenedorId = default(Guid),
                    EstablecimientoId = default(Guid),
                    Numero = aprendizaje.Numero,
                    Descripcion = aprendizaje.Descripcion
                });
            }

            RecursoCurricular.Synchronization synchronization = new RecursoCurricular.Synchronization();

            synchronization.SyncUp(unidadAnio);
        }
    }
}