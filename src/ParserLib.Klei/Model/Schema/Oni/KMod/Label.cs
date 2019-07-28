using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace SheepReaper.GameSaves.Klei.Schema.KMod
{
    [JsonObject(MemberSerialization.Fields)]
    [DebuggerDisplay("{title}")]
    public struct Label
    {
        public Label.DistributionPlatform distribution_platform;
        public string id;
        public long version;
        public string title;

        [JsonIgnore]
        private string distribution_platform_name { get; }

        [JsonIgnore]
        public string install_path { get; }

        [JsonIgnore]
        public DateTime time_stamp { get; }

        public override string ToString()
        {
            return this.title;
        }

        public bool Match(Label rhs)
        {
            return this.id == rhs.id && this.distribution_platform == rhs.distribution_platform;
        }

        public enum DistributionPlatform
        {
            Local,
            Steam,
            Epic,
            Rail,
            Dev,
        }
    }
}
