using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecursoCurricular.Web.UI.Areas.RecursosCurriculares.Models
{
    public class AprendizajeEsperado : RecursoCurricular.RecursosCurriculares.Aprendizaje
    {
        public int CMO
        {
            get;
            set;
        }

        public int OFV
        {
            get;
            set;
        }

        public string Accion
        {
            get;
            set;
        }

        public string TipoEducacionNombre
        {
            get;
            set;
        }

        public string GradoNombre
        {
            get;
            set;
        }

        public string SectorNombre
        {
            get;
            set;
        }

        public string DetalleIndicadores
        {
            get;
            set;
        }

        public Indicador IndicadorItem
        {
            get;
            set;
        }

        public List<RecursoCurricular.RecursosCurriculares.Contenido> Contenidos
        {
            get;
            set;
        }

        public List<RecursoCurricular.RecursosCurriculares.ObjetivoVertical> ObjetivosVerticales
        {
            get;
            set;
        }

        public class Indicador : RecursoCurricular.RecursosCurriculares.AprendizajeIndicador
        {
            public string Accion
            {
                get;
                set;
            }

            [Display(Name = "Habilidad Taxonómica:")]
            public new string Categoria
            {
                get;
                set;
            }
        }

        public class AprendizajesEsperados
        {
            public List<AprendizajeEsperado> data
            {
                get;
                set;
            }
        }

        public class Indicadores
        {
            public List<Indicador> data
            {
                get;
                set;
            }
        }
    }
}