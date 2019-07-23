using System;
using System.Collections.Generic;

namespace SheepReaper.GameSaves.Model.SaveFile.Schema
{
    [Serializable]
    public class World
    {
        public int HeightInCells { get; set; }
        public List<object> Streamed { get; set; }
        public int WidthInCells { get; set; }
        public string WorldId { get; set; }
        public List<object> RequiredMods { get; set; }
        public List<object> Active_Mods { get; set; }
    }
}