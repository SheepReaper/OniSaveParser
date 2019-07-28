using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.Serialization;

namespace SheepReaper.GameSaves.Klei.Schema
{
    [Serializable]
    public struct ModInfo
    {
        [DataMember(Order = 2, Name = "assetID")]
        public string AssetId;

        [DataMember(Order = 3, Name = "assetPath")]
        public string AssetPath;

        [DataMember(Order = 7, Name = "description")]
        public string Description;

        [DataMember(Order = 4, Name = "enabled")]
        public bool Enabled;

        [DataMember(Order = 8, Name = "lastModifiedTime")]
        public ulong LastModifiedTime;

        [DataMember(Order = 5, Name = "markedForDelete")]
        public bool MarkedForDelete;

        [DataMember(Order = 6, Name = "markedForUpdate")]
        public bool MarkedForUpdate;

        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Order = 0, Name = "source")]
        public ModInfo.Source source;

        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Order = 1, Name = "type")]
        public ModInfo.ModType Type;

        public enum ModType
        {
            WorldGen,
            Scenario,
            Mod,
        }

        public enum Source
        {
            Local,
            Steam,
            Rail,
        }
    }
}