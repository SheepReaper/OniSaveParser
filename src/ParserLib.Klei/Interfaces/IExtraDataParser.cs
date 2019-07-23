using System.Collections.Generic;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;

namespace SheepReaper.GameSaves.Interfaces
{
    public interface IExtraDataParser
    {
        public List<object> Parse(IKleiDataReader reader, List<Template> templates);
    }
}