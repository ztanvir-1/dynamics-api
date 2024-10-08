using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace APCrmAPI.Models.RPCode
{
    [DataContract]
    public class Note_RpCode
    {
        [DataMember(Name = "guid")]
        public string Guid { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "objectId")]
        public string ObjectId { get; set; }

        [DataMember(Name = "mimetype")]
        public string Mimetype { get; set; }

        [DataMember(Name = "documentBody")]
        public string DocumentBody { get; set; }

        [DataMember(Name = "fileName")]
        public string FileName { get; set; }
        
        [DataMember(Name = "partnerUrl")]
        public string PartnerUrl { get; set; }
        
        [DataMember(Name = "partnerName")]
        public string PartnerName { get; set; }
    }
}