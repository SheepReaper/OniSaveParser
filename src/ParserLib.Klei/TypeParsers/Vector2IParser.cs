using System.Collections.Generic;
using SheepReaper.GameSaves.Interfaces;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;

namespace SheepReaper.GameSaves.TypeParsers
{
    public class Vector2IParser : IParser<Vector2I>
    {
        public Vector2I Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates)
        {
            return reader.ReadVector2I();
        }

        object IParser.Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates) => Parse(reader, info, templates);
    }
}