using System;
using System.Collections.Generic;
using System.Linq;
namespace RecursoCurricular.RecursosCurriculares
{
    public partial class Aprendizaje
    {
        const string urlSincronizacion = "/api/RecursosCurriculares/Aprendizajes";

        public static int Last(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            IQueryable<Aprendizaje> aprendizajes = Query.GetAprendizajes(anio, grado, sector);

            return aprendizajes.Count<Aprendizaje>() > 0 ? aprendizajes.Count<Aprendizaje>() + 1 : 1;
        }

        public static Aprendizaje Get(int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid id)
        {
            return Query.GetAprendizajes().SingleOrDefault<RecursoCurricular.RecursosCurriculares.Aprendizaje>(x => x.AnoNumero.Equals(anioNumero) && x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.GradoCodigo.Equals(gradoCodigo) && x.SectorId.Equals(sectorId) && x.Id.Equals(id));
        }

        public static List<Aprendizaje> GetAll()
        {
            return
                (
                from query in Query.GetAprendizajes()
                select query
                ).ToList<Aprendizaje>();
        }

        public static List<Aprendizaje> GetAll(RecursoCurricular.Anio anio, RecursoCurricular.Educacion.Grado grado, RecursoCurricular.Educacion.Sector sector)
        {
            return
                 (
                 from query in Query.GetAprendizajes(anio, grado, sector)
                 orderby query.Numero
                 select query
                 ).ToList<Aprendizaje>();
        }

        public static List<Aprendizaje> GetAll(RecursoCurricular.Anio anio, RecursoCurricular.RecursosCurriculares.Unidad unidad)
        {
            return
                 (
                 from query in Query.GetAprendizajes(anio, unidad)
                 orderby query.Numero
                 select query
                 ).ToList<Aprendizaje>();
        }

        public static Result Save(RecursoCurricular.RecursosCurriculares.Aprendizaje aprendizaje, List<RecursoCurricular.RecursosCurriculares.AprendizajeContenido> aprendizajeContenidos, List<RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical> aprendizajeObjetivoVerticales)
        {
            try
            {
                using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                {
                    foreach (RecursoCurricular.RecursosCurriculares.AprendizajeContenido aprendizajeContenido in RecursoCurricular.RecursosCurriculares.AprendizajeContenido.GetAll(aprendizaje))
                    {
                        aprendizajeContenido.Delete(context);
                    }

                    foreach (RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical aprendizajeObjetivoVertical in RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical.GetAll(aprendizaje))
                    {
                        aprendizajeObjetivoVertical.Delete(context);
                    }

                    context.SubmitChanges();
                }

                using (RecursoCurricular.RecursosCurriculares.Context context = new RecursoCurricular.RecursosCurriculares.Context())
                {
                    aprendizaje.Save(context);

                    if (aprendizajeContenidos != null)
                    {
                        foreach (RecursoCurricular.RecursosCurriculares.AprendizajeContenido aprendizajeContenido in aprendizajeContenidos)
                        {
                            new RecursoCurricular.RecursosCurriculares.AprendizajeContenido
                            {
                                AnoNumero = aprendizaje.AnoNumero,
                                TipoEducacionCodigo = aprendizajeContenido.TipoEducacionCodigo,
                                GradoCodigo = aprendizajeContenido.GradoCodigo,
                                SectorId = aprendizajeContenido.SectorId,
                                AprendizajeId = aprendizajeContenido.AprendizajeId,
                                EjeId = aprendizajeContenido.EjeId,
                                ContenidoId = aprendizajeContenido.ContenidoId
                            }.Save(context);
                        }
                    }

                    if (aprendizajeObjetivoVerticales != null)
                    {
                        foreach (RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical aprendizajeObjetivoVertical in aprendizajeObjetivoVerticales)
                        {
                            new RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical
                            {
                                AnoNumero = aprendizaje.AnoNumero,
                                TipoEducacionCodigo = aprendizajeObjetivoVertical.TipoEducacionCodigo,
                                GradoCodigo = aprendizajeObjetivoVertical.GradoCodigo,
                                SectorId = aprendizajeObjetivoVertical.SectorId,
                                AprendizajeId = aprendizajeObjetivoVertical.AprendizajeId,
                                ObjetivoVerticalId = aprendizajeObjetivoVertical.ObjetivoVerticalId
                            }.Save(context);
                        }
                    }

                    context.SubmitChanges();

                    aprendizaje.SyncUp();

                    return new RecursoCurricular.Result
                    {
                        Status = "200"
                    };
                }
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
            List<RecursoCurricular.RecursosCurriculares.AprendizajeContenido> aprendizajeContenidos = RecursoCurricular.RecursosCurriculares.AprendizajeContenido.GetAll(this);
            List<RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical> aprendizajeObjetivosVerticales = RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical.GetAll(this);

            RecursoCurricular.RecursosCurriculares.PassingObject.AprendizajeAno aprendizajeAno = new RecursoCurricular.RecursosCurriculares.PassingObject.AprendizajeAno
            {
                TipoEducacionCodigo = this.TipoEducacionCodigo,
                GradoCodigo = this.GradoCodigo,
                SectorId = this.SectorId,
                AprendizajeId = this.Id,
                AnoNumero = this.AnoNumero,
                Aprendizaje = new RecursoCurricular.RecursosCurriculares.PassingObject.Aprendizaje
                {
                    TipoEducacionCodigo = this.TipoEducacionCodigo,
                    GradoCodigo = this.GradoCodigo,
                    SectorId = this.SectorId,
                    Id = this.Id,
                    AmbitoCodigo = 1,
                    SostenedorId = default(Guid),
                    EstablecimientoId = default(Guid),
                    Numero = this.Numero,
                    Descripcion = this.Descripcion,
                    AprendizajeContenidos = new List<RecursoCurricular.RecursosCurriculares.PassingObject.AprendizajeContenido>(),
                    AprendizajeObjetivosVerticales = new List<RecursoCurricular.RecursosCurriculares.PassingObject.AprendizajeObjetivoVertical>()
                },
                UrlSincronizacion = urlSincronizacion
            };

            foreach (RecursoCurricular.RecursosCurriculares.AprendizajeContenido aprendizajeContenido in aprendizajeContenidos)
            {
                aprendizajeAno.Aprendizaje.AprendizajeContenidos.Add(new RecursoCurricular.RecursosCurriculares.PassingObject.AprendizajeContenido
                {
                    TipoEducacionCodigo = aprendizajeContenido.TipoEducacionCodigo,
                    GradoCodigo = aprendizajeContenido.GradoCodigo,
                    SectorId = aprendizajeContenido.SectorId,
                    AprendizajeId = aprendizajeContenido.AprendizajeId,
                    EjeId = aprendizajeContenido.EjeId,
                    ContenidoId = aprendizajeContenido.ContenidoId
                });
            }

            foreach (RecursoCurricular.RecursosCurriculares.AprendizajeObjetivoVertical aprendizajeObjetivoVertical in aprendizajeObjetivosVerticales)
            {
                aprendizajeAno.Aprendizaje.AprendizajeObjetivosVerticales.Add(new RecursoCurricular.RecursosCurriculares.PassingObject.AprendizajeObjetivoVertical
                {
                    TipoEducacionCodigo = aprendizajeObjetivoVertical.TipoEducacionCodigo,
                    GradoCodigo = aprendizajeObjetivoVertical.GradoCodigo,
                    SectorId = aprendizajeObjetivoVertical.SectorId,
                    AprendizajeId = aprendizajeObjetivoVertical.AprendizajeId,
                    ObjetivoVerticalId = aprendizajeObjetivoVertical.ObjetivoVerticalId
                });
            }

            RecursoCurricular.Synchronization synchronization = new RecursoCurricular.Synchronization();

            synchronization.SyncUp(aprendizajeAno);
        }
    }
}