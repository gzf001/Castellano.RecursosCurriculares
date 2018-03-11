using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.EducacionParvularia.Models
{
    public class Parametro : RecursoCurricular.Api.Models.Parametro
    {
        public int AmbitoExperienciaAprendizajeCodigo
        {
            get;
            set;
        }

        public Guid NucleoAprendizajeId
        {
            get;
            set;
        }

        public int CicloCodigo
        {
            get;
            set;
        }

        public Guid Id
        {
            get;
            set;
        }
    }
}