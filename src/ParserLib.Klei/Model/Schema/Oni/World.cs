using System.Collections.Generic;
using Newtonsoft.Json;

namespace SheepReaper.GameSaves.Klei.Schema.Oni
{
    public class World
    {
        [JsonProperty("active_mods")]
        public List<object> ActiveMods { get; set; }
        public int HeightInCells { get; set; }
        public List<object> RequiredMods { get; set; }
        public List<object> Streamed { get; set; }
        public int WidthInCells { get; set; }
        public string WorldId { get; set; }
    }
}