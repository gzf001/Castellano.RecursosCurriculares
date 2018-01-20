using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models
{
    public class AprendizajeEsperado : RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo
    {
        public string EjeNombre
        {
            get;
            set;
        }

        public string IdEje
        {
            get;
            set;
        }

        public string Accion
        {
            get;
            set;
        }

        public class AprendizajesEsperados
        {
            public List<AprendizajeEsperado> data
            {
                get;
                set;
            }
        }
    }
}