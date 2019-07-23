using SheepReaper.GameSaves.Interfaces;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;
using System.Collections.Generic;

namespace SheepReaper.GameSaves.TypeParsers
{
    public class IntegerParser : IParser<int>
    {
        public int Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates) => reader.ReadInt32();

        object IParser.Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates) => Parse(reader, info, templates);
    }
}