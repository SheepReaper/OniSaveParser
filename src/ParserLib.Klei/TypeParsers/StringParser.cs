using SheepReaper.GameSaves.Interfaces;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;
using System.Collections.Generic;

namespace SheepReaper.GameSaves.TypeParsers
{
    public class StringParser : IParser<string>
    {
        public string Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates)
        {
            return reader.ReadString();
        }

        object IParser.Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates)
        {
            return Parse(reader, info, templates);
        }
    }
}