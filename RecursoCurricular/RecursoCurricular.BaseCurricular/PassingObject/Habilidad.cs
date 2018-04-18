using System;
using System.Collections.Generic;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular.PassingObject
{
    public class Habilidad : RecursoCurricular.PassingObject.BaseObject
    {
        public Guid Id
        {
            get;
            set;
        }

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

        public List<RecursoCurricular.BaseCurricular.PassingObject.SubHabilidad> SubHabilidades
        {
            get;
            set;
        }
    }
}