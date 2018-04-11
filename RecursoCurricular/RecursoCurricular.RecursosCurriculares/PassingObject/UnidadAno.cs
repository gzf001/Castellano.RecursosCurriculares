using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursoCurricular.RecursosCurriculares.PassingObject
{
    public class UnidadAno : RecursoCurricular.PassingObject.BaseObject
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

        public Guid UnidadId
        {
            get;
            set;
        }

        public int AnoNumero
        {
            get;
            set;
        }

        public Unidad Unidad
        {
            get;
            set;
        }
    }
}