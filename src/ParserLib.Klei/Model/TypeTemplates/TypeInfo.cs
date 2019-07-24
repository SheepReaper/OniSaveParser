using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei
{
    public class TypeInfo
    {
        public SerializationTypeCode InferredType { get; internal set; }
        public int InferredTypeInt { get; internal set; }
        public SerializationTypeCode Info { get; set; }
        public List<TypeInfo> SubTypes { get; set; }

        public string TemplateName { get; set; }

        //public SerializationTypeCode Inferred { get; set; }
        public int TypeCodeInt { get; set; }
    }
}