using System;
using System.Collections.Generic;
using System.Data.Linq;
namespace RecursoCurricular.BaseCurricular.PassingObject
{
    public class Unidad : RecursoCurricular.PassingObject.BaseObject
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

        public string Proposito
        {
            get;
            set;
        }

        public string ConocimientoPrevio
        {
            get;
            set;
        }

        public string PalabraClave
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

        public List<RecursoCurricular.BaseCurricular.PassingObject.Habilidad> Habilidades
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.PassingObject.Eje> Ejes
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.PassingObject.Actitud> Actitudes
        {
            get;
            set;
        }

        public List<RecursoCurricular.BaseCurricular.PassingObject.Conocimiento> Conocimientos
        {
            get;
            set;
        }
    }
}