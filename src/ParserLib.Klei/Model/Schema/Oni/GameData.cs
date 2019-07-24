using System.Numerics;

namespace SheepReaper.GameSaves.Klei.Schema.Oni
{
    public class GameData
    {
        public bool AdvancedPersonalPriorities { get; set; }
        public bool AutoPrioritizeRoles { get; set; }
        public CustomGameSettings CustomGameSettings { get; set; }
        public bool DebugWasUsed { get; set; }
        public FallingWater FallingWater { get; set; }
        public ConduitFlow GasConduitFlow { get; set; }
        public ConduitFlow LiquidConduitFlow { get; set; }
        public SavedInfo SavedInfo { get; set; }
        public Vector2 SimActiveRegionMax { get; set; }

        //public object SimActiveRegionMax { get; set; }
        public Vector2 SimActiveRegionMin { get; set; }

        //public object SimActiveRegionMin { get; set; }
        public UnstableGround UnstableGround { get; set; }

        public WorldDetail WorldDetail { get; set; }
    }
}