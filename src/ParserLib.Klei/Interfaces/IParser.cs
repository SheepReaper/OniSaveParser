using System.Collections.Generic;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;

namespace SheepReaper.GameSaves.Interfaces
{
    public interface IParser
    {
        object Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates);
    }

    public interface IParser<out T> : IParser
    {
        new T Parse(IKleiDataReader reader, TypeInfoElement info, List<Template> templates);
    }
}