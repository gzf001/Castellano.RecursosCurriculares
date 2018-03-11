using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.EducacionParvularia.Models
{
    public class Nucleo : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.BaseCurricular.NucleoAprendizaje Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.NucleoAprendizaje> NucleosAprendizaje
        {
            get;
            set;
        }
    }
}