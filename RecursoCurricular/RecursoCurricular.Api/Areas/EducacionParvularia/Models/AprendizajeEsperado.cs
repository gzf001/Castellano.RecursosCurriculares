using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.EducacionParvularia.Models
{
    public class AprendizajeEsperado : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.AprendizajeEsperadoParvulo> AprendizajesEsperadosParvulo
        {
            get;
            set;
        }
    }
}