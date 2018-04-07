using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursoCurricular.RecursosCurriculares.PassingObject
{
    class EjeAno : RecursoCurricular.PassingObject.BaseObject
    {
        public Int32 TipoEducacionCodigo
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

        public Int32 AnoNumero
        {
            get;
            set;
        }

        public Eje Eje
        {
            get;
            set;
        }
    }
}