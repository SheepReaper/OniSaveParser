using System.Collections.Generic;
using SheepReaper.GameSaves.Interfaces;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;

namespace SheepReaper.GameSaves.TypeParsers
{
    public class DictionaryParser : IParser<object[][]>
    {
        public object[][] Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates)
        {
            
            var keyType = info.SubTypes[0];
            var valueType = info.SubTypes[1];
            var dataLength = reader.ReadInt32();
            var elementCount = reader.ReadInt32();
            if (elementCount >= 0)
            {
                var pairs = new object[elementCount][];
                for (var i = 0; i < elementCount; i++)
                {
                    pairs[i] = new object[2];
                    pairs[i][1] = reader.Parse(templates, valueType);
                }

                for (var i = 0; i < elementCount; i++)
                {
                    pairs[i][0] = reader.Parse(templates, keyType);
                }
                return pairs;
            }
            else
            {
                return null;
            }
        }

        object IParser.Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates)
        {
            return Parse(reader, info, templates);
        }
    }
}