using System.Collections.Generic;
using SheepReaper.GameSaves.Interfaces;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;

namespace SheepReaper.GameSaves.TypeParsers
{
    public class SingleParser : IParser<float>
    {
        public float Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates)
        {
            return reader.ReadSingle();
        }

        object IParser.Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates) => Parse(reader, info, templates);
    }
}