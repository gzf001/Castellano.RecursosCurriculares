using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.EducacionParvularia.Models
{
    public class Eje : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.BaseCurricular.EjeParvulo Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.EjeParvulo> EjesParvulo
        {
            get;
            set;
        }
    }
}