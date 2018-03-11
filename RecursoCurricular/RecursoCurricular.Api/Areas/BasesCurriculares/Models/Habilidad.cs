using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Models
{
    public class Habilidad : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.BaseCurricular.Habilidad Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.Habilidad> Habilidades
        {
            get;
            set;
        }
    }
}