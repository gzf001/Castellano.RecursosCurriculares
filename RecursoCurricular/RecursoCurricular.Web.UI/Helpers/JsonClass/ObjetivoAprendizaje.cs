using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Helpers.JsonClass
{
    public class ObjetivoAprendizaje : Base
    {
        public List<ObjetivoAprendizaje> children
        {
            get;
            set;
        }

        public static List<ObjetivoAprendizaje> GetAll(Guid unidadId, int anioNumero, int tipoEducacionCodigo, int gradoCodigo, Guid sectorId)
        {
            RecursoCurricular.Anio anio = RecursoCurricular.Anio.Get(anioNumero);

            RecursoCurricular.Educacion.Grado grado = RecursoCurricular.Educacion.Grado.Get(tipoEducacionCodigo, gradoCodigo);

            RecursoCurricular.Educacion.Sector sector = RecursoCurricular.Educacion.Sector.Get(sectorId);

            List<ObjetivoAprendizaje> lista = new List<ObjetivoAprendizaje>();

            foreach (RecursoCurricular.BaseCurricular.Eje eje in RecursoCurricular.BaseCurricular.Eje.GetEjesObjetivoAprendizajeIndicador(anio, sector, grado.TipoEducacion))
            {
                ObjetivoAprendizaje e = new ObjetivoAprendizaje
                {
                    key = "eje",
                    title = eje.Nombre,
                    folder = true,
                    children = new List<ObjetivoAprendizaje>()
                };

                foreach (RecursoCurricular.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje in RecursoCurricular.BaseCurricular.ObjetivoAprendizaje.GetObjetivosAprendizajeIndicador(anio, grado, sector, eje))
                {
                    ObjetivoAprendizaje o = new ObjetivoAprendizaje
                    {
                        key = "objetivoAprendizaje",
                        title = objetivoAprendizaje.Descripcion,
                        folder = true,
                        children = new List<ObjetivoAprendizaje>()
                    };

                    foreach (RecursoCurricular.BaseCurricular.Indicador indicador in RecursoCurricular.BaseCurricular.Indicador.GetAll(objetivoAprendizaje))
                    {
                        RecursoCurricular.BaseCurricular.UnidadIndicador unidadIndicador = RecursoCurricular.BaseCurricular.UnidadIndicador.Get(tipoEducacionCodigo, anioNumero, gradoCodigo, sectorId, unidadId, indicador.EjeId, indicador.ObjetivoAprendizajeId, indicador.Id);

                        o.children.Add(new ObjetivoAprendizaje
                        {
                            key = string.Format("{0}{1}{2}", indicador.EjeId, indicador.ObjetivoAprendizajeId, indicador.Id),
                            title = indicador.Descripcion,
                            selected = unidadIndicador != null,
                            folder = false
                        });
                    }

                    e.children.Add(o);
                }

                lista.Add(e);
            }

            return lista;
        }
    }
}