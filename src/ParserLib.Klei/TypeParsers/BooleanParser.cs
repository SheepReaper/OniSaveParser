using SheepReaper.GameSaves.Interfaces;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;
using System.Collections.Generic;

namespace SheepReaper.GameSaves.TypeParsers
{
    public class BooleanParser : IParser<bool>
    {
        public bool Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates) => reader.ReadBoolean();

        object IParser.Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates) => Parse(reader, info, templates);
    }
}