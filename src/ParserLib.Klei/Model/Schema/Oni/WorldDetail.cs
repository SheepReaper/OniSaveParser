namespace SheepReaper.GameSaves.Klei.Schema.Oni
{
    public class WorldDetail
    {
        public int GlobalNoiseSeed { get; set; }
        public int GlobalTerrainSeed { get; set; }
        public int GlobalWorldLayoutSeed { get; set; }
        public int GlobalWorldSeed { get; set; }
        public object[] OverworldCells { get; set; }
    }
}