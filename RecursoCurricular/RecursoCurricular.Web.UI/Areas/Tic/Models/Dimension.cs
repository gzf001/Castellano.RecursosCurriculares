using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.Tic.Models
{
    public class Dimension : RecursoCurricular.DimensionHabilidadTic
    {
        public string Accion
        {
            get;
            set;
        }

        public class DimensionHabilidadTices
        {
            public List<Dimension> data
            {
                get;
                set;
            }
        }
    }
}