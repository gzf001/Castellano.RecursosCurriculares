using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursoCurricular.RecursosCurriculares.PassingObject
{
    public class AprendizajeContenido
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

        public Guid EjeId
        {
            get;
            set;
        }

        public Guid ContenidoId
        {
            get;
            set;
        }
    }
}