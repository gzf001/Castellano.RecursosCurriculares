using System;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular.PassingObject
{
    public class UnidadIndicador : RecursoCurricular.PassingObject.BaseObject
    {
        public int TipoEducacionCodigo
        {
            get;
            set;
        }

        public int AnoNumero
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

        public Guid EjeId
        {
            get;
            set;
        }

        public Guid ObjetivoAprendizajeId
        {
            get;
            set;
        }

        public Guid IndicadorId
        {
            get;
            set;
        }

        public int Orden
        {
            get;
            set;
        }
    }
}