using System.Collections.Generic;

namespace SheepReaper.GameSaves.Model.SaveFile.Schema
{
    public class CustomGameSettings
    {
        public List<QualityLevelSetting> CurrentQualityLevelsBySetting { get; set; }
        public int CustomGameMode { get; set; }
        public bool IsCustomGame { get; set; }
    }
}