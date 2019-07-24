using System.Collections.Generic;
using System.Numerics;

namespace SheepReaper.GameSaves.Klei.TypeParsers
{
    public class Vector2Parser : IParser<Vector2>
    {
        public Vector2 Parse(IDataReader reader, TypeInfo info, List<Template> templates) => reader.ReadVector2();

        object IParser.Parse(IDataReader reader, TypeInfo info, List<Template> templates) => Parse(reader, info, templates);
    }
}