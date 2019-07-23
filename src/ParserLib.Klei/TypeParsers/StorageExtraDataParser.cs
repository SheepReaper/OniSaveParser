using System.Collections.Generic;
using SheepReaper.GameSaves.Interfaces;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;

namespace SheepReaper.GameSaves.TypeParsers
{
    public class StorageExtraDataParser : IExtraDataParser
    {
        public List<object> Parse(IKleiDataReader reader, List<Template> templates)
        {
            var itemCount = reader.ReadInt32();
            var items = new List<object>(itemCount);

            for (var i = 0; i < itemCount; i++)
            {
                var name = reader.ValidateDotNetIdentifierName(reader.ReadKleiString());
                var gameObject = reader.ParseGameObject();
                items[i] = new
                {
                    name,
                    gameObject,
                };
            }
            return items;
        }
    }
}