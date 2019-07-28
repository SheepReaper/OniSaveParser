using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SheepReaper.GameSaves.Klei
{
    [DataContract]
    public class Template
    {
        [DataMember(Order = 1)]
        public List<TemplateMember> Fields { get; set; }

        [DataMember(Order = 0)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public List<TemplateMember> Properties;
    }
}