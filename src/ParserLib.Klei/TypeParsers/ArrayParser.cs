using System;
using System.Collections.Generic;
using SheepReaper.GameSaves.Interfaces;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;

namespace SheepReaper.GameSaves.TypeParsers
{
    public class ArrayParser : IParser
    {
        public object Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates)
        {
            var elementType = info.SubTypes[0];

            var mysteryData = reader.ReadInt32();

            var length = reader.ReadInt32();

            if (length == -1)
            {
                return null;
            }
            else if (length >= 0)
            {
                var typeCode = elementType.Info.GetImpliedType();
                if (typeCode == SerializationTypeCode.Byte)
                {
                    var data = reader.ReadBytes(length);
                    return data;
                }
                else if (elementType.Info.IsValueType())
                {
                    if (typeCode != SerializationTypeCode.UserDefined)
                    {
                        //while (true)
                        //{
                        //    Console.ReadKey();
                        //    Console.WriteLine(reader.ReadByte());
                        //}
                        throw new InvalidOperationException($"Type {typeCode} cannot be parsed as value-type.");
                    }
                    var elements = new object[length];
                    var typeName = elementType.TemplateName;

                    for (var i = 0; i < length; i++)
                    {
                        elements[i] = reader.Parse(templates, typeName);
                    }

                    return elements;
                }
                else
                {
                    var elements = new object[length];
                    for (var i = 0; i < length; i++)
                    {
                        elements[i] = reader.Parse(templates, elementType);
                    }
                    return elements;
                }
            }
            else
            {
                throw new InvalidOperationException($"Failed to parse array: Invalid lenght: {length}.");
            }
        }
    }
}