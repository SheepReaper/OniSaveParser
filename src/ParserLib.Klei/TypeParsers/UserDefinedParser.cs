using SheepReaper.GameSaves.Interfaces;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;
using System;
using System.Collections.Generic;

namespace SheepReaper.GameSaves.TypeParsers
{
    public class UserDefinedParser : IParser
    {
        public object Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates)
        {
            var templateName = info.TemplateName;
            var dataLength = reader.ReadInt32();

            if (dataLength < 0) return null;

            var parseStart = reader.Position;
            var obj = reader.Parse(templates, templateName);
            var parseEnd = reader.Position;

            var parseLength = parseEnd - parseStart;

            if(parseLength<dataLength)
            {
                var bytes = reader.ReadBytes(dataLength - parseLength);

                foreach (var thisByte in bytes)
                {
                    Console.WriteLine(Convert.ToChar(thisByte));
                }
            }

            if (parseLength != dataLength)
                throw new InvalidOperationException(
                //Console.Error.WriteLine(
                    $"Failed to parse object: Template name: {templateName} parsed {parseLength - dataLength} more than expected.");

            return obj;
        }
    }
}