using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models
{
    public class Habilidad : RecursoCurricular.BaseCurricular.Habilidad
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

        public string SectorNombre
        {
            get;
            set;
        }

        public class Habilidades
        {
            public List<Habilidad> data
            {
                get;
                set;
            }
        }
    }
}