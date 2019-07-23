using System.Collections.Generic;

namespace SheepReaper.GameSaves.Model.SaveFile.TypeTemplates
{
    public class Template
    {
        public List<TemplateMember> Fields { get; set; }
        public string Name { get; set; }
        public List<TemplateMember> Properties { get; set; }
    }
}