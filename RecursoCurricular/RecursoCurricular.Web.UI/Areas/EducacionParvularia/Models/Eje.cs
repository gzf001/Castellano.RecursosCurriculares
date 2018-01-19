using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models
{
    public class Eje : RecursoCurricular.BaseCurricular.EjeParvulo
    {
        public string Accion
        {
            get;
            set;
        }

        public class Ejes
        {
            public List<Eje> data
            {
                get;
                set;
            }
        }
    }
}