using System;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular.PassingObject
{
    public class NucleoAprendizaje : RecursoCurricular.PassingObject.BaseObject
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