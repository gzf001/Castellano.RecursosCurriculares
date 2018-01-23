using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models
{
    public class ObjetivoAprendizaje : RecursoCurricular.BaseCurricular.ObjetivoAprendizaje
    {
        public string Accion
        {
            get;
            set;
        }

        public class ObjetivosAprendizaje
        {
            public List<ObjetivoAprendizaje> data
            {
                get;
                set;
            }
        }
    }
}