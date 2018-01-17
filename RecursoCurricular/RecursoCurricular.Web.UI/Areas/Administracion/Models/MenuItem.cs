using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.Administracion.Models
{
    public class MenuItem : RecursoCurricular.Membresia.MenuItem
    {
        [Display(Name = "Aplicación:")]
        public string NombreAplicacion
        {
            get;
            set;
        }
    }
}