using System.Collections.Generic;
using System.Numerics;

namespace SheepReaper.GameSaves.Klei.TypeParsers
{
    public class Vector3Parser : IParser<Vector3>
    {
        public Vector3 Parse(IDataReader reader, TypeInfo info, List<Template> templates) => reader.ReadVector3();

        object IParser.Parse(IDataReader reader, TypeInfo info, List<Template> templates) => Parse(reader, info, templates);
    }
}