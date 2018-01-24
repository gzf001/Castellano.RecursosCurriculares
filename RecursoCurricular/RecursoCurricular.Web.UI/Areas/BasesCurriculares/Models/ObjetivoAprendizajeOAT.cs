using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models
{
    public class ObjetivoAprendizajeOAT : RecursoCurricular.BaseCurricular.ObjetivoAprendizajeTransversal
    {
        public string Accion
        {
            get;
            set;
        }

        public class ObjetivosAprendizajeOAT
        {
            public List<ObjetivoAprendizajeOAT> data
            {
                get;
                set;
            }
        }
    }
}