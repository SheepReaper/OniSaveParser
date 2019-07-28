using System.Runtime.Serialization;

namespace SheepReaper.GameSaves.Klei.Schema.Oni
{
    public class CustomGameSettings
    {
        public object[][] CurrentQualityLevelsBySetting { get; set; }
        public int CustomGameMode { get; set; }

        [DataMember(Name = "is_Custom_Game")]
        public bool IsCustomGame { get; set; }
    }
}