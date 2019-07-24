using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei.TypeParsers
{
    public class StringParser : IParser<string>
    {
        public string Parse(IDataReader reader, TypeInfo info, List<Template> templates)
        {
            return reader.ReadString();
        }

        object IParser.Parse(IDataReader reader, TypeInfo info, List<Template> templates)
        {
            return Parse(reader, info, templates);
        }
    }
}