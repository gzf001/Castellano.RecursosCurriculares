using System;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular.PassingObject
{
    public class Conocimiento : RecursoCurricular.PassingObject.BaseObject
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

        public Guid SectorId
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