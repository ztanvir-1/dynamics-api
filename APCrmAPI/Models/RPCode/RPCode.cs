using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace APCrmAPI.Models.RPCode
{
    [DataContract]
    public class RPCode
    {
        [DataMember(Name = "guid")]
        public string Guid { get; set; }
        
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "partnerUrl")]
        public string PartnerUrl { get; set; }
    }
}