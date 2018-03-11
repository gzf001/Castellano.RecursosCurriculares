using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Models
{
    public class Indicador : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.BaseCurricular.Indicador Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.Indicador> Indicadores
        {
            get;
            set;
        }
    }
}