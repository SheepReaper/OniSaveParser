using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei
{
    public class GameSave
    {
        public GameSaveBodyPart Body { get; set; }
        public StreamHeader Header { get; set; }
        public List<Template> Templates { get; set; }
        public bool BodyIsCompressed { get; set; }
        public int BodyStartIndex { get; set; }
    }
}