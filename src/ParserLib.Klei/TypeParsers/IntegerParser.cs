using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei.TypeParsers
{
    public class IntegerParser : IParser<int>
    {
        public int Parse(IDataReader reader, TypeInfo info, List<Template> templates) => reader.ReadInt32();

        object IParser.Parse(IDataReader reader, TypeInfo info, List<Template> templates) => Parse(reader, info, templates);
    }
}