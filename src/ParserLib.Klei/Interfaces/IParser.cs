using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;
using System.Collections.Generic;

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