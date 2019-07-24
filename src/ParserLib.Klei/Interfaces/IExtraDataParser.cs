using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;
using System.Collections.Generic;

namespace SheepReaper.GameSaves.Interfaces
{
    public interface IExtraDataParser
    {
        public List<object> Parse(IKleiDataReader reader, List<Template> templates);
    }
}