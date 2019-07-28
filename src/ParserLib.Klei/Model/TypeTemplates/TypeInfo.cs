using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei
{
    public class TypeInfo
    {
        public SerializationTypeCode InferredType { get; internal set; }
        public int InferredTypeInt => (int)InferredType;
        public SerializationTypeCode Info { get; set; }
        public bool TypeMatch => TypeCodeInt == InferredTypeInt;
        public List<TypeInfo> SubTypes { get; set; }

        public string TemplateName { get; set; }
        public int TypeCodeInt => (int)Info;
        public bool HasSubTypes => SubTypes.Count > 0;
        public bool CorrelatesSubTypesAndNumber => !TypeMatch && HasSubTypes;
    }
}