using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei.TypeParsers
{
    public class SingleParser : IParser<float>
    {
        public float Parse(IDataReader reader, TypeInfo info, List<Template> templates)
        {
            return reader.ReadSingle();
        }

        object IParser.Parse(IDataReader reader, TypeInfo info, List<Template> templates) => Parse(reader, info, templates);
    }
}