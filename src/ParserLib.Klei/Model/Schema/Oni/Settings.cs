namespace SheepReaper.GameSaves.Klei.Schema.Oni
{
    public class Settings : SectionType
    {
        public bool BaseAlreadyCreated { get; set; }
        public int GameId { get; set; }
        public int NextUniqueId { get; set; }
    }
}