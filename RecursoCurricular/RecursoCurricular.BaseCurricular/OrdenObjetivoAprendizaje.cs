using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursoCurricular.BaseCurricular
{
    public abstract class OrdenObjetivoAprendizaje
    {
        public int TipoEducacionCodigo
        {
            get;
            set;
        }

        public int AnioNumero
        {
            get;
            set;
        }

        public int GradoCodigo
        {
            get;
            set;
        }

        public Guid SectorId
        {
            get;
            set;
        }

        public Guid UnidadId
        {
            get;
            set;
        }

        public List<Orden> Ordenes
        {
            get;
            set;
        }

        public static void Ordenar(OrdenObjetivoAprendizaje ordenObjetivoAprendizaje)
        {
            using (RecursoCurricular.BaseCurricular.Context context = new RecursoCurricular.BaseCurricular.Context())
            {
                int i = 1;
                int j = 1;

                foreach (RecursoCurricular.BaseCurricular.OrdenObjetivoAprendizaje.Orden orden in ordenObjetivoAprendizaje.Ordenes)
                {
                    RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje unidadObjetivoAprendizaje = RecursoCurricular.BaseCurricular.UnidadObjetivoAprendizaje.Get(ordenObjetivoAprendizaje.TipoEducacionCodigo, ordenObjetivoAprendizaje.AnioNumero, ordenObjetivoAprendizaje.GradoCodigo, ordenObjetivoAprendizaje.SectorId, ordenObjetivoAprendizaje.UnidadId, orden.EjeId, orden.ObjetivoAprendizajeId);

                    unidadObjetivoAprendizaje.Orden = i;
                    unidadObjetivoAprendizaje.Save(context);

                    foreach (Guid indicadorId in orden.Indicadores)
                    {
                        RecursoCurricular.BaseCurricular.UnidadIndicador unidadIndicador = RecursoCurricular.BaseCurricular.UnidadIndicador.Get(ordenObjetivoAprendizaje.TipoEducacionCodigo, ordenObjetivoAprendizaje.AnioNumero, ordenObjetivoAprendizaje.GradoCodigo, ordenObjetivoAprendizaje.SectorId, ordenObjetivoAprendizaje.UnidadId, orden.EjeId, orden.ObjetivoAprendizajeId, indicadorId);

                        unidadIndicador.Orden = j;
                        unidadIndicador.Save(context);

                        j++;
                    }

                    i++;
                }

                context.SubmitChanges();
            }
        }

        public class Orden
        {
            public Guid EjeId
            {
                get;
                set;
            }

            public Guid ObjetivoAprendizajeId
            {
                get;
                set;
            }

            public List<Guid> Indicadores
            {
                get;
                set;
            }
        }
    }
}
