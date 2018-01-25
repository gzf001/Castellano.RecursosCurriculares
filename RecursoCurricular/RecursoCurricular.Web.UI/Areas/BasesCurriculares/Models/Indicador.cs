using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models
{
    public class Indicador : RecursoCurricular.BaseCurricular.Indicador
    {
        public string Accion
        {
            get;
            set;
        }

        public class Indicadores
        {
            public List<Indicador> data
            {
                get;
                set;
            }
        }

        public class Objetivo : RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models.ObjetivoAprendizaje
        {
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

            public string EjeNombre
            {
                get;
                set;
            }

            public string Indicadores
            {
                get;
                set;
            }
        }
    }
}