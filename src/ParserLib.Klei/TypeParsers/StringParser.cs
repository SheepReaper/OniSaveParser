using System.Collections.Generic;
using SheepReaper.GameSaves.Interfaces;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;

namespace SheepReaper.GameSaves.TypeParsers
{
    public class StringParser : IParser<string>
    {
        public string Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates)
        {
            return reader.ReadKleiString();
        }

        object IParser.Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates)
        {
            return Parse(reader, info, templates);
        }
    }
}