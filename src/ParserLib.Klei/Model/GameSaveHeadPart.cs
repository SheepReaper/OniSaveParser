using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei
{
    public class GameSaveHeadPart
    {
        public bool BodyIsCompressed { get; set; }
        public int BodyStartIndex { get; set; }
        public StreamHeader header { get; set; }
        public List<Template> templates { get; set; }
    }
}