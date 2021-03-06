using System;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular.PassingObject
{
    public class Indicador : RecursoCurricular.PassingObject.BaseObject
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

        public string Descripcion
        {
            get;
            set;
        }
    }
}