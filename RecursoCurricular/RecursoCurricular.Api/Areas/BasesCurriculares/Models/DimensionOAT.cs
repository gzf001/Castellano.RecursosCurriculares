using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Models
{
    public class DimensionOAT : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.BaseCurricular.DimensionOAT Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.DimensionOAT> DimensionesOAT
        {
            get;
            set;
        }
    }
}