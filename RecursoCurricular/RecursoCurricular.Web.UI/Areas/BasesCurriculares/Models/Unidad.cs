using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.BasesCurriculares.Models
{
    public class Unidad : RecursoCurricular.BaseCurricular.Unidad
    {
        public List<string> SubHabilidadesId
        {
            get;
            set;
        }

        public List<string> IndicadoresId
        {
            get;
            set;
        }

        public List<Guid> ActitudesId
        {
            get;
            set;
        }

        public List<Guid> ConocimientosId
        {
            get;
            set;
        }

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

        public class Unidades
        {
            public List<Unidad> data
            {
                get;
                set;
            }
        }
    }
}