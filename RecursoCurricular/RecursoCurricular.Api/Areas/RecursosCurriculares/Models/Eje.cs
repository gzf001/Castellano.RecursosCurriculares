using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RecursoCurricular.Api.Areas.RecursosCurriculares.Models
{
    [Serializable]
    [DataContract]
    public class Eje : RecursoCurricular.Api.Models.Result
    {
        [DataMember]
        public RecursoCurricular.RecursosCurriculares.Eje Item
        {
            get;
            set;
        }

        [DataMember]
        public List<RecursoCurricular.RecursosCurriculares.Eje> Lista
        {
            get;
            set;
        }
    }
}