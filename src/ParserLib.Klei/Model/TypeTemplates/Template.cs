using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei
{
    public class Template
    {
        public List<TemplateMember> Fields { get; set; }
        public string Name { get; set; }
        public List<TemplateMember> Properties { get; set; }
    }
}