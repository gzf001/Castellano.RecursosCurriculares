using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.Administracion.Models
{
    public class RolAccion : RecursoCurricular.Membresia.RolAccion
    {
        public RolAccion() : base()
        {
            this.SelectedAccion = new List<int>();

            this.Acciones = new List<SelectListItem>();
        }

        public string AccionNombre
        {
            get;
            set;
        }

        public string MenuItemNombre
        {
            get;
            set;
        }

        public List<int> SelectedAccion
        {
            get;
            set;
        }

        public List<SelectListItem> Acciones
        {
            get;
            set;
        }

        public class RolAcciones
        {
            public RolAcciones()
            {
                this.data = new List<RolAccion>();
            }

            public List<RolAccion> data
            {
                get;
                set;
            }
        }
    }
}