using SheepReaper.GameSaves.Interfaces;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;
using System.Collections.Generic;

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
                var name = reader.ValidateDotNetIdentifierName(reader.ReadString());
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