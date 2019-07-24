using SheepReaper.GameSaves.Klei.Schema.Oni;

namespace SheepReaper.GameSaves.Klei
{
    public class StreamHeader
    {
        public bool BodyIsCompressed { get; set; }
        public int BuildVersion { get; set; }
        public GameInfo GameInfo { get; set; }
        public int HeaderVersion { get; set; }
    }
}