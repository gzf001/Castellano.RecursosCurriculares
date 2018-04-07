using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Models.PassingObject
{
    [Serializable]
    [DataContract]
    public class EjeAno
    {
        [DataMember]
        public Int32 TipoEducacionCodigo
        {
            get;
            set;
        }

        [DataMember]
        public Guid SectorId
        {
            get;
            set;
        }

        [DataMember]
        public Guid EjeId
        {
            get;
            set;
        }

        [DataMember]
        public Int32 AnoNumero
        {
            get;
            set;
        }

        [DataMember]
        public Eje Eje
        {
            get;
            set;
        }
    }
}