using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei.TypeParsers
{
    public class StorageExtraDataParser : IExtraDataParser
    {
        public List<object> Parse(IDataReader reader, List<Template> templates)
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
                    gameObject
                };
            }
            return items;
        }
    }
}