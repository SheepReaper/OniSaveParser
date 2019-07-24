using SheepReaper.GameSaves.Interfaces;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;
using System.Collections.Generic;
using System.Numerics;

namespace SheepReaper.GameSaves.TypeParsers
{
    public class Vector3Parser : IParser<Vector3>
    {
        public Vector3 Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates) => reader.ReadVector3();

        object IParser.Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates) => Parse(reader, info, templates);
    }
}