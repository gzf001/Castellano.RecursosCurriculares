using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursoCurricular.RecursosCurriculares.PassingObject
{
    public class ContenidoAno : RecursoCurricular.PassingObject.BaseObject
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

        public int AnoNumero
        {
            get;
            set;
        }

        public Contenido Contenido
        {
            get;
            set;
        }
    }
}
