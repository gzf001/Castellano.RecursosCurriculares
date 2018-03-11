using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Api.Areas.BasesCurriculares.Models
{
    public class Conocimiento : RecursoCurricular.Api.Models.Result
    {
        public RecursoCurricular.BaseCurricular.Conocimiento Item
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.Conocimiento> Conocimientos
        {
            get;
            set;
        }
    }
}