using System;
using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei.TypeParsers
{
    public class UserDefinedParser : IParser
    {
        public object Parse(IDataReader reader, TypeInfo info, List<Template> templates)
        {
            var templateName = info.TemplateName;
            var dataLength = reader.ReadInt32();

            if (dataLength < 0) return null;

            var parseStart = reader.PositionInt;
            var obj = reader.Parse(templates, templateName);
            var parseEnd = reader.PositionInt;

            var parseLength = parseEnd - parseStart;

            if (parseLength < dataLength)
            {
                var extraString = reader.ReadString();
                obj = new
                {
                    obj,
                    extraString,
                };
                //var bytes = reader.ReadBytes(dataLength - parseLength);

                //foreach (var thisByte in bytes)
                //{
                //    Console.WriteLine(Convert.ToChar(thisByte));
                //}
                Console.WriteLine($"{extraString} was not parsed");
            }

            var parseNewEnd = reader.PositionInt;
            parseLength = parseNewEnd - parseStart;

            if (parseLength != dataLength)
                throw new InvalidOperationException(
                //Console.Error.WriteLine(
                    $"Failed to parse object: Template name: {templateName} parsed {parseLength - dataLength} more than expected.");

            return obj;
        }
    }
}