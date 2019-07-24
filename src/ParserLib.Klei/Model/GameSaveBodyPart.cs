using SheepReaper.GameSaves.Klei.Schema.Oni;
using System;
using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei
{
    public class GameSaveBodyPart
    {
        public GameData GameData { get; set; }
        public List<GameObjectGroup> GameObjects { get; set; }
        public Settings Settings { get; set; }
        public Version Version { get; set; }
        public World World { get; set; }
    }
}