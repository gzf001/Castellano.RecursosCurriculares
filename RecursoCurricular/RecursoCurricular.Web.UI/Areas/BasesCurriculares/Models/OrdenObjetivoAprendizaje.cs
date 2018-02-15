using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models
{
    public class OrdenObjetivoAprendizaje
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

        public class Orden
        {
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