using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Models
{
    public class Unidad : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.BaseCurricular.Unidad Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.Unidad> Unidades
        {
            get;
            set;
        }
    }
}