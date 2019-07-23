using System.Collections.Generic;
using SheepReaper.GameSaves.Model.SaveFile.Schema;

namespace SheepReaper.GameSaves.Model
{
    public class GameObjectGroup : List<GameObject>
    {
        public List<GameObject> GameObjects { get; set; }
        public string Name { get; set; }
    }
}