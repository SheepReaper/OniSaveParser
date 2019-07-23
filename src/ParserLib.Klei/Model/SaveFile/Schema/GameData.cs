using System.Numerics;

namespace SheepReaper.GameSaves.Model.SaveFile.Schema
{
    public class GameData
    {
        public bool AdvancedPersonalPriorities { get; set; }
        public object AutoPrioritizeRoles { get; set; }
        public CustomGameSettings CustomGameSettings { get; set; }
        public bool DebugWasUsed { get; set; }
        public object FallingWater { get; set; }
        public object GasConduitFlow { get; set; }
        public object LiquidConduitFlow { get; set; }
        public SavedInfo SavedInfo { get; set; }
        public Vector2 SimActiveRegionMax { get; set; }
        public Vector2 SimActiveRegionMin { get; set; }
        public object UnstableGround { get; set; }
        public object WorldDetail { get; set; }
    }
}