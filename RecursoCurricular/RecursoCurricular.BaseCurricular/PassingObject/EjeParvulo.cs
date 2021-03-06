using System;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular.PassingObject
{
    public class EjeParvulo : RecursoCurricular.PassingObject.BaseObject
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

        public Guid NucleoId
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

        public int Numero
        {
            get;
            set;
        }

        public string Nombre
        {
            get;
            set;
        }
    }
}