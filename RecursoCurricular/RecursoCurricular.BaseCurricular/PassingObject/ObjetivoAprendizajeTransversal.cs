using System;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular.PassingObject
{
    public class ObjetivoAprendizajeTransversal : RecursoCurricular.PassingObject.BaseObject
    {
        public Guid DimensionOATId
        {
            get;
            set;
        }

        public int AnoNumero
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

        public string Descripcion
        {
            get;
            set;
        }
    }
}