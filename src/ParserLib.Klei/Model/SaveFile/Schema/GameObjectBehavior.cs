namespace SheepReaper.GameSaves.Model.SaveFile.Schema
{
    public class GameObjectBehavior
    {
        public object ExtraData { get; set; }
        public byte[] ExtraRaw { get; set; }
        public string Name { get; set; }
        public object TemplateData { get; set; }
    }
}