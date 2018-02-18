using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models
{
    public class Eje : RecursoCurricular.RecursosCurriculares.Eje
    {
        public Eje() : base()
        {
            this.SelectedTipoEducacion = new List<int>();

            this.TiposEducacion = new List<SelectListItem>();
        }

        public string Accion
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Debe seleccionar al menos un tipo de educación")]
        public List<int> SelectedTipoEducacion
        {
            get;
            set;
        }

        public List<SelectListItem> TiposEducacion
        {
            get;
            set;
        }

        public string TipoEducacionNombre
        {
            get;
            set;
        }

        public new class Ejes
        {
            public List<Eje> data
            {
                get;
                set;
            }
        }
    }
}