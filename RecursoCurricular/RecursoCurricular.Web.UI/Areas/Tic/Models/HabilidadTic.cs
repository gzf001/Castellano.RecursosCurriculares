using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.Tic.Models
{
    public class HabilidadTic : RecursoCurricular.HabilidadTic
    {
        public string Accion
        {
            get;
            set;
        }

        public class HabilidadTices
        {
            public List<HabilidadTic> data
            {
                get;
                set;
            }
        }
    }
}