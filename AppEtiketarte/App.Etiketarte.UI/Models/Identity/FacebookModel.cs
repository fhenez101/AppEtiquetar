using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace App.Etiketarte.UI.Models.Identity
{
    [DataContract]
    public class FacebookModel : IUser
    {
        [DataMember(Name = "id")]
        public string FacebookId { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }
        [DataMember(Name = "last_name")]
        public string LastName { get; set; }
        [DataMember(Name = "gender")]
        public string Gender { get; set; }
        [DataMember(Name = "locale")]
        public string Locale { get; set; }
        [DataMember(Name = "link")]
        public string Link { get; set; }
        [DataMember(Name = "timezone")]
        public int Timezone { get; set; }
        [DataMember(Name = "picture")]
        public Picture Picture { get; set; }

        [IgnoreDataMember]
        [XmlIgnore]
        public string Id { get; set; }
        [IgnoreDataMember]
        [XmlIgnore]
        public string UserName { get; set; }

        [IgnoreDataMember]
        [XmlIgnore]
        public bool IsInSession { get; set; }

        [IgnoreDataMember]
        [XmlIgnore]
        public bool IsAdmin { get; set; }

        public FacebookModel()
        {
            Id = Guid.NewGuid().ToString();
            UserName = string.Empty;
        }
    }

    [DataContract]
    public class Data
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }

    [DataContract]
    public class Picture
    {
        [DataMember(Name = "data")]
        public Data Data { get; set; }
    }
}