using System.Collections.Generic;
using SheepReaper.GameSaves.Interfaces;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;

namespace SheepReaper.GameSaves.TypeParsers
{
    public class BooleanParser : IParser<bool>
    {
        public bool Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates)
        {
            return reader.ReadByte() > 0;
        }

        object IParser.Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates)
        {
            return Parse(reader, info, templates);
        }
    }
}