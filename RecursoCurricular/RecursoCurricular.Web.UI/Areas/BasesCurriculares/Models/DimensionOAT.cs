using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models
{
    public class DimensionOAT : RecursoCurricular.BaseCurricular.DimensionOAT
    {
        public string Accion
        {
            get;
            set;
        }

        public class DimensionesOAT
        {
            public List<DimensionOAT> data
            {
                get;
                set;
            }
        }
    }
}