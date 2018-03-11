using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Models
{
    public class SubHabilidad : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.BaseCurricular.SubHabilidad Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.SubHabilidad> SubHabilidades
        {
            get;
            set;
        }
    }
}