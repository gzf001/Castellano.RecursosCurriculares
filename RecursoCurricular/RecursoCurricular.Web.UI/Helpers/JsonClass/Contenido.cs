using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Helpers.JsonClass
{
    public class Contenido : Base
    {
        public List<Contenido> children
        {
            get;
            set;
        }

        public static List<Contenido> GetAll(int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid aprendizajeId)
        {
            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(tipoEducacionCodigo, gradoCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            List<Contenido> lista = new List<Contenido>();

            foreach (RecursoCurricular.RecursosCurriculares.Eje eje in RecursoCurricular.RecursosCurriculares.Eje.GetEjesContenidos(anio, sector, grado))
            {
                Contenido e = new Contenido
                {
                    key = "eje",
                    title = eje.Nombre,
                    folder = true,
                    children = new List<Contenido>()
                };

                foreach (RecursoCurricular.RecursosCurriculares.Contenido contenido in RecursoCurricular.RecursosCurriculares.Contenido.GetAll(grado, sector, eje))
                {
                    RecursoCurricular.RecursosCurriculares.AprendizajeContenido aprendizajeContenido = RecursosCurriculares.AprendizajeContenido.Get(anioNumero, tipoEducacionCodigo, gradoCodigo, sectorId, aprendizajeId, eje.Id, contenido.Id);

                    Contenido o = new Contenido
                    {
                        key = "contenido",
                        title = contenido.Descripcion,
                        folder = false,
                        selected = aprendizajeContenido != null,
                        children = new List<Contenido>()
                    };

                    e.children.Add(o);
                }

                lista.Add(e);
            }

            return lista;
        }
    }
}