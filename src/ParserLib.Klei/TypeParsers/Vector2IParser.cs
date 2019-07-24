using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei.TypeParsers
{
    public class Vector2IParser : IParser<Vector2I>
    {
        public Vector2I Parse(IDataReader reader, TypeInfo info, List<Template> templates)
        {
            return reader.ReadVector2I();
        }

        object IParser.Parse(IDataReader reader, TypeInfo info, List<Template> templates) => Parse(reader, info, templates);
    }
}