using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.EducacionParvularia.Models
{
    public class PrincipioPedagogico : RecursoCurricular.BaseCurricular.PrincipioPedagogico
    {
        public class PrincipiosPedagogicos
        {
            public List<PrincipioPedagogico> data
            {
                get;
                set;
            }
        }
    }
}