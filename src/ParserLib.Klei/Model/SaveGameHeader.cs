using SheepReaper.GameSaves.Model.SaveFile.Schema;

namespace SheepReaper.GameSaves.Model
{
    public class SaveGameHeader
    {
        public bool BodyIsCompressed { get; set; }
        public int BuildVersion { get; set; }
        public GameInfo GameInfo { get; set; }
        public int HeaderVersion { get; set; }
    }
}