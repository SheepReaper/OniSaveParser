using SheepReaper.GameSaves.Model.SaveFile.Schema;
using System.Collections.Generic;

namespace SheepReaper.GameSaves.Model
{
    public class GameObjectGroup
    {
        public List<GameObject> GameObjects { get; set; }
        public string Name { get; set; }
    }
}