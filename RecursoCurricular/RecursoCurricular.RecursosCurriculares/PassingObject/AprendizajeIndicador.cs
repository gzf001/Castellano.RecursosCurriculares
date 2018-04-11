using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursoCurricular.RecursosCurriculares.PassingObject
{
    public class AprendizajeIndicador
    {
        public int TipoEducacionCodigo
        {
            get;
            set;
        }

        public int GradoCodigo
        {
            get;
            set;
        }

        public Guid SectorId
        {
            get;
            set;
        }

        public Guid AprendizajeId
        {
            get;
            set;
        }

        public Guid Id
        {
            get;
            set;
        }

        public int AmbitoCodigo
        {
            get;
            set;
        }

        public Nullable<Guid> SostenedorId
        {
            get;
            set;
        }

        public Nullable<Guid> EstablecimientoId
        {
            get;
            set;
        }

        public Nullable<int> CategoriaCodigo
        {
            get;
            set;
        }

        public int Numero
        {
            get;
            set;
        }

        public string Descripcion
        {
            get;
            set;
        }
    }
}