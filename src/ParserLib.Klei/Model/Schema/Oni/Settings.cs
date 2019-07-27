namespace SheepReaper.GameSaves.Klei.Schema.Oni
{
    public class Settings : SectionType
    {
        public bool BaseAlreadyCreated { get; set; }
        public int GameID { get; set; }
        public int NextUniqueID { get; set; }
    }
}