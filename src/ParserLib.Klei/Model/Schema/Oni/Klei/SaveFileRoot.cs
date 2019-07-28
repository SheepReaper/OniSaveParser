using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SheepReaper.GameSaves.Klei.Schema.Klei
{
    public class SaveFileRoot
    {
        [DataMember(Order = 5, Name = "active_mods")]
        public List<KMod.Label> ActiveMods;

        [DataMember(Order = 1)]
        public int HeightInCells;

        [DataMember(Order = 4, Name = "requiredMods")]
        public List<ModInfo> RequiredMods;

        [DataMember(Order = 2, Name = "streamed")]
        public Dictionary<string, byte[]> Streamed;

        [DataMember(Order = 0)]
        public int WidthInCells;

        [DataMember(Order = 3, Name = "worldID")]
        public string WorldId;

        public SaveFileRoot()
        {
            Streamed = new Dictionary<string, byte[]>();
        }
    }
}