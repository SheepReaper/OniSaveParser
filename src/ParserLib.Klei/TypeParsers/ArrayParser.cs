using System;
using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei.TypeParsers
{
    public class ArrayParser : IParser
    {
        public object Parse(IDataReader reader, TypeInfo info, List<Template> templates)
        {
            var elementType = info.SubTypes[0];

            _ = reader.ReadInt32(); // TODO: Mystery Data

            var length = reader.ReadInt32();

            if (length == -1)
                return null;

            if (length < 0)
                throw new InvalidOperationException($"Failed to parse array: Invalid length: {length}.");

            var typeCode = elementType.Info.GetImpliedType();

            if (typeCode == SerializationTypeCode.Byte)
            {
                var data = reader.ReadBytes(length);

                return data;
            }

            if (elementType.Info.IsValueType())
            {
                if (typeCode != SerializationTypeCode.UserDefined)
                    throw new InvalidOperationException($"Type {typeCode} cannot be parsed as value-type.");

                var elements = new object[length];
                var typeName = elementType.TemplateName;

                for (var i = 0; i < length; i++) elements[i] = reader.Parse(templates, typeName);

                return elements;
            }
            else
            {
                var elements = new object[length];

                for (var i = 0; i < length; i++) elements[i] = reader.Parse(templates, elementType);

                return elements;
            }
        }
    }
}