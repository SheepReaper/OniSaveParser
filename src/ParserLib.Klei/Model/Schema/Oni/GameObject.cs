using System.Collections.Generic;
using System.Numerics;

namespace SheepReaper.GameSaves.Klei.Schema.Oni
{
    public class GameObject
    {
        public List<GameObjectBehavior> Behaviors { get; set; }
        public int Folder { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 Scale { get; set; }
    }
}