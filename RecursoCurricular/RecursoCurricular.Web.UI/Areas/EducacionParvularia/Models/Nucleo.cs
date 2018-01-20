using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models
{
    public class Nucleo : RecursoCurricular.BaseCurricular.NucleoAprendizaje
    {
        public string Accion
        {
            get;
            set;
        }

        public new class Nucleos
        {
            public List<Nucleo> data
            {
                get;
                set;
            }
        }
    }
}