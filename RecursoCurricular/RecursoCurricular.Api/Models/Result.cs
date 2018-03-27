using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RecursoCurricular.Api.Models
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public string Status
        {
            get;
            set;
        }

        [DataMember]
        public string Message
        {
            get;
            set;
        }
    }
}