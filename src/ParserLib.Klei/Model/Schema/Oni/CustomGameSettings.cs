using Newtonsoft.Json;

namespace SheepReaper.GameSaves.Klei.Schema.Oni
{
    public class CustomGameSettings
    {
        public object[][] CurrentQualityLevelsBySetting { get; set; }
        public int CustomGameMode { get; set; }

        [JsonProperty("is_Custom_Game")]
        public bool IsCustomGame { get; set; }
    }
}