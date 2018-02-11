using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Helpers.JsonClass
{
    public abstract class Base
    {
        public string key
        {
            get;
            set;
        }

        public string title
        {
            get;
            set;
        }

        public bool folder
        {
            get;
            set;
        }

        public bool selected
        {
            get;
            set;
        }
    }
}