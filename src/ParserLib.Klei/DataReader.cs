using Newtonsoft.Json;
using SheepReaper.GameSaves.Klei.Schema.Oni;
using SheepReaper.GameSaves.Klei.TypeParsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SheepReaper.GameSaves.Klei
{
    public class DataReader : BufferBinaryReader, IDataReader
    {
        private static readonly List<SerializationTypeCode> GenericTypes = new List<SerializationTypeCode>
        {
            SerializationTypeCode.List,
            SerializationTypeCode.Pair,
            SerializationTypeCode.Dictionary,
            SerializationTypeCode.HashSet,
            SerializationTypeCode.UserDefined,
            SerializationTypeCode.Color,
        };

        private static readonly List<SerializationTypeCode> UserTypes = new List<SerializationTypeCode>
        {
        };

        //private Dictionary<string, int> TrackedOffsets = new Dictionary<string, int>();

        private readonly Dictionary<string, IExtraDataParser> _extraDataParsers =
            new Dictionary<string, IExtraDataParser>()
            {
                ["storage"] = new StorageExtraDataParser(),
            };

        private readonly Dictionary<SerializationTypeCode, IParser> _typeParsers =
            new Dictionary<SerializationTypeCode, IParser>()
            {
                [SerializationTypeCode.Boolean] = new BooleanParser(),
                [SerializationTypeCode.Int32] = new IntegerParser(),
                [SerializationTypeCode.Dictionary] = new DictionaryParser(),
                [SerializationTypeCode.Array] = new ArrayParser(),
                [SerializationTypeCode.String] = new StringParser(),
                [SerializationTypeCode.List] = new ArrayParser(),
                [SerializationTypeCode.UserDefined] = new UserDefinedParser(),
                [SerializationTypeCode.Single] = new SingleParser(),
                [SerializationTypeCode.Vector2I] = new Vector2IParser(),
                [SerializationTypeCode.HashSet] = new ArrayParser(),
                [SerializationTypeCode.Enumeration] = new IntegerParser(),
                [SerializationTypeCode.Vector3] = new Vector3Parser(),
                [SerializationTypeCode.Vector2] = new Vector2Parser(),
            };

        private Version Version { get; set; }

        private GameData ParseGameData() => ParseSection<GameData>();

        private GameObjectBehavior ParseGameObjectBehavior()
        {
            var name = ValidateDotNetIdentifierName(ReadString());
            var dataLength = ReadInt32();
            var preParsePosition = PositionInt;
            var templateData = Parse(Templates, name);
            var haveAppropriateExtraParser = _extraDataParsers.ContainsKey(name);
            var extraData = new List<object>();

            if (haveAppropriateExtraParser)
            {
                var extraDataParser = _extraDataParsers[name];

                extraData = extraDataParser.Parse(this, Templates);
            }

            var postParsePosition = PositionInt;
            var dataRemaining = dataLength - (postParsePosition - preParsePosition);

            if (dataRemaining < 0)
            {
                throw new InvalidOperationException(
                    $"GameObjectBehavior {name} deserialized more type data than expected.");
            }

            if (dataRemaining <= 0)
                return new GameObjectBehavior
                {
                    Name = name,
                    TemplateData = templateData,
                    ExtraData = extraData,
                };

            if (haveAppropriateExtraParser)
                throw new InvalidOperationException(
                    $"GameObjectBehavior {name} extraData parser did not consume all the extra data.");

            var extraRaw = ReadBytes(dataRemaining);

            return new GameObjectBehavior
            {
                Name = name,
                TemplateData = templateData,
                ExtraRaw = extraRaw,
            };
        }

        private GameObjectGroup ParseGameObjectGroup()
        {
            var prefabName = ValidateDotNetIdentifierName(ReadString());
            var instanceCount = ReadInt32();
            var dataLength = ReadInt32();
            var preParsePosition = PositionInt;
            var gameObjects = new List<GameObject>(new GameObject[instanceCount]);

            for (var i = 0; i < instanceCount; i++)
            {
                gameObjects[i] = ParseGameObject();
            }

            var postParsePosition = PositionInt;
            var bytesRemaining = dataLength - (postParsePosition - preParsePosition);

            if (bytesRemaining < 0)
            {
                throw new InvalidOperationException(
                    $"GameObject {prefabName} parse consumed -{bytesRemaining} more bytes than its declared length of {dataLength}.");
            }

            if (bytesRemaining > 0)
            {
                throw new InvalidOperationException(
                    $"GameObject {prefabName} parse consumed {bytesRemaining} less than its declared length of {dataLength}.");
            }

            return new GameObjectGroup
            {
                Name = prefabName,
                GameObjects = gameObjects,
            };
        }

        private List<GameObjectGroup> ParseGameObjects()
        {
            var count = ReadInt32();
            var newGroups = new List<GameObjectGroup>(new GameObjectGroup[count]);

            for (var i = 0; i < count; i++)
            {
                newGroups[i] = ParseGameObjectGroup();
            }

            return newGroups;
        }

        private void ParseKSav()
        {
            const string expected = "KSAV";

            var actual = new string(ReadChars(expected.Length));

            if (actual != expected) throw new InvalidOperationException($"Expected '{expected}' next, but got '{actual}' instead.");

            var majorVersion = ReadInt32();
            var minorVersion = ReadInt32();

            Version = new Version(majorVersion, minorVersion);
            //Console.ReadKey();
        }

        private T ParseSection<T>() where T : SectionType
        {
            var expectedAssemblyNames = new[]
            {
                "Game+GameSaveData",
                "Game+Settings",
            };

            var typeName = ValidateDotNetIdentifierName(ReadString());

            if (!expectedAssemblyNames.Contains(typeName))
                throw new InvalidDataException($"Type '{typeName}' is unexpected. Expected '{expectedAssemblyNames}'.");

            var obj = Parse(Templates, typeName);
            var serialized = JsonConvert.SerializeObject(obj);
            var deserialized = JsonConvert.DeserializeObject<T>(serialized);

            return deserialized;
        }

        private Settings ParseSettings()
        {
            var settings = ParseSection<Settings>();

            ParseKSav();

            return settings;
        }

        /// <summary>
        /// WARNING: Sequence sensitive!
        /// </summary>
        /// <returns></returns>
        private StreamHeader ParseStreamHeader()
        {
            // TODO: Add safety checks in case this method is called out of sequence

            // Read
            var buildVersion = ReadInt32();
            var gameInfoStringByteLength = ReadInt32();
            var headerVersion = ReadInt32();
            _ = ReadInt32(); // Not sure what this value is. Header Minor version maybe? Discarding it for now
            var gameInfoStringBytes = new Span<byte>(ReadBytes(gameInfoStringByteLength));

            // Process
            BodyIsCompressed = headerVersion >= 1;

            dynamic gameInfo = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(gameInfoStringBytes));

            return new StreamHeader
            {
                BuildVersion = buildVersion,
                HeaderVersion = headerVersion,
                BodyIsCompressed = BodyIsCompressed,
                GameInfo = new GameInfo
                {
                    NumberOfCycles = gameInfo.numberOfCycles,
                    NumberOfDuplicants = gameInfo.numberOfDuplicants,
                    BaseName = gameInfo.baseName,
                    IsAutoSave = gameInfo.isAutoSave,
                    OriginalSaveName = gameInfo.originalSaveName,
                    SaveMajorVersion = gameInfo.saveMajorVersion,
                    SaveMinorVersion = gameInfo.saveMinorVersion
                }
            };
        }

        private List<Template> ParseTemplateList(int templateCount)
        {
            var templates = new List<Template>(new Template[templateCount]);

            for (var i = 0; i < templateCount; i++)
            {
                var name = ValidateDotNetIdentifierName(ReadString());
                var fieldCount = ReadInt32();
                var propertyCount = ReadInt32();

                templates[i] = new Template
                {
                    Name = name,
                    Fields = ParseTemplateMemberList(fieldCount),
                    Properties = ParseTemplateMemberList(propertyCount)
                };

                //Console.WriteLine($"Parsed template {i + 1} of {templateCount}: {templates[i].Name} with {templates[i].Fields.Count} fields and {templates[i].Properties.Count} properties. At Stream Position {PositionInt} of {BaseStream.Length}.");
            }

            Templates = templates;

            //Console.WriteLine(JsonConvert.SerializeObject(templates, Formatting.Indented));

            return templates;
        }

        private List<TemplateMember> ParseTemplateMemberList(int memberCount)
        {
            var members = new List<TemplateMember>(new TemplateMember[memberCount]);

            for (var i = 0; i < memberCount; i++)
            {
                members[i] = new TemplateMember
                {
                    Name = ValidateDotNetIdentifierName(ReadString()),
                    Type = ParseTypeInfo()
                };
            }

            return members;
        }

        // TODO: Add safety checks in case this method is called out of sequence
        /// <summary>
        /// WARNING: Sequence sensitive!
        /// </summary>
        /// <returns></returns>
        private List<Template> ParseTemplates() => ParseTemplateList(ReadInt32());

        private TypeInfo ParseTypeInfo()
        {
            var typeCodeInt = ReadByte();
            //Console.WriteLine(typeCodeInt.ToString());
            var info = (SerializationTypeCode)typeCodeInt;
            var inferredInt = (int)info & 0x7f;
            var inferredType = (SerializationTypeCode)inferredInt;
            var type = inferredType;
            var templateName = "";
            var subTypes = new List<TypeInfo>();

            if (type == SerializationTypeCode.UserDefined
                || type == SerializationTypeCode.Enumeration
                || type.IsValueType())
            {
                var userTypeName = ReadString();

                if (string.IsNullOrEmpty(userTypeName))
                    throw new InvalidDataException("Type name cannot be null for a user-defined or enumeration type.");

                templateName = userTypeName;
            }

            if (((byte)info & (byte)SerializationTypeFlags.IS_GENERIC_TYPE) ==
                (byte)SerializationTypeFlags.IS_GENERIC_TYPE)
            {
                if (!GenericTypes.Contains(type))
                    throw new NotSupportedException($"Unsupported non-generic type: {type} marked as generic.");

                var subTypeCount = ReadByte();

                for (var subTypeIndex = 0; subTypeIndex < subTypeCount; subTypeIndex++)
                {
                    subTypes.Add(ParseTypeInfo());
                }
            }
            else if (type == SerializationTypeCode.Array)
            {
                var subType = ParseTypeInfo();

                subTypes.Add(subType);
            }

            return new TypeInfo
            {
                Info = info,
                TypeCodeInt = typeCodeInt,
                InferredType = inferredType,
                InferredTypeInt = inferredInt,
                TemplateName = templateName,
                SubTypes = subTypes,
            };
        }

        private World ParseWorld()
        {
            var worldMarker = ReadString();

            if (worldMarker != "world")
            {
                Console.Error.WriteLine("Expected 'World' string.");
            }

            var typename = ValidateDotNetIdentifierName(ReadString());

            if (typename != "Klei.SaveFileRoot")
                throw new InvalidOperationException($"Expected type name Klei.SaveFileRoot but got {typename}.");

            var obj = Parse(Templates, typename);
            var serialized = JsonConvert.SerializeObject(obj);
            var deserialized = JsonConvert.DeserializeObject<World>(serialized);

            //Console.WriteLine(JsonConvert.SerializeObject(obj));

            //Console.ReadLine();

            return deserialized;
        }

        public List<Template> Templates;

        public DataReader(Memory<byte> buffer) : base(buffer)
        {
        }

        public DataReader(Stream stream) : base(stream)
        {
        }

        public bool BodyIsCompressed { get; private set; }

        public object Parse(List<Template> templates, string templateName)
        {
            var template = templates.Find(t => t.Name == templateName);

            if (null == template)
                throw new KeyNotFoundException(
                    $"Template name {templateName} was not found in the template collection.");

            var result = new Dictionary<string, object>();

            foreach (var field in template.Fields)
            {
                var value = Parse(templates, field.Type);

                result[field.Name] = value;
            }

            foreach (var property in template.Properties)
            {
                var value = Parse(templates, property.Type);

                result[property.Name] = value;
            }

            //Console.WriteLine(JsonConvert.SerializeObject(new { template, result },Formatting.Indented));
            return result;
        }

        public object Parse(List<Template> templates, TypeInfo type)
        {
            var typeElem = type.Info.GetImpliedType();

            try
            {
                var parser = _typeParsers[typeElem];
                var result = parser.Parse(this, type, templates);

                //Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                return result;
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"No parser is registered for {type}. (typeinfo: {typeElem}).", ex);
            }
        }

        public GameObject ParseGameObject()
        {
            var position = ReadVector3();
            var rotation = ReadQuaternion();
            var scale = ReadVector3();
            var folder = ReadByte();
            var behaviorCount = ReadInt32();
            var newBehaviors = new List<GameObjectBehavior>(new GameObjectBehavior[behaviorCount]);

            for (var i = 0; i < behaviorCount; i++)
            {
                newBehaviors[i] = ParseGameObjectBehavior();
            }

            return new GameObject
            {
                Position = position,
                Rotation = rotation,
                Scale = scale,
                Folder = folder,
                Behaviors = newBehaviors,
            };
        }

        public GameSave ParseHeaderAndTemplates()
        {
            return new GameSave
            {
                Header = ParseStreamHeader(),
                Templates = ParseTemplates(),
                BodyIsCompressed = BodyIsCompressed,
                BodyStartIndex = PositionInt,
            };
        }

        public GameSaveBodyPart ParseSaveFileBody()
        {
            return new GameSaveBodyPart
            {
                World = ParseWorld(),
                Settings = ParseSettings(),
                Version = Version,
                GameObjects = ParseGameObjects(),
                GameData = ParseGameData(),
            };
        }

        /// <summary>
        /// WARNING: Sequence Sensitive!
        /// </summary>
        /// <returns></returns>
        public override string ReadString()
        {
            var stringLength = ReadInt32();
            //Console.WriteLine($"Call to Klei String: Requested string length: {stringLength}");

            switch (stringLength)
            {
                case -1:
                    return null;

                case 0:
                    return string.Empty;

                default:
                    if (stringLength < -1)
                        throw new InvalidOperationException(
                            $"The string length read from stream: {stringLength} is invalid. Likely, the next bytes are not a string. Aborting operation.");

                    string value;

                    value = Encoding.UTF8.GetString(GetBufferSpan().Slice(PositionInt, stringLength));

                    BaseStream.Position += stringLength;

                    //Console.WriteLine($"value of decoded string: {value}");
                    return value;
            }
        }

        public string ValidateDotNetIdentifierName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(".NET identifiers cannot be null, empty, or just whitespace", nameof(name));

            if (name.Length >= 512)
                throw new ArgumentOutOfRangeException(nameof(name), name,
                    ".NET identifier exceeds 511 characters. Likely, this is a parse error");

            var regexTester = new Regex(@"^[a-zA-Z0-9_\+\.]+(\`\d+)?(\+[a-zA-Z0-9_\+\.]+)?(\[\[.+\]\])?$");

            if (!regexTester.IsMatch(name))
                throw new ArgumentOutOfRangeException(nameof(name), name,
                    "The tested name does not conform to .NET serialization patterns");

            return name;
        }
    }
}