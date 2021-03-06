using System;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular.PassingObject
{
    public class AprendizajeEsperadoParvulo : RecursoCurricular.PassingObject.BaseObject
    {
        public int AnoNumero
        {
            get;
            set;
        }

        public int AmbitoExperienciaAprendizajeCodigo
        {
            get;
            set;
        }

        public Guid NucleoAprendizajeId
        {
            get;
            set;
        }

        public int CicloCodigo
        {
            get;
            set;
        }

        public Guid Id
        {
            get;
            set;
        }

        public Nullable<Guid> EjeParvuloId
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