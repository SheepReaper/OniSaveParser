using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei.Schema.Oni
{
    public class World
    {
        public List<object> Active_Mods { get; set; }
        public int HeightInCells { get; set; }
        public List<object> RequiredMods { get; set; }
        public List<object> Streamed { get; set; }
        public int WidthInCells { get; set; }
        public string WorldId { get; set; }
    }
}