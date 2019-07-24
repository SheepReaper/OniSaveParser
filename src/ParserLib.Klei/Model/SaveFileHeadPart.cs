using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;
using System.Collections.Generic;

namespace SheepReaper.GameSaves.Model
{
    public class SaveFileHeadPart
    {
        public bool BodyIsCompressed { get; set; }
        public int BodyStartIndex { get; set; }
        public SaveGameHeader header { get; set; }
        public List<Template> templates { get; set; }
    }
}