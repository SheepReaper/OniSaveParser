using SheepReaper.GameSaves.Model.SaveFile.Schema;
using System;
using System.Collections.Generic;

namespace SheepReaper.GameSaves.Model
{
    public class SaveGameBody
    {
        public GameData GameData { get; set; }
        public List<GameObjectGroup> GameObjects { get; set; }
        public Settings Settings { get; set; }
        public Version Version { get; set; }
        public World World { get; set; }
    }
}