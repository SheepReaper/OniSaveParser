using System;
using System.Collections.Generic;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;

namespace SheepReaper.GameSaves.Model
{
    [Serializable]
    public class SaveFileHeadPart
    {
        public bool BodyIsCompressed { get; set; }
        public int BodyStartIndex { get; set; }
        public SaveGameHeader header { get; set; }
        public List<Template> templates { get; set; }
    }
}