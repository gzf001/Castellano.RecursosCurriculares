using System;
using System.Collections.Generic;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular.PassingObject
{
    public class ObjetivoAprendizaje : RecursoCurricular.PassingObject.BaseObject
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

        public List<RecursoCurricular.BaseCurricular.PassingObject.Indicador> Indicadores
        {
            get;
            set;
        }
    }
}