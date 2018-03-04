using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models
{
    public class ObjetivoTransversal : RecursoCurricular.RecursosCurriculares.ObjetivoTransversal
    {
        public string Accion
        {
            get;
            set;
        }

        public string TipoEducacionNombre
        {
            get;
            set;
        }

        public string GradoNombre
        {
            get;
            set;
        }

        public string SectorNombre
        {
            get;
            set;
        }

        public string UnidadNombre
        {
            get;
            set;
        }

        public string DetalleIndicadores
        {
            get;
            set;
        }

        public Indicador IndicadorItem
        {
            get;
            set;
        }

        public class Indicador : RecursoCurricular.RecursosCurriculares.ObjetivoTransversalIndicador
        {
            public string Accion
            {
                get;
                set;
            }
        }

        public class ObjetivoTransversales
        {
            public List<ObjetivoTransversal> data
            {
                get;
                set;
            }
        }

        public class Indicadores
        {
            public List<Indicador> data
            {
                get;
                set;
            }
        }
    }
}