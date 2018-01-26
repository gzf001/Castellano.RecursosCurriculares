using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models
{
    public class SubHabilidad : RecursoCurricular.BaseCurricular.SubHabilidad
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

        public string HabilidadNombre
        {
            get;
            set;
        }

        public class SubHabilidades
        {
            public List<SubHabilidad> data
            {
                get;
                set;
            }
        }
    }
}