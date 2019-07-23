using System.Collections.Generic;
using SheepReaper.GameSaves.Interfaces;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;

namespace SheepReaper.GameSaves.TypeParsers
{
    public class IntegerParser : IParser<int>
    {
        public int Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates)
        {
            return reader.ReadInt32();
        }

        object IParser.Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates)
        {
            return Parse(reader, info, templates);
        }
    }
}