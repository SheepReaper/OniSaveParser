using System.Collections.Generic;

namespace SheepReaper.GameSaves.Model.SaveFile.TypeTemplates
{
    public class TypeInfoElement
    {
        public SerializationTypeCode InferredType { get; internal set; }
        public int InferredTypeInt { get; internal set; }
        public SerializationTypeCode Info { get; set; }
        public List<TypeInfoElement> SubTypes { get; set; }

        public string TemplateName { get; set; }

        //public SerializationTypeCode Inferred { get; set; }
        public int TypeCodeInt { get; set; }
    }
}